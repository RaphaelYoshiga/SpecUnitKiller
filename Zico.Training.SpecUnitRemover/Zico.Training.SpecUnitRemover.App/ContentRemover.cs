namespace Zico.Training.SpecUnitRemover.App
{
    public interface IContentRemover
    {
        string Remove(string value);
    }

    public class ContentRemover : IContentRemover
    {
        private static int _endParenthesisIndex;
        private static int _clauseStartIndex;
        private static int _parametersIndex;
        private const string SEMICOLON = ";";
        public string Remove(string value)
        {
            var removeGiven = RemoveCommandAndApplySemiColon(value, "Given(");
            var removeWhen = RemoveCommandAndApplySemiColon(removeGiven, ".When(");
            var removeAnd = RemoveCommandAndApplySemiColon(removeWhen, ".And(");
            return RemoveCommandAndApplySemiColon(removeAnd, ".Then(");
        }

        private static string RemoveCommandAndApplySemiColon(string clause, string searchPattern)
        {
            _clauseStartIndex = clause.IndexOf(searchPattern);

            while (_clauseStartIndex > 0)
            {
                clause = RemoveClauseStart(clause, searchPattern);

                CalculateIndexes(clause);

                if (_parametersIndex > 0)
                {
                    clause = UpdateParameterMethod(clause);
                }
                else
                {
                    clause = UpdateSimpleMehthod(clause);
                }

                _clauseStartIndex = clause.IndexOf(searchPattern);
            }
            return clause;
        }

        private static void CalculateIndexes(string clause)
        {
            _endParenthesisIndex = clause.IndexOf(")", _clauseStartIndex);

            _parametersIndex = clause.IndexOf(",", _clauseStartIndex, _endParenthesisIndex - _clauseStartIndex);
        }

        private static string UpdateParameterMethod(string clause)
        {
            var result = clause.Insert(_parametersIndex + 1, "(").Remove(_parametersIndex, 1);

            var endIndex = SkipMethodsInParameters(result);

            if (result.IndexOf(";",endIndex) == endIndex +1)
                return result;

            return result.Insert(endIndex + 1, SEMICOLON);
        }

        private static int SkipMethodsInParameters(string result)
        {
            var endIndex = _endParenthesisIndex;
            var openIndex = result.IndexOf("(", _endParenthesisIndex - 1);

            while (openIndex + 1 == endIndex)
            {
                endIndex = result.IndexOf(")", endIndex + 1);
                openIndex = result.IndexOf("(", openIndex + 1);
            }
            return endIndex;
        }

        private static string UpdateSimpleMehthod(string value)
        {
            var result = value.Insert(_endParenthesisIndex, "(").Insert(_endParenthesisIndex + 2, SEMICOLON);
            return result;
        }

        private static string RemoveClauseStart(string value, string searchPattern)
        {
            var removeIndentation = _clauseStartIndex - 4;
            if (searchPattern == "Given(" || removeIndentation > 0 == false)
                return value.Remove(_clauseStartIndex, searchPattern.Length);
            return value.Remove(removeIndentation, searchPattern.Length + 4);
        }
    }
}
namespace Zico.Training.SpecUnitRemover.App
{
    public interface IContentRemover
    {
        string Remove(string value);
    }

    public class ContentRemover : IContentRemover
    {
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
            var clauseStartIndex = clause.IndexOf(searchPattern);
            while (clauseStartIndex > 0)
            {
                var parametersIndex = clause.IndexOf(",");
                clause = RemoveClauseStart(clause, clauseStartIndex, searchPattern.Length);

                if (parametersIndex > 0)
                {
                   clause = UpdateParameterMethod(clause);
                }
                else
                {
                    clause = UpdateSimpleMehthod(clause, clauseStartIndex);
                }

                clauseStartIndex = clause.IndexOf(searchPattern);
            }
            return clause;
        }

        private static string UpdateParameterMethod(string clause)
        {
            var indexOfComma = clause.IndexOf(",");
            int parenthesisIndex = clause.IndexOf(")");
            var result = clause.Insert(indexOfComma+1, "(").Remove(indexOfComma, 1).Insert(parenthesisIndex + 1, SEMICOLON);
            return result;
        }

        private static string UpdateSimpleMehthod(string value, int index)
        {
            int parenthesisIndex = value.IndexOf(")", index);
            var result = value.Insert(parenthesisIndex, "(").Insert(parenthesisIndex + 2, SEMICOLON);
            return result;
        }

        private static string RemoveClauseStart(string value, int index, int startIndex)
        {
            return value.Remove(index, startIndex);
        }
    }
}
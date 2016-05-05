namespace Zico.Training.SpecUnitRemover.App
{
    public class ContentRemover
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
                clause = RemoveClauseStart(clause, clauseStartIndex, searchPattern.Length);
                var parenthesisIndex = clause.IndexOf(")", clauseStartIndex);
                var parametersIndex = clause.IndexOf(",",clauseStartIndex,parenthesisIndex-clauseStartIndex);

                if (parametersIndex > 0)
                {
                   clause = UpdateParameterMethod(clause, parenthesisIndex, parametersIndex);
                }
                else
                {
                    clause = UpdateSimpleMehthod(clause, parenthesisIndex);
                }

                clauseStartIndex = clause.IndexOf(searchPattern);
            }
            return clause;
        }

        private static string UpdateParameterMethod(string clause, int parenthesisIndex, int indexOfComma)
        {
            var result = clause.Insert(indexOfComma+1, "(").Remove(indexOfComma, 1);

            var endIndex = result.IndexOf(")", indexOfComma);
            if (result.IndexOf(";",endIndex) == endIndex +1)
                return result;

            return result.Insert(parenthesisIndex + 1, SEMICOLON);
        }

        private static string UpdateSimpleMehthod(string value, int parenthesisIndex)
        {
            var result = value.Insert(parenthesisIndex, "(").Insert(parenthesisIndex + 2, SEMICOLON);
            return result;
        }

        private static string RemoveClauseStart(string value, int index, int startIndex)
        {
            return value.Remove(index, startIndex);
        }
    }
}
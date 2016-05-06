using System.Linq;

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
                clause = RemoveClauseStart(clause, clauseStartIndex, searchPattern);
                var endParenthesisIndex = clause.IndexOf(")", clauseStartIndex);
                var parametersIndex = clause.IndexOf(",",clauseStartIndex,endParenthesisIndex-clauseStartIndex);

                if (parametersIndex > 0)
                {
                   clause = UpdateParameterMethod(clause, endParenthesisIndex, parametersIndex);
                }
                else
                {
                    clause = UpdateSimpleMehthod(clause, endParenthesisIndex);
                }

                clauseStartIndex = clause.IndexOf(searchPattern);
            }
            return clause;
        }

        private static string UpdateParameterMethod(string clause, int endParenthesisIndex, int indexOfComma)
        {
            var result = clause.Insert(indexOfComma+1, "(").Remove(indexOfComma, 1);

            var endIndex = SkipMethodsInParameters(endParenthesisIndex, result);

            if (result.IndexOf(";",endIndex) == endIndex +1)
                return result;

            return result.Insert(endIndex + 1, SEMICOLON);
        }

        private static int SkipMethodsInParameters(int endParenthesisIndex, string result)
        {
            var endIndex = endParenthesisIndex;
            var openIndex = result.IndexOf("(", endParenthesisIndex - 1);

            while (openIndex + 1 == endIndex)
            {
                endIndex = result.IndexOf(")", endIndex + 1);
                openIndex = result.IndexOf("(", openIndex + 1);
            }
            return endIndex;
        }

        private static string UpdateSimpleMehthod(string value, int parenthesisIndex)
        {
            var result = value.Insert(parenthesisIndex, "(").Insert(parenthesisIndex + 2, SEMICOLON);
            return result;
        }

        private static string RemoveClauseStart(string value, int index, string searchPattern)
        {
            var removeIndentation = index-4;
            if (searchPattern == "Given(" || removeIndentation > 0 == false)
                return value.Remove(index, searchPattern.Length);
            return value.Remove(removeIndentation, searchPattern.Length + 4);
        }
    }
}
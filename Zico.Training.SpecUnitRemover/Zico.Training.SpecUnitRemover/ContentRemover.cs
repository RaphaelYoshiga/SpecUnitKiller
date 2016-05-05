namespace Zico.Training.SpecUnitRemover
{
    class ContentRemover
    {
        private const string SEMICOLON = ";";
        public string Remove(string value)
        {
            var removeGiven = RemoveCommandAndApplySemiColon(value, "Given(");
            var removeWhen = RemoveCommandAndApplySemiColon(removeGiven, ".When(");
            var removeAnd = RemoveCommandAndApplySemiColon(removeWhen, ".And(");
            return RemoveCommandAndApplySemiColon(removeAnd, ".Then(");
        }

        private static string RemoveCommandAndApplySemiColon(string removeWhen, string searchPattern)
        {
            var whenIndex = removeWhen.IndexOf(searchPattern);
            if (whenIndex > 0)
                return UpdateString(removeWhen, whenIndex, searchPattern.Length);
            return removeWhen;
        }

        private static string UpdateString(string value, int index, int startIndex)
        {
            string remove = value.Remove(index, startIndex);
            int parenthesisIndex = remove.IndexOf(")", index);
            var result = remove.Insert(parenthesisIndex, "(").Insert(parenthesisIndex + 2, SEMICOLON);
            return result;
        }
    }
}
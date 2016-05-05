namespace Zico.Training.SpecUnitRemover
{
    class ContentRemover
    {
        public string Remove(string parse)
        {
            var removeGiven = RemoveGiven(parse);

            var removeWhen = RemoveWhen(removeGiven);

            return RemoveAnd(removeWhen);
        }

        private static string RemoveAnd(string removeWhen)
        {
            var whenIndex = removeWhen.IndexOf(".And(");
            if (whenIndex > 0)
                return UpdateString(removeWhen, whenIndex, 5);
            return removeWhen;
        }

        private static string RemoveWhen(string removeGiven)
        {
            var whenIndex = removeGiven.IndexOf(".When(");
            if (whenIndex > 0)
                return UpdateString(removeGiven, whenIndex, 6);
            return removeGiven;
        }

        private static string RemoveGiven(string parse)
        {
            var givenIndex = parse.IndexOf("Given(");
            if (givenIndex > 0)
                return UpdateString(parse, givenIndex, 6);
            return parse;
        }

        private static string UpdateString(string parse, int index, int startIndex)
        {
            string remove = parse.Remove(index, startIndex);
            int parenthesisIndex = remove.IndexOf(")", index);
            var result = remove.Insert(parenthesisIndex, "(").Insert(parenthesisIndex + 2, ";");
            return result;
        }
    }
}
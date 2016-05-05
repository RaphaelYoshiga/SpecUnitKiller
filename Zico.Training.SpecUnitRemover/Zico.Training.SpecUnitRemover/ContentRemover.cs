namespace Zico.Training.SpecUnitRemover
{
    class ContentRemover
    {
        public string Remove(string parse)
        {
            var givenIndex = parse.IndexOf("Given(");
            string noGivenString = parse.Remove(givenIndex, 6);
            int parenthesisIndex = noGivenString.IndexOf(")", givenIndex);
            return noGivenString.Insert(parenthesisIndex, "(").Insert(parenthesisIndex + 2, ";");
        }
    }
}
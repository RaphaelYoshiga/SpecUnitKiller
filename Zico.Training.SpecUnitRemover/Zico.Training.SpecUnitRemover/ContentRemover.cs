namespace Zico.Training.SpecUnitRemover
{
    class ContentRemover
    {
        public string Remove(string parse)
        {
            var index = parse.IndexOf("Given(");
            var foo = parse.Remove(index, 6);

            //foo.IndexOf()
        }
    }
}
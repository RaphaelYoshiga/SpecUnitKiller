using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zico.Training.SpecUnitRemover.App
{
    class Program
    {
        static void Main()
        {
            string solutionPath = "C:\\Test\\";
            Console.WriteLine("Starting to remove SpecUnit from: {0}", solutionPath);

            InstanciateRemoverOrchestrator().RemoveSpecUnitFromTests(solutionPath);

            Console.WriteLine("Finished removing spec unit");
            Console.Read();
        }

        private static SpecUnitRemoverOrchestrator InstanciateRemoverOrchestrator()
        {
            SpecUnitFileTransformer fileTranformer = new SpecUnitFileTransformer(new DotNetFileReader(), new ContentRemover(),
                new DotNetFileWriter());
            var orchestrator = new SpecUnitRemoverOrchestrator(new CSharpFilesProvider(), fileTranformer);
            return orchestrator;
        }
    }
}

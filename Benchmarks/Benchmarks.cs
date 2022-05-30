using BenchmarkDotNet.Attributes;
using DocumentWorkflow.Core.Services;

namespace DocumentWorkflowBenchmarks
{
    [MemoryDiagnoser(false)]
    public class MyBenchmarks
    {
        public List<TemplateField> Fields = new TemplateParser().GetFields(@"C:\Users\drdgu\source\repos\DocumentWorkflow\DocumentWorkflow\Templates\certificate.html");

        public void GetFields()
        {
            new TemplateParser().GetFields(@"C:\Users\drdgu\source\repos\DocumentWorkflow\DocumentWorkflow\Templates\certificate.html");
        }

        
        public void CreateDocumnet1()
        {
            new DocumentCreator().Fill(Fields, @"C:\Users\drdgu\source\repos\DocumentWorkflow\DocumentWorkflow\Templates\certificate.html", "cert.html");
        }
    }
}

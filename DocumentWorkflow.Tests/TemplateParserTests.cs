using DocumentWorkflow.Core.Services;
using Xunit;

namespace DocumentWorkflow.Tests
{
    public class TemplateParserTests
    {
        [Fact]
        public void GetFields_Contains16Fields_True()
        {
            var templateParser = new TemplateParser();

            var fields = templateParser.GetFields(@"C:\Users\drdgu\source\repos\DocumentWorkflow\DocumentWorkflow\Templates\certificate.html");

            Assert.True(fields.Count == 16);
        }
    }
}
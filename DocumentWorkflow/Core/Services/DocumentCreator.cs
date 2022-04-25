using System.Text;
using DocumentWorkflow.Core.DAL.Repositories;
using DocumentWorkflow.Core.Models;

namespace DocumentWorkflow.Core.Services
{
    public class DocumentCreator
    {
        private readonly CategoriesRepository _categoriesRepository;
        private readonly DocumentsRepository _documentsRepository;
        private readonly string _documentsFolder = AppContext.BaseDirectory + @"/Documents/";

        public DocumentCreator(CategoriesRepository categoriesRepository, DocumentsRepository documentsRepository)
        {
            _categoriesRepository = categoriesRepository;
            _documentsRepository = documentsRepository;
            Directory.CreateDirectory(_documentsFolder);
        }

        private void Fill(List<ReplaceField> fields, string templateFilename, string documentFilename)
        {
            File.Copy(templateFilename, documentFilename, true);

            var file = File.ReadAllLines(documentFilename);
            var newFile = new StringBuilder();

            foreach (var line in file)
            {
                var newLine = string.Empty;
                fields.ForEach(f =>
                {
                    newLine = line.Replace(f.Name, f.Value);
                });
                newFile.AppendLine(newLine);
            }

            File.WriteAllText(documentFilename, newFile.ToString());
        }

        public void Create(NewDocument document)
        {
            var category = _categoriesRepository.GetCategory(document.CategoryId);
            var template = category.CustomTemplateFileName ??= category.DocumentType.TemplateFileName;

            var docFilename = $"{category.Name}_{category.LogBook.LastDocumentNumber + 1}_{template.Split("\\").Last()}";
            Fill(document.Fields, template, docFilename);

            var content = "";
            var docName = docFilename;

            _documentsRepository.AddDocument(document.CategoryId, docFilename, content, docName);
        }
    }
}

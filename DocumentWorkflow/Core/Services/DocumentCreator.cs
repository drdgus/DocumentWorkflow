using System.Text;
using System.Text.RegularExpressions;
using DocumentWorkflow.Core.DAL.Entities;
using DocumentWorkflow.Core.DAL.Repositories;
using DocumentWorkflow.Core.Models;

namespace DocumentWorkflow.Core.Services
{
    public class DocumentCreator
    {
        private readonly CategoriesRepository _categoriesRepository;
        private readonly DocumentsRepository _documentsRepository;
        private readonly string _documentsFolder = Path.Combine(AppContext.BaseDirectory, "Documents");

        public DocumentCreator(CategoriesRepository categoriesRepository, DocumentsRepository documentsRepository)
        {
            _categoriesRepository = categoriesRepository;
            _documentsRepository = documentsRepository;
            Directory.CreateDirectory(_documentsFolder);
        }

        public int Create(NewDocument document)
        {
            //TODO: Несколько ответственностей
            var category = _categoriesRepository.GetCategory(document.CategoryId);
            var template = category.CustomTemplateFileName ??= category.DocumentType.TemplateFileName;
            template = Path.Combine(AppContext.BaseDirectory, template);
            var templateFileName = new Regex(@"[^(\/|\\)]+(?=.html)").Match(template).Value;


            var fileFolder = _documentsFolder;
            fileFolder = category.ParentCategoryId == null ? Path.Combine(_documentsFolder, category.Name) : Path.Combine(_documentsFolder, category.ParentCategory.Name, category.Name);

            var docName = $"{document.Fields.Single(f => f.Name == "$Ученик_ФИО$").Value}";
            var docNameWithCurrentTemplate = $"{category.Name}_№{category.LogBook.LastDocumentNumber + 1}_{templateFileName}_{document.Fields.Single(f => f.Name == "$Ученик_ФИО$").Value.Replace(" ", "_")}.html";

            var docFileName = Path.Combine(fileFolder, docNameWithCurrentTemplate);
            
            Fill(document.Fields, template, fileFolder, docNameWithCurrentTemplate);

            var content = string.Join(",", document.Fields.Select(x => x.Value));
            

            return _documentsRepository.AddDocument(document.CategoryId, docFileName, content, docName);
        }

        private void Fill(List<ReplaceField> fields, string templateFilename, string folder, string documentFilename)
        {
            //TODO: Несколько ответственностей
            Directory.CreateDirectory(folder);
            File.Copy(templateFilename, Path.Combine(folder, documentFilename), true);

            var file = File.ReadAllText(Path.Combine(folder, documentFilename));

            var gender = fields.SingleOrDefault(f => f.Name == "$Местоимение_на_основании_пола$");
            if(gender != null)
                gender.Value = GetGenderPronoun(gender.Value);

            fields.ForEach(f =>
            {
                file = file.Replace(f.Name, f.Value);
            });

            File.WriteAllText(Path.Combine(folder, documentFilename), file);
        }

        private string GetGenderPronoun(string gender)
        {
            return gender switch
            {
                "М" => "он",
                "Ж" => "она",
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };
        }
    }
}

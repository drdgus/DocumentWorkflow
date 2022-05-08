using System.Text;
using DocumentWorkflow.Core.DAL.Entities;
using DocumentWorkflow.Core.DAL.Repositories;
using DocumentWorkflow.Core.Models;

namespace DocumentWorkflow.Core.Services
{
    public class DocumentCreator
    {
        private readonly CategoriesRepository _categoriesRepository;
        private readonly DocumentsRepository _documentsRepository;
        private readonly string _documentsFolder = AppContext.BaseDirectory + @"\Documents\";

        public DocumentCreator(CategoriesRepository categoriesRepository, DocumentsRepository documentsRepository)
        {
            _categoriesRepository = categoriesRepository;
            _documentsRepository = documentsRepository;
            Directory.CreateDirectory(_documentsFolder);
        }

        public void Create(NewDocument document)
        {
            //TODO: Несколько ответственностей
            var category = _categoriesRepository.GetCategory(document.CategoryId);
            var template = category.CustomTemplateFileName ??= category.DocumentType.TemplateFileName;

            var fileFolder = @"Documents\";
            fileFolder += category.ParentCategoryId == null ? @$"{category.Name}\" : @$"{category.ParentCategory.Name}\{category.Name}\";

            var docFilename = $"{category.Name}_{category.LogBook.LastDocumentNumber + 1}_{template.Split("\\").Last()}";
            Fill(document.Fields, template, fileFolder, docFilename);

            var content = string.Join(",", document.Fields.Select(x => x.Value));
            
            //TODO: заменить.
            var docName = fileFolder + docFilename;

            _documentsRepository.AddDocument(document.CategoryId, docName, content, docName);
        }

        private void Fill(List<ReplaceField> fields, string templateFilename, string folder, string documentFilename)
        {
            //TODO: Несколько ответственностей
            Directory.CreateDirectory(folder);
            File.Copy(templateFilename, @$"{folder}{documentFilename}", true);

            var file = File.ReadAllText(@$"{folder}{documentFilename}");

            var gender = fields.SingleOrDefault(f => f.Name == "$Местоимение_на_основании_пола$");
            if(gender != null)
                gender.Value = GetGenderPronoun(gender.Value);

            fields.ForEach(f =>
            {
                file = file.Replace(f.Name, f.Value);
            });

            File.WriteAllText(@$"{folder}{documentFilename}", file);
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

using System.Linq;

namespace DocumentWorkflow.Core.Services
{
    public class TemplateParser
    {
        public TemplateParser()
        {
            
        }

        private List<TemplateField> _replaceFields = new List<TemplateField>
        {
            new() { Name = "$Полное_наименование_организации$", Type = TemplateField.InputTypes.text},
            new () { Name = "$ИНН$", Type = TemplateField.InputTypes.text},
            new () { Name = "$КПП$", Type = TemplateField.InputTypes.text},
            new () { Name = "$Адрес$", Type = TemplateField.InputTypes.text},
            new () { Name = "$Телефон$", Type = TemplateField.InputTypes.text},
            new () { Name = "$Адрес_эп$", Type = TemplateField.InputTypes.text},
            new () { Name = "$Номер_документа$", Type = TemplateField.InputTypes.text},
            new () { Name = "$День$", Type = TemplateField.InputTypes.number},
            new () { Name = "$Месяц_прописью$", Type = TemplateField.InputTypes.text},
            new () { Name = "$Год$", Type = TemplateField.InputTypes.number},
            new () { Name = "$Ученик_ФИО$", Type = TemplateField.InputTypes.text, VisibleForUser = true},
            new () { Name = "$Местоимение_на_основании_пола$", Type = TemplateField.InputTypes.text},
            new () { Name = "$Учебный_год$", Type = TemplateField.InputTypes.number},
            new () { Name = "$Ученик_класс$", Type = TemplateField.InputTypes.text},
            new () { Name = "$Дата_окончания_уг$", Type = TemplateField.InputTypes.number},
            new () { Name = "$Полное_наименование_организации_в_родительном_падеже$", Type = TemplateField.InputTypes.text},
        };

        public List<TemplateField> GetFields(string filename)
        {
            var fields = new List<TemplateField>();

            if (filename.Contains("certificate.html") == false) return fields;

            var file = File.ReadAllLines(filename);

            foreach (var line in file)
            {
                fields.AddRange(_replaceFields.Where(replaceField => replaceField.VisibleForUser && line.Contains(replaceField.Name)));
            }

            return fields;
        }
    }
}

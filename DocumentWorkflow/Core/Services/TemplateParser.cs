using System.Linq;
using DocumentWorkflow.Core.Extensions;
using static DocumentWorkflow.Core.Services.TemplateField.InputTypes;

namespace DocumentWorkflow.Core.Services
{
    public class TemplateParser
    {
        private readonly OrgSettings _settings;

        public TemplateParser(OrgSettings settings)
        {
            _settings = settings;

            _replaceFields = new List<TemplateField>
            {
                new ("$Полное_наименование_организации_в_родительном_падеже$", text, 0, _settings.FullNameGenitiveCase),
                new ("$Полное_наименование_организации$", text, 0, _settings.FullName),
                new ("$ИНН$", text, 0, _settings.INN),
                new ("$КПП$", text, 0, _settings.KPP),
                new ("$Адрес$", text, 0, _settings.Address),
                new ("$Телефон$", text, 0, _settings.Phone),
                new ("$Адрес_эп$", text, 0, _settings.Email),
                new ("$День$", number, 0, DateTime.Now.Day.ToString()),
                new ("$Месяц_прописью$", text, 0, DateTime.Now.ToMonthGenitiveCaseName()),
                new ("$Год$", number, 0, DateTime.Now.Year.ToString()),
                new ("$Ученик_ФИО$", text, 3) { VisibleForUser = true},
                new ("$Местоимение_на_основании_пола$", text, 4),
                new ("$Ученик_класс$", text, 5) { VisibleForUser = true, IsDisabled = true},
                new ("$Учебный_год$", number, 6, GetStartEducationYear()) {VisibleForUser = true, IsDisabled = true},
                new ("$Дата_окончания_уг$", number, 7, GetEndEducationYear()) {VisibleForUser = true, IsDisabled = true},
            };
        }

        private string GetStartEducationYear()
        {
            return DateTime.Now.Month < 9 ? DateTime.Now.AddYears(-1).Year.ToString() : DateTime.Now.Year.ToString();
        }

        private string GetEndEducationYear()
        {
            return DateTime.Now.Month < 9 ? DateTime.Now.Year.ToString() : DateTime.Now.AddYears(1).Year.ToString();
        }

        private List<TemplateField> _replaceFields;

        public List<TemplateField> GetFields(string filename)
        {
            var fields = new List<TemplateField>();

            if (string.IsNullOrWhiteSpace(filename)) return fields;

            //TODO: Убрать когда появятся все шаблоны
            if (filename.Contains("certificate.html") == false) return fields;
            //

            var file = File.ReadAllLines(filename);

            foreach (var line in file)
            {
                fields.AddRange(_replaceFields.Where(replaceField => line.Contains(replaceField.Name)));
            }

            return fields;
        }
    }
}

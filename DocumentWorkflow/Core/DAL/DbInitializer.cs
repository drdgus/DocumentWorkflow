using DocumentWorkflow.Core.DAL.Entities;
using System.Security.Cryptography;
using System.Text;

namespace DocumentWorkflow.Core.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(DbContext context)
        {
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Roles.AddRange(new[]
            {
                new Role
                {
                    Id = 1,
                    Name = "Админ",
                    IsAdmin = true,
                    CanChangeDate = true
                },
                new Role{
                    Id = 2,
                    Name = "Все категории и типы",
                    IsAdmin = false,
                    CanChangeDate = false
                },
                new Role{
                    Id = 3,
                    Name = "Изменение даты документа",
                    IsAdmin = false,
                    CanChangeDate = true
                }
            });
            context.SaveChanges();

            context.Users.AddRange(new[]
            {
                new User
                {
                    Id = 1,
                    Name = "CoolAdmin_007",
                    Password = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("123123")),
                },
                new User
                {
                    Id = 2,
                    Name = "User_DocumentCreator",
                    Password = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("Admin")),
                }
            });
            context.SaveChanges();

            context.UsersRoles.AddRange(new[]
            {
                new UserRoles
                {
                    UserId = 1,
                    RoleId = 1
                },
                new UserRoles
                {
                    UserId = 2,
                    RoleId = 2
                },
                new UserRoles
                {
                    UserId = 2,
                    RoleId = 3
                }
            });
            context.SaveChanges();

            context.DocumentTypes.AddRange(new[]
            {
                new DocumentType
                {
                    Id = 1,
                    Name = "Приказы",
                    TemplateFileName = "Приказы.html"
                },
                new DocumentType
                {
                    Id = 2,
                    Name = "Заявления",
                    TemplateFileName = "Заявления.html"
                },
                new DocumentType
                {
                    Id = 3,
                    Name = "Ходатайства",
                    TemplateFileName = "Ходатайства.html"
                },
                new DocumentType
                {
                    Id = 4,
                    Name = "Объяснительные",
                    TemplateFileName = "Объяснительные.html"
                },
                new DocumentType
                {
                    Id = 5,
                    Name = "Справки",
                    TemplateFileName = @"Templates\certificate.html"
                },
            });
            context.SaveChanges();

            context.LogBooks.Add(new LogBook
            {
                Id = 1,
                Name = "Журнал регистрации справок",
                LastDocumentNumber = 0,
                NumberingResetDate = new DateTime(2022, 10, 1),
                LastNumberingResetDate = default
            });
            context.SaveChanges();

            context.DocumentCategories.AddRange(new[]
            {
                new DocumentCategory
                {
                    Id = 1,
                    ParentCategoryId = null,
                    Name = "Все",
                    DocumentTypeId = 5,
                    CustomTemplateFileName = null,
                    LogBookId = 1,
                },
                new DocumentCategory
                {
                    Id = 2,
                    ParentCategoryId = 1,
                    Name = "Справка",
                    DocumentTypeId = 5,
                    CustomTemplateFileName = null,
                    LogBookId = 1,
                    RequiredModule = RequiredModule.Students
                },
                new DocumentCategory
                {
                    Id = 3,
                    ParentCategoryId = 1,
                    Name = "Справка для работника",
                    DocumentTypeId = 5,
                    CustomTemplateFileName = null,
                    LogBookId = 1,
                    RequiredModule = RequiredModule.Employees
                }
            });
            context.SaveChanges();

            context.CategoriesRights.AddRange(new []
            {
                new CategoryRights
                {
                    CanWrite = true,
                    RoleId = 2,
                    DocumentCategoryId = null,
                }
            });

            var rnd = new Random();
            context.Students.AddRange(Enumerable.Range(1, 370).Select(i => new Student
            {
                Id = i,
                FullName = $"Ученик №{i}",
                BirthDay = new DateTime(2015, rnd.Next(1, 12), rnd.Next(1, 28)),
                Class = rnd.Next(1, 11) + (i % 3 == 0 ? "А" : "Б"),
                Gender = i % 2 == 0 ? Student.Genders.Male : Student.Genders.Female
            }));

            context.Employees.AddRange(Enumerable.Range(1, 67).Select(i => new Employee
            {
                Id = i,
                FullName = $"Работник № {i}",
                BirthDay = new DateTime(rnd.Next(1960, 2003), rnd.Next(1, 12), rnd.Next(1, 28)),
                Position = $"Должность {rnd.Next(1, 20)}"
            }));

            context.Documents.AddRange(Enumerable.Range(1, 100).Select(i => new Document
            {
                Id = i,
                Number = (i % 10 == 0 ? i + 0.1f : i),
                CreatedDate = DateTime.Now.AddHours(-1).AddSeconds(i),
                Name = $"Название {i}",
                Content = "Et aliquip lorem et eu et facilisi sed sit tempor amet ipsum vel amet justo eirmod sed ipsum sea rebum",
                FileName = $"{i}.html",
                UserId = 1,
                DocumentCategoryId = rnd.Next(2,3)
            }));
            context.SaveChanges();

            context.DocumentsHistory.AddRange(Enumerable.Range(1, 100).Select(i => new History
            {
                Id = i,
                DocumentId = i,
                ChangeDate = DateTime.Now.AddHours(-1).AddSeconds(i),
                EditedField = "Создание",
                OldValue = "",
                NewValue = ""
            }));

            context.LogBooks.First(b => b.Id == 1).LastDocumentNumber = 100;

            context.SaveChanges();
        }
    }
}

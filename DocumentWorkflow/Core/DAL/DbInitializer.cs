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
                new UserRoles(1, 1),
                new UserRoles(2, 2),
                new UserRoles(2, 3)

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
                    TemplateFileName = "Справки.html"
                },
            });
            context.SaveChanges();

            context.LogBooks.Add(new LogBook
            {
                Id = 1,
                Name = "Журнал регистрации справок",
                NumberingResetDate = new DateTime(2022, 10, 1),
                LastNumberingResetDate = default
            });
            context.SaveChanges();

            context.DocumentCategories.AddRange(new[]
            {
                new DocumentCategory
                {
                    Id = 1,
                    ParentId = null,
                    Name = "Все",
                    DocumentTypeId = 5,
                    CustomTemplateFileName = null,
                    LogBookId = 1,
                }
            });
            context.SaveChanges();

            context.CategoriesRights.AddRange(new []
            {
                new CategoryRights(2, null, true)
            });

            var rnd = new Random();
            context.Students.AddRange(Enumerable.Range(1, 370).Select(i => new Student
            {
                Id = i,
                FullName = $"Ученик №{i}",
                BirthDay = new DateTime(2015, rnd.Next(1, 12), rnd.Next(1, 30)),
                Class = rnd.Next(1, 11) + (i % 3 == 0 ? "А" : "Б"),
                Gender = i % 2 == 0 ? Student.Genders.Male : Student.Genders.Female
            }));

            context.Employees.AddRange(Enumerable.Range(1, 67).Select(i => new Employee
            {
                Id = i,
                FullName = $"Работник № {i}",
                BirthDay = new DateTime(rnd.Next(1960, 2003), rnd.Next(1, 12), rnd.Next(1, 30)),
                Position = $"Должность {rnd.Next(1, 20)}"
            }));

            context.SaveChanges();
        }
    }
}

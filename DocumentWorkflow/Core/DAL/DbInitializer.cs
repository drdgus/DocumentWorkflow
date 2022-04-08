using System.Security.Cryptography;
using System.Text;
using DocumentWorkflow.Core.DAL.Entities;

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

            context.DocumentCategories.AddRange(new[]
            {
                new DocumentCategory
                {
                    Id = 1,
                    ParentId = null,
                    Name = "Категория документа №1"
                },
                new DocumentCategory
                {
                    Id = 2,
                    ParentId = 1,
                    Name = "Под категория документа №1_1"
                },
            });
            context.SaveChanges();

            context.DocumentTypes.AddRange(new []
            {
                new DocumentType
                {
                    Id = 1,
                    Name = "Тип документа №1",
                    DocumentCategoryId = 1
                },
                new DocumentType
                {
                    Id = 2,
                    Name = "Тип документа №2_1",
                    DocumentCategoryId = 2
                },
                new DocumentType
                {
                    Id = 2,
                    Name = "Тип документа №2_2",
                    DocumentCategoryId = 2
                }
            });
            context.SaveChanges();

            context.Templates.AddRange(new[]
            {
                new Template
                {
                    Id = 1,
                    DocumentTypeId = 1,
                    CreatedDate = DateTime.Now.AddDays(-5),
                    Filename = "TemplateFile1_1.xls"
                },
                new Template
                {
                    Id = 2,
                    DocumentTypeId = 1,
                    CreatedDate = DateTime.Now,
                    Filename = "TemplateFile1-2.xls"
                },
                new Template
                {
                    Id = 3,
                    DocumentTypeId = 2,
                    CreatedDate = DateTime.Now,
                    Filename = "TemplateFile2_1.xls"
                }
            });
            context.SaveChanges();

            context.Roles.AddRange(new []
            {
                new Role
                {
                    Id = 1,
                    Name = "Администратор",
                    IsAdmin = true,
                },
                new Role
                {
                    Id = 2,
                    Name = "Секретарь",
                    IsAdmin = false
                }
            });
            context.SaveChanges();

            context.Rights.AddRange(new []
            {
                new Right
                {
                    RoleId = 1,
                    DocumentCategoryId = 0,
                    CanWrite = true
                },
                new Right
                {
                    RoleId = 2,
                    DocumentCategoryId = 1,
                    CanWrite = false
                },
                new Right
                {
                    RoleId = 2,
                    DocumentCategoryId = 2,
                    CanWrite = true
                },
            });

            context.Users.AddRange(new []
            {
                new User
                {
                    Id = 1,
                    Name = "Cool_Admin007",
                    Password = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("Admin")),
                },
                new User
                {
                    Id = 2,
                    Name = "User_DocumentCreator",
                    Password = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes("123123")),
                }
            });
            context.SaveChanges();

            context.UsersRoles.AddRange(new []
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
            });
            context.SaveChanges();

            context.Documents.Add(new Document
            {
                Id = 1,
                Number = "№1",
                Name = "Doc №1_1",
                Content =
                    "Kasd dolores nulla et tempor et eum rebum invidunt rebum lorem eu takimata sed lorem lorem est aliquyam sed consetetur",
                CreatedDate = DateTime.Now,
                UserId = 2,
                DocumentCategoryId = 1
            });
            context.SaveChanges();

            context.DocumentsHistory.Add(new History
            {
                Id = 1,
                DocumentId = 1,
                ChangeDate = DateTime.Now,
                EditedField = "All",
                OldValue = "",
                NewValue = "Created"
            });

            context.SaveChanges();
        }
    }
}

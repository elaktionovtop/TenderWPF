﻿using Microsoft.Extensions.DependencyInjection;

using TenderApp.Models;

namespace TenderApp.Data
{
    public static class DbVerification
    {
        static TenderDbContext _db = null!;

        public static void Perform()
        {
            _db = App.Services.GetRequiredService<TenderDbContext>();
            _db.Database.EnsureCreated();

            VerifyRoles();
            VerifyUsers();
            VerifyCriteria();
            VerifyTenders();
            VerifyTenderCriteria();
            VerifyProposals();
            VerifyProposalValues();
            VerifyContracts();
        }

        static void VerifyRoles()
        {
            if(!_db.Roles.Any())
            {
                _db.Roles.AddRange
                (
                    new Role 
                    { 
                        Code = RoleCode.Admin, 
                        Name = "Admin" 
                    },
                    new Role 
                    { 
                        Code = RoleCode.CategoryManager, 
                        Name = "CategoryManager" 
                    },
                    new Role 
                    { 
                        Code = RoleCode.Buyer, 
                        Name = "Buyer" 
                    }
                );
                _db.SaveChanges();
            }
        }

        static void VerifyUsers()
        {
            if(!_db.Users.Any())
            {
                _db.Users.AddRange
                (
                    new User 
                    { 
                        Name = "Админ", 
                        Login = "Admin", 
                        Password = "123", 
                        RoleId = 1 
                    },
                    new User 
                    { 
                        Name = "Менеджер Категорий",
                        Login = "Man", 
                        Password = "333",
                        RoleId = 2 }
                    ,
                    new User 
                    { 
                        Name = "Байер 1", 
                        Login = "Buyer1",
                        Password = "111",
                        RoleId = 3 
                    },
                    new User 
                    { 
                        Name = "Байер 2",
                        Login = "Buyer2",
                        Password = "222",
                        RoleId = 3 
                    }
                );
                _db.SaveChanges();
            }
        }

        static void VerifyCriteria()
        {
            if(!_db.Criteria.Any())
            {
                _db.Criteria.AddRange
                (
                    new Criterion 
                    { 
                        Name = "Срок поставки",
                        Type = CriterionType.Numeric 
                    },
                    new Criterion 
                    { 
                        Name = "Опыт поставщика",
                        Type = CriterionType.Text 
                    }
                );
                _db.SaveChanges();
            }
        }

        static void VerifyTenders()
        {
            if(!_db.Tenders.Any())
            {
                var creatorId = _db.Users.First(u => u.Role.Code == RoleCode.CategoryManager).Id;
                _db.Tenders.AddRange
                (
                    new Tender 
                    {
                        Name = "Тендер 1",
                        Product = "Кабель", 
                        Budget = 100000, 
                        Quantity = 100, 
                        CreatedById = creatorId
                    },
                    new Tender 
                    {
                        Name = "Тендер 2",
                        Product = "Щиты", 
                        Budget = 150000, 
                        Quantity = 50, 
                        CreatedById = creatorId
                    }
                );
                _db.SaveChanges();
            }
        }

        static void VerifyTenderCriteria()
        {
            if(!_db.TenderCriteria.Any())
            {
                var tender1 = _db.Tenders.First(t => t.Id == 1);
                var crit1 = _db.Criteria.First(c => c.Name == "Срок поставки");
                var crit2 = _db.Criteria.First(c => c.Name == "Опыт поставщика");

                _db.TenderCriteria.AddRange
                (
                    new TenderCriterion 
                    { 
                        TenderId = tender1.Id, 
                        CriterionId = crit1.Id, 
                        Weight = 5, 
                        IsRequired = true 
                    },
                    new TenderCriterion 
                    { 
                        TenderId = tender1.Id, 
                        CriterionId = crit2.Id, 
                        Weight = 3, 
                        IsRequired = false 
                    }
                );
                _db.SaveChanges();
            }
        }

        static void VerifyProposals()
        {
            if(!_db.Proposals.Any())
            {
                var tender = _db.Tenders.First(t => t.Id == 1);
                var byers = _db.Users.Where(u => u.Role.Code == RoleCode.Buyer).Take(2).ToArray();

                _db.Proposals.AddRange
                (
                    new Proposal 
                    { 
                        TenderId = tender.Id, 
                        ByerId = byers[0].Id, 
                        Comment = "Вариант 1" }
                    ,
                    new Proposal { 
                        TenderId = tender.Id, 
                        ByerId = byers[1].Id, 
                        Comment = "Вариант 2" 
                    }
                );
                _db.SaveChanges();
            }
        }

        static void VerifyProposalValues()
        {
            if(!_db.CriterionValues.Any())
            {
                var proposals = _db.Proposals.ToList();
                var crits = _db.Criteria.ToList();

                _db.CriterionValues.AddRange
                (
                    new CriterionValue
                    { 
                        ProposalId = proposals[0].Id, 
                        TenderCriterionId = crits[0].Id, 
                        Value = "10" 
                    },
                    new CriterionValue
                    { 
                        ProposalId = proposals[0].Id,
                        TenderCriterionId = crits[1].Id, 
                        Value = "5 лет" 
                    },
                    new CriterionValue
                    { 
                        ProposalId = proposals[1].Id,
                        TenderCriterionId = crits[0].Id, 
                        Value = "8" 
                    },
                    new CriterionValue
                    { 
                        ProposalId = proposals[1].Id,
                        TenderCriterionId = crits[1].Id, 
                        Value = "3 года" 
                    }
                );
                _db.SaveChanges();
            }
        }

        static void VerifyContracts()
        {
            //if(!_db.Contracts.Any())
            //{
            //    var proposal = _db.Proposals.First(p => p.Id == 1);

            //    _db.Contracts.Add
            //    (
            //        new Contract
            //        {
            //            ProposalId = proposal.Id,
            //            Details = "Новый контракт",
            //            FilePath = "Контракт01.docx"
            //        }
            //    );
            //    _db.SaveChanges();
            //}
        }
    }
}

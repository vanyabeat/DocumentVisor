using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DocumentVisor.Model.Data;

namespace DocumentVisor.Model
{
    public class DataWorker
    {
        #region Person

        public static List<Person> GetAllPersons()
        {
            using var db = new ApplicationContext();
            var result = db.Persons.ToList();
            return result;
        }

        public static string CreatePerson(string name, string info, string phone, string rank, PersonType personType)
        {
            var result = Dictionary["Insert"].ToString();
            using var db = new ApplicationContext();
            var checkIsExist = db.Persons.Any(el => el.Name == name && el.Phone == phone && el.Type == personType);
            if (!checkIsExist)
            {
                db.Persons.Add(
                    new Person {Name = name, Phone = phone, Info = info, Rank = rank, TypeId = personType.Id});
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string DeletePerson(Person person)
        {
            var result = Dictionary["PersonNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                db.Persons.Remove(person);
                db.SaveChanges();
                result = $"{Dictionary["PersonDeleted"]} {person}";
            }

            return result;
        }

        public static string EditPerson(Person oldPerson, string newName, string newInfo, string newPhone,
            string newRank, PersonType newType)
        {
            var result = Dictionary["PersonTypeNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                var person = db.Persons.FirstOrDefault(d => d.Id == oldPerson.Id);
                person.Name = newName;
                person.Info = newInfo;
                person.Phone = newPhone;
                person.TypeId = newType.Id;
                person.Type = newType;
                person.Rank = newRank;

                db.SaveChanges();
                result = $"{Dictionary["PersonEdited"]} {person}";
            }

            return result;
        }

        #endregion

        #region System

        private static readonly ResourceDictionary Dictionary = new ResourceDictionary()
        {
            Source = new Uri(@"pack://application:,,,/Resources/StringResource.xaml")
        };

        #endregion

        #region PersonTypes

        public static List<PersonType> GetAllPersonTypes()
        {
            using var db = new ApplicationContext();
            var result = db.PersonTypes.ToList();
            return result;
        }

        public static string CreatePersonType(string name, string info)
        {
            var result = Dictionary["Insert"].ToString();
            using var db = new ApplicationContext();
            var checkIsExist = db.PersonTypes.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                db.PersonTypes.Add(new PersonType {Name = name, Info = info});
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string DeletePersonType(PersonType personType)
        {
            var result = Dictionary["PersonTypeNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                try
                {
                    db.PersonTypes.Remove(personType);
                    db.SaveChanges();
                    result = $"{Dictionary["PersonTypeDeleted"]} {personType}";
                }
                catch (Exception e)
                {
                    result = $"{Dictionary["PersonTypeDeleteError"]}\n{e.Message}";
                }
            }

            return result;
        }

        public static string EditPersonType(PersonType oldPersonType, string newName, string newInfo)
        {
            var result = Dictionary["PersonTypeNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                var personType = db.PersonTypes.FirstOrDefault(d => d.Id == oldPersonType.Id);
                personType.Name = newName;
                personType.Info = newInfo;
                db.SaveChanges();
                result = $"{Dictionary["PersonTypeEdited"]} {personType}";
            }

            return result;
        }

        public static PersonType GetPersonTypeById(int typeId)
        {
            using (var db = new ApplicationContext())
            {
                var pos = db.PersonTypes.FirstOrDefault(p => p.Id == typeId);
                return pos;
            }
        }

        #endregion

        #region Privacies

        public static List<Privacy> GetAllPrivacies()
        {
            using var db = new ApplicationContext();
            var result = db.Privacies.ToList();
            return result;
        }

        public static string CreatePrivacy(string name, string info)
        {
            var result = Dictionary["Insert"].ToString();
            using var db = new ApplicationContext();
            var checkIsExist = db.Privacies.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                db.Privacies.Add(new Privacy {Name = name, Info = info});
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string DeletePrivacy(Privacy privacy)
        {
            var result = Dictionary["PrivacyNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                try
                {
                    db.Privacies.Remove(privacy);
                    db.SaveChanges();
                    result = $"{Dictionary["PrivacyDeleted"]} {privacy}";
                }
                catch (Exception e)
                {
                    result = $"{Dictionary["PrivacyDeleteError"]}\n{e.Message}";
                }
            }

            return result;
        }

        public static string EditPrivacy(Privacy oldPrivacy, string newName, string newInfo)
        {
            var result = Dictionary["PrivacyNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                var privacy = db.Privacies.FirstOrDefault(d => d.Id == oldPrivacy.Id);
                privacy.Name = newName;
                privacy.Info = newInfo;
                db.SaveChanges();
                result = $"{Dictionary["PrivacyEdited"]} {oldPrivacy}";
            }

            return result;
        }

        public static Privacy GetPrivacyById(int privacyId)
        {
            using (var db = new ApplicationContext())
            {
                var pos = db.Privacies.FirstOrDefault(p => p.Id == privacyId);
                return pos;
            }
        }

        #endregion

        #region Themes

        public static List<Theme> GetAllThemes()
        {
            using var db = new ApplicationContext();
            var result = db.Themes.ToList();
            return result;
        }

        public static string CreateTheme(string name, string info)
        {
            var result = Dictionary["Insert"].ToString();
            using var db = new ApplicationContext();
            var checkIsExist = db.Themes.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                db.Themes.Add(new Theme {Name = name, Info = info});
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string DeleteTheme(Theme theme)
        {
            var result = Dictionary["PrivacyNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                try
                {
                    db.Themes.Remove(theme);
                    db.SaveChanges();
                    result = $"{Dictionary["ThemeDeleted"]} {theme}";
                }
                catch (Exception e)
                {
                    result = $"{Dictionary["ThemeDeleteError"]}\n{e.Message}";
                }
            }

            return result;
        }

        public static string EditTheme(Theme oldTheme, string themeName, string themeInfo)
        {
            var result = Dictionary["PrivacyNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                var privacy = db.Themes.FirstOrDefault(d => d.Id == oldTheme.Id);
                privacy.Name = themeName;
                privacy.Info = themeInfo;
                db.SaveChanges();
                result = $"{Dictionary["PrivacyEdited"]} {oldTheme}";
            }

            return result;
        }

        #endregion

        #region Divisions

        public static List<Division> GetAllDivisions()
        {
            using var db = new ApplicationContext();
            var result = db.Divisions.ToList();
            return result;
        }

        public static string CreateDivision(string name, string address, string info)
        {
            var result = Dictionary["Insert"].ToString();
            using var db = new ApplicationContext();
            var checkIsExist = db.Divisions.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                db.Divisions.Add(new Division {Name = name, Address = address, Info = info});
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string DeleteDivision(Division division)
        {
            var result = Dictionary["PrivacyNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                try
                {
                    db.Divisions.Remove(division);
                    db.SaveChanges();
                    result = $"{Dictionary["DivisionDeleted"]} {division}";
                }
                catch (Exception e)
                {
                    result = $"{Dictionary["DivisionDeleteError"]}\n{e.Message}";
                }
            }

            return result;
        }

        public static string EditDivision(Division oldDiv, string divName, string divAddress, string divInfo)
        {
            var result = Dictionary["DivisionNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                var privacy = db.Divisions.FirstOrDefault(d => d.Id == oldDiv.Id);
                privacy.Name = divName;
                privacy.Info = divInfo;
                privacy.Address = divAddress;
                db.SaveChanges();
                result = $"{Dictionary["PrivacyEdited"]} {oldDiv}";
            }

            return result;
        }

        #endregion

        #region Articles

        public static List<Article> GetAllArticles()
        {
            using var db = new ApplicationContext();
            var result = db.Articles.ToList();
            return result;
        }

        public static string CreateArticle(string name, string extendedName, string info)
        {
            var result = Dictionary["Insert"].ToString();
            using var db = new ApplicationContext();
            var checkIsExist = db.Articles.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                db.Articles.Add(new Article {Name = name, ExtendedName = extendedName, Info = info});
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string DeleteArticle(Article article)
        {
            var result = Dictionary["ArticleNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                try
                {
                    db.Articles.Remove(article);
                    db.SaveChanges();
                    result = $"{Dictionary["ArticleDeleted"]} {article}";
                }
                catch (Exception e)
                {
                    result = $"{Dictionary["ArticleDeleteError"]}\n{e.Message}";
                }
            }

            return result;
        }

        public static string EditArticle(Article oldArticle, string articleName, string articleExtendedName,
            string articleInfo)
        {
            var result = Dictionary["ArticleNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                var article = db.Articles.FirstOrDefault(d => d.Id == oldArticle.Id);
                article.Name = articleName;
                article.Info = articleInfo;
                article.ExtendedName = articleExtendedName;
                db.SaveChanges();
                result = $"{Dictionary["ArticleEdited"]} {oldArticle}";
            }

            return result;
        }

        #endregion

        #region QueryTypes

        public static List<QueryType> GetAllQueryTypes()
        {
            using var db = new ApplicationContext();
            var result = db.QueryTypes.ToList();
            return result;
        }

        public static string CreateQueryType(string name, string info)
        {
            var result = Dictionary["Insert"].ToString();
            using var db = new ApplicationContext();
            var checkIsExist = db.QueryTypes.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                db.QueryTypes.Add(new QueryType { Name = name, Info = info });
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string DeleteQueryType(QueryType docType)
        {
            var result = Dictionary["QueryTypeNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                try
                {
                    db.QueryTypes.Remove(docType);
                    db.SaveChanges();
                    result = $"{Dictionary["QueryTypeDeleted"]} {docType}";
                }
                catch (Exception e)
                {
                    result = $"{Dictionary["QueryTypeDeleteError"]}\n{e.Message}";
                }
            }

            return result;
        }

        public static string EditQueryType(QueryType oldDocType, string docTypeName, string docTypeInfo)
        {
            var result = Dictionary["PrivacyNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                var type = db.QueryTypes.FirstOrDefault(d => d.Id == oldDocType.Id);
                type.Name = docTypeName;
                type.Info = docTypeInfo;
                db.SaveChanges();
                result = $"{Dictionary["QueryTypeEdited"]} {oldDocType}";
            }

            return result;
        }

        #endregion

        #region Actions

        public static List<Action> GetAllActions()
        {
            using var db = new ApplicationContext();
            var result = db.Actions.ToList();
            return result;
        }

        public static string CreateAction(string name, string number, string info)
        {
            var result = Dictionary["Insert"].ToString();
            using var db = new ApplicationContext();
            var checkIsExist = db.Actions.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                db.Actions.Add(new Action { Name = name, Number = number, Info = info });
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string DeleteAction(Action action)
        {
            var result = Dictionary["ActionNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                try
                {
                    db.Actions.Remove(action);
                    db.SaveChanges();
                    result = $"{Dictionary["ActionDeleted"]} {action}";
                }
                catch (Exception e)
                {
                    result = $"{Dictionary["ActionDeleteError"]}\n{e.Message}";
                }
            }

            return result;
        }

        public static string EditAction(Action oldAction, string actionName, string actionNumber, string actionInfo)
        {
            var result = Dictionary["ActionNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                var action = db.Actions.FirstOrDefault(d => d.Id == oldAction.Id);
                action.Name = actionName;
                action.Info = actionInfo;
                action.Number = actionNumber;
                db.SaveChanges();
                result = $"{Dictionary["ActionEdited"]} {oldAction}";
            }

            return result;
        }

        #endregion

        #region Queries

        public static List<Query> GetAllQueries()
        {
            using var db = new ApplicationContext();
            var result = db.Queries.ToList();
            return result;
        }

        #endregion
    }
}
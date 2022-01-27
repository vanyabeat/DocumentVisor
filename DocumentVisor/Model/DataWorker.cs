using DocumentVisor.Model.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;

namespace DocumentVisor.Model
{
    public class DataWorker
    {
        #region QueryBlobData
        public static int CreateQueryBlobData(int queryId, uint size, byte[] data)
        {
            using var db = new ApplicationContext();
            int result = -1;
            var checkIsExist = db.QueriesBlobDatas.Any(el => el.Id == queryId);
            if (!checkIsExist)
            {
                var entity =  db.QueriesBlobDatas.Add(new QueryBlobData { Id = queryId, BytesSize = size, Data = data});
                db.SaveChanges();
                result = entity.Entity.Id;
            }

            return result;
        }

        public static int EditQueryBlobData(int queryId, uint size, byte[] data)
        {
            int result = -1;
            using (var db = new ApplicationContext())
            {
                var n = db.QueriesBlobDatas.FirstOrDefault(d => d.Id == queryId);
                n.BytesSize = size;
                n.Data = data;
                db.SaveChanges();
                result = n.Id;
            }

            return result;
        }


        #endregion
        #region IdentifierTypes
        public static ObservableCollection<IdentifierType> GetAllIdentifierTypes()
        {
            using var db = new ApplicationContext();
            return new ObservableCollection<IdentifierType>(db.IdentifierTypes);
        }

        public static string GetJsonString(object obj)
        {
           return JsonConvert.SerializeObject(obj);
        }
        public static string CreateIdentifierType(string name, string info)
        {
            var result = Dictionary["Insert"].ToString();
            using var db = new ApplicationContext();
            var checkIsExist = db.IdentifierTypes.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                db.IdentifierTypes.Add(new IdentifierType { Name = name, Info = info });
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static int CreateIdentifier(string content, int typeId)
        {
            var result = -1;
            using var db = new ApplicationContext();
            var checkIsExist = db.Identifiers.Any(el => el.Content == content && el.IdentifierTypeId == typeId);
            try
            {
                if (!checkIsExist)
                {
                    var entity = db.Identifiers.Add(new Identifier { Content = content, IdentifierTypeId = typeId });
                    db.SaveChanges();
                    result = entity.Entity.Id;
                }
            }
            catch (Exception e)
            {
                ////
            }


            return result;
        }

        public static string DeleteIdentifierType(IdentifierType idT)
        {
            var result = Dictionary["PersonTypeNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                try
                {
                    db.IdentifierTypes.Remove(idT);
                    db.SaveChanges();
                    result = $"{Dictionary["IdentifierTypeDeleted"]} {idT}";
                }
                catch (Exception e)
                {
                    result = $"{Dictionary["IdentifierTypeDeleteError"]}\n{e.Message}";
                }
            }

            return result;
        }

        public static string EditIdentifierType(IdentifierType old, string newName, string newInfo)
        {
            var result = Dictionary["IdentifierTypeNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                var n = db.IdentifierTypes.FirstOrDefault(d => d.Id == old.Id);
                n.Name = newName;
                n.Info = newInfo;
                db.SaveChanges();
                result = $"{Dictionary["IdentifierTypeEdited"]} {n}";
            }

            return result;
        }

        public static IdentifierType GetIdentifierTypeById(int id)
        {
            using (var db = new ApplicationContext())
            {
                var pos = db.IdentifierTypes.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        public static Identifier GetIdentifierById(int id)
        {
            using (var db = new ApplicationContext())
            {
                var pos = db.Identifiers.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        #endregion
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

        public static Person GetPersonById(int id)
        {
            using (var db = new ApplicationContext())
            {
                var pos = db.Persons.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        #endregion

        #region System

        private static readonly ResourceDictionary Dictionary = new ResourceDictionary()
        {
            Source = new Uri(@"pack://application:,,,/Resources/StringResource.xaml")
        };

        #endregion

        #region PersonTypes

        public static ObservableCollection<PersonType> GetAllPersonTypes()
        {
            using var db = new ApplicationContext();
            return new ObservableCollection<PersonType>(db.PersonTypes);
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

        public static Theme GetThemeById(int id)
        {
            using (var db = new ApplicationContext())
            {
                var pos = db.Themes.FirstOrDefault(p => p.Id == id);
                return pos;
            }
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

        public static Division GetDivisionById(int id)
        {
            using (var db = new ApplicationContext())
            {
                var pos = db.Divisions.FirstOrDefault(p => p.Id == id);
                return pos;
            }
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

        public static Article GetArticleById(int id)
        {
            using (var db = new ApplicationContext())
            {
                var pos = db.Articles.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        #endregion

        #region Types

        public static List<Type> GetAllQueryTypes()
        {
            using var db = new ApplicationContext();
            var result = db.Types.ToList();
            return result;
        }

        public static string CreateQueryType(string name, string info)
        {
            var result = Dictionary["Insert"].ToString();
            using var db = new ApplicationContext();
            var checkIsExist = db.Types.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                db.Types.Add(new Type {Name = name, Info = info});
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string DeleteQueryType(Type docType)
        {
            var result = Dictionary["QueryTypeNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                try
                {
                    db.Types.Remove(docType);
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

        public static string EditQueryType(Type oldDocType, string docTypeName, string docTypeInfo)
        {
            var result = Dictionary["PrivacyNotExist"].ToString();
            using (var db = new ApplicationContext())
            {
                var type = db.Types.FirstOrDefault(d => d.Id == oldDocType.Id);
                type.Name = docTypeName;
                type.Info = docTypeInfo;
                db.SaveChanges();
                result = $"{Dictionary["QueryTypeEdited"]} {oldDocType}";
            }

            return result;
        }

        public static Type GetTypeById(int id)
        {
            using (var db = new ApplicationContext())
            {
                var pos = db.Types.FirstOrDefault(p => p.Id == id);
                return pos;
            }
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
            var checkIsExist = db.Actions.Any(el => el.Name == name && el.Number == number);
            if (!checkIsExist)
            {
                db.Actions.Add(new Action {Name = name, Number = number, Info = info});
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

        public static Action GetActionById(int id)
        {
            using var db = new ApplicationContext();
            var pos = db.Actions.FirstOrDefault(p => p.Id == id);
            return pos;
        }

        #endregion

        #region Queries

        public static Query GetQueryById(int id)
        {
            using var db = new ApplicationContext();
            var pos = db.Queries.FirstOrDefault(p => p.Id == id);
            return pos;
        }

        public static int GetQueryByGuid(string id)
        {
            using var db = new ApplicationContext();
            var pos = db.Queries.FirstOrDefault(p => p.Guid == id);
            return pos.Id;
        }

        public static List<Query> GetAllQueries()
        {
            using var db = new ApplicationContext();
            var result = db.Queries.ToList();
            return result;
        }

        public static void QueryPersonLink(int queryId, int personId)
        {
            using var db = new ApplicationContext();
            var entity = db.QueryPersons.Add(new QueryPerson {QueryId = queryId, PersonId = personId});
            db.SaveChanges();
        }

        public static void QueryPersonUnLink(int queryId)
        {
            using var db = new ApplicationContext();
            db.QueryPersons.RemoveRange(db.QueryPersons.Where(x => x.QueryId == queryId));
            db.SaveChanges();
        }

        public static void QueryActionLink(int queryId, int actionId)
        {
            using var db = new ApplicationContext();
            var entity = db.QueryActions.Add(new QueryAction {QueryId = queryId, ActionId = actionId});
            db.SaveChanges();
        }

        public static void QueryActionUnLink(int queryId)
        {
            using var db = new ApplicationContext();
            db.QueryActions.RemoveRange(db.QueryActions.Where(x => x.QueryId == queryId));
            db.SaveChanges();
        }

        public static void QueryThemeLink(int queryId, int themeId)
        {
            using var db = new ApplicationContext();
            var entity = db.QueryThemes.Add(new QueryTheme {QueryId = queryId, ThemeId = themeId});
            db.SaveChanges();
        }

        public static void QueryThemeUnLink(int queryId)
        {
            using var db = new ApplicationContext();
            db.QueryThemes.RemoveRange(db.QueryThemes.Where(x => x.QueryId == queryId));
            db.SaveChanges();
        }

        public static void QueryIdentifierLink(int queryId, int identifierId)
        {
            using var db = new ApplicationContext();
            var entity = db.QueryIdentifiers.Add(new QueryIdentifier { QueryId = queryId, IdentifierId = identifierId });
            db.SaveChanges();
        }

        public static void QueryIdentifierUnLink(int identifierId)
        {
            using var db = new ApplicationContext();
            db.QueryIdentifiers.RemoveRange(db.QueryIdentifiers.Where(x => x.QueryId == identifierId));
            db.SaveChanges();
        }

        public static void QueryArticleLink(int queryId, int articleId)
        {
            using var db = new ApplicationContext();
            var entity = db.QueryArticles.Add(new QueryArticle {QueryId = queryId, ArticleId = articleId});
            db.SaveChanges();
        }

        public static void QueryArticleUnLink(int queryId)
        {
            using var db = new ApplicationContext();
            db.QueryArticles.RemoveRange(db.QueryArticles.Where(x => x.QueryId == queryId));
            db.SaveChanges();
        }

        public static List<Theme> GetAllQueryThemes(int queryId)
        {
            using var db = new ApplicationContext();
            return db.QueryThemes.ToList().FindAll(item => item.QueryId == queryId).Select(qt => GetThemeById(qt.ThemeId)).ToList();
        }

        public static List<Article> GetAllQueryArticles(int queryId)
        {
            using var db = new ApplicationContext();
            return db.QueryArticles.ToList().FindAll(item => item.QueryId == queryId).Select(qt => GetArticleById(qt.ArticleId)).ToList();
        }

        public static List<Action> GetAllQueryAction(int queryId)
        {
            using var db = new ApplicationContext();
            return db.QueryActions.ToList().FindAll(item => item.QueryId == queryId).Select(qt => GetActionById(qt.ActionId)).ToList();
        }


        public static List<Identifier> GetAllQueryIdentifiers(int queryId)
        {
            using var db = new ApplicationContext();
            return db.QueryIdentifiers.ToList().FindAll(item => item.QueryId == queryId).Select(qt => GetIdentifierById(qt.IdentifierId)).ToList();
        }

        public static List<Person> GetAllQueryExecutors(int queryId)
        {
            using var db = new ApplicationContext();
            return db.QueryPersons.ToList().FindAll(item => item.QueryId == queryId).Select(qt => GetPersonById(qt.PersonId)).ToList();
        }

        public static int CreateQuery(string name,
            string info,
            string guid,
            Privacy privacy,
            Division division,
            Person signPerson,
            Type type,
            DateTime queryOuterSecretaryDateTime,
            string queryOuterSecretaryNumber,
            DateTime queryInnerSecretaryDateTime,
            string queryInnerSecretaryNumber,
            DateTime queryCentralSecretaryDateTime,
            string queryCentralSecretaryNumber,
            bool hasCd,
            bool various,
            bool empty,
            bool complete
        )
        {
            var result = -1;
            using var db = new ApplicationContext();
            var checkIsExist = db.Queries.Any(el => el.Name == name && el.Guid == guid);
            if (!checkIsExist)
            {
                var entity = db.Queries.Add(new Query
                {
                    Name = name,
                    Info = info,
                    Guid = guid,
                    PrivacyId = privacy.Id,
                    DivisionId = division.Id,
                    SignPersonId = signPerson.Id,
                    TypeId = type.Id,
                    OuterSecretaryDateTime = queryOuterSecretaryDateTime,
                    OuterSecretaryNumber = queryOuterSecretaryNumber,
                    InnerSecretaryDateTime = queryInnerSecretaryDateTime,
                    InnerSecretaryNumber = queryInnerSecretaryNumber,
                    CentralSecretaryDateTime = queryCentralSecretaryDateTime,
                    CentralSecretaryNumber = queryCentralSecretaryNumber,
                    IsCd = hasCd,
                    Empty = empty,
                    Various = various,
                    Complete = complete
                });
                db.SaveChanges();
                result = entity.Entity.Id;
            }

            DataWorker.CreateQueryBlobData(result, 0, null);
            return result;
        }

        public static int EditQuery(Query oldQuery, string newName,
            string newInfo,
            string newGuid,
            Privacy newPrivacy,
            Division newDivision,
            Person newSignPerson,
            Type newType,
            DateTime newQueryOuterSecretaryDateTime,
            string newQueryOuterSecretaryNumber,
            DateTime newQueryInnerSecretaryDateTime,
            string newQueryInnerSecretaryNumber,
            DateTime newQueryCentralSecretaryDateTime,
            string newQueryCentralSecretaryNumber,
            bool newHasCd,
            bool newVarious,
            bool newEmpty
        )
        {
            var result = -1;
            using var db = new ApplicationContext();
            var entity = db.Queries.FirstOrDefault(d => d.Id == oldQuery.Id);
            entity.Name = newName;
            entity.Info = newInfo;
            entity.Guid = newGuid;
            entity.PrivacyId = newPrivacy.Id;
            entity.DivisionId = newDivision.Id;
            entity.SignPersonId = newSignPerson.Id;
            entity.TypeId = newType.Id;
            entity.OuterSecretaryDateTime = newQueryOuterSecretaryDateTime;
            entity.OuterSecretaryNumber = newQueryOuterSecretaryNumber;
            entity.InnerSecretaryDateTime = newQueryInnerSecretaryDateTime;
            entity.InnerSecretaryNumber = newQueryInnerSecretaryNumber;
            entity.CentralSecretaryDateTime = newQueryCentralSecretaryDateTime;
            entity.CentralSecretaryNumber = newQueryCentralSecretaryNumber;
            entity.IsCd = newHasCd;
            entity.Various = newVarious;
            entity.Empty = newEmpty;
            db.SaveChanges();
            result = entity.Id;
            return result;
        }

        public static int EditQueryImport(
            string queryGuid,
            string newInfo,
            int hasCd,
            int isEmpty,
            string jsonIds,
            int outputDivisionId,
            string outputNumber,
            long outputNumberDate,
            string blobData64
        )
        {
            var result = -1;
            using var db = new ApplicationContext();
            var checkIsExist = db.Queries.Any(el => el.Guid == queryGuid);
            if (!checkIsExist)
            {
                return result;
            }
            var entity = db.Queries.FirstOrDefault(d => d.Guid == queryGuid);
            entity.Info = newInfo;
            entity.HasCd = hasCd;
            entity.IsEmpty = isEmpty;
            entity.OutputSecretaryDate = outputNumberDate;
            entity.OutputSecretaryNumber = outputNumber;
            entity.OutputDivisionId = outputDivisionId;
            entity.IsComplete = 1;
            byte[] data64;
            data64 = Convert.FromBase64String(blobData64 ?? "UEsDBAoAAAAAAIpqLFT9HIsaCwAAAAsAAAANAAAAZXJyb3IudHh0LnR4dNCf0YPRgdGC0L4hUEsBAj8ACgAAAAAAimosVP0cixoLAAAACwAAAA0AJAAAAAAAAAAgAAAAAAAAAGVycm9yLnR4dC50eHQKACAAAAAAAAEAGAAFLjn/nQfYAScWCAOeB9gB94d18J0H2AFQSwUGAAAAAAEAAQBfAAAANgAAAAAA");
            EditQueryBlobData(entity.Id, (uint)data64.Length, data64);
            db.SaveChanges();
            result = entity.Id;
            return result;
        }

        public static int EditQueryLinkedData(Query query, SortedSet<Person> persons, SortedSet<Article> articles,
            SortedSet<Theme> themes, SortedSet<Action> actions)
        {
            QueryActionUnLink(query.Id);
            QueryPersonUnLink(query.Id);
            QueryThemeUnLink(query.Id);
            QueryArticleUnLink(query.Id);
            foreach (var p in persons)
            {
                QueryPersonLink(query.Id, p.Id);
            }

            foreach (var a in articles)
            {
                QueryArticleLink(query.Id, a.Id);
            }

            foreach (var t in themes)
            {
                QueryThemeLink(query.Id, t.Id);
            }

            foreach (var a in actions)
            {
                QueryActionLink(query.Id, a.Id);
            }
            return query.Id;
        }
        public static List<Query> GetAllQueriesByDate(DateTime begin, DateTime end)
        {
            if (begin > end) return new List<Query>();
            using var db = new ApplicationContext();
            var result = from q in db.Queries
                where q.InnerSecretaryDate >= begin.Ticks && q.InnerSecretaryDate <= end.Ticks
                select q;
            return result.ToList();
        }

        public static Tuple<int, SortedDictionary<string, SortedSet<string>>> GetReportByDivisions(DateTime begin, DateTime end)
        {
            var result = new SortedDictionary<string, SortedSet<string>>();
            if (begin > end) return new Tuple<int, SortedDictionary<string, SortedSet<string>>>(0, new SortedDictionary<string, SortedSet<string>>());
            using var db = new ApplicationContext();
            var queries = (from q in db.Queries
                where q.OutputSecretaryDate >= begin.Ticks && q.OutputSecretaryDate <= end.Ticks && q.IsComplete == 1
                select q).ToList();
            foreach (var query in queries)
            {
                var actions  = query.LinkedActions.Select(a => a.ToString()).ToList();
                if (!result.ContainsKey(query.Division.Name))
                {
                    result.Add(query.Division.Name, new SortedSet<string>(actions));
                }
                else
                {
                    result[query.Division.Name].UnionWith(actions);
                }

            }

            return new Tuple<int, SortedDictionary<string, SortedSet<string>>>(queries.Count, result);
        }

        public static int GetAllQueriesByDateCompleted(DateTime begin, DateTime end)
        {
            if (begin > end) return 0;
            using var db = new ApplicationContext();
            var result = (from q in db.Queries
                where q.InnerSecretaryDate >= begin.Ticks && q.InnerSecretaryDate <= end.Ticks && q.IsComplete == 1 
                select q).ToList();
            return result.Count();
        }

        public static int GetAllQueriesNonComplete()
        {
            using var db = new ApplicationContext();
            var result = (from q in db.Queries
                where q.IsComplete == 0
                select q).ToList();
            return result.Count();
        }

        public static SortedDictionary<Person, Tuple<int, int>> GetAllQueriesStatisticsByPerson(List<Query> queries)
        {
            using var db = new ApplicationContext();
            var statistics = new SortedDictionary<Person, Tuple<int, int>>();
            foreach (var q in queries)
            {
                if (!q.Complete) continue;
                var person = q.LinkedPersons[0];
                var complete = (q.IsEmpty == 1);
                if (statistics.ContainsKey(person))
                {
                    var it1 = statistics[person].Item1;
                    var it2 = statistics[person].Item2;
                    statistics[person] = new Tuple<int, int>(++it1, (complete) ? ++it2 : it2);
                }
                else
                {

                    statistics.Add(person, new Tuple<int, int>(1, (complete) ? 1 : 0));
                }


            }
            return statistics;
        }

        public static byte[] GetExecutorRecordData(Query query)
        {
            var empty = Convert.FromBase64String(
                "UEsDBAoAAAAAAIpqLFT9HIsaCwAAAAsAAAANAAAAZXJyb3IudHh0LnR4dNCf0YPRgdGC0L4hUEsBAj8ACgAAAAAAimosVP0cixoLAAAACwAAAA0AJAAAAAAAAAAgAAAAAAAAAGVycm9yLnR4dC50eHQKACAAAAAAAAEAGAAFLjn/nQfYAScWCAOeB9gB94d18J0H2AFQSwUGAAAAAAEAAQBfAAAANgAAAAAA");
            using var db = new ApplicationContext();
            try
            {
                var q = db.QueriesBlobDatas.FirstOrDefault(x => x.Id == query.Id);
                return q.Data.Length == 0 ? (empty) : q.Data;
          
            }
            catch (Exception e)
            {
                return empty;
            }
        }
        #endregion
    }
}
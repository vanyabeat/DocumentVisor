using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Automation;
using DocumentVisor.Data;
using Microsoft.EntityFrameworkCore.Internal;

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

        public static string CreatePerson(string name, string info, string phone, PersonType personType)
        {


            var result = Dictionary["Insert"].ToString();
            using ApplicationContext db = new ApplicationContext();
            var checkIsExist = db.Persons.Any(el => el.Name == name && el.Type == personType && el.Phone == phone);
            if (!checkIsExist)
            {
                db.Persons.Add(new Person { Name = name, Phone = phone, Info = info, TypeId = personType.Id });
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string CreatePersonType(string name, string info)
        {

            var result = Dictionary["Insert"].ToString();
            using ApplicationContext db = new ApplicationContext();
            var checkIsExist = db.PersonTypes.Any(el => el.Name == name);
            if (!checkIsExist)
            {
                db.PersonTypes.Add(new PersonType { Name = name, Info = info });
                db.SaveChanges();
                result = Dictionary["Complete"].ToString();
            }

            return result;
        }

        public static string DeletePerson(Person person)
        {
            var result = Dictionary["PersonNotExist"].ToString();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Persons.Remove(person);
                db.SaveChanges();
                result = $"{Dictionary["PersonDeleted"]} {person}";
            }
            return result;
        }

        public static string EditPersonName(Person oldPerson, string newName)
        {
            var result = Dictionary["PersonNotExist"].ToString();
            using (ApplicationContext db = new ApplicationContext())
            {
                var person = db.Persons.FirstOrDefault(p => p.Id == oldPerson.Id);
                person.Name = newName;
                db.SaveChanges();
                result = $"{Dictionary["PersonEdited"]} {person}";
            }
            return result;
        }

        public static string EditPersonPhone(Person oldPerson, string newPhone)
        {
            var result = Dictionary["PersonNotExist"].ToString();
            using (ApplicationContext db = new ApplicationContext())
            {
                var person = db.Persons.FirstOrDefault(p => p.Id == oldPerson.Id);
                person.Phone = newPhone;
                db.SaveChanges();
                result = $"{Dictionary["PersonEdited"]} {person}";
            }
            return result;
        }

        public static string EditPersonInfo(Person oldPerson, string newInfo)
        {
            var result = Dictionary["PersonNotExist"].ToString();
            using (ApplicationContext db = new ApplicationContext())
            {
                var person = db.Persons.FirstOrDefault(p => p.Id == oldPerson.Id);
                person.Info = newInfo;
                db.SaveChanges();
                result = $"{Dictionary["PersonEdited"]} {person}";
            }
            return result;
        }

        public static string EditPersonType(Person oldPerson, PersonType newType)
        {
            var result = Dictionary["PersonNotExist"].ToString();
            using (ApplicationContext db = new ApplicationContext())
            {
                var person = db.Persons.FirstOrDefault(p => p.Id == oldPerson.Id);
                person.Type = newType;
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

        public static string DeletePersonType(PersonType personType)
        {
            var result = Dictionary["PersonTypeNotExist"].ToString();
            using (ApplicationContext db = new ApplicationContext())
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
            string result = Dictionary["PersonTypeNotExist"].ToString();
            using (ApplicationContext db = new ApplicationContext())
            {
                PersonType personType = db.PersonTypes.FirstOrDefault(d => d.Id == oldPersonType.Id);
                personType.Name = newName;
                personType.Info = newInfo;
                db.SaveChanges();
                result = $"{Dictionary["PersonTypeEdited"]} {personType}";
            }
            return result;
        }

        public static PersonType GetPersonTypeById(int typeId)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                PersonType pos = db.PersonTypes.FirstOrDefault(p => p.Id == typeId);
                return pos;
            }
        }
        #endregion


    }


}

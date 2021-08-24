using System;
using System.Collections.Generic;
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
            var checkIsExist = db.Persons.Any(el => el.Name == name && el.Type == personType);
            if (!checkIsExist)
            {
                db.Persons.Add(new Person { Name = name, Phone = phone, Info = info, Type = personType });
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

    }


}

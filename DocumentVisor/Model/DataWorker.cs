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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Person> GetAllPersons()
        {
            using var db = new ApplicationContext();
            var result = db.Persons.ToList();
            return result;
        }

        public static string CreatePerson(string name, string info, string phone, PersonType personType)
        {


            var result = (string) Dictionary["Insert"];
            using (var db = new ApplicationContext())
            {
                var checkIsExist = db.Persons.Any(el => el.Name == name && el.Type == personType);
                if (!checkIsExist)
                {
                    db.Persons.Add(new Person {Name = name, Phone = phone, Info = info, Type = personType});
                    db.SaveChanges();
                    result = (string)Dictionary["Complete"];
                }
            }
            return result;
        }

        private static readonly ResourceDictionary Dictionary = new ResourceDictionary(){Source = new Uri($"../Resources/StringResource.xaml") };
    }


}

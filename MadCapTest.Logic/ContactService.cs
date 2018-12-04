using MadCapTest.Logic.Abstractions;
using MadCapTest.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MadCapTest.Logic
{
    public class ContactService : IContactService
    {
        private readonly INotificationService _notificationService;
        private readonly List<Contact> _data;

        public ContactService(INotificationService notificationService)
        {
            _notificationService = notificationService;
            _data = new List<Contact>
            {
                new Contact { FirstName = "Joe", LastName = "Smith", Email = "jsmith@acme.com", Phone = "8585551212"},
                new Contact { FirstName = "Ed", LastName = "Thomson", Email = "ethomson@acme.com", Phone = "8585558855"},
                new Contact { FirstName = "Sue", LastName = "Jones", Email = "sjones@acme.com", Phone = "6195551234"},
                new Contact { FirstName = "Bob", LastName = "Walters", Email = "bwalters@acme.com", Phone = "2065555555" }
            };
        }

        public List<Contact> GetContactsWithFirstNameContaining(string criteria)
        {
            return _data.Where(x => x.FirstName.Contains(criteria, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public List<Contact> GetContactsWithAreaCode(string areaCode)
        {
            var contacts = _data.Where(x => x.Phone.Substring(0, 3).Equals(areaCode)).ToList();
            contacts.ForEach(x => x.Phone = FormatPhone(x.Phone));

            return contacts;
        }

        public string ListContactPhones(string areaCode)
        {
            var list = _data.Where(x => x.Phone.Substring(0, 3).Equals(areaCode))
                .Select(x => FormatPhone(x.Phone)).ToList();

            return string.Join(",", list);
        }

        public List<Tuple<string, string>> GetContactEmails()
        {
            return _data.Select(x => SplitEmail(x.Email)).ToList();
        }

        // TODO: NOT SURE IF I UNDERSTAND THE REQUIREMENTS CORRECTLY ALL PROPERTIES ARE PUBLIC AND READ-WRITE
        public List<string> GetContactPropertyNames(bool readWriteOnly)
        {
            var properties = typeof(Contact).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            var names = new List<string>();

            foreach (var p in properties)
            {
                if (!p.CanWrite || !p.CanRead && readWriteOnly) { continue; }

                var get = p.GetGetMethod(false);
                var set = p.GetSetMethod(false);

                if (get == null)
                {
                    continue;
                }
                if (set == null)
                {
                    continue;
                }

                names.Add(p.Name);
            }

            return names;
        }

        public bool ContactExists(string criteria)
        {
            foreach (var contact in _data)
            {
                if (contact.FirstName.Contains(criteria) || contact.LastName.Contains(criteria) ||
                    contact.Email.Contains(criteria) || contact.Phone.Contains(criteria))
                {
                    return true;
                }
            }

            return false;
        }

        public void SendMessage(string sender, string subject, string body)
        {
            _notificationService.SendMessage(sender, subject, body, _data.Select(x => x.Email).ToList());
        }

        public Dictionary<string, Contact> GetContactsByEmail()
        {
            return _data.ToDictionary(x => x.Email);
        }

        public int GetContactCharacterCount()
        {
            var count = 0;
            foreach (var contact in _data)
            {
                count += contact.Email.Length + contact.FirstName.Length + contact.LastName.Length +
                         contact.Phone.Length;
            }

            return count;
        }


        private string FormatPhone(string phone)
        {
            return Regex.Replace(phone, @"(\d{3})(\d{3})(\d{4})", "$1-$2-$3");
            //return $"{phone:###-###-####}";
            //return phone;
        }

        private Tuple<string, string> SplitEmail(string email)
        {
            var ar = email.Split('@');
            if (ar.Length != 2)
            {
                throw new Exception("Invalid Email");
            }
            return new Tuple<string, string>(ar[0], ar[1]);
        }

    }
}

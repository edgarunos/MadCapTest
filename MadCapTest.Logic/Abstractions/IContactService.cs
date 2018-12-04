using MadCapTest.Logic.Models;
using System;
using System.Collections.Generic;

namespace MadCapTest.Logic.Abstractions
{
    public interface IContactService
    {
        /// <summary>
        /// Returns a list of contacts with unformatted phone numbers where first name contains the given string.
        /// </summary>
        /// <param name="criteria">The string to match.</param>
        /// <returns>A list of contacts.</returns>
        List<Contact> GetContactsWithFirstNameContaining(string criteria);

        /// <summary>
        /// Returns a list of contacts with formatted phone numbers that have the given area code.
        /// </summary>
        /// <param name="areaCode">The area code on which to match.</param>
        /// <returns>A list of contacts with phone numbers formatted as XXX-XXX-XXXX.</returns>
        List<Contact> GetContactsWithAreaCode(string areaCode);

        /// <summary>
        /// Returns a comma delimited list of formatted phone numbers that have the given area code.
        /// </summary>
        /// <param name="areaCode">The area code on which to match.</param>
        /// <returns>A list of phone numbers formatted XXX-XXX-XXXX.</returns>
        string ListContactPhones(string areaCode);

        /// <summary>
        /// Returns a tuple-list of email address and domain.
        /// </summary>
        /// <returns>A tuple-list of email address and domain.</returns>
        List<Tuple<string, string>> GetContactEmails();

        /// <summary>
        /// Returns a list of contact property names.
        /// </summary>
        /// <param name="readWriteOnly">Specifies to only list read-write properties; otherwise list all  properties.</param>
        /// <returns>A list of property names.</returns>
        /// // TODO: NOT SURE IF I UNDERSTAND THE REQUIREMENTS CORRECTLY ALL PROPERTIES ARE PUBLIC AND READ-WRITE
        List<string> GetContactPropertyNames(bool readWriteOnly);

        /// <summary>
        /// Determines if any contact contains the given string value in any of its properties.
        /// </summary>
        /// <param name="criteria">The string for which to search.</param>
        /// <returns>True if the criteria is found in any contact's properties, false otherwise.</returns>
        bool ContactExists(string criteria);

        /// <summary>
        /// Sends an email message via SMTP to all contacts.
        /// </summary>
        /// <param name="sender">The sender's email address.</param>
        /// <param name="subject">The email message's subject.</param>
        /// <param name="body">The email message's body.</param>
        void SendMessage(string sender, string subject, string body);

        /// <summary>
        /// Returns a dictionary of contacts where the key is the contact's email address.
        /// </summary>
        /// <returns>A dictionary of contacts.</returns>
        Dictionary<string, Contact> GetContactsByEmail();

        /// <summary>
        /// Returns the total number of characters in all properties of contacts.
        /// </summary>
        /// <returns></returns>
        int GetContactCharacterCount();
    }
}

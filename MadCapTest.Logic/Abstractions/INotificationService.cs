using System.Collections.Generic;

namespace MadCapTest.Logic.Abstractions
{
    public interface INotificationService
    {
        // TODO: THIS WILL NOT WORK, SMPT PARAMETERS ARE PLACEHOLDERS
        void SendMessage(string sender, string subject, string body, List<string> recipients);
    }
}

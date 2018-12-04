using MadCapTest.Logic;
using Xunit;

namespace MadCapTest.Tests
{
    public class ServiceTests
    {

        [Fact]
        public void ShouldGetContactContaining()
        {
            // ARRANGE
            var service = new ContactService(new NotificationService());

            var result = service.GetContactsWithFirstNameContaining("O");

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ShouldGetContactWithAreaCode()
        {
            // ARRANGE
            var service = new ContactService(new NotificationService());

            var result = service.GetContactsWithAreaCode("858");

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void ShouldListPhones()
        {
            // ARRANGE
            var service = new ContactService(new NotificationService());

            var result = service.ListContactPhones("858");

            Assert.Contains("858-555-1212", result);
        }

        [Fact]
        public void ShouldGetContactEmails()
        {
            // ARRANGE
            var service = new ContactService(new NotificationService());

            var result = service.GetContactEmails();

            Assert.Equal(4, result.Count);
            foreach (var tuple in result)
            {
                Assert.Equal("acme.com", tuple.Item2);
            }
        }

        [Fact]
        public void ShouldGetProperties()
        {
            // ARRANGE
            var service = new ContactService(new NotificationService());

            var result = service.GetContactPropertyNames(false);

            Assert.Equal(4, result.Count);
        }

        [Fact]
        public void ShouldGetIfContactExists()
        {
            // ARRANGE
            var service = new ContactService(new NotificationService());

            var result = service.ContactExists("bwal");

            Assert.True(result);
        }


        [Fact]
        public void ShouldGetContactDictionary()
        {
            // ARRANGE
            var service = new ContactService(new NotificationService());

            var result = service.GetContactsByEmail();

            Assert.Equal(4, result.Count);
        }

    }
}

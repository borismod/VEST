using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using NUnit.Framework;

namespace VEST_FileSystemSample.Tests
{
    [TestFixture]
    public class VestAttendeesRepositoryTests
    {
        [Test]
        public void Single_Attendee_Selected()
        {
            var fileSystem = new MockFileSystem();
            fileSystem.AddFile(@"C:\files\attendees.csv", new MockFileData("Boris"));

            var attendeesRepository = new AttendeesRepository(fileSystem, @"C:\files\attendees.csv");

            var selected = attendeesRepository.GetRandom(1);

            Assert.AreEqual("Boris", selected.Single());
        }
    }
}
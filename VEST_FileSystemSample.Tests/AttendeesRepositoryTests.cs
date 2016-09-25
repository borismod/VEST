using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;

namespace VEST_FileSystemSample.Tests
{
    [TestFixture]
    public class AttendeesRepositoryTests
    {
        [Test]
        public void GetRandom_OneAttendee_Selected()
        {
            var fileSystem = A.Fake<IFileSystem>();
            A.CallTo(() => fileSystem.File.ReadAllLines(@"C:\attendees.txt"))
                .Returns(new[] {"Boris Modylevsky"});

            var attendeesRepository = new AttendeesRepository(fileSystem, @"C:\attendees.txt");

            var randomAttendees = attendeesRepository.GetRandom(1);

            Assert.AreEqual("Boris Modylevsky", randomAttendees.Single());
        }

        [Test]
        public void AddAttendee_Once_SavedToFile()
        {
            var fileSystem = A.Fake<IFileSystem>();

            var attendeesRepository = new AttendeesRepository(fileSystem, @"C:\attendees.txt");

            attendeesRepository.AddAttendee("Boris Modylevsky");

            A.CallTo(() => fileSystem.File.AppendAllLines(@"C:\attendees.txt",
                A<IEnumerable<string>>.That.IsSameSequenceAs(new[] {"Boris Modylevsky"})))
                .MustHaveHappened();
        }
    }
}
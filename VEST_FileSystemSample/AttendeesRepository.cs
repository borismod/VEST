using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace VEST_FileSystemSample
{
    public interface IAttendeesRepository
    {
        void AddAttendee(string name);
        List<string> GetRandom(int ammount);
    }

    public class AttendeesRepository : IAttendeesRepository
    {
        private readonly string _path;
        private readonly IFileSystem _fs;
        private readonly Random _randomizer = new Random();

        public AttendeesRepository(IFileSystem fs, string path)
        {
            _fs = fs;
            _path = path;
        }

        public void AddAttendee(string name)
        {
            _fs.File.AppendAllLines(_path, new [] { name });
        }

        public List<string> GetRandom(int ammount)
        {
            var names = _fs.File.ReadAllLines(_path);
            return GetRandomFromList(ammount, names);
        }

        private List<string> GetRandomFromList(int ammount, string[] attendees)
        {
            var selected = new List<string>();
            for (var i = 0; i < ammount; i++)
            {
                var randomNumber = _randomizer.Next(attendees.Length);
                var randomAttendee = attendees[randomNumber];
                selected.Add(randomAttendee);
            }
            return selected;
        }
    }
}
using System;
using System.IO.Abstractions;

namespace VEST_FileSystemSample.CommandLine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var attendeesRepository = new AttendeesRepository(new FileSystem(), 
                @"C:\attendees.csv");

            Console.WriteLine("Type attendees names, each in a new line:");
            var line = Console.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                attendeesRepository.AddAttendee(line);
                line = Console.ReadLine();
            }

            Console.WriteLine("And the winners are:");
            foreach (var winner in attendeesRepository.GetRandom(6))
            {
                Console.WriteLine(winner);
            }
            Console.ReadKey();
        }
    }
}
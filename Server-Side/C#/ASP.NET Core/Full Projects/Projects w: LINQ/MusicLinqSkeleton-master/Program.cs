using System;
using System.Collections.Generic;
using System.Linq;
using JsonData;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Collections to work with
            List<Artist> Artists = JsonToFile<Artist>.ReadJson();
            List<Group> Groups = JsonToFile<Group>.ReadJson();

            //========================================================
            //Solve all of the prompts below using various LINQ queries
            //========================================================

            //There is only one artist in this collection from Mount Vernon, what is their name and age?
            IEnumerable<Artist> mountVernonArtists  = Artists.Where(info => info.Hometown == "Mount Vernon");
            // foreach (Artist artist in mountVernonArtists)
            // {
            //     Console.WriteLine($"Artist Name: {artist.RealName} Artist Age: {artist.Age}");
            // }


            //Who is the youngest artist in our collection of artists?
            Artist youngestArtist = Artists.OrderBy(info => info.Age).First();
            // Console.WriteLine(youngestArtist.RealName);

            //Display all artists with 'William' somewhere in their real name
            IEnumerable<Artist> namedWilliam = Artists.Where(artist => artist.RealName.Contains("William"));
            // foreach (Artist artist in namedWilliam)
            // {
            //     Console.WriteLine(artist.RealName);
            // }

            //Display the 3 oldest artist from Atlanta
            IEnumerable<Artist> threeOldestAtlanta = Artists.Where(artist => artist.Hometown == "Atlanta").OrderByDescending(artist => artist.Age).Take(3);
            // foreach (Artist artist in threeOldestAtlanta)
            // {
            //     Console.WriteLine(artist.RealName);
            // }

            //Display all groups with names less than 8 characters in length.
            IEnumerable<Group> lessThanEight = Groups.Where(group => group.GroupName.Length < 8);
            foreach (Group group in lessThanEight)
            {
                Console.WriteLine(group.GroupName);
            }

	    // Console.WriteLine(Groups.Count);
        }
    }
}

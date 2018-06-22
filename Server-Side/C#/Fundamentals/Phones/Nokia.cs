using System;
using System.Collections.Generic;

namespace Phones
{
    public class Nokia : Phone, IRingable 
    {

        public Nokia(string versionNumber, int batteryPercentage, string carrier, string ringTone) 
        : base(versionNumber, batteryPercentage, carrier, ringTone) {}
        public string Ring() 
        {
            Console.WriteLine("ring ring ring!");
            return "ring";
        }
        public string Unlock() 
        {
            Console.WriteLine("Phone is now unlocked");
            return "unlocked";
        }
        public override void DisplayInfo() 
        {
            Console.WriteLine("Your phone version is {0} and the service is provided by {1}. Your battery percentage is {2} and your ringtone is {3}", _versionNumber, _carrier, _batteryPercentage, _ringTone);      
        }
    }
}
using System;
using System.Collections.Generic;

namespace Phones
{
    public class Nokia : Phone, IRingable
    {
        public Nokia(string versionNumber, int batteryPercentage, string carrier, string ringTone) 
                   : base(versionNumber, batteryPercentage, carrier, ringTone)
            {}
        
        public string Ring()
        {
            string message = "ring ring ring";
            return message;
        }

        public string Unlock()
        {
            string message = "phone unlocked!";
            return message;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("here's a bunch of info! Version Number: {0}, batter percent: {1}, carrier: {2}, ringtone: {3}",_versionNumber, _batteryPercentage, _carrier, _ringTone);
        }



        
    }
}
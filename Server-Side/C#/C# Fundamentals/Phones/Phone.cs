using System;
using System.Collections.Generic;

namespace Phones
{
    public abstract class Phone 
    {
        public string _versionNumber;
        public int _batteryPercentage;
        public string _carrier;
        public string _ringTone;
        public Phone(string versionNumber, int batteryPercentage, string carrier, string ringTone){
            _versionNumber = versionNumber;
            _batteryPercentage = batteryPercentage;
            _carrier = carrier;
            _ringTone = ringTone;
        }
        public abstract void DisplayInfo();
    }
}
        
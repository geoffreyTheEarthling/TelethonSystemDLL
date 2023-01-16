using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelethonSystemDLL
{
    class Prize
    {
        string prizeID, description, sponsorID;
        double value, donationLimit;
        int originalAvailable, currentAvailable;

        public Prize(string priID, string desc, string sponID, 
            double val, double donLim, int origAv)
        {
            this.prizeID = priID;
            this.description = desc;
            this.sponsorID = sponID;
            this.value = val;
            this.donationLimit = donLim;
            this.originalAvailable = origAv;
            this.currentAvailable = origAv;
        }   

        public string PrizeID
        {
            get { return prizeID; }
            set { prizeID = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string SponsorID
        {
            get { return sponsorID; }
            set { sponsorID = value; }
        }

        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        public double DonationLimit
        {
            get { return donationLimit; }
            set { donationLimit = value; }
        }

        public int OriginalAvailable
        {
            get { return originalAvailable; }
            set { originalAvailable = value; }
        }

        public int CurrentAvailable
        {
            get { return currentAvailable; }
            set { currentAvailable = value; }
        }

        public string toString()
        {
            return "Prize ID: " + PrizeID + "\nDescription: " + Description + "\nSponsor ID: " + SponsorID + "\nValue per Prize: " + Value + "\nMinimum Donation: " + DonationLimit + "\nOriginal Available: " + OriginalAvailable + "\nCurrent Available: " + CurrentAvailable + "\n";
        }

        public string getPrizeID()
        {
            return PrizeID;
        }

        public void decrease(int num)
        {
            CurrentAvailable -=  num;
        }

    }
}

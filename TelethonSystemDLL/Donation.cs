using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelethonSystemDLL
{
    class Donation
    {
        string donationID, donationDate, donorID, prizeID;
        double donationAmount;

        public Donation (string donatID, string donDate, string donID, string prID, double donAmount)
        {
            this.donationID = donatID;
            this.donationDate = donDate;
            this.donorID = donID;
            this.prizeID = prID;
            this.donationAmount = donAmount;
        }

        public string DonationID
        {
            get { return donationID; }
            set { donationID = value; }
        }

        public string DonationDate
        {
            get { return donationDate; }
            set { donationDate = value; }
        }

        public string DonorID
        {
            get { return donorID; }
            set { donorID = value; }
        }

        public string PrizeID
        {
            get { return prizeID; }
            set { prizeID = value; }
        }

        public double DonationAmount
        {
            get { return donationAmount; }
            set { donationAmount = value; }
        }

        public string toString()
        {
            return "Donation ID: " + DonationID + "\nDonation Date: " + DonationDate + "\nDonorID: " + DonorID + "\nPrize ID: " + PrizeID + "\nDonation Amount: " + DonationAmount + "\n";
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TelethonSystemDLL
{
    public class ETSManager
    {

        static string txtPath = @".\TextFiles\donors_Info.txt";
        public ETSManager() { }

        Sponsors sponsors = new Sponsors();
        Donors donors = new Donors();
        Prizes prizes = new Prizes();
        Donations donations = new Donations();

        public bool donorIDVerifier(string donID)
        {
            bool flag = false;
            foreach (Donor don in donors)
            {
                if(don.DonorID == donID)
                {
                    flag = true; 
                }
            }
            return flag;
        }

        public string addDonor(string firstname, string lastname, string donorID, string addr,
                     string phone, string cardNb, string cardExp, char type)
        {
            string message = null;
            if (donorID.Length != 4)
            {
                message = "Error! Donor ID must have 4 characters";
            }
            else
            {
                if (donorIDVerifier(donorID) == true)
                {
                    message = "Error! This donor ID already exists";
                }
                else
                {
                    if (firstname.Length > 15)
                    {
                        message = "Error! Firstname should be 15 characters maximum";
                    }
                    else
                    {
                        if (lastname.Length > 15)
                        {
                            message = "Error! Lastname should be 15 characters maximum";
                        }
                        else
                        {
                            if(addr.Length > 40)
                            {
                                message = "Error! Address should be 40 characters maximum";
                            }
                            else
                            {
                                Regex phoneNum = new Regex(@"^[0-9]{3}\s[0-9]{3}-[0-9]{4}$");
                                if (phoneNum.IsMatch(phone.Trim()) != true)
                                {
                                    message = "Error! Phone number format must be: 999 999-9999";
                                }
                                else
                                {
                                    if (type != 'V' && type != 'v' && type != 'M' && type != 'm' &&
                                        type != 'A' && type != 'a')
                                    {
                                        message = "Error! Type must be either 'V' for Visa, " +
                                            "'M' for Mastercard, or 'A' for American Express";
                                    }
                                    else
                                    {
                                        if (type == 'V' && cardNb.Substring(0,1) != "4"){
                                            message = "Error! Visa credit card number must start with 4";
                                        }
                                        else
                                        {
                                            if (type == 'M' && cardNb.Substring(0,1) != "5")
                                            {
                                                message = "Error! Mastercard credit card number must start with 5";
                                            }
                                            else
                                            {
                                                if (type == 'A' && cardNb.Substring(0,1) != "3")
                                                {
                                                    message = "Error! American Express credit card number must start with 3";
                                                }
                                                else
                                                {
                                                    if ((type == 'V' && cardNb.Length != 16) ||
                                           (type == 'M' && cardNb.Length != 16) ||
                                           (type == 'A' && cardNb.Length != 15))
                                                    {
                                                        message = "Error! Visa cards and Mastercards must be" + " exactly 16 digits, and American Express cards 15 digits";
                                                    }
                                                    else
                                                    {
                                                        Regex expDate = new Regex(@"^(0[1-9]|1[0-2])\/([0-9]{2})$");
                                                        if (expDate.IsMatch(cardExp.Trim()) != true)
                                                        {
                                                            message = "Error! Credit card expiry date format must be: MM/YY";
                                                        }
                                                        else
                                                        {
                                                            Donor donor = new Donor(firstname, lastname, donorID, addr, phone, cardNb, cardExp, type);
                                                            donors.add(donor);
                                                            message = "Donor " + firstname + " " + lastname + " has been added to the list";
                                                        }
                                                    }
                                                }
                                            }
                                        }                                           
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return message;
        }

        public bool sponsorIDVerifier(string sponID)
        {
            bool flag = false;
            foreach (Sponsor spon in sponsors)
            {
                if (spon.getID() == sponID)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public string addSponsor(string firstname, string lastname, string sponsorID)
        {
            string message = null;
            if (sponsorID.Length != 4)
            {
                message = "Error! Sponsor ID must have 4 characters";
            }
            else
            {
                if (sponsorIDVerifier(sponsorID) == true)
                {
                    message = "Error! This sponsor ID already exists";
                }
                else
                {
                    if (firstname.Length + lastname.Length > 30)
                    {
                        message = "Error! Firstname and lastname should be a total of " +
                            "30 characters maximum";
                    }
                    else
                    {
                        Sponsor sponsor = new Sponsor(firstname, lastname, sponsorID);
                        sponsors.add(sponsor);
                        message = "Sponsor " + firstname + " " + lastname +
                                            " has been added to the list";
                    }
                }
            }
            return message;
        }

        public bool prizeIDVerifier(string prID)
        {
            bool flag = false;
            foreach (Prize pr in prizes)
            {
                if (pr.getPrizeID() == prID)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public string addPrize(string prizeID, string desc, string sponsorID,
            double value, double donationLimit, int quantity)
        {
            string message = null;
            if (prizeID.Length != 4)
            {
                message = "Error! Prize ID must have 4 characters";
            }
            else
            {
                if (prizeIDVerifier(prizeID) == true)
                {
                    message = "Error! This Prize ID already exists";
                }
                else
                {
                    if (desc.Length > 15)
                    {
                        message = "Error! Description should be 15 characters maximum";
                    }
                    else
                    {
                        if (sponsorID.Length != 4)
                        {
                            message = "Error! Sponsor ID must have 4 characters";
                        }
                        else
                        {
                            if (sponsorIDVerifier(sponsorID) == false)
                            {
                                message = "Error! Sponsor ID not found";
                            }
                            else
                            {
                                if (value > 299.99)
                                {
                                    message = "Error! The value must be less than $300.00";
                                }
                                else
                                {
                                    if (donationLimit < 5 || donationLimit > 999999.99)
                                    {
                                        message = "Error! Donation limit must be between" +
                                            " $5.00 and $999,999.99";
                                    }
                                    else
                                    {
                                        if (quantity > 999)
                                        {
                                            message = "Error! Quantity must be less than " +
                                                "1000";
                                        }
                                        else
                                        {
                                            Prize prize = new Prize(prizeID, desc, sponsorID,                                       value, donationLimit, quantity);
                                            prizes.add(prize);
                                            updateTotalPrizeValue(sponsorID, quantity, value);
                                            message = "Prize " + prizeID + " has been added to the list";
                                        }                                     
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return message;
        }//end addPrize

        public bool donationIDVerifier(string donID)
        {
            bool flag = false;
            foreach(Donation don in donations)
            {
                if(don.DonationID == donID)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public string addDonation(string donationID, string donationDate, string donorID, 
            string prizeID, double donationAmount)
        {
            string message = null;
            if (donationID.Length != 4)
            {
                message = "Error! Donation ID must have 4 characters";
            }
            else
            {
                if (donationIDVerifier(donationID) == true)
                {
                    message = "Error! This donation ID already exists";
                }
                else
                {
                    Regex donDate = new Regex(@"^(0[1-9]|1[0-2])\/(0[1-9]|1[0-9]|2[0-9]|3[0-1])\/(202[2-9])$");
                    if (donDate.IsMatch(donationDate.Trim()) != true)
                    {
                        message = "Error! Date format must be: MM/DD/YYYY";
                    }
                    else
                    {
                        if (donorID.Length != 4)
                        {
                            message = "Error! Donor ID must have 4 characters";
                        }
                        else
                        {
                            if (donorIDVerifier(donorID) != true)
                            {
                               message = "Error! Donor ID not found";
                            }
                            else
                            {
                                if (prizeID.Length != 4)
                                {
                                    message = "Error! Prize ID must have 4 characters";
                                }
                                else
                                {
                                    if (prizeIDVerifier(prizeID) != true)
                                    {
                                        message = "Error! Prize ID not found";
                                    }
                                    else
                                    {
                                        if (donationAmount < 5 || donationAmount > 999999.99)
                                        {
                                            message = "Error! Donation amount must be between" +
                                                " $5.00 and $999,999.99";
                                        }
                                        else
                                        {
                                            Donation donation = new Donation(donationID,                                donationDate, donorID, prizeID, donationAmount);
                                            donations.add(donation);
                                            message = "Donation " + donationID + " has been added to the list";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return message;
        }// end addDonation

        public string listDonors()
        {
            string list = null;
            foreach (Donor don in donors)
            {
                list += don.toString();
                list += Environment.NewLine;
            }
            return list;
        }

        public string listSponsors()
        {
            string list = null;
            foreach (Sponsor spon in sponsors)
            {
                list += spon.toString();
                list += Environment.NewLine;
            }
            return list;
        }

        public string listPrizes()
        {
            string list = null;
            foreach (Prize priz in prizes)
            {
                list += priz.toString();
                list += Environment.NewLine;
            }
            return list;
        }

        public string listDonations()
        {
            string list = null;
            foreach (Donation don in donations)
            {
                list += don.toString();
                list += Environment.NewLine;
            }
            return list;
        }

        public string listQualifiedPrizes(double number)
        {
            string list = null;
            string message = null;
            foreach (Prize priz in prizes)
            {
                if (priz.Value <= number)
                {
                    list += priz.toString();
                    list += Environment.NewLine;
                }
                else
                {
                    message = "Sorry, there is no prize with a value of $" + number + " or less";
                }
            }
            return list + message;
        }

        public bool recordDonation(string prizeID, int numPrizeAwarded, string donorID,
            double donationAmount, string donationDate, string donationID)
        {
            bool flag = false;
            string message = null;
            foreach (Prize priz in prizes)
            {
                if (priz.getPrizeID() == prizeID)
                {
                    if (priz.CurrentAvailable >= numPrizeAwarded)
                    {
                        flag = true;
                        priz.decrease(numPrizeAwarded);
                    }
                    else if (priz.CurrentAvailable < numPrizeAwarded)
                    {
                        flag = false;                      
                        message = "There is " + priz.CurrentAvailable + " left.\n Donation " 
                            + donationID + " could not be added";                     
                    }
                }
                else
                {
                    message = "Error! Prize ID incorrect";                  
                }
            }
            return flag;
        }

        public void updateTotalPrizeValue(string prizeSponsID, int quantity, double value)
        {
            foreach(Sponsor spon in sponsors)
            {
                if(spon.SponsorID == prizeSponsID)
                {
                    spon.TotalPrizeValue += quantity * value;
                }
            }
        }

        public int getCurrentAvailable(string PrizID)
        {
            int currentAvailable = 0;   
            foreach(Prize p in prizes)
            {
                if (p.PrizeID == PrizID)
                {
                    currentAvailable = p.CurrentAvailable;
                }
            }
            return currentAvailable;
        }

        public string saveDonorInfo(string firstname, string lastname, string donorID, string addr, string phone, string cardNb, string cardExp, char type)
        {
            string textToWrite = null;
            FileStream fs = null; 
            try
            {
                fs = new FileStream(txtPath, FileMode.Append, FileAccess.Write);
                StreamWriter textOut = new StreamWriter(fs);
                textOut.WriteLine(firstname.Trim() + "," + lastname.Trim() + "," + donorID.Trim() + "," + addr.Trim() + "," + phone.Trim() + "," + cardNb.Trim() + "," + cardExp.Trim() + "," + type);
                textOut.Close();
            }
            catch(IOException ex)
            {
                textToWrite = ex.Message;
            }
            finally
            {
                if(fs != null) { fs.Close(); }  
            }
            return textToWrite; 
        }

    }//end of ETSManager
}

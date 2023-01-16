using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelethonSystemDLL
{
    class Donor: Person
    {
        string donorID, address, phone, cardNumber, cardExpiry;
        char cardType;

        public Donor(string firstname, string lastname, string donID, string addr, 
                     string ph, string cardNb, string cardEx, char cardTp): base(firstname, lastname)
        {
            this.donorID = donID;
            this.address = addr;
            this.phone = ph;
            this.cardNumber = cardNb;
            this.cardExpiry = cardEx;
            this.cardType = cardTp;
        }   

        public string DonorID
        {
            get { return this.donorID; }
            set { this.donorID = value; }   
        }

        public string Address
        {
            get { return this.address; }    
            set { this.address = value; }   
        }

        public string Phone
        {
            get { return this.phone; }
            set { this.phone = value; }
        }

        public string CardNumber
        {
            get { return this.cardNumber; }
            set { this.cardNumber = value; }
        }

        public string CardExpiry
        {
            get { return this.cardExpiry; }
            set { this.cardExpiry = value; }
        }

        public char CardType
        {
            get { return this.cardType; }
            set { this.cardType = value; }
        }

        public override string toString()
        {
            return base.toString() + "Donor ID: " + DonorID + "\nAddress " + Address + "\nPhone: " + Phone + "\nCard Number " + CardNumber + "\nExpiry Date: " + CardExpiry + "\nCard Type: " + CardType + "\n";
        }

    }
}

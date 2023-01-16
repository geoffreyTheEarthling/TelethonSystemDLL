using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelethonSystemDLL
{
    class Donations : CollectionBase
    {
        public void add (Donation don)
        {
            List.Add(don);
        }

        public Donation this[int index]
        {
            get { return (Donation)List[index]; }
            set { List[index] = value; }
        }
    }
}

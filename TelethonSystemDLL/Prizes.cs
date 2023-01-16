using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelethonSystemDLL
{
    class Prizes : CollectionBase 
    {
        public void add(Prize prize)
        {
            List.Add(prize);
        }

        public void clearPrize(Prize prize)
        {
            List.Remove(prize);
        }

        public Prize this[int index]
        {
            get { return (Prize)List[index]; }
            set { List[index] = value; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelethonSystemDLL
{
    abstract class Person
    {
        string firstname, lastname;   

        public Person(string fn, string ln)
        {
            this.firstname = fn;
            this.lastname = ln;       
        }

        public virtual string toString()
        {
            return "Name: " + this.firstname + " " + this.lastname + "\n";
        }
    }
}

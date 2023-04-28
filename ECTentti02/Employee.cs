using System;
using System.Collections.Generic;
using System.Text;

namespace ECTentti02
{
    class Employee
    {
        //Ominaisuudet
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Job> Jobs { get; set; }
    }
}

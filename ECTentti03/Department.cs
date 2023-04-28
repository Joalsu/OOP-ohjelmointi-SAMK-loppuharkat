using System;
using System.Collections.Generic;
using System.Text;

namespace ECTentti03
{
    public class Department
    {
        //Ominaisuudet
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Employee> Employees { get; set; }
        public int EmployeeCount { get => Employees.Count; }

        //Konstruktorit
        public Department() { Employees = new List<Employee>(); }

        public Department(int id, string name)
            :this() { Id = id; Name = name; }

        //Metodit
        public override string ToString() => $"{Name} ({EmployeeCount})";
    }
}

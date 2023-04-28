using System;
using System.Collections.Generic;
using System.Text;

namespace ECTentti03
{
    public class Employee
    {
        //Kenttämuuttujat
        private double _salary;

        //Ominaisuudet
        public int Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Department Department { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string Name => $"{LastName} {FirstName}";

        //Palauttaa lasketun DateOfBirth arvon vähenmälällä henkilön syntymävuoden
        //nykyisestä vuodesta, jos sillä ei ole arvoa palautetaan 0
        public int Age
        {
            get
            {
                if (!DateOfBirth.HasValue)
                {
                    return 0;
                }

                return StartDate.Year - DateOfBirth.Value.Year;
            }
        }

        //Kapseloidaan kenttämuuttuja palauttamalla arvo pyöristettynä 2 desimaaliin
        //jos annettu arvo on negatiivinen lähetetään poikkeus
        public double Salary
        {
            get => _salary;

            set
            {
                if (value < 0)
                {
                    throw new ApplicationException("Negatiivinen palkka.");
                }
                else
                {
                    _salary = Math.Round(value, 2);
                }
            }
        }

        //konstruktorit
        public Employee(int id, string first, string last, DateTime? dob, double salary)
        {
            StartDate = DateTime.Today;
            EndDate = null;

            Id = id;
            FirstName = first;
            LastName = last;
            DateOfBirth = dob;
            Salary = salary;
        }

        //Metodit
        public override string ToString() => $"{Id} {FirstName} {LastName}";
    }
}

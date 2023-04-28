using System;
using System.Collections.Generic;
using static System.Console;
using System.Text;

namespace ECTentti02
{
    static class Application
    {
        private static ConsoleControl JobMenu;
        private static ConsoleControl JobDetails;
        private static ConsoleControl JobEmployees;

        //Metodit

        private static void BindMenuData(List<Job> jobs)
        {
            //Tarkistetaan onko items ominaisuus null
            //jos on lisätään siihen uusi merkkijonolista
            if (JobMenu.Items == null)
            {
                JobMenu.Items = new List<string>();
            }

            //jos ei tyhjätään se
            else
            {
                JobMenu.Items.Clear();
            }

            //käydään lista läpi lisämällä siihen merkkijono arvo yhdisteet.
            foreach (var item in jobs)
            {
                string values = (item.Id + " " + item.Title);
                JobMenu.Items.Add(values);
            }
        }

        //Metodi, jossa sama items käsittely kun edellisessä
        //Lisätään oikeat parametrin arvot Items listan ominaisuuteen
        private static void BindDetailsData(Job job)
        {
            if (JobDetails.Items == null)
            {
                JobDetails.Items = new List<string>();
            }

            else
            {
                JobDetails.Items.Clear();
            }

            JobDetails.Items.Add("TYÖN TIEDOT");

            JobDetails.Items.Add($"id: {job.Id}");
            JobDetails.Items.Add($"Nimi: {job.Title}");
            JobDetails.Items.Add($"Alkaa: {job.StartDate.ToShortDateString()}");
            JobDetails.Items.Add($"Loppuu: {job.EndDate.ToShortDateString()}");
        }

        //Metodi jossa lisätään Items ominaisuuteen käymällä Data.employees
        //lista läpi lisäämällä Name ominaisuuden arvot
        private static void  BindEmployeesData(Job job)
        {
            if (JobEmployees.Items == null)
            {
                JobEmployees.Items = new List<string>();
            }

            else
            {
                JobEmployees.Items.Clear();
            }

            JobEmployees.Items.Add("TYÖN TEKIJÄT");

            foreach (var e in Data.employees)
            {
                JobEmployees.Items.Add(e.Name);
            }

        }

        private static void Initialize()
        {
            //Sijoitetaan staattisiin muuttujiin uudet ConsoleControl oliot,
            //joille annetaan tarvittavat arvot konstrukorille ja
            //ominaisuuksiin asetetaan väri arvot.

            int w = WindowWidth;
            w = (w / 2) - 1;

            int h = Data.jobs.Count;

            JobMenu = new ConsoleControl(1, 2, w, h)
            {
                BackColor = ConsoleColor.Gray,
                TextColor = ConsoleColor.Blue
            };

            int c = WindowWidth;
            c = (c / 2) + 1;

            JobDetails = new ConsoleControl(c, 2, w, 5)
            {
                BackColor = ConsoleColor.Gray,
                TextColor = ConsoleColor.Green
            };

            int r = JobDetails.Height + 3;

            int h2 = WindowHeight;
            h2 = (h2 - JobDetails.Height) - 1;

            JobEmployees = new ConsoleControl(c, r, w, h2)
            {
                BackColor = ConsoleColor.Gray,
                TextColor = ConsoleColor.Red
            };

            BindMenuData(Data.jobs);

            //Uusi tapahtuma-olio Mediator‐oliolle ja kutsutaan sen jälkeen
            //metodeja joille arvoksi olion job ominaisuus arvo.

            JobChangedEventArgs jobchan = new JobChangedEventArgs();

            jobchan.Job = new Job();

            BindDetailsData(jobchan.Job);
            BindEmployeesData(jobchan.Job);
        }

        //Metodi jossa käydään läpi Data.Jobs lista ja jos parametrinid täsmää ominaisuuteen
        //items.id synnytetään Mediator‐oliolle tapahtuma ja tulostetaan arvot Draw ominaisuuksissa.
        private static void MenuSelectionChanged(int jobId)
        {
            foreach (var item in Data.jobs)
            {
                if (jobId == item.Id)
                {
                    Mediator.Instance.OnJobChanged(JobMenu, item);

                    JobDetails.Draw();
                    JobEmployees.Draw();
                }
            }
        }

        public static void Run()
        {
            Initialize();
            JobMenu.Draw();

            int id;

            //kysytään id:tä niin kauvan, kunnes annetaan 0, jos
            //virheellinen arvo lähetetään poikkeus ja tulostetaan virhe viesti
            while (true)
            {
                try
                {
                    SetCursorPosition(0, 0);
                    ResetColor();
                    Write("Valitse työn id (nolla lopettaa):");
                    id = int.Parse(ReadLine());

                    if (id == 0)
                    {
                        break;
                    }

                    if (id < 0 || id > JobMenu.Items.Count)
                    {
                        SetCursorPosition(0, 0);
                        throw new Exception("Virheellinen syöte. Paina Enter.:");
                    }

                    //Jos kelvollinen syöte suoritetaan metodi ja annetaan arvoksi annettu id
                    MenuSelectionChanged(id);
                }
                catch (Exception)
                {
                    throw new Exception("Virheellinen syöte. Paina Enter.:");
                }
            }

        }
    }
}

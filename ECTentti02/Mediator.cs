using System;
using System.Collections.Generic;
using System.Text;

namespace ECTentti02
{
    sealed class Mediator
    {
        private static Mediator instance = new Mediator();

        //Ominaisuus, jossa palautetaan muuttujan instance arvo
        public static Mediator Instance 
        {
            get => instance;
        }

        //konstruktorit
        private Mediator()
        {

        }

        //Tapahtumat
        public event EventHandler<JobChangedEventArgs> JobChanged;

        //Metodi, jossa asetetaan paikalliseen muuttujaan tapahtuman arvo
        //jos muuta kun null asetetaan metodin paremetri arvot siihe
        public void OnJobChanged(object sender, Job job)
        {
            EventHandler<JobChangedEventArgs> jobChangeDelegate = JobChanged;

            if (jobChangeDelegate != null)
            {
                jobChangeDelegate(sender, new JobChangedEventArgs { Job = job });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ECTentti03
{
    public class MenuItem
    {
        //Ominaisuudet
        public int Id { get; set; }
        public string Name { get; set; }

        //Tapahtumat
        public event EventHandler<MenuItemEventArgs> ItemSelected;

        //Metodi, jossa suoritetaan mahdollinen tapahtuma
        public void Select()
        {
            ItemSelected?.Invoke(this, new MenuItemEventArgs { ItemId = Id });
        }

        public override string ToString() => $"{Id}. {Name}";
    }
}

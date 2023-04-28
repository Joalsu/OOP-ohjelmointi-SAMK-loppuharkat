using System;
using static System.Console;

namespace Tentti01
{
    class Program
    {
        static void Main()
        {
            //Asetetaan komentokehoteikkunan otsikkorivin teksti
            Title = "ISBN-tarkastaja";
            
            string isbn_numero; //merkkijono johonka tulee käyttäjän antama syöte
            int kelvolliset_kpl = 0; //arvo muuttuja johonka talletetaan kuinka monta kertaa annetaan virheellinen isbn numero
            int virheelliset_kpl = 0; //arvo muuttuja johonka talletetaan kuinka monta kertaa annetaan oikea isbn numero

            do //aloitetaan ikuinen looppi
            {
                Write("Anna numerosarja: ");
                isbn_numero = ReadLine();

                if (isbn_numero.Length == 0) //jos annettu syötteen pituus on 0 eli tyhjä tehdään tulostukset ja lopetetaan ohjelman suoritus.
                {
                    WriteLine();
                    WriteLine($"Kelvollisia numerosarjoja {kelvolliset_kpl} kpl.");
                    WriteLine($"virheellsiä numerosarjoja {virheelliset_kpl} kpl.");
                    break;
                }

                try
                {
                    IsbnTarkistus(isbn_numero);//siirrytään staattisen metodin suoritukseen jolle viedään syötteestä saatu arvo
                    kelvolliset_kpl++;//kasvatetaan arvoa yhdellä (+1) jos isb_numero oli oikein.
                }

                catch (DivideByZeroException virhetarkiste)//tähän mennään vain jos Tarkistemerkin_laskenta staattinen metodi oli epätosi
                {
                    ForegroundColor = ConsoleColor.Yellow; //muutetaan tekstin tulostus väriksi keltainen
                    WriteLine(virhetarkiste.Message);
                    virheelliset_kpl++;//kasvatetaan arvoa yhdellä (+1) koska tarkiste oli virheellinen
                    ResetColor(); //palautetaan tekstin tulostusväri normaaliksi, että ei enää tulosteta keltaisella
                }

                catch (Exception virhenumerosarja) //siirrtytään tähän jos IsbnTarkistus staattisissa metodeissa tapahtui virhe
                {
                    ForegroundColor = ConsoleColor.Yellow; 
                    WriteLine(virhenumerosarja.Message);
                    ResetColor();
                }

            } while (true);
        }

        static void IsbnTarkistus(string isbn_numero)
        {
            //ASCII koodi taulukko char merkeillä eli jos antaa syötteessä vaikka kirjaimen b
            //sen arvo vastaa numerisesti 98 ja jos antaisi vaikka 3 joka on kirjain 3 ei luku 3 nii sen arvo olisi 51
            // 0 = 48     1 = 49     2 = 50    3 = 51     4 = 52 ...
            // a = 97     b = 98     c = 99    d = 100    e = 101 ...
            foreach (char arvo in isbn_numero) // käydään jokainen merkki kerrallaan läpi merkkijonosta isbn_numero
            {
                //jos merkkijonon isbn_numero tällähetkellä käsitteltävä merkki on pienempi kuin kirjain '0' eli ASCII koodi 48 tai suurempi kuin kirjain '9' eli 57,
                //lähetetään virheviesti yllä olevan pääohjelman napattavaksi ja mennään pois tästä staattisesta metodista ja lopetetaan foreach looppi
                // jos arvo on epätosi ei tehdä mitään ja mennään  seuraavaan kirjaimeen merkkijonossa
                if (arvo < '0' || arvo > '9')
                    throw new Exception("Syötteen muoto on virheellinen.");
            }

            //siirrytään tähän if lauseeseen jos yllä oleva if lause oli kokonaisuudessaan eätotta
            if (isbn_numero.Length != 13) //jos merkkijonon isbn_numero pituus ei ole 13...
            {
                throw new Exception("Syötteen pituus on virheellinen."); //lähetetään virheviesti yllä olevan pääohjelman napattavaksi ja mennään pois tästä staattisesta metodista
            }

            Tarkistemerkin_laskenta(isbn_numero); //jos kummatkin ylläolevat if lauseet olivat epätotta eli isbn_numero oli okein siirrtyään alla olevaan staattiseen metodiin jolle viedään merkkijono
        }

        static void Tarkistemerkin_laskenta(string isbn_numero)
        {

            int summa = 0; //arvo muuttuja johonka talletetaan isbn_numero lukujen yhteen laskettu summa
            int jarjestysnumero = 0; //arvo muuttuja joka pitää muistissa kuinka mones kirjain on menossa
            string lukuStringiksi;
            int lukuIntiksi;
            string isbn_poistavika = isbn_numero.Substring(0, 12); //uusi merkkijono jota on poitettu viimeinen merkki merkkijonosta isbn_numero

            foreach (char luku in isbn_poistavika)// käydään jokainen merkki kerrallaan läpi merkkijonosta;
            {
                lukuStringiksi = luku.ToString(); //muuttetaan ensiksi merkki stringiksi koska char arvoa ei voi suoraan muuntaa int luku arvoksi
                lukuIntiksi = int.Parse(lukuStringiksi); //muunnetaan sitten string merkkijono intiksi eli luvuksi
                jarjestysnumero++; //kasvatetaan arvoa yhdellä (+1) niin tiedeetään kuinka mones luku merkkijonosta on käsittelyssä

                if (jarjestysnumero % 2 == 0) //jos jakojäännökseksi tulee 0 kun jaetaan kadella järjestysnumero (eli kuinka mones luku) eli kyseessä on parillinen luku
                {
                    summa = summa + lukuIntiksi * 3; //kerrotaan luku kolmella ja lisätään tulos summaan
                }

                if (jarjestysnumero % 2 == 1) //jos jakojäännökseksi tulee 1 kun jaetaan kadella järjestysnuymero (eli kuinka mones luku) eli kyseessä on pariton luku
                {
                    summa = summa + lukuIntiksi * 1; //kerrotaan luku yhdellä ja lisätään tulos summaan

                }

            }

            //kun ylläoleva looppi on käyty loppuun eli kaikki merkit on käyty yksi kerrallaan läpi ja laskettu niiden yhteinen summa

            string viimeinenarvo = isbn_numero.Substring(12);//otetaan viimeinen eli 13 arvo merkkijonosta
            int tarkistusmerkki = int.Parse(viimeinenarvo);//muutetaan se int luvuksi

            int jaannos = summa % 10; //otetaan talteen mikä on jakojäännös kun yllä laskettu summa jaetaan kymmenellä
            int tarkistusluku = 10 - jaannos; //vähennetään 10 jakojäännöksestä

            if (tarkistusluku > 9) //jos jakojäännös on enemmän kuin 9 esim 10 niin otetaan siitä jakojäännös että saadaan vain viimenen luku
            {
               tarkistusluku = tarkistusluku % 10;
            }

            if (tarkistusmerkki == tarkistusluku) // jos viimeinen arvo merkkijonossa on sama kuin laskettu tarkistusluku
            {
                WriteLine("ISBN.");
            }

            else //jos epätotta
            {
                //lähetetään virheviesti yllä olevan pääohjelman napattavaksi ja mennään pois tästä staattisesta metodista
                throw new DivideByZeroException("virheellinen tarkiste.");
            }
  
        }

    }
}

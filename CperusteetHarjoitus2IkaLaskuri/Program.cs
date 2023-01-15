using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CperusteetHarjoitus2IkaLaskuri
{

    //Tehdään laskuri, joka laskee eletyn ajanhetken ja käyttäjän syöttämän ajan erotuksen.
    //Laskuri antaa sukupuolen ja annetun syntymäpäivän perusteella eliniän ennusteen.
    //Muunna ensin tehty rutiini omaksi alirutiinikseen. Lisää Main-ohjelmalohkoon silmukka, joka kysyy halutaanko ikälaskenta suorittaa uudelleen.
    //Jos halutaan, kutsutaan luotua alirutiinia. Jos ei haluta, ohjelma lopetetaan.
    //Lisää ikälaskuriin värit: jos sukupuoleksi annetaan M, tekstin taustaväriksi laitetaan sininen ja jos N, laitetaan punainen.
    //Jäljellä olevan eliniän mukaan: ikää jäljellä > 20 vuotta, tausta green, fontti black; ikää jäljellä < 20 vuotta, tausta blue, fontti darkyellow; 
    //ikää jäljellä < 2 vuotta, tausta DarkRed, fontti white sekä lisäksi annetaan piippaus.
    //ENNEN tarkistusta: laskuri toimii, eliniän ennusteen antaminen sukupuolen perusteella toimii. Alirutiini ja silmukka toimii. Sukupuolen kohdalla väri vaihtuu oikein.
    //Värin vaihto iän/ikäluokituksen mukaan ei toimi, kaikille tulee taustaväriksi vihreä ja fontiksi musta.
    //Tarkistuksen jälkeen: Värinvaihto ikäluokituksen mukaan toimii, kun vaihdettiin if-else rakenteesta ikäluokat toisin päin.
    //LISÄÄ: jos eletty kauemmin kuin eliniän ennuste on, miten lisättäisiin sen tulostus ohjelmaan?
    class Program
    {
        static void Main(string[] args)
        {
            IanLaskenta(); //Ensin kutsutaan IanLAskenta-alirutiinia
            Boolean jatka = true; //asetetaan boolean-muuttujan lähtöarvoksi true

            while (jatka == true)
            {
            Console.WriteLine("Haluatko suorittaa ikälaskennan? K=Kyllä/E=Ei"); //Kysytään käyttäjältä, haluaako hän suorittaa laskennan
            //voitaisi laittaa myös: if (Console.ReadLine().ToUpper() == "E") jatka = false;
            if (Console.ReadLine().ToUpper() == "E") //Jos konsolilta luettu, käyttäjän syöttämä tieto on isoksi muutettuna E
                {
                    jatka = false; //asetetaan jatka-muuttujan arvoksi false (ja poistutaan silmukasta, koska se pyörii vain, kun jatka == true)
                } else //jos käyttäjän syöttämä vastaus on jotakin muuta kuin "E"
                {
                    //jatka = true; //jatka-muuttuja asetetaan trueksi (EI VAADI TÄTÄ,koska lähtöarvoksi asetettu true)
                    IanLaskenta(); //kutsutaan IanLaskenta-alirutiinia
                }
            } 
        }

        private static void IanLaskenta()
        {
            Double aikaJaljella = 0; //Double-tyyppisessä muuttujassa on desimaalit mukana
            int elinIanOdote = 0, VuodetJaljella = 0;
            string alkuAika = "", sp = "", vuodetKuukaudetPaivat = "";
            DateTime syntymaAikaDT, elinianOdotusDT; //tähän muuttujaan tallennetaan käyttäjän syöttämä aika
            DateTime tanaan = DateTime.Today; //tähän muuttujaan annetaan arvoksi kuluva ajanhetki. DateTime.Now sisältää kellonajan, DateTime.Today:ssä olisi vain päivämäärä
            string formaatti = "dd.MM.yyyy"; //luodaan string-apumuuttuja -formaatti, jossa kerrotaan, missä muodossa päivämäärätieto tulee käyttäjältä.
            CultureInfo kulttuuri = CultureInfo.InvariantCulture; //luodaan CultureInfo

            Console.WriteLine("Kerro sukupuoli, M=Mies/N=Nainen");
            sp = Console.ReadLine().ToUpper();
            switch (sp) //mitä tutkitaan (sp-muuttujan arvoa)
            {
                case "M": //jos käyttäjä on syöttänyt M. Pitää olla lainausmerkit, koska on string eikä char.
                    elinIanOdote = 78; //int-muuttuja elinIanOdote on 78
                    Console.BackgroundColor = ConsoleColor.Blue; //muutetaan tekstin taustaväriksi sininen
                    break;

                case "N": //jos käyttäjä on syöttänyt N
                    elinIanOdote = 84;
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                default: //jos ei ole mikään yllä olevista
                    Console.WriteLine("Virheellinen valinta!");
                    elinIanOdote = 0; //ei välttämätön, koska lähtöarvoksi on syötetty jo nolla.
                    break;
            }

            //Pyydetään käyttäjää syöttämään syntymäaika
            Console.WriteLine("Anna syntymäaika muodossa PP.KK.VVVV");
            alkuAika = Console.ReadLine(); //sijoitetaan string-muuttuja syntymaAikaan käyttäjän syöttämä syntymäaika. HUOM! Se on string-muodossa, kuten käyttäjän syöte aina.
            //Muutetaan yllä saatu string-tyyppinen tieto päivämääräksi:
            //Lisätään try-catch -käsittely
            try //try-catchia käytetään virheen käsittelyyn
            {
                //tätä ohjelma yrittää tehdä
                syntymaAikaDT = DateTime.ParseExact(alkuAika, formaatti, kulttuuri); //Muunnetaan käyttäjän syöttämä päivämäärä oikeaan muotoon. ParseExact tarvitsee useamman parametrin, ensin muunnettava string-muuttuja, sitten formaatti, missä tieto tulee ja sitten CultureInfo
                elinianOdotusDT = syntymaAikaDT.AddYears(elinIanOdote); //lisätään syntymäaikaan eliniän odote AddYears-metodilla, jolle annetaan parametriksi int elinIanOdote
                //Lasketaan päivämäärien erotus. Huom: molempien muuttujien oltava DateTime-tyyppisiä jotta laskenta voidaan tehdä
                aikaJaljella = elinianOdotusDT.Subtract(tanaan).TotalDays; //Vähennetään elinianOdotusDT-muuttujasta tänään eli DateTime.Now -arvo.
                if (aikaJaljella < 0) aikaJaljella = 0; //jos laskennan tulokseksi tulee alle nolla, asetetaan aikaJaljella-muuttujan arvoksi 0.
                DateTime paivat = new DateTime(new TimeSpan((int)aikaJaljella + 1, 0, 0, 0).Ticks); //luodaan DateTime-olio, joka on samalla Timespan-olio jolle annetaan parametriksi int-muotoon castattu aikaJaljella-double.
                vuodetKuukaudetPaivat = string.Format("{0} vuotta {1} kuukautta ja {2} päivää", paivat.Year - 1, paivat.Month - 1, paivat.Day - 1); //tähän otetaan parametriksi paivat, koska sinne sijoitettiin TimeSpaniin aikaJaljella
                VuodetJaljella = paivat.Year - 1; //asetetaan VuodetJaljella muuttujan arvoksi paivat.Yeaar-1               
            }
            catch (Exception ee) //Muodostettu exceptionista oma objekti. Jos ohjelma menee virheeseen, tapahtuu näin:
            {
                Console.WriteLine("Ohjelma ei osannut laskea päivämääräerotusta. Tarkista pvm-formaatti!"); //eli tulee tämä itse muotoiltu virheilmoitus
                Console.WriteLine(ee.Message); //Tulostetaan konsolille Exceptionin sisältämä message
                aikaJaljella = 0;
            }

            if (VuodetJaljella < 2) //jos VuodetJaljella muuttujan arvo on alle 2 (eli 0-2, koska muuttujan aikaJaljella arvoksi on annettu 0, jos se menee alle nollan)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed; //tekstin taustaväriksi tummanpunainen
                Console.ForegroundColor = ConsoleColor.White; //fontin väriksi valkoinen
                Console.Beep(); //konsoli piippaa
            }
            else if (VuodetJaljella < 20)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine("Odotettua elinaikaa jäljellä " + vuodetKuukaudetPaivat + ".");
            Console.ReadLine();
            Console.ResetColor();
        }
    }
}

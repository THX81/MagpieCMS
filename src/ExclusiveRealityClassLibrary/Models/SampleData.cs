using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace ExclusiveReality.Models
{
    public class SampleData
    {
        public void Create()
        {
            #region templates
            ExclusiveReality.Models.PageTemplate hpTemplate = new ExclusiveReality.Models.PageTemplate();
            hpTemplate.Name = "hppage";
            hpTemplate.Title = "Home Page";
            hpTemplate.Create();

            ExclusiveReality.Models.PageTemplate contentTemplate = new ExclusiveReality.Models.PageTemplate();
            contentTemplate.Name = "contentpage";
            contentTemplate.Title = "Obsahov� �ablona";
            contentTemplate.Create();

            ExclusiveReality.Models.PageTemplate indexTemplate = new ExclusiveReality.Models.PageTemplate();
            indexTemplate.Name = "indexpage";
            indexTemplate.Title = "Str�nka s v�pisem obsahu sekce";
            indexTemplate.Create();

            ExclusiveReality.Models.PageTemplate contentEstatesTemplate = new ExclusiveReality.Models.PageTemplate();
            contentEstatesTemplate.Name = "contentestatestemplate";
            contentEstatesTemplate.Title = "Obsahov� �ablona sekce nemovitost�";
            contentEstatesTemplate.Create();


            ExclusiveReality.Models.PageTemplate flatsProjectsTemplate = new ExclusiveReality.Models.PageTemplate();
            flatsProjectsTemplate.Name = "developerprojectspage";
            flatsProjectsTemplate.Title = "�ablona pro v�pis bytov�ch projekt�";
            flatsProjectsTemplate.Create();


            ExclusiveReality.Models.PageTemplate flatsSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            flatsSaleTemplate.Name = "flatssalepage";
            flatsSaleTemplate.Title = "�ablona pro v�pis prodeje byt�";
            flatsSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate flatsRentTemplate = new ExclusiveReality.Models.PageTemplate();
            flatsRentTemplate.Name = "flatsrentpage";
            flatsRentTemplate.Title = "�ablona pro v�pis pron�jmu byt�";
            flatsRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate rdSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            rdSaleTemplate.Name = "rdsalepage";
            rdSaleTemplate.Title = "�ablona pro v�pis prodeje dom� a vil";
            rdSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate rdRentTemplate = new ExclusiveReality.Models.PageTemplate();
            rdRentTemplate.Name = "rdrentpage";
            rdRentTemplate.Title = "�ablona pro v�pis pron�jmu dom� a vil";
            rdRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate flatsHousesSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            flatsHousesSaleTemplate.Name = "flatshousessalepage";
            flatsHousesSaleTemplate.Title = "�ablona pro v�pis prodeje �in�ovn�ch dom�";
            flatsHousesSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate flatsHousesRentTemplate = new ExclusiveReality.Models.PageTemplate();
            flatsHousesRentTemplate.Name = "flatshousesrentpage";
            flatsHousesRentTemplate.Title = "�ablona pro v�pis pron�jmu �in�ovn�ch dom�";
            flatsHousesRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate noLivePlacesSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            noLivePlacesSaleTemplate.Name = "noliveplacessalepage";
            noLivePlacesSaleTemplate.Title = "�ablona pro v�pis prodeje nebytov�ch prostor";
            noLivePlacesSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate noLivePlacesRentTemplate = new ExclusiveReality.Models.PageTemplate();
            noLivePlacesRentTemplate.Name = "noliveplacesrentpage";
            noLivePlacesRentTemplate.Title = "�ablona pro v�pis pron�jmu nebytov�ch prostor";
            noLivePlacesRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate estatePropertiesSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            estatePropertiesSaleTemplate.Name = "estatepropertiessalepage";
            estatePropertiesSaleTemplate.Title = "�ablona pro v�pis prodeje pozemk�";
            estatePropertiesSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate estatePropertiesRentTemplate = new ExclusiveReality.Models.PageTemplate();
            estatePropertiesRentTemplate.Name = "estatepropertiesrentpage";
            estatePropertiesRentTemplate.Title = "�ablona pro v�pis pron�jmu pozemk�";
            estatePropertiesRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate koSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            koSaleTemplate.Name = "kosalepage";
            koSaleTemplate.Title = "�ablona pro v�pis prodeje komer�n�ch prostor";
            koSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate koRentTemplate = new ExclusiveReality.Models.PageTemplate();
            koRentTemplate.Name = "korentpage";
            koRentTemplate.Title = "�ablona pro v�pis pron�jmu komer�n�ch prostor";
            koRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate recreationObjectsSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            recreationObjectsSaleTemplate.Name = "recreationobjectssalepage";
            recreationObjectsSaleTemplate.Title = "�ablona pro v�pis prodeje rekrea�n�ch objekt�";
            recreationObjectsSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate recreationObjectsRentTemplate = new ExclusiveReality.Models.PageTemplate();
            recreationObjectsRentTemplate.Name = "recreationobjectsrentpage";
            recreationObjectsRentTemplate.Title = "�ablona pro v�pis pron�jmu rekrea�n�ch objekt�";
            recreationObjectsRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate hotelsSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            hotelsSaleTemplate.Name = "hotelssalepage";
            hotelsSaleTemplate.Title = "�ablona pro v�pis prodeje hotel�, penzion�";
            hotelsSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate hotelsRentTemplate = new ExclusiveReality.Models.PageTemplate();
            hotelsRentTemplate.Name = "hotelsrentpage";
            hotelsRentTemplate.Title = "�ablona pro v�pis pron�jmu hotel�, penzion�";
            hotelsRentTemplate.Create();
            #endregion


            #region cultures
            ExclusiveReality.Models.Culture cultureCz = new ExclusiveReality.Models.Culture(1029, true);
            cultureCz.Create();

            ExclusiveReality.Models.Culture cultureEn = new ExclusiveReality.Models.Culture(2057);
            cultureEn.Create();
            #endregion


            #region site
            ExclusiveReality.Models.Section root = new ExclusiveReality.Models.Section(null, "Root", String.Empty);
            root.OrderPriority = 0;
            root.CreateIndexPage = false;
            root.Published = true;
            root.Culture = cultureCz;
            root.Create();


            ExclusiveReality.Models.Page domu = new ExclusiveReality.Models.Page(root, "�vod", "index", "");
            domu.Published = true;
            domu.OrderPriority = 0;
            domu.PageTemplate = hpTemplate;
            domu.Create();



            ExclusiveReality.Models.Section sluzby = new ExclusiveReality.Models.Section(root, "Na�e slu�by", "sluzby");
            sluzby.OrderPriority = 2;
            sluzby.Published = false;
            sluzby.Create();

            ExclusiveReality.Models.Page vykupNemovitosti = new ExclusiveReality.Models.Page(sluzby, "V�kup nemovitost�", "vykup-nemovitosti", String.Empty);
            vykupNemovitosti.Published = true;
            vykupNemovitosti.OrderPriority = 1;
            vykupNemovitosti.PageTemplate = contentTemplate;
            vykupNemovitosti.Create();

            ExclusiveReality.Models.Page realitniSluzby = new ExclusiveReality.Models.Page(sluzby, "Realitn� sluzby", "realitni-sluzby", String.Empty);
            realitniSluzby.Published = true;
            realitniSluzby.OrderPriority = 2;
            realitniSluzby.PageTemplate = contentTemplate;
            realitniSluzby.Create();

            ExclusiveReality.Models.Page financniSluzby = new ExclusiveReality.Models.Page(sluzby, "Finan�n� slu�by", "financni-sluzby", String.Empty);
            financniSluzby.Published = true;
            financniSluzby.OrderPriority = 1;
            financniSluzby.PageTemplate = contentTemplate;
            financniSluzby.Create();

            ExclusiveReality.Models.Page zprostredkovaniProdeje = new ExclusiveReality.Models.Page(sluzby, "Zprost�edkov�n� prodeje", "zprostredkovani-prodeje", String.Empty);
            zprostredkovaniProdeje.Published = true;
            zprostredkovaniProdeje.OrderPriority = 1;
            zprostredkovaniProdeje.PageTemplate = contentTemplate;
            zprostredkovaniProdeje.Create();

            ExclusiveReality.Models.Page poradenstvi = new ExclusiveReality.Models.Page(sluzby, "Poradenstv�", "poradenstvi", String.Empty);
            poradenstvi.Published = true;
            poradenstvi.OrderPriority = 1;
            poradenstvi.PageTemplate = contentTemplate;
            poradenstvi.Create();

            ExclusiveReality.Models.Page pravniPoradna = new ExclusiveReality.Models.Page(sluzby, "Pr�vn� poradna", "pravni-poradna", String.Empty);
            pravniPoradna.Published = true;
            pravniPoradna.OrderPriority = 1;
            pravniPoradna.PageTemplate = contentTemplate;
            poradenstvi.Create();

            ExclusiveReality.Models.Page pojisteni = new ExclusiveReality.Models.Page(sluzby, "Poji�t�n�", "pojisteni", String.Empty);
            pojisteni.Published = true;
            pojisteni.OrderPriority = 1;
            pojisteni.PageTemplate = contentTemplate;
            pojisteni.Create();






            ExclusiveReality.Models.Section financovani = new ExclusiveReality.Models.Section(root, "Financov�n�", "financovani");
            financovani.OrderPriority = 3;
            financovani.Published = false;
            financovani.Create();

            ExclusiveReality.Models.Page obecneInformace = new ExclusiveReality.Models.Page(financovani, "Obecn� informace", "obecne-informace ", String.Empty);
            obecneInformace.Published = true;
            obecneInformace.OrderPriority = 1;
            obecneInformace.PageTemplate = contentTemplate;
            obecneInformace.Create();

            ExclusiveReality.Models.Page financovaniNemovitosti = new ExclusiveReality.Models.Page(financovani, "Financov�n� nemovitost�", "financovani-nemovitosti", String.Empty);
            financovaniNemovitosti.Published = true;
            financovaniNemovitosti.OrderPriority = 2;
            financovaniNemovitosti.PageTemplate = contentTemplate;
            financovaniNemovitosti.Create();

            ExclusiveReality.Models.Page pojisteniNemovitosti = new ExclusiveReality.Models.Page(financovani, "Poji�t�n� nemovitost�", "pojisteni-nemovitosti", String.Empty);
            pojisteniNemovitosti.Published = true;
            pojisteniNemovitosti.OrderPriority = 3;
            pojisteniNemovitosti.PageTemplate = contentTemplate;
            pojisteniNemovitosti.Create();

            ExclusiveReality.Models.Page hypoteka = new ExclusiveReality.Models.Page(financovani, "Hypot�ka", "hypoteka", String.Empty);
            hypoteka.Published = true;
            hypoteka.OrderPriority = 4;
            hypoteka.PageTemplate = contentTemplate;
            hypoteka.Create();

            ExclusiveReality.Models.Page kalkulaceHypoteky = new ExclusiveReality.Models.Page(financovani, "Kalkulace hypot�ky", "kalkulace-hypoteky", String.Empty);
            kalkulaceHypoteky.Published = true;
            kalkulaceHypoteky.OrderPriority = 5;
            kalkulaceHypoteky.PageTemplate = contentTemplate;
            kalkulaceHypoteky.Create();

            ExclusiveReality.Models.Page financniPoradenstvi = new ExclusiveReality.Models.Page(financovani, "Finan�n� poradenstv�", "financni-poradenstvi", String.Empty);
            financniPoradenstvi.Published = true;
            financniPoradenstvi.OrderPriority = 6;
            financniPoradenstvi.PageTemplate = contentTemplate;
            financniPoradenstvi.Create();

            ExclusiveReality.Models.Page ostatniProdukty = new ExclusiveReality.Models.Page(financovani, "Ostatn� produkty", "ostatni-produkty", String.Empty);
            ostatniProdukty.Published = true;
            ostatniProdukty.OrderPriority = 7;
            ostatniProdukty.PageTemplate = contentTemplate;
            ostatniProdukty.Create();






            ExclusiveReality.Models.Section development = new ExclusiveReality.Models.Section(root, "Development", "development");
            development.OrderPriority = 4;
            development.Published = false;
            development.Create();

            ExclusiveReality.Models.Page noveProjektyDev = new ExclusiveReality.Models.Page(development, "Nov� projekty pro developery a investory", "nove-projekty-developeri", String.Empty);
            noveProjektyDev.Published = true;
            noveProjektyDev.OrderPriority = 1;
            noveProjektyDev.PageTemplate = flatsProjectsTemplate;
            noveProjektyDev.Create();

            ExclusiveReality.Models.Page proveditelnostProjektu = new ExclusiveReality.Models.Page(development, "Proveditelnost projektu", "proveditelnost-projektu", String.Empty);
            proveditelnostProjektu.Published = true;
            proveditelnostProjektu.OrderPriority = 2;
            proveditelnostProjektu.PageTemplate = contentTemplate;
            proveditelnostProjektu.Create();

            ExclusiveReality.Models.Page marketing = new ExclusiveReality.Models.Page(development, "Marketing", "marketing", String.Empty);
            marketing.Published = true;
            marketing.OrderPriority = 3;
            marketing.PageTemplate = contentTemplate;
            marketing.Create();







            ExclusiveReality.Models.Section onas = new ExclusiveReality.Models.Section(root, "O n�s", "o-nas");
            onas.OrderPriority = 6;
            onas.Published = false;
            onas.Create();

            ExclusiveReality.Models.Page profil = new ExclusiveReality.Models.Page(onas, "Profil spole�nosti", "profil-spolecnosti", String.Empty);
            profil.Published = true;
            profil.OrderPriority = 1;
            profil.PageTemplate = contentTemplate;
            profil.Create();

            ExclusiveReality.Models.Page nastym= new ExclusiveReality.Models.Page(onas, "N� t�m", "nas-tym", String.Empty);
            nastym.Published = true;
            nastym.OrderPriority = 2;
            nastym.PageTemplate = contentTemplate;
            nastym.Create();

            ExclusiveReality.Models.Page dalsiInfo = new ExclusiveReality.Models.Page(onas, "Dal�� info o spole�nosti", "dalsi-info-o-spolecnosti", String.Empty);
            dalsiInfo.Published = true;
            dalsiInfo.OrderPriority = 3;
            dalsiInfo.PageTemplate = contentTemplate;
            dalsiInfo.Create();

            ExclusiveReality.Models.Page etickyKodex = new ExclusiveReality.Models.Page(onas, "Etick� kodex", "eticky-kodex", String.Empty);
            etickyKodex.Published = true;
            etickyKodex.OrderPriority = 4;
            etickyKodex.PageTemplate = contentTemplate;
            etickyKodex.Create();





            ExclusiveReality.Models.Page kontakt = new ExclusiveReality.Models.Page(root, "Kontakt", "kontakt", "<p>Kontakt.</p>");
            kontakt.Published = true;
            kontakt.OrderPriority = 7;
            kontakt.PageTemplate = contentTemplate;
            kontakt.Create();

            //ExclusiveReality.Models.Page vyhledavani = new ExclusiveReality.Models.Page(root, "Vyhled�v�n�", "vyhledavani", "<p>Vyhled�v�n�.</p>");
            //vyhledavani.Published = true;
            //vyhledavani.OrderPriority = 8;
            //vyhledavani.PageTemplate = contentTemplate;
            //vyhledavani.Create();

            ExclusiveReality.Models.Page mapa = new ExclusiveReality.Models.Page(root, "Mapa webu", "mapa-webu", "<p>Mapa webu</p>");
            mapa.Published = true;
            mapa.OrderPriority = 9;
            mapa.PageTemplate = contentTemplate;
            mapa.Create();






            ExclusiveReality.Models.Section nabidka = new ExclusiveReality.Models.Section(root, "Nab�dka nemovitost�", "nabidka");
            nabidka.OrderPriority = 1;
            nabidka.CreateIndexPage = false;
            nabidka.Published = true;
            nabidka.Create();


            ExclusiveReality.Models.Page nabidkaIndex = new ExclusiveReality.Models.Page(nabidka, "Nab�dka nemovitost�", "index", "<p>Na�e nab�dky...</p>");
            nabidkaIndex.Published = true;
            nabidkaIndex.OrderPriority = 0;
            nabidkaIndex.PageTemplate = contentEstatesTemplate;
            nabidkaIndex.Create();





            ExclusiveReality.Models.Section prodej = new ExclusiveReality.Models.Section(nabidka, "Prodej", "prodej");
            prodej.OrderPriority = 1;
            prodej.Published = true;
            prodej.Create();

            ExclusiveReality.Models.Page prodejByty = new ExclusiveReality.Models.Page(prodej, "Byty", "byty", String.Empty);
            prodejByty.Published = true;
            prodejByty.OrderPriority = 1;
            prodejByty.PageTemplate = flatsSaleTemplate;
            prodejByty.Create();

            ExclusiveReality.Models.Page prodejDomy = new ExclusiveReality.Models.Page(prodej, "Rodinn� domy", "rodinne-domy", String.Empty);
            prodejDomy.Published = true;
            prodejDomy.OrderPriority = 2;
            prodejDomy.PageTemplate = rdSaleTemplate;
            prodejDomy.Create();

            ExclusiveReality.Models.Page prodejCinzovniDomy = new ExclusiveReality.Models.Page(prodej, "�in�ovn� domy", "cinzovni-domy", String.Empty);
            prodejCinzovniDomy.Published = true;
            prodejCinzovniDomy.OrderPriority = 3;
            prodejCinzovniDomy.PageTemplate = flatsHousesSaleTemplate;
            prodejCinzovniDomy.Create();

            ExclusiveReality.Models.Page prodejNebytoveProstory = new ExclusiveReality.Models.Page(prodej, "Nebytov� prostory", "nebytove-prostory", String.Empty);
            prodejNebytoveProstory.Published = true;
            prodejNebytoveProstory.OrderPriority = 4;
            prodejNebytoveProstory.PageTemplate = noLivePlacesSaleTemplate;
            prodejNebytoveProstory.Create();

            ExclusiveReality.Models.Page prodejPozemky = new ExclusiveReality.Models.Page(prodej, "Pozemky", "pozemky", String.Empty);
            prodejPozemky.Published = true;
            prodejPozemky.OrderPriority = 5;
            prodejPozemky.PageTemplate = estatePropertiesSaleTemplate;
            prodejPozemky.Create();

            ExclusiveReality.Models.Page prodejKomercniProstory = new ExclusiveReality.Models.Page(prodej, "Komer�n� prostory", "komercni-prostory", String.Empty);
            prodejKomercniProstory.Published = true;
            prodejKomercniProstory.OrderPriority = 6;
            prodejKomercniProstory.PageTemplate = koSaleTemplate;
            prodejKomercniProstory.Create();

            ExclusiveReality.Models.Page prodejRekreacniObjekty = new ExclusiveReality.Models.Page(prodej, "Rekrea�n� objekty", "rekreacni-objekty", String.Empty);
            prodejRekreacniObjekty.Published = true;
            prodejRekreacniObjekty.OrderPriority = 7;
            prodejRekreacniObjekty.PageTemplate = recreationObjectsSaleTemplate;
            prodejRekreacniObjekty.Create();

            ExclusiveReality.Models.Page prodejHotely = new ExclusiveReality.Models.Page(prodej, "Hotely, penziony", "hotely-penziony", String.Empty);
            prodejHotely.Published = true;
            prodejHotely.OrderPriority = 8;
            prodejHotely.PageTemplate = hotelsSaleTemplate;
            prodejHotely.Create();





            ExclusiveReality.Models.Section pronajem = new ExclusiveReality.Models.Section(nabidka, "Pron�jem", "pronajem");
            pronajem.OrderPriority = 2;
            pronajem.Published = true;
            pronajem.Create();

            ExclusiveReality.Models.Page pronajemByty = new ExclusiveReality.Models.Page(pronajem, "Byty", "byty", String.Empty);
            pronajemByty.Published = true;
            pronajemByty.OrderPriority = 1;
            pronajemByty.PageTemplate = flatsRentTemplate;
            pronajemByty.Create();

            ExclusiveReality.Models.Page pronajemDomy = new ExclusiveReality.Models.Page(pronajem, "Rodinn� domy", "rodinne-domy", String.Empty);
            pronajemDomy.Published = true;
            pronajemDomy.OrderPriority = 2;
            pronajemDomy.PageTemplate = rdRentTemplate;
            pronajemDomy.Create();

            ExclusiveReality.Models.Page pronajemCinzovniDomy = new ExclusiveReality.Models.Page(pronajem, "�in�ovn� domy", "cinzovni-domy", String.Empty);
            pronajemCinzovniDomy.Published = true;
            pronajemCinzovniDomy.OrderPriority = 3;
            pronajemCinzovniDomy.PageTemplate = flatsHousesRentTemplate;
            pronajemCinzovniDomy.Create();

            ExclusiveReality.Models.Page pronajemNebytoveProstory = new ExclusiveReality.Models.Page(pronajem, "Nebytov� prostory", "nebytove-prostory", String.Empty);
            pronajemNebytoveProstory.Published = true;
            pronajemNebytoveProstory.OrderPriority = 4;
            pronajemNebytoveProstory.PageTemplate = noLivePlacesRentTemplate;
            pronajemNebytoveProstory.Create();

            ExclusiveReality.Models.Page pronajemPozemky = new ExclusiveReality.Models.Page(pronajem, "Pozemky", "pozemky", String.Empty);
            pronajemPozemky.Published = true;
            pronajemPozemky.OrderPriority = 5;
            pronajemPozemky.PageTemplate = estatePropertiesRentTemplate;
            pronajemPozemky.Create();

            ExclusiveReality.Models.Page pronajemKomercniProstory = new ExclusiveReality.Models.Page(pronajem, "Komer�n� prostory", "komercni-prostory", String.Empty);
            pronajemKomercniProstory.Published = true;
            pronajemKomercniProstory.OrderPriority = 6;
            pronajemKomercniProstory.PageTemplate = koRentTemplate;
            pronajemKomercniProstory.Create();

            ExclusiveReality.Models.Page pronajemRekreacniObjekty = new ExclusiveReality.Models.Page(pronajem, "Rekrea�n� objekty", "rekreacni-objekty", String.Empty);
            pronajemRekreacniObjekty.Published = true;
            pronajemRekreacniObjekty.OrderPriority = 7;
            pronajemRekreacniObjekty.PageTemplate = recreationObjectsRentTemplate;
            pronajemRekreacniObjekty.Create();

            ExclusiveReality.Models.Page pronajemHotely = new ExclusiveReality.Models.Page(pronajem, "Hotely, penziony", "hotely-penziony", String.Empty);
            pronajemHotely.Published = true;
            pronajemHotely.OrderPriority = 8;
            pronajemHotely.PageTemplate = hotelsRentTemplate;
            pronajemHotely.Create();





            ExclusiveReality.Models.Section zahranicniNemovitosti = new ExclusiveReality.Models.Section(nabidka, "Zahrani�n� nemovitosti", "zahranicni-nemovitosti");
            zahranicniNemovitosti.OrderPriority = 3;
            zahranicniNemovitosti.Published = true;
            zahranicniNemovitosti.Create();








            ////////ExclusiveReality.Models.Section secEN = new ExclusiveReality.Models.Section(root, "EN", "en");
            ////////secEN.OrderPriority = 8;
            ////////secEN.Culture = cultureEn;
            ////////secEN.CreateIndexPage = false;
            ////////secEN.Create();


            ////////ExclusiveReality.Models.Page HomePage = new ExclusiveReality.Models.Page(secEN, "Home Page", "index", "<p>HP EN EN EN lorem ipsum. Lorem ipsum. Lorem ipsum. Lorem ipsum. Lorem ipsum. Lorem ipsum.</p>");
            ////////HomePage.Published = true;
            ////////HomePage.OrderPriority = 0;
            ////////HomePage.PageTemplate = contentTemplate;
            ////////HomePage.Create();
            ////////HomePage.SetConnectedPage(domu);

            ////////ExclusiveReality.Models.Page services = new ExclusiveReality.Models.Page(secEN, "Our services", "services", "<p>Our services.</p>");
            ////////services.Published = true;
            ////////services.OrderPriority = 2;
            ////////services.PageTemplate = contentTemplate;
            ////////services.Create();
            ////////services.SetConnectedPage(sluzby);

            ////////ExclusiveReality.Models.Page faq = new ExclusiveReality.Models.Page(secEN, "FAQ", "faq", "<p>FAQ.</p>");
            ////////faq.Published = true;
            ////////faq.OrderPriority = 3;
            ////////faq.PageTemplate = contentTemplate;
            ////////faq.Create();
            ////////faq.SetConnectedPage(poradna);

            ////////ExclusiveReality.Models.Page aboutus = new ExclusiveReality.Models.Page(secEN, "About us", "about-us", "<p>About us.</p>");
            ////////aboutus.Published = true;
            ////////aboutus.OrderPriority = 4;
            ////////aboutus.PageTemplate = contentTemplate;
            ////////aboutus.Create();
            ////////aboutus.SetConnectedPage(onas);

            ////////ExclusiveReality.Models.Page contact = new ExclusiveReality.Models.Page(secEN, "Contact", "contact", "<p>Contact.</p>");
            ////////contact.Published = true;
            ////////contact.OrderPriority = 5;
            ////////contact.PageTemplate = contentTemplate;
            ////////contact.Create();
            ////////contact.SetConnectedPage(kontakt);

            ////////ExclusiveReality.Models.Page search = new ExclusiveReality.Models.Page(secEN, "Search", "search", "<p>Search.</p>");
            ////////search.Published = true;
            ////////search.OrderPriority = 6;
            ////////search.PageTemplate = contentTemplate;
            ////////search.Create();
            ////////search.SetConnectedPage(vyhledavani);

            ////////ExclusiveReality.Models.Page sitemap = new ExclusiveReality.Models.Page(secEN, "Sitemap", "sitemap", "<p>Sitemap</p>");
            ////////sitemap.Published = true;
            ////////sitemap.OrderPriority = 7;
            ////////sitemap.PageTemplate = contentTemplate;
            ////////sitemap.Create();
            ////////sitemap.SetConnectedPage(mapa);

            ////////ExclusiveReality.Models.Section real_estates = new ExclusiveReality.Models.Section(secEN, "Real estates", "real-estates");
            ////////real_estates.OrderPriority = 1;
            ////////real_estates.CreateIndexPage = false;
            ////////real_estates.Create();

            ////////ExclusiveReality.Models.Page real_estatesIndex = new ExclusiveReality.Models.Page(real_estates, "Real estates", "index", "<p>Our offer...</p>");
            ////////real_estatesIndex.Published = true;
            ////////real_estatesIndex.OrderPriority = 0;
            ////////real_estatesIndex.PageTemplate = contentEstatesTemplate;
            ////////real_estatesIndex.Create();

            ////////ExclusiveReality.Models.Page offerEstates = new ExclusiveReality.Models.Page(real_estates, "Estates projects", "flats-projects", "<p>Estates projects.</p>");
            ////////offerEstates.Published = true;
            ////////offerEstates.OrderPriority = 1;
            ////////offerEstates.PageTemplate = flatsProjectsTemplate;
            ////////offerEstates.Create();

            ////////offerEstates = new ExclusiveReality.Models.Page(real_estates, "Estates - sale", "flats-sale", "<p>Estates - sale.</p>");
            ////////offerEstates.Published = true;
            ////////offerEstates.OrderPriority = 2;
            ////////offerEstates.PageTemplate = flatsSaleTemplate;
            ////////offerEstates.Create();

            ////////offerEstates = new ExclusiveReality.Models.Page(real_estates, "Estates - rent", "flats-rent", "<p>Estates - rent.</p>");
            ////////offerEstates.Published = true;
            ////////offerEstates.OrderPriority = 3;
            ////////offerEstates.PageTemplate = flatsRentTemplate;
            ////////offerEstates.Create();



            ////////if (real_estates.Pages.Count > 0 && nabidka.Pages.Count > 0)
            ////////    real_estates.Pages[0].SetConnectedPage(nabidka.Pages[0]);
            #endregion


            #region brokers
            EstateManCard broker1 = new EstateManCard();
            broker1.FirstName = "Emil";
            broker1.LastName = "SURENAME";
            broker1.Email = "info@email.cz";
            broker1.Mobil = "00420111222333";
            broker1.Create();
            #endregion


            #region companies
            CompanyInfo dev1 = new CompanyInfo();
            dev1.FirmName = "EmilSURENAME";
            dev1.Street = "Streat";
            dev1.City = "Praha";
            dev1.Zip = "110 00";
            dev1.State = "Czech Republic";
            dev1.Www = "www.company.cz";
            dev1.Email = "info@email.cz";
            dev1.Telephone = "00420111222333";
            dev1.Create();
            #endregion


            #region projects
            EstateAddressInfo project1BasicInfo = new EstateAddressInfo();
            project1BasicInfo.Street = "Havlova";
            project1BasicInfo.City = "Praha";
            project1BasicInfo.Zip = "10000";

            ProjectNextInfo project1NextInfo = new ProjectNextInfo();
            project1NextInfo.BuildStartDate = "2006";
            project1NextInfo.BuildEndDate = "2008";

            DeveloperProject project1 = new DeveloperProject();
            project1.EstateManCard = broker1;
            project1.EstateAddressInfo = project1BasicInfo;
            project1.ProjectNextInfo = project1NextInfo;
            project1.Publish = true;
            project1.Create();

            DeveloperProjectCulture p1CZ = new DeveloperProjectCulture();
            p1CZ.DeveloperProject = project1;
            p1CZ.Culture = cultureCz;
            p1CZ.Name = "Bytov� d�m hr�de�ek";
            p1CZ.BasicDescription = "Kr�tk� popis....";
            p1CZ.FullDescription = "Dlouh� popis....";
            p1CZ.LocalityDescription = "kr�sn� p��roda a dostupnost v�echo :).";
            p1CZ.Parking = "Soukrom� parkov�n� pro 40 aut.";
            p1CZ.Create();

            DeveloperProjectCulture p1EN = new DeveloperProjectCulture();
            p1EN.DeveloperProject = project1;
            p1EN.Culture = cultureEn;
            p1EN.Name = "Block of flats";
            p1EN.BasicDescription = "Short description....";
            p1EN.FullDescription = "Long description....";
            p1EN.LocalityDescription = "All inclusive.";
            p1EN.Parking = "Private parking for 40 cars.";
            p1EN.Create();




            EstateAddressInfo project2BasicInfo = new EstateAddressInfo();
            project2BasicInfo.Street = "Hermitova 11";
            project2BasicInfo.City = "Praha";
            project2BasicInfo.Zip = "11000";

            ProjectNextInfo project2NextInfo = new ProjectNextInfo();
            project2NextInfo.BuildStartDate = "2006";
            project2NextInfo.BuildEndDate = "2008";

            DeveloperProject project2 = new DeveloperProject();
            project2.EstateManCard = broker1;
            project2.EstateAddressInfo = project2BasicInfo;
            project2.ProjectNextInfo = project2NextInfo;
            project2.HotTip = true;
            project2.Publish = true;
            project2.Create();

            DeveloperProjectCulture p2CZ = new DeveloperProjectCulture();
            p2CZ.DeveloperProject = project2;
            p2CZ.Culture = cultureCz;
            p2CZ.Name = "Rodinn� domy";
            p2CZ.BasicDescription = "Kr�tk� popis....";
            p2CZ.FullDescription = "Dlouh� popis....";
            p2CZ.LocalityDescription = "kr�sn� p��roda a dostupnost v�echo :).";
            p2CZ.Parking = "Soukrom� st�n�.";
            p2CZ.Create();

            DeveloperProjectCulture p2EN = new DeveloperProjectCulture();
            p2EN.DeveloperProject = project2;
            p2EN.Culture = cultureEn;
            p2EN.Name = "Houses";
            p2EN.BasicDescription = "Short description....";
            p2EN.FullDescription = "Long description....";
            p2EN.LocalityDescription = "All inclusive.";
            p2EN.Parking = "Private parking.";
            p2EN.Create();
            #endregion


            #region actualities
            Actuality actuality1 = new Actuality();
            actuality1.Created = DateTime.Now;
            actuality1.Publish = true;
            actuality1.Create();

            ActualityCulture a1CZ = new ActualityCulture();
            a1CZ.Actuality = actuality1;
            a1CZ.Culture = cultureCz;
            a1CZ.Heading = "Aktualita 1";
            a1CZ.Perex = "Kr�tk� perex";
            a1CZ.Content = "<p>Dlouh� obsah...</p>";
            a1CZ.Create();

            ActualityCulture a1EN = new ActualityCulture();
            a1EN.Actuality = actuality1;
            a1EN.Culture = cultureEn;
            a1EN.Heading = "Actuality 1";
            a1EN.Perex = "Short perex";
            a1EN.Content = "<p>Long content...</p>";
            a1EN.Create();


            Actuality actuality2 = new Actuality();
            actuality2.Created = DateTime.Now;
            actuality2.Publish = true;
            actuality2.Create();

            ActualityCulture a2CZ = new ActualityCulture();
            a2CZ.Actuality = actuality2;
            a2CZ.Culture = cultureCz;
            a2CZ.Heading = "Aktualita 2";
            a2CZ.Perex = "Kr�tk� perex";
            a2CZ.Content = "<p>Dlouh� obsah...</p>";
            a2CZ.Create();

            ActualityCulture a2EN = new ActualityCulture();
            a2EN.Actuality = actuality2;
            a2EN.Culture = cultureEn;
            a2EN.Heading = "Actuality 2";
            a2EN.Perex = "Short perex";
            a2EN.Content = "<p>Long content...</p>";
            a2EN.Create();
            #endregion


            #region regions
            new Region("Bene�ov").Create();
            new Region("Beroun").Create();
            new Region("Blansko").Create();
            new Region("B�eclav").Create();
            new Region("Brno-m�sto").Create();
            new Region("Brno-venkov").Create();
            new Region("Brunt�l").Create();
            new Region("�esk� L�pa").Create();
            new Region("�esk� Bud�jovice").Create();
            new Region("�esk� Krumlov").Create();
            new Region("Cheb").Create();
            new Region("Chomutov").Create();
            new Region("Chrudim").Create();
            new Region("D���n").Create();
            new Region("Doma�lice").Create();
            new Region("Fr�dek-M�stek").Create();
            new Region("Havl��k�v Brod").Create();
            new Region("Hodon�n").Create();
            new Region("Hradec Kr�lov�").Create();
            new Region("Jablonec nad Nisou").Create();
            new Region("Jesen�k").Create();
            new Region("Ji��n").Create();
            new Region("Jihlava").Create();
            new Region("Jind�ich�v Hradec").Create();
            new Region("Karlovy Vary").Create();
            new Region("Karvin�").Create();
            new Region("Kladno").Create();
            new Region("Klatovy").Create();
            new Region("Kol�n").Create();
            new Region("Krom���").Create();
            new Region("Kutn� Hora").Create();
            new Region("Liberec").Create();
            new Region("Litom��ice").Create();
            new Region("Louny").Create();
            new Region("M�ln�k").Create();
            new Region("Mlad� Boleslav").Create();
            new Region("Most").Create();
            new Region("N�chod").Create();
            new Region("Nov� Ji��n").Create();
            new Region("Nymburk").Create();
            new Region("Olomouc").Create();
            new Region("Opava").Create();
            new Region("Ostrava-m�sto").Create();
            new Region("Pardubice").Create();
            new Region("Pelh�imov").Create();
            new Region("P�sek").Create();
            new Region("Plze�-jih").Create();
            new Region("Plze�-m�sto").Create();
            new Region("Plze�-sever").Create();
            new Region("Prachatice").Create();
            new Region("Praha").Create();
            new Region("Praha 1").Create();
            new Region("Praha 10").Create();
            new Region("Praha 11").Create();
            new Region("Praha 12").Create();
            new Region("Praha 13").Create();
            new Region("Praha 14").Create();
            new Region("Praha 15").Create();
            new Region("Praha 16").Create();
            new Region("Praha 17").Create();
            new Region("Praha 18").Create();
            new Region("Praha 19").Create();
            new Region("Praha 2").Create();
            new Region("Praha 20").Create();
            new Region("Praha 21").Create();
            new Region("Praha 22").Create();
            new Region("Praha 3").Create();
            new Region("Praha 4").Create();
            new Region("Praha 5").Create();
            new Region("Praha 6").Create();
            new Region("Praha 7").Create();
            new Region("Praha 8").Create();
            new Region("Praha 9").Create();
            new Region("Praha-B�chovice").Create();
            new Region("Praha-Benice").Create();
            new Region("Praha-B�ezin�ves").Create();
            new Region("Praha-�akovice").Create();
            new Region("Praha-��blice").Create();
            new Region("Praha-Doln� Chabry").Create();
            new Region("Praha-Doln� M�cholupy").Create();
            new Region("Praha-Doln� Po�ernice").Create();
            new Region("Praha-Dube�").Create();
            new Region("Praha-Kl�novice").Create();
            new Region("Praha-Kolod�je").Create();
            new Region("Praha-Kolovraty").Create();
            new Region("Praha-Kr�lovice").Create();
            new Region("Praha-K�eslice").Create();
            new Region("Praha-Kunratice").Create();
            new Region("Praha-Libu�").Create();
            new Region("Praha-Lipence").Create();
            new Region("Praha-Lochkov").Create();
            new Region("Praha-Lysolaje").Create();
            new Region("Praha-Nebu�ice").Create();
            new Region("Praha-Nedv�z�").Create();
            new Region("Praha-Petrovice").Create();
            new Region("Praha-P�edn� Kopanina").Create();
            new Region("Praha-�eporyje").Create();
            new Region("Praha-Satalice").Create();
            new Region("Praha-�eberov").Create();
            new Region("Praha-Slivenec").Create();
            new Region("Praha-�t�rboholy").Create();
            new Region("Praha-Suchdol").Create();
            new Region("Praha-Troja").Create();
            new Region("Praha-�jezd").Create();
            new Region("Praha-Velk� Chuchle").Create();
            new Region("Praha-Vino�").Create();
            new Region("Praha-v�chod").Create();
            new Region("Praha-z�pad").Create();
            new Region("Praha-Zbraslav").Create();
            new Region("Praha-Zli��n").Create();
            new Region("P�erov").Create();
            new Region("P��bram").Create();
            new Region("Prost�jov").Create();
            new Region("Rakovn�k").Create();
            new Region("Rokycany").Create();
            new Region("Rychnov nad Kn�nou").Create();
            new Region("Semily").Create();
            new Region("Sokolov").Create();
            new Region("Strakonice").Create();
            new Region("�umperk").Create();
            new Region("Svitavy").Create();
            new Region("T�bor").Create();
            new Region("Tachov").Create();
            new Region("Teplice").Create();
            new Region("T�eb��").Create();
            new Region("Trutnov").Create();
            new Region("Uhersk� Hradi�t�").Create();
            new Region("�st� nad Labem").Create();
            new Region("�st� nad Orlic�").Create();
            new Region("Vset�n").Create();
            new Region("Vy�kov").Create();
            new Region("���r nad S�zavou").Create();
            new Region("Zl�n").Create();
            new Region("Znojmo").Create();
            #endregion


            #region estate types
            EstateType estateType = new EstateType();
            estateType.Create();

            EstateTypeCulture etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "Byty";
            etCZ.Create();

            EstateTypeCulture etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "Flats";
            etEN.Create();


            estateType = new EstateType();
            estateType.Create();

            etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "Rodinn� domy";
            etCZ.Create();

            etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "Houses";
            etEN.Create();


            estateType = new EstateType();
            estateType.Create();

            etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "�in�ovn� domy";
            etCZ.Create();

            etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "Flats houses";
            etEN.Create();


            estateType = new EstateType();
            estateType.Create();

            etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "Nebytov� prostory";
            etCZ.Create();

            etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "No living spaces";
            etEN.Create();


            estateType = new EstateType();
            estateType.Create();

            etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "Pozemky";
            etCZ.Create();

            etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "Propertis";
            etEN.Create();


            estateType = new EstateType();
            estateType.Create();

            etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "Komer�n� prostory";
            etCZ.Create();

            etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "Commercial spaces";
            etEN.Create();


            estateType = new EstateType();
            estateType.Create();

            etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "Rekrea�n� objekty";
            etCZ.Create();

            etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "Recreation objects";
            etEN.Create();


            estateType = new EstateType();
            estateType.Create();

            etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "Hotely, penziony";
            etCZ.Create();

            etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "Hotels, pensions";
            etEN.Create();
            #endregion


            #region estate offer types
            EstateOfferType estateOfferType = new EstateOfferType();
            estateOfferType.Create();

            EstateOfferTypeCulture eotCZ = new EstateOfferTypeCulture(cultureCz);
            eotCZ.EstateOfferType = estateOfferType;
            eotCZ.Name = "Prodej";
            eotCZ.Create();

            EstateOfferTypeCulture eotEN = new EstateOfferTypeCulture(cultureEn);
            eotEN.EstateOfferType = estateOfferType;
            eotEN.Name = "Sale";
            eotEN.Create();


            estateOfferType = new EstateOfferType();
            estateOfferType.Create();

            eotCZ = new EstateOfferTypeCulture(cultureCz);
            eotCZ.EstateOfferType = estateOfferType;
            eotCZ.Name = "Pron�jem";
            eotCZ.Create();

            eotEN = new EstateOfferTypeCulture(cultureEn);
            eotEN.EstateOfferType = estateOfferType;
            eotEN.Name = "Rent";
            eotEN.Create();
            #endregion


            #region currency types
            new CurrencyType("CZK").Create();
            new CurrencyType("EUR").Create();
            new CurrencyType("USD").Create();
            #endregion

            
            #region price types
            PriceType priceType = new PriceType();
            priceType.Create();

            PriceTypeCulture ptcCZ = new PriceTypeCulture(cultureCz);
            ptcCZ.PriceType = priceType;
            ptcCZ.Name = "za m�s�c";
            ptcCZ.Create();

            PriceTypeCulture ptcEN = new PriceTypeCulture(cultureEn);
            ptcEN.PriceType = priceType;
            ptcEN.Name = "per month";
            ptcEN.Create();


            priceType = new PriceType();
            priceType.Create();

            ptcCZ = new PriceTypeCulture(cultureCz);
            ptcCZ.PriceType = priceType;
            ptcCZ.Name = "za m<sup>2</sup>/m�s�c";
            ptcCZ.Create();

            ptcEN = new PriceTypeCulture(cultureEn);
            ptcEN.PriceType = priceType;
            ptcEN.Name = "per m<sup>2</sup>/month";
            ptcEN.Create();


            priceType = new PriceType();
            priceType.Create();

            ptcCZ = new PriceTypeCulture(cultureCz);
            ptcCZ.PriceType = priceType;
            ptcCZ.Name = "za m<sup>2</sup>/rok";
            ptcCZ.Create();

            ptcEN = new PriceTypeCulture(cultureEn);
            ptcEN.PriceType = priceType;
            ptcEN.Name = "per m<sup>2</sup>/year";
            ptcEN.Create();


            priceType = new PriceType();
            priceType.Create();

            ptcCZ = new PriceTypeCulture(cultureCz);
            ptcCZ.PriceType = priceType;
            ptcCZ.Name = "za rok";
            ptcCZ.Create();

            ptcEN = new PriceTypeCulture(cultureEn);
            ptcEN.PriceType = priceType;
            ptcEN.Name = "per year";
            ptcEN.Create();


            priceType = new PriceType();
            priceType.Create();

            ptcCZ = new PriceTypeCulture(cultureCz);
            ptcCZ.PriceType = priceType;
            ptcCZ.Name = "za den";
            ptcCZ.Create();

            ptcEN = new PriceTypeCulture(cultureEn);
            ptcEN.PriceType = priceType;
            ptcEN.Name = "per day";
            ptcEN.Create();
            #endregion


            #region estate1
            EstateExtendedInfo estateExtendedInfo1 = new EstateExtendedInfo();
            estateExtendedInfo1.OrderNumber = "K 111";

            EstatePriceInfo pInfo = new EstatePriceInfo();
            pInfo.PriceValue = 4990000;
            //pInfo.CurrencyType = "CZK";

            EstateAddressInfo aInfo = new EstateAddressInfo();

            Estate estate1 = new Estate();
            estate1.EstateType = EstateType.GetById(2);
            estate1.EstateOfferType = EstateOfferType.GetById(1);
            estate1.EstateExtendedInfo = estateExtendedInfo1;
            estate1.EstateManCard = broker1;
            estate1.EstatePriceInfo = pInfo;
            estate1.EstateAddressInfo = aInfo;
            estate1.HotTip = true;
            estate1.Publish = true;


            EstateProperties estateEstateProperties1 = new EstateProperties();
            estateEstateProperties1.Estate = estate1;
            estateEstateProperties1.Create();

            EstatePropertiesCulture efp1CZ = new EstatePropertiesCulture();
            efp1CZ.EstateProperties = estateEstateProperties1;
            efp1CZ.Culture = cultureCz;
            efp1CZ.Create();

            EstatePropertiesCulture efp1EN = new EstatePropertiesCulture();
            efp1EN.EstateProperties = estateEstateProperties1;
            efp1EN.Culture = cultureEn;
            efp1EN.Create();


            EstateCulture e1CZ = new EstateCulture();
            e1CZ.Estate = estate1;
            e1CZ.Culture = cultureCz;
            e1CZ.Name = "Prodej nadstandardn�  novostavby �RD 4+KK/GAR��  NEHVIZDY";
            e1CZ.BasicDescription = "Nab�z�me v obci NEHVIZDY k prodeji patrov� �adov� d�m situov�n jako 4+kk  se zahradou o v�m��e 88m2 a obytn� plochy s gar�� 123m2,  zastav�n� o v�m��e 177m2.";
            e1CZ.FullDescription = "D�m je ve velice dobr�m stavu, ur�en k okam�it�mu nast�hov�n� a bydlen�. Interi�rov� ��st domu je vybavena luxusn�mi prvky v�etn� st. materi�lu, dubov� podlahov� krytina, Italsk� dla�by, kuchy�sk� linka s vest. spot�ebi�i, alarm, internet, satelitn� TV, plynov� et�. topen� a pod. Kolaudov�n byl v roce 2006. Loka�n� je vsazen do klidov� �tvrti �adov�ch dom� v t�to obci. D�m doporu�ujeme!! V p��pad� z�jmu o n�kup nemovitosti formou hypote�n�ho �v�ru je zaji�t�no poskytnut� financov�n� na tento d�m od �SOB a.s. nebo HYPOTE�N� BANKY a.s..";
            e1CZ.AdditionalDescription = "V p��pad� z�jmu o prohl�dku t�to nemovitosti �i bli���ch informac� k domu volejte : 603784314, pro informace k bankovn� �i pr�vn� metodice koup� tohoto domu volejte : 608963665.\n"+
                                                        "Cena 4.990 000.-K�";
            e1CZ.Create();

            EstateCulture e1EN = new EstateCulture();
            e1EN.Estate = estate1;
            e1EN.Culture = cultureEn;
            e1EN.Name = "Prodej nadstandardn�  novostavby �RD 4+KK/GAR��  NEHVIZDY";
            e1EN.BasicDescription = "Nab�z�me v obci NEHVIZDY k prodeji patrov� �adov� d�m situov�n jako 4+kk  se zahradou o v�m��e 88m2 a obytn� plochy s gar�� 123m2,  zastav�n� o v�m��e 177m2.";
            e1EN.FullDescription = "D�m je ve velice dobr�m stavu, ur�en k okam�it�mu nast�hov�n� a bydlen�. Interi�rov� ��st domu je vybavena luxusn�mi prvky v�etn� st. materi�lu, dubov� podlahov� krytina, Italsk� dla�by, kuchy�sk� linka s vest. spot�ebi�i, alarm, internet, satelitn� TV, plynov� et�. topen� a pod. Kolaudov�n byl v roce 2006. Loka�n� je vsazen do klidov� �tvrti �adov�ch dom� v t�to obci. D�m doporu�ujeme!! V p��pad� z�jmu o n�kup nemovitosti formou hypote�n�ho �v�ru je zaji�t�no poskytnut� financov�n� na tento d�m od �SOB a.s. nebo HYPOTE�N� BANKY a.s..";
            e1EN.AdditionalDescription = "V p��pad� z�jmu o prohl�dku t�to nemovitosti �i bli���ch informac� k domu volejte : 603784314, pro informace k bankovn� �i pr�vn� metodice koup� tohoto domu volejte : 608963665.\n" +
                                                        "Cena 4.990 000.-K�";
            e1EN.Create();

            EstateImage image = new EstateImage(estate1, "/img/Estates/" + estate1.Id + "_photo1", "Bla", "Bla bla");
            image.IsMain = true;
            image.Create();
            #endregion

            #region estate2
            EstateExtendedInfo estateExtendedInfo2 = new EstateExtendedInfo();
            estateExtendedInfo2.OrderNumber = "K 110";

            Estate estate2 = new Estate();
            estate2.EstateType = EstateType.GetById(7);
            estate2.EstateOfferType = EstateOfferType.GetById(1);
            estate2.EstateExtendedInfo = estateExtendedInfo2;
            estate2.EstateManCard = broker1;
            estate2.HotTip = false;
            estate2.Publish = true;


            EstateProperties estateEstateProperties2 = new EstateProperties();
            estateEstateProperties2.Estate = estate2;
            estateEstateProperties2.Create();

            EstatePropertiesCulture efp2CZ = new EstatePropertiesCulture();
            efp2CZ.EstateProperties = estateEstateProperties2;
            efp2CZ.Culture = cultureCz;
            efp2CZ.Create();

            EstatePropertiesCulture efp2EN = new EstatePropertiesCulture();
            efp2EN.EstateProperties = estateEstateProperties2;
            efp2EN.Culture = cultureEn;
            efp2EN.Create();


            EstateCulture e2CZ = new EstateCulture();
            e2CZ.Estate = estate2;
            e2CZ.Culture = cultureCz;
            e2CZ.Name = "Semily, Horn� Rokytnice ( Rokytnice nad Jizerou)";
            e2CZ.BasicDescription = "Kr�sn� rouben� chalupa v chr�n�n�m �zem� KRNAP\n" +
                                                "\n" +
                                                "Rouben� chalupa po celkov� rekonstrukci v r.1998, zastav�n� plocha\n"+
                                                "181m2, vedlej�� stavby - stodola, zastav�n� plocha 199 m2( stavebn� parcela) + k�lna + d�evn�k.";
            e2CZ.FullDescription = "Chalupa - v p��zem� 2 velk� m�stnosti( 1- zachov�n starod�vn� krkono�sk� r�z, v�etn� n�bytku a kachlov�ch kamen atd.,2 - novodob� r�z s krbem a mal�m barem) z t�to m�stnosti v�chod na velkou terasu, koupelna, WC, komory, ve velk�m podkrov� za��zena 1 m�stnost - mo�nost p�dn� dostavby.V chalup� zavedena voda a elekt�ina.";
            e2CZ.AdditionalDescription = "K chalup� pat�� kr�sn� ovocn� zahrada, kterou prot�k� mal� pot��ek, vlastn� pozemky o celkov� rozloze cca 49 000 m2, v�e v kr�sn�m a klidn�m prost�ed� uprost�ed krkono�sk�ch hor, ly�a�sk� vlek p��mo v obci. Chalupa se nach�z� v obc� Franti�kov - p��stup naprosto bezprobl�mov� v ka�d�m ro�n�m obdoby. Pozemky jsou vedeny jako louka a les s povolen�m k�cet.\n"+
                                                        "\n" +
                                                        "Cena 4.990 000.-K�";
            e2CZ.Create();

            EstateCulture e2EN = new EstateCulture();
            e2EN.Estate = estate2;
            e2EN.Culture = cultureEn;
            e2EN.Name = "Semily, Horn� Rokytnice ( Rokytnice nad Jizerou)";
            e2EN.BasicDescription = "Kr�sn� rouben� chalupa v chr�n�n�m �zem� KRNAP\n" +
                                                "\n" +
                                                "Rouben� chalupa po celkov� rekonstrukci v r.1998, zastav�n� plocha\n" +
                                                "181m2, vedlej�� stavby - stodola, zastav�n� plocha 199 m2( stavebn� parcela) + k�lna + d�evn�k.";
            e2EN.FullDescription = "Chalupa - v p��zem� 2 velk� m�stnosti( 1- zachov�n starod�vn� krkono�sk� r�z, v�etn� n�bytku a kachlov�ch kamen atd.,2 - novodob� r�z s krbem a mal�m barem) z t�to m�stnosti v�chod na velkou terasu, koupelna, WC, komory, ve velk�m podkrov� za��zena 1 m�stnost - mo�nost p�dn� dostavby.V chalup� zavedena voda a elekt�ina.";
            e2EN.AdditionalDescription = "K chalup� pat�� kr�sn� ovocn� zahrada, kterou prot�k� mal� pot��ek, vlastn� pozemky o celkov� rozloze cca 49 000 m2, v�e v kr�sn�m a klidn�m prost�ed� uprost�ed krkono�sk�ch hor, ly�a�sk� vlek p��mo v obci. Chalupa se nach�z� v obc� Franti�kov - p��stup naprosto bezprobl�mov� v ka�d�m ro�n�m obdoby. Pozemky jsou vedeny jako louka a les s povolen�m k�cet.\n" +
                                                        "\n" +
                                                        "Cena 4.990 000.-K�";
            e2EN.Create();
            #endregion

            #region estate3
            EstateExtendedInfo estateExtendedInfo3 = new EstateExtendedInfo();
            estateExtendedInfo3.OrderNumber = "K 009";

            Estate estate3 = new Estate();
            estate3.EstateType = EstateType.GetById(8);
            estate3.EstateOfferType = EstateOfferType.GetById(1);
            estate3.EstateExtendedInfo = estateExtendedInfo3;
            estate3.EstateManCard = broker1;
            estate3.HotTip = false;
            estate3.Publish = true;


            EstateProperties estateEstateProperties3 = new EstateProperties();
            estateEstateProperties3.Estate = estate3;
            estateEstateProperties3.Create();

            EstatePropertiesCulture efp3CZ = new EstatePropertiesCulture();
            efp3CZ.EstateProperties = estateEstateProperties2;
            efp3CZ.Culture = cultureCz;
            efp3CZ.Create();

            EstatePropertiesCulture efp3EN = new EstatePropertiesCulture();
            efp3EN.EstateProperties = estateEstateProperties2;
            efp3EN.Culture = cultureEn;
            efp3EN.Create();


            EstateCulture e3CZ = new EstateCulture();
            e3CZ.Estate = estate3;
            e3CZ.Culture = cultureCz;
            e3CZ.Name = "Prodej penzionu u vodn� n�dr�e Lipno.";
            e3CZ.BasicDescription = "Lipno nad Vltavou\n" +
                                                "\n" +
                                                "Objekt se nach�z� v nejkr�sn�j��m koutu �umavy, �irok� vyu�it� slu�eb, jak v l�t� tak i v zim�.";
            e3CZ.FullDescription = "Chalupa - v p��zem� 2 velk� m�stnosti( 1- zachov�n starod�vn� krkono�sk� r�z, v�etn� n�bytku a kachlov�ch kamen atd.,2 - novodob� r�z s krbem a mal�m barem) z t�to m�stnosti v�chod na velkou terasu, koupelna, WC, komory, ve velk�m podkrov� za��zena 1 m�stnost - mo�nost p�dn� dostavby.V chalup� zavedena voda a elekt�ina.";
            e3CZ.AdditionalDescription = "Jedn� se o dvou podla�� objekt vhodn� k rekonstrukci, d��ve slou��c� jako penzion v bl�zkosti vodn� n�dr�e Lipno, Ski are�lu a Aqua parku.. Na pozemku plyn, voda, elekt�ina a mo�nost p�ipojen� na obecn� kanalizaci cca 300m. Zastav�n� plocha 650m2, pozemek 4672m2. Vhodn� taky na apartm�ny, v�hodn� investice!\n" +
                                                        "\n" +
                                                        "Cena 8.290 000.-K�";
            e3CZ.Create();

            EstateCulture e3EN = new EstateCulture();
            e3EN.Estate = estate3;
            e3EN.Culture = cultureEn;
            e3EN.Name = "Prodej penzionu u vodn� n�dr�e Lipno.";
            e3EN.BasicDescription = "Lipno nad Vltavou\n" +
                                                "\n" +
                                                "Objekt se nach�z� v nejkr�sn�j��m koutu �umavy, �irok� vyu�it� slu�eb, jak v l�t� tak i v zim�.";
            e3EN.FullDescription = "Chalupa - v p��zem� 2 velk� m�stnosti( 1- zachov�n starod�vn� krkono�sk� r�z, v�etn� n�bytku a kachlov�ch kamen atd.,2 - novodob� r�z s krbem a mal�m barem) z t�to m�stnosti v�chod na velkou terasu, koupelna, WC, komory, ve velk�m podkrov� za��zena 1 m�stnost - mo�nost p�dn� dostavby.V chalup� zavedena voda a elekt�ina.";
            e3EN.AdditionalDescription = "Jedn� se o dvou podla�� objekt vhodn� k rekonstrukci, d��ve slou��c� jako penzion v bl�zkosti vodn� n�dr�e Lipno, Ski are�lu a Aqua parku.. Na pozemku plyn, voda, elekt�ina a mo�nost p�ipojen� na obecn� kanalizaci cca 300m. Zastav�n� plocha 650m2, pozemek 4672m2. Vhodn� taky na apartm�ny, v�hodn� investice!\n" +
                                                        "\n" +
                                                        "Cena 8.290 000.-K�";
            e3EN.Create();
            #endregion

            #region estate4
            EstateExtendedInfo estateExtendedInfo4 = new EstateExtendedInfo();
            estateExtendedInfo4.OrderNumber = "K 008";

            Estate estate4 = new Estate();
            estate4.EstateType = EstateType.GetById(1);
            estate4.EstateOfferType = EstateOfferType.GetById(2);
            estate4.EstateExtendedInfo = estateExtendedInfo4;
            estate4.EstateManCard = broker1;
            estate4.HotTip = false;
            estate4.Publish = true;


            EstateProperties estateEstateProperties4 = new EstateProperties();
            estateEstateProperties4.Estate = estate4;
            estateEstateProperties4.Create();

            EstatePropertiesCulture efp4CZ = new EstatePropertiesCulture();
            efp4CZ.EstateProperties = estateEstateProperties4;
            efp4CZ.Culture = cultureCz;
            efp4CZ.Create();

            EstatePropertiesCulture efp4EN = new EstatePropertiesCulture();
            efp4EN.EstateProperties = estateEstateProperties4;
            efp4EN.Culture = cultureEn;
            efp4EN.Create();


            EstateCulture e4CZ = new EstateCulture();
            e4CZ.Estate = estate4;
            e4CZ.Culture = cultureCz;
            e4CZ.Name = "Byt 3+1 75m2, Praha 6 �epy ul. �panielova";
            e4CZ.BasicDescription = "Praha 6 - �epy, ulice �panielova, nab�dka pron�jmu bytov� jednotky v dispozici 3+1+L, v panelov�m patrov�m bytov�m dom� s v�tahem, t�et� patro ze sedmi, 75m2, nepr�choz� pokoje, byt je vybaven pouze novou pln� funk�n� kuchy�skou linkou, plovouc� podlahy, jedn� se o prostorn� byt ve velice dobr�m stavu! Cena 12.000,- K� bez su�eb.";
            e4CZ.Create();

            EstateCulture e4EN = new EstateCulture();
            e4EN.Estate = estate4;
            e4EN.Culture = cultureEn;
            e4EN.Name = "Byt 3+1 75m2, Praha 6 �epy ul. �panielova";
            e4EN.BasicDescription = "Praha 6 - �epy, ulice �panielova, nab�dka pron�jmu bytov� jednotky v dispozici 3+1+L, v panelov�m patrov�m bytov�m dom� s v�tahem, t�et� patro ze sedmi, 75m2, nepr�choz� pokoje, byt je vybaven pouze novou pln� funk�n� kuchy�skou linkou, plovouc� podlahy, jedn� se o prostorn� byt ve velice dobr�m stavu! Cena 12.000,- K� bez su�eb.";
            e4EN.Create();
            #endregion

            #region estate5
            EstateExtendedInfo estateExtendedInfo5 = new EstateExtendedInfo();
            estateExtendedInfo5.OrderNumber = "K 007";

            Estate estate5 = new Estate();
            estate5.EstateType = EstateType.GetById(1);
            estate5.EstateOfferType = EstateOfferType.GetById(1);
            estate5.EstateExtendedInfo = estateExtendedInfo5;
            estate5.EstateManCard = broker1;
            estate5.HotTip = false;
            estate5.Publish = true;


            EstateProperties estateEstateProperties5 = new EstateProperties();
            estateEstateProperties5.Estate = estate5;
            estateEstateProperties5.Create();

            EstatePropertiesCulture efp5CZ = new EstatePropertiesCulture();
            efp5CZ.EstateProperties = estateEstateProperties2;
            efp5CZ.Culture = cultureCz;
            efp5CZ.Create();

            EstatePropertiesCulture efp5EN = new EstatePropertiesCulture();
            efp5EN.EstateProperties = estateEstateProperties2;
            efp5EN.Culture = cultureEn;
            efp5EN.Create();


            EstateCulture e5CZ = new EstateCulture();
            e5CZ.Estate = estate5;
            e5CZ.Culture = cultureCz;
            e5CZ.Name = "Prodej dru�stevn�ho bytu 3+1+lod�ie v Nymburce v ulici Bo�sk�.";
            e5CZ.BasicDescription = "Prodej dru�stevn�ho bytu 3+1+lod�ie v Nymburce v ulici Zbo�sk�, cca 80m2, sklep, v�tah, 4.patro, byt se nach�z� v panelov�m 7.patrov�m bytov�m dom�, k nast�hov�n� od 01/08.";
            e5CZ.AdditionalDescription = "Bytov� jednotka je v dobr�m p�vodn�m stavu.\n" +
                                                        "Financov�n� formou hypot�ky mo�n�!( zaji�t�no )\n" +
                                                        "Cena 1790000.-K�";
            e5CZ.Create();

            EstateCulture e5EN = new EstateCulture();
            e5EN.Estate = estate5;
            e5EN.Culture = cultureEn;
            e5EN.Name = "Prodej penzionu u vodn� n�dr�e Lipno.";
            e5EN.BasicDescription = "Lipno nad Vltavou\n" +
                                                "\n" +
                                                "Objekt se nach�z� v nejkr�sn�j��m koutu �umavy, �irok� vyu�it� slu�eb, jak v l�t� tak i v zim�.";
            e5EN.FullDescription = "Chalupa - v p��zem� 2 velk� m�stnosti( 1- zachov�n starod�vn� krkono�sk� r�z, v�etn� n�bytku a kachlov�ch kamen atd.,2 - novodob� r�z s krbem a mal�m barem) z t�to m�stnosti v�chod na velkou terasu, koupelna, WC, komory, ve velk�m podkrov� za��zena 1 m�stnost - mo�nost p�dn� dostavby.V chalup� zavedena voda a elekt�ina.";
            e5EN.AdditionalDescription = "Jedn� se o dvou podla�� objekt vhodn� k rekonstrukci, d��ve slou��c� jako penzion v bl�zkosti vodn� n�dr�e Lipno, Ski are�lu a Aqua parku.. Na pozemku plyn, voda, elekt�ina a mo�nost p�ipojen� na obecn� kanalizaci cca 300m. Zastav�n� plocha 650m2, pozemek 4672m2. Vhodn� taky na apartm�ny, v�hodn� investice!\n" +
                                                        "\n" +
                                                        "Cena 8.290 000.-K�";
            e5EN.Create();
            #endregion


            #region test
            //TestAddress address1 = new TestAddress();
            //address1.Street = "Ulice 123";
            //address1.City = "Praha";
            //TestAddressAnother address2 = new TestAddressAnother();
            //address2.Street = "Ulice 123";
            //address2.City = "Praha";

            //Test test1 = new Test();
            //test1.Name = "Test item";
            //test1.TestAddress = address1;
            //test1.TestAddressAnother = address2;
            //test1.Create();

            //TestPerson testPerson1 = new TestPerson(test1);
            //testPerson1.Name = "Richard";
            //testPerson1.Create();






            //Customer cus = new Customer();
            //cus.Name = "john doe";
            //cus.Save();

            //CustomerAddress cusAdd = new CustomerAddress();
            //cusAdd.Customer = cus;
            //cusAdd.Address = "Street";
            //cusAdd.Save();

            //CustomerHobby cusHobby = new CustomerHobby();
            //cusHobby.Customer = cus;
            //cusHobby.Hobby = "Pets";
            //cusHobby.Save();



            #endregion
        }
    }
}

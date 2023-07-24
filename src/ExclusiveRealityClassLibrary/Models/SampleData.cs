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
            contentTemplate.Title = "Obsahová šablona";
            contentTemplate.Create();

            ExclusiveReality.Models.PageTemplate indexTemplate = new ExclusiveReality.Models.PageTemplate();
            indexTemplate.Name = "indexpage";
            indexTemplate.Title = "Stránka s výpisem obsahu sekce";
            indexTemplate.Create();

            ExclusiveReality.Models.PageTemplate contentEstatesTemplate = new ExclusiveReality.Models.PageTemplate();
            contentEstatesTemplate.Name = "contentestatestemplate";
            contentEstatesTemplate.Title = "Obsahová šablona sekce nemovitostí";
            contentEstatesTemplate.Create();


            ExclusiveReality.Models.PageTemplate flatsProjectsTemplate = new ExclusiveReality.Models.PageTemplate();
            flatsProjectsTemplate.Name = "developerprojectspage";
            flatsProjectsTemplate.Title = "Šablona pro výpis bytových projektù";
            flatsProjectsTemplate.Create();


            ExclusiveReality.Models.PageTemplate flatsSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            flatsSaleTemplate.Name = "flatssalepage";
            flatsSaleTemplate.Title = "Šablona pro výpis prodeje bytù";
            flatsSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate flatsRentTemplate = new ExclusiveReality.Models.PageTemplate();
            flatsRentTemplate.Name = "flatsrentpage";
            flatsRentTemplate.Title = "Šablona pro výpis pronájmu bytù";
            flatsRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate rdSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            rdSaleTemplate.Name = "rdsalepage";
            rdSaleTemplate.Title = "Šablona pro výpis prodeje domù a vil";
            rdSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate rdRentTemplate = new ExclusiveReality.Models.PageTemplate();
            rdRentTemplate.Name = "rdrentpage";
            rdRentTemplate.Title = "Šablona pro výpis pronájmu domù a vil";
            rdRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate flatsHousesSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            flatsHousesSaleTemplate.Name = "flatshousessalepage";
            flatsHousesSaleTemplate.Title = "Šablona pro výpis prodeje èinžovních domù";
            flatsHousesSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate flatsHousesRentTemplate = new ExclusiveReality.Models.PageTemplate();
            flatsHousesRentTemplate.Name = "flatshousesrentpage";
            flatsHousesRentTemplate.Title = "Šablona pro výpis pronájmu èinžovních domù";
            flatsHousesRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate noLivePlacesSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            noLivePlacesSaleTemplate.Name = "noliveplacessalepage";
            noLivePlacesSaleTemplate.Title = "Šablona pro výpis prodeje nebytových prostor";
            noLivePlacesSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate noLivePlacesRentTemplate = new ExclusiveReality.Models.PageTemplate();
            noLivePlacesRentTemplate.Name = "noliveplacesrentpage";
            noLivePlacesRentTemplate.Title = "Šablona pro výpis pronájmu nebytových prostor";
            noLivePlacesRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate estatePropertiesSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            estatePropertiesSaleTemplate.Name = "estatepropertiessalepage";
            estatePropertiesSaleTemplate.Title = "Šablona pro výpis prodeje pozemkù";
            estatePropertiesSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate estatePropertiesRentTemplate = new ExclusiveReality.Models.PageTemplate();
            estatePropertiesRentTemplate.Name = "estatepropertiesrentpage";
            estatePropertiesRentTemplate.Title = "Šablona pro výpis pronájmu pozemkù";
            estatePropertiesRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate koSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            koSaleTemplate.Name = "kosalepage";
            koSaleTemplate.Title = "Šablona pro výpis prodeje komerèních prostor";
            koSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate koRentTemplate = new ExclusiveReality.Models.PageTemplate();
            koRentTemplate.Name = "korentpage";
            koRentTemplate.Title = "Šablona pro výpis pronájmu komerèních prostor";
            koRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate recreationObjectsSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            recreationObjectsSaleTemplate.Name = "recreationobjectssalepage";
            recreationObjectsSaleTemplate.Title = "Šablona pro výpis prodeje rekreaèních objektù";
            recreationObjectsSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate recreationObjectsRentTemplate = new ExclusiveReality.Models.PageTemplate();
            recreationObjectsRentTemplate.Name = "recreationobjectsrentpage";
            recreationObjectsRentTemplate.Title = "Šablona pro výpis pronájmu rekreaèních objektù";
            recreationObjectsRentTemplate.Create();

            ExclusiveReality.Models.PageTemplate hotelsSaleTemplate = new ExclusiveReality.Models.PageTemplate();
            hotelsSaleTemplate.Name = "hotelssalepage";
            hotelsSaleTemplate.Title = "Šablona pro výpis prodeje hotelù, penzionù";
            hotelsSaleTemplate.Create();

            ExclusiveReality.Models.PageTemplate hotelsRentTemplate = new ExclusiveReality.Models.PageTemplate();
            hotelsRentTemplate.Name = "hotelsrentpage";
            hotelsRentTemplate.Title = "Šablona pro výpis pronájmu hotelù, penzionù";
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


            ExclusiveReality.Models.Page domu = new ExclusiveReality.Models.Page(root, "Úvod", "index", "");
            domu.Published = true;
            domu.OrderPriority = 0;
            domu.PageTemplate = hpTemplate;
            domu.Create();



            ExclusiveReality.Models.Section sluzby = new ExclusiveReality.Models.Section(root, "Naše služby", "sluzby");
            sluzby.OrderPriority = 2;
            sluzby.Published = false;
            sluzby.Create();

            ExclusiveReality.Models.Page vykupNemovitosti = new ExclusiveReality.Models.Page(sluzby, "Výkup nemovitostí", "vykup-nemovitosti", String.Empty);
            vykupNemovitosti.Published = true;
            vykupNemovitosti.OrderPriority = 1;
            vykupNemovitosti.PageTemplate = contentTemplate;
            vykupNemovitosti.Create();

            ExclusiveReality.Models.Page realitniSluzby = new ExclusiveReality.Models.Page(sluzby, "Realitní sluzby", "realitni-sluzby", String.Empty);
            realitniSluzby.Published = true;
            realitniSluzby.OrderPriority = 2;
            realitniSluzby.PageTemplate = contentTemplate;
            realitniSluzby.Create();

            ExclusiveReality.Models.Page financniSluzby = new ExclusiveReality.Models.Page(sluzby, "Finanèní služby", "financni-sluzby", String.Empty);
            financniSluzby.Published = true;
            financniSluzby.OrderPriority = 1;
            financniSluzby.PageTemplate = contentTemplate;
            financniSluzby.Create();

            ExclusiveReality.Models.Page zprostredkovaniProdeje = new ExclusiveReality.Models.Page(sluzby, "Zprostøedkování prodeje", "zprostredkovani-prodeje", String.Empty);
            zprostredkovaniProdeje.Published = true;
            zprostredkovaniProdeje.OrderPriority = 1;
            zprostredkovaniProdeje.PageTemplate = contentTemplate;
            zprostredkovaniProdeje.Create();

            ExclusiveReality.Models.Page poradenstvi = new ExclusiveReality.Models.Page(sluzby, "Poradenství", "poradenstvi", String.Empty);
            poradenstvi.Published = true;
            poradenstvi.OrderPriority = 1;
            poradenstvi.PageTemplate = contentTemplate;
            poradenstvi.Create();

            ExclusiveReality.Models.Page pravniPoradna = new ExclusiveReality.Models.Page(sluzby, "Právní poradna", "pravni-poradna", String.Empty);
            pravniPoradna.Published = true;
            pravniPoradna.OrderPriority = 1;
            pravniPoradna.PageTemplate = contentTemplate;
            poradenstvi.Create();

            ExclusiveReality.Models.Page pojisteni = new ExclusiveReality.Models.Page(sluzby, "Pojištìní", "pojisteni", String.Empty);
            pojisteni.Published = true;
            pojisteni.OrderPriority = 1;
            pojisteni.PageTemplate = contentTemplate;
            pojisteni.Create();






            ExclusiveReality.Models.Section financovani = new ExclusiveReality.Models.Section(root, "Financování", "financovani");
            financovani.OrderPriority = 3;
            financovani.Published = false;
            financovani.Create();

            ExclusiveReality.Models.Page obecneInformace = new ExclusiveReality.Models.Page(financovani, "Obecné informace", "obecne-informace ", String.Empty);
            obecneInformace.Published = true;
            obecneInformace.OrderPriority = 1;
            obecneInformace.PageTemplate = contentTemplate;
            obecneInformace.Create();

            ExclusiveReality.Models.Page financovaniNemovitosti = new ExclusiveReality.Models.Page(financovani, "Financování nemovitostí", "financovani-nemovitosti", String.Empty);
            financovaniNemovitosti.Published = true;
            financovaniNemovitosti.OrderPriority = 2;
            financovaniNemovitosti.PageTemplate = contentTemplate;
            financovaniNemovitosti.Create();

            ExclusiveReality.Models.Page pojisteniNemovitosti = new ExclusiveReality.Models.Page(financovani, "Pojištìní nemovitostí", "pojisteni-nemovitosti", String.Empty);
            pojisteniNemovitosti.Published = true;
            pojisteniNemovitosti.OrderPriority = 3;
            pojisteniNemovitosti.PageTemplate = contentTemplate;
            pojisteniNemovitosti.Create();

            ExclusiveReality.Models.Page hypoteka = new ExclusiveReality.Models.Page(financovani, "Hypotéka", "hypoteka", String.Empty);
            hypoteka.Published = true;
            hypoteka.OrderPriority = 4;
            hypoteka.PageTemplate = contentTemplate;
            hypoteka.Create();

            ExclusiveReality.Models.Page kalkulaceHypoteky = new ExclusiveReality.Models.Page(financovani, "Kalkulace hypotéky", "kalkulace-hypoteky", String.Empty);
            kalkulaceHypoteky.Published = true;
            kalkulaceHypoteky.OrderPriority = 5;
            kalkulaceHypoteky.PageTemplate = contentTemplate;
            kalkulaceHypoteky.Create();

            ExclusiveReality.Models.Page financniPoradenstvi = new ExclusiveReality.Models.Page(financovani, "Finanèní poradenství", "financni-poradenstvi", String.Empty);
            financniPoradenstvi.Published = true;
            financniPoradenstvi.OrderPriority = 6;
            financniPoradenstvi.PageTemplate = contentTemplate;
            financniPoradenstvi.Create();

            ExclusiveReality.Models.Page ostatniProdukty = new ExclusiveReality.Models.Page(financovani, "Ostatní produkty", "ostatni-produkty", String.Empty);
            ostatniProdukty.Published = true;
            ostatniProdukty.OrderPriority = 7;
            ostatniProdukty.PageTemplate = contentTemplate;
            ostatniProdukty.Create();






            ExclusiveReality.Models.Section development = new ExclusiveReality.Models.Section(root, "Development", "development");
            development.OrderPriority = 4;
            development.Published = false;
            development.Create();

            ExclusiveReality.Models.Page noveProjektyDev = new ExclusiveReality.Models.Page(development, "Nové projekty pro developery a investory", "nove-projekty-developeri", String.Empty);
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







            ExclusiveReality.Models.Section onas = new ExclusiveReality.Models.Section(root, "O nás", "o-nas");
            onas.OrderPriority = 6;
            onas.Published = false;
            onas.Create();

            ExclusiveReality.Models.Page profil = new ExclusiveReality.Models.Page(onas, "Profil spoleènosti", "profil-spolecnosti", String.Empty);
            profil.Published = true;
            profil.OrderPriority = 1;
            profil.PageTemplate = contentTemplate;
            profil.Create();

            ExclusiveReality.Models.Page nastym= new ExclusiveReality.Models.Page(onas, "Náš tým", "nas-tym", String.Empty);
            nastym.Published = true;
            nastym.OrderPriority = 2;
            nastym.PageTemplate = contentTemplate;
            nastym.Create();

            ExclusiveReality.Models.Page dalsiInfo = new ExclusiveReality.Models.Page(onas, "Další info o spoleènosti", "dalsi-info-o-spolecnosti", String.Empty);
            dalsiInfo.Published = true;
            dalsiInfo.OrderPriority = 3;
            dalsiInfo.PageTemplate = contentTemplate;
            dalsiInfo.Create();

            ExclusiveReality.Models.Page etickyKodex = new ExclusiveReality.Models.Page(onas, "Etický kodex", "eticky-kodex", String.Empty);
            etickyKodex.Published = true;
            etickyKodex.OrderPriority = 4;
            etickyKodex.PageTemplate = contentTemplate;
            etickyKodex.Create();





            ExclusiveReality.Models.Page kontakt = new ExclusiveReality.Models.Page(root, "Kontakt", "kontakt", "<p>Kontakt.</p>");
            kontakt.Published = true;
            kontakt.OrderPriority = 7;
            kontakt.PageTemplate = contentTemplate;
            kontakt.Create();

            //ExclusiveReality.Models.Page vyhledavani = new ExclusiveReality.Models.Page(root, "Vyhledávání", "vyhledavani", "<p>Vyhledávání.</p>");
            //vyhledavani.Published = true;
            //vyhledavani.OrderPriority = 8;
            //vyhledavani.PageTemplate = contentTemplate;
            //vyhledavani.Create();

            ExclusiveReality.Models.Page mapa = new ExclusiveReality.Models.Page(root, "Mapa webu", "mapa-webu", "<p>Mapa webu</p>");
            mapa.Published = true;
            mapa.OrderPriority = 9;
            mapa.PageTemplate = contentTemplate;
            mapa.Create();






            ExclusiveReality.Models.Section nabidka = new ExclusiveReality.Models.Section(root, "Nabídka nemovitostí", "nabidka");
            nabidka.OrderPriority = 1;
            nabidka.CreateIndexPage = false;
            nabidka.Published = true;
            nabidka.Create();


            ExclusiveReality.Models.Page nabidkaIndex = new ExclusiveReality.Models.Page(nabidka, "Nabídka nemovitostí", "index", "<p>Naše nabídky...</p>");
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

            ExclusiveReality.Models.Page prodejDomy = new ExclusiveReality.Models.Page(prodej, "Rodinné domy", "rodinne-domy", String.Empty);
            prodejDomy.Published = true;
            prodejDomy.OrderPriority = 2;
            prodejDomy.PageTemplate = rdSaleTemplate;
            prodejDomy.Create();

            ExclusiveReality.Models.Page prodejCinzovniDomy = new ExclusiveReality.Models.Page(prodej, "Èinžovní domy", "cinzovni-domy", String.Empty);
            prodejCinzovniDomy.Published = true;
            prodejCinzovniDomy.OrderPriority = 3;
            prodejCinzovniDomy.PageTemplate = flatsHousesSaleTemplate;
            prodejCinzovniDomy.Create();

            ExclusiveReality.Models.Page prodejNebytoveProstory = new ExclusiveReality.Models.Page(prodej, "Nebytové prostory", "nebytove-prostory", String.Empty);
            prodejNebytoveProstory.Published = true;
            prodejNebytoveProstory.OrderPriority = 4;
            prodejNebytoveProstory.PageTemplate = noLivePlacesSaleTemplate;
            prodejNebytoveProstory.Create();

            ExclusiveReality.Models.Page prodejPozemky = new ExclusiveReality.Models.Page(prodej, "Pozemky", "pozemky", String.Empty);
            prodejPozemky.Published = true;
            prodejPozemky.OrderPriority = 5;
            prodejPozemky.PageTemplate = estatePropertiesSaleTemplate;
            prodejPozemky.Create();

            ExclusiveReality.Models.Page prodejKomercniProstory = new ExclusiveReality.Models.Page(prodej, "Komerèní prostory", "komercni-prostory", String.Empty);
            prodejKomercniProstory.Published = true;
            prodejKomercniProstory.OrderPriority = 6;
            prodejKomercniProstory.PageTemplate = koSaleTemplate;
            prodejKomercniProstory.Create();

            ExclusiveReality.Models.Page prodejRekreacniObjekty = new ExclusiveReality.Models.Page(prodej, "Rekreaèní objekty", "rekreacni-objekty", String.Empty);
            prodejRekreacniObjekty.Published = true;
            prodejRekreacniObjekty.OrderPriority = 7;
            prodejRekreacniObjekty.PageTemplate = recreationObjectsSaleTemplate;
            prodejRekreacniObjekty.Create();

            ExclusiveReality.Models.Page prodejHotely = new ExclusiveReality.Models.Page(prodej, "Hotely, penziony", "hotely-penziony", String.Empty);
            prodejHotely.Published = true;
            prodejHotely.OrderPriority = 8;
            prodejHotely.PageTemplate = hotelsSaleTemplate;
            prodejHotely.Create();





            ExclusiveReality.Models.Section pronajem = new ExclusiveReality.Models.Section(nabidka, "Pronájem", "pronajem");
            pronajem.OrderPriority = 2;
            pronajem.Published = true;
            pronajem.Create();

            ExclusiveReality.Models.Page pronajemByty = new ExclusiveReality.Models.Page(pronajem, "Byty", "byty", String.Empty);
            pronajemByty.Published = true;
            pronajemByty.OrderPriority = 1;
            pronajemByty.PageTemplate = flatsRentTemplate;
            pronajemByty.Create();

            ExclusiveReality.Models.Page pronajemDomy = new ExclusiveReality.Models.Page(pronajem, "Rodinné domy", "rodinne-domy", String.Empty);
            pronajemDomy.Published = true;
            pronajemDomy.OrderPriority = 2;
            pronajemDomy.PageTemplate = rdRentTemplate;
            pronajemDomy.Create();

            ExclusiveReality.Models.Page pronajemCinzovniDomy = new ExclusiveReality.Models.Page(pronajem, "Èinžovní domy", "cinzovni-domy", String.Empty);
            pronajemCinzovniDomy.Published = true;
            pronajemCinzovniDomy.OrderPriority = 3;
            pronajemCinzovniDomy.PageTemplate = flatsHousesRentTemplate;
            pronajemCinzovniDomy.Create();

            ExclusiveReality.Models.Page pronajemNebytoveProstory = new ExclusiveReality.Models.Page(pronajem, "Nebytové prostory", "nebytove-prostory", String.Empty);
            pronajemNebytoveProstory.Published = true;
            pronajemNebytoveProstory.OrderPriority = 4;
            pronajemNebytoveProstory.PageTemplate = noLivePlacesRentTemplate;
            pronajemNebytoveProstory.Create();

            ExclusiveReality.Models.Page pronajemPozemky = new ExclusiveReality.Models.Page(pronajem, "Pozemky", "pozemky", String.Empty);
            pronajemPozemky.Published = true;
            pronajemPozemky.OrderPriority = 5;
            pronajemPozemky.PageTemplate = estatePropertiesRentTemplate;
            pronajemPozemky.Create();

            ExclusiveReality.Models.Page pronajemKomercniProstory = new ExclusiveReality.Models.Page(pronajem, "Komerèní prostory", "komercni-prostory", String.Empty);
            pronajemKomercniProstory.Published = true;
            pronajemKomercniProstory.OrderPriority = 6;
            pronajemKomercniProstory.PageTemplate = koRentTemplate;
            pronajemKomercniProstory.Create();

            ExclusiveReality.Models.Page pronajemRekreacniObjekty = new ExclusiveReality.Models.Page(pronajem, "Rekreaèní objekty", "rekreacni-objekty", String.Empty);
            pronajemRekreacniObjekty.Published = true;
            pronajemRekreacniObjekty.OrderPriority = 7;
            pronajemRekreacniObjekty.PageTemplate = recreationObjectsRentTemplate;
            pronajemRekreacniObjekty.Create();

            ExclusiveReality.Models.Page pronajemHotely = new ExclusiveReality.Models.Page(pronajem, "Hotely, penziony", "hotely-penziony", String.Empty);
            pronajemHotely.Published = true;
            pronajemHotely.OrderPriority = 8;
            pronajemHotely.PageTemplate = hotelsRentTemplate;
            pronajemHotely.Create();





            ExclusiveReality.Models.Section zahranicniNemovitosti = new ExclusiveReality.Models.Section(nabidka, "Zahranièní nemovitosti", "zahranicni-nemovitosti");
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
            p1CZ.Name = "Bytový dùm hrádeèek";
            p1CZ.BasicDescription = "Krátký popis....";
            p1CZ.FullDescription = "Dlouhý popis....";
            p1CZ.LocalityDescription = "krásná pøíroda a dostupnost všecho :).";
            p1CZ.Parking = "Soukromé parkování pro 40 aut.";
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
            p2CZ.Name = "Rodinné domy";
            p2CZ.BasicDescription = "Krátký popis....";
            p2CZ.FullDescription = "Dlouhý popis....";
            p2CZ.LocalityDescription = "krásná pøíroda a dostupnost všecho :).";
            p2CZ.Parking = "Soukromé stání.";
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
            a1CZ.Perex = "Krátký perex";
            a1CZ.Content = "<p>Dlouhý obsah...</p>";
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
            a2CZ.Perex = "Krátký perex";
            a2CZ.Content = "<p>Dlouhý obsah...</p>";
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
            new Region("Benešov").Create();
            new Region("Beroun").Create();
            new Region("Blansko").Create();
            new Region("Bøeclav").Create();
            new Region("Brno-mìsto").Create();
            new Region("Brno-venkov").Create();
            new Region("Bruntál").Create();
            new Region("Èeská Lípa").Create();
            new Region("Èeské Budìjovice").Create();
            new Region("Èeský Krumlov").Create();
            new Region("Cheb").Create();
            new Region("Chomutov").Create();
            new Region("Chrudim").Create();
            new Region("Dìèín").Create();
            new Region("Domažlice").Create();
            new Region("Frýdek-Místek").Create();
            new Region("Havlíèkùv Brod").Create();
            new Region("Hodonín").Create();
            new Region("Hradec Králové").Create();
            new Region("Jablonec nad Nisou").Create();
            new Region("Jeseník").Create();
            new Region("Jièín").Create();
            new Region("Jihlava").Create();
            new Region("Jindøichùv Hradec").Create();
            new Region("Karlovy Vary").Create();
            new Region("Karviná").Create();
            new Region("Kladno").Create();
            new Region("Klatovy").Create();
            new Region("Kolín").Create();
            new Region("Kromìøíž").Create();
            new Region("Kutná Hora").Create();
            new Region("Liberec").Create();
            new Region("Litomìøice").Create();
            new Region("Louny").Create();
            new Region("Mìlník").Create();
            new Region("Mladá Boleslav").Create();
            new Region("Most").Create();
            new Region("Náchod").Create();
            new Region("Nový Jièín").Create();
            new Region("Nymburk").Create();
            new Region("Olomouc").Create();
            new Region("Opava").Create();
            new Region("Ostrava-mìsto").Create();
            new Region("Pardubice").Create();
            new Region("Pelhøimov").Create();
            new Region("Písek").Create();
            new Region("Plzeò-jih").Create();
            new Region("Plzeò-mìsto").Create();
            new Region("Plzeò-sever").Create();
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
            new Region("Praha-Bìchovice").Create();
            new Region("Praha-Benice").Create();
            new Region("Praha-Bøezinìves").Create();
            new Region("Praha-Èakovice").Create();
            new Region("Praha-Ïáblice").Create();
            new Region("Praha-Dolní Chabry").Create();
            new Region("Praha-Dolní Mìcholupy").Create();
            new Region("Praha-Dolní Poèernice").Create();
            new Region("Praha-Dubeè").Create();
            new Region("Praha-Klánovice").Create();
            new Region("Praha-Kolodìje").Create();
            new Region("Praha-Kolovraty").Create();
            new Region("Praha-Královice").Create();
            new Region("Praha-Køeslice").Create();
            new Region("Praha-Kunratice").Create();
            new Region("Praha-Libuš").Create();
            new Region("Praha-Lipence").Create();
            new Region("Praha-Lochkov").Create();
            new Region("Praha-Lysolaje").Create();
            new Region("Praha-Nebušice").Create();
            new Region("Praha-Nedvìzí").Create();
            new Region("Praha-Petrovice").Create();
            new Region("Praha-Pøední Kopanina").Create();
            new Region("Praha-Øeporyje").Create();
            new Region("Praha-Satalice").Create();
            new Region("Praha-Šeberov").Create();
            new Region("Praha-Slivenec").Create();
            new Region("Praha-Štìrboholy").Create();
            new Region("Praha-Suchdol").Create();
            new Region("Praha-Troja").Create();
            new Region("Praha-Újezd").Create();
            new Region("Praha-Velká Chuchle").Create();
            new Region("Praha-Vinoø").Create();
            new Region("Praha-východ").Create();
            new Region("Praha-západ").Create();
            new Region("Praha-Zbraslav").Create();
            new Region("Praha-Zlièín").Create();
            new Region("Pøerov").Create();
            new Region("Pøíbram").Create();
            new Region("Prostìjov").Create();
            new Region("Rakovník").Create();
            new Region("Rokycany").Create();
            new Region("Rychnov nad Knìžnou").Create();
            new Region("Semily").Create();
            new Region("Sokolov").Create();
            new Region("Strakonice").Create();
            new Region("Šumperk").Create();
            new Region("Svitavy").Create();
            new Region("Tábor").Create();
            new Region("Tachov").Create();
            new Region("Teplice").Create();
            new Region("Tøebíè").Create();
            new Region("Trutnov").Create();
            new Region("Uherské Hradištì").Create();
            new Region("Ústí nad Labem").Create();
            new Region("Ústí nad Orlicí").Create();
            new Region("Vsetín").Create();
            new Region("Vyškov").Create();
            new Region("Žïár nad Sázavou").Create();
            new Region("Zlín").Create();
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
            etCZ.Name = "Rodinné domy";
            etCZ.Create();

            etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "Houses";
            etEN.Create();


            estateType = new EstateType();
            estateType.Create();

            etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "Èinžovní domy";
            etCZ.Create();

            etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "Flats houses";
            etEN.Create();


            estateType = new EstateType();
            estateType.Create();

            etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "Nebytové prostory";
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
            etCZ.Name = "Komerèní prostory";
            etCZ.Create();

            etEN = new EstateTypeCulture(cultureEn);
            etEN.EstateType = estateType;
            etEN.Name = "Commercial spaces";
            etEN.Create();


            estateType = new EstateType();
            estateType.Create();

            etCZ = new EstateTypeCulture(cultureCz);
            etCZ.EstateType = estateType;
            etCZ.Name = "Rekreaèní objekty";
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
            eotCZ.Name = "Pronájem";
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
            ptcCZ.Name = "za mìsíc";
            ptcCZ.Create();

            PriceTypeCulture ptcEN = new PriceTypeCulture(cultureEn);
            ptcEN.PriceType = priceType;
            ptcEN.Name = "per month";
            ptcEN.Create();


            priceType = new PriceType();
            priceType.Create();

            ptcCZ = new PriceTypeCulture(cultureCz);
            ptcCZ.PriceType = priceType;
            ptcCZ.Name = "za m<sup>2</sup>/mìsíc";
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
            e1CZ.Name = "Prodej nadstandardní  novostavby ØRD 4+KK/GARÁŽ  NEHVIZDY";
            e1CZ.BasicDescription = "Nabízíme v obci NEHVIZDY k prodeji patrový øadový dùm situován jako 4+kk  se zahradou o výmìøe 88m2 a obytné plochy s garáží 123m2,  zastavìné o výmìøe 177m2.";
            e1CZ.FullDescription = "Dùm je ve velice dobrém stavu, urèen k okamžitému nastìhování a bydlení. Interiérová èást domu je vybavena luxusními prvky vèetnì st. materiálu, dubová podlahová krytina, Italské dlažby, kuchyòská linka s vest. spotøebièi, alarm, internet, satelitní TV, plynové etáž. topení a pod. Kolaudován byl v roce 2006. Lokaènì je vsazen do klidové ètvrti øadových domù v této obci. Dùm doporuèujeme!! V pøípadì zájmu o nákup nemovitosti formou hypoteèního úvìru je zajištìno poskytnutí financování na tento dùm od ÈSOB a.s. nebo HYPOTEÈNÍ BANKY a.s..";
            e1CZ.AdditionalDescription = "V pøípadì zájmu o prohlídku této nemovitosti èi bližších informací k domu volejte : 603784314, pro informace k bankovní èi právní metodice koupì tohoto domu volejte : 608963665.\n"+
                                                        "Cena 4.990 000.-Kè";
            e1CZ.Create();

            EstateCulture e1EN = new EstateCulture();
            e1EN.Estate = estate1;
            e1EN.Culture = cultureEn;
            e1EN.Name = "Prodej nadstandardní  novostavby ØRD 4+KK/GARÁŽ  NEHVIZDY";
            e1EN.BasicDescription = "Nabízíme v obci NEHVIZDY k prodeji patrový øadový dùm situován jako 4+kk  se zahradou o výmìøe 88m2 a obytné plochy s garáží 123m2,  zastavìné o výmìøe 177m2.";
            e1EN.FullDescription = "Dùm je ve velice dobrém stavu, urèen k okamžitému nastìhování a bydlení. Interiérová èást domu je vybavena luxusními prvky vèetnì st. materiálu, dubová podlahová krytina, Italské dlažby, kuchyòská linka s vest. spotøebièi, alarm, internet, satelitní TV, plynové etáž. topení a pod. Kolaudován byl v roce 2006. Lokaènì je vsazen do klidové ètvrti øadových domù v této obci. Dùm doporuèujeme!! V pøípadì zájmu o nákup nemovitosti formou hypoteèního úvìru je zajištìno poskytnutí financování na tento dùm od ÈSOB a.s. nebo HYPOTEÈNÍ BANKY a.s..";
            e1EN.AdditionalDescription = "V pøípadì zájmu o prohlídku této nemovitosti èi bližších informací k domu volejte : 603784314, pro informace k bankovní èi právní metodice koupì tohoto domu volejte : 608963665.\n" +
                                                        "Cena 4.990 000.-Kè";
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
            e2CZ.Name = "Semily, Horní Rokytnice ( Rokytnice nad Jizerou)";
            e2CZ.BasicDescription = "Krásná roubená chalupa v chránìném území KRNAP\n" +
                                                "\n" +
                                                "Roubená chalupa po celkové rekonstrukci v r.1998, zastavìná plocha\n"+
                                                "181m2, vedlejší stavby - stodola, zastavìná plocha 199 m2( stavební parcela) + kùlna + døevník.";
            e2CZ.FullDescription = "Chalupa - v pøízemí 2 velké místnosti( 1- zachován starodávný krkonošský ráz, vèetnì nábytku a kachlových kamen atd.,2 - novodobý ráz s krbem a malým barem) z této místnosti východ na velkou terasu, koupelna, WC, komory, ve velkém podkroví zaøízena 1 místnost - možnost pùdní dostavby.V chalupì zavedena voda a elektøina.";
            e2CZ.AdditionalDescription = "K chalupì patøí krásná ovocná zahrada, kterou protéká malý potùèek, vlastní pozemky o celkové rozloze cca 49 000 m2, vše v krásném a klidném prostøedí uprostøed krkonošských hor, lyžaøský vlek pøímo v obci. Chalupa se nachází v obcí Františkov - pøístup naprosto bezproblémový v každém roèním obdoby. Pozemky jsou vedeny jako louka a les s povolením kácet.\n"+
                                                        "\n" +
                                                        "Cena 4.990 000.-Kè";
            e2CZ.Create();

            EstateCulture e2EN = new EstateCulture();
            e2EN.Estate = estate2;
            e2EN.Culture = cultureEn;
            e2EN.Name = "Semily, Horní Rokytnice ( Rokytnice nad Jizerou)";
            e2EN.BasicDescription = "Krásná roubená chalupa v chránìném území KRNAP\n" +
                                                "\n" +
                                                "Roubená chalupa po celkové rekonstrukci v r.1998, zastavìná plocha\n" +
                                                "181m2, vedlejší stavby - stodola, zastavìná plocha 199 m2( stavební parcela) + kùlna + døevník.";
            e2EN.FullDescription = "Chalupa - v pøízemí 2 velké místnosti( 1- zachován starodávný krkonošský ráz, vèetnì nábytku a kachlových kamen atd.,2 - novodobý ráz s krbem a malým barem) z této místnosti východ na velkou terasu, koupelna, WC, komory, ve velkém podkroví zaøízena 1 místnost - možnost pùdní dostavby.V chalupì zavedena voda a elektøina.";
            e2EN.AdditionalDescription = "K chalupì patøí krásná ovocná zahrada, kterou protéká malý potùèek, vlastní pozemky o celkové rozloze cca 49 000 m2, vše v krásném a klidném prostøedí uprostøed krkonošských hor, lyžaøský vlek pøímo v obci. Chalupa se nachází v obcí Františkov - pøístup naprosto bezproblémový v každém roèním obdoby. Pozemky jsou vedeny jako louka a les s povolením kácet.\n" +
                                                        "\n" +
                                                        "Cena 4.990 000.-Kè";
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
            e3CZ.Name = "Prodej penzionu u vodní nádrže Lipno.";
            e3CZ.BasicDescription = "Lipno nad Vltavou\n" +
                                                "\n" +
                                                "Objekt se nachází v nejkrásnìjším koutu Šumavy, široké využití služeb, jak v létì tak i v zimì.";
            e3CZ.FullDescription = "Chalupa - v pøízemí 2 velké místnosti( 1- zachován starodávný krkonošský ráz, vèetnì nábytku a kachlových kamen atd.,2 - novodobý ráz s krbem a malým barem) z této místnosti východ na velkou terasu, koupelna, WC, komory, ve velkém podkroví zaøízena 1 místnost - možnost pùdní dostavby.V chalupì zavedena voda a elektøina.";
            e3CZ.AdditionalDescription = "Jedná se o dvou podlaží objekt vhodný k rekonstrukci, døíve sloužící jako penzion v blízkosti vodní nádrže Lipno, Ski areálu a Aqua parku.. Na pozemku plyn, voda, elektøina a možnost pøipojení na obecní kanalizaci cca 300m. Zastavìná plocha 650m2, pozemek 4672m2. Vhodné taky na apartmány, výhodná investice!\n" +
                                                        "\n" +
                                                        "Cena 8.290 000.-Kè";
            e3CZ.Create();

            EstateCulture e3EN = new EstateCulture();
            e3EN.Estate = estate3;
            e3EN.Culture = cultureEn;
            e3EN.Name = "Prodej penzionu u vodní nádrže Lipno.";
            e3EN.BasicDescription = "Lipno nad Vltavou\n" +
                                                "\n" +
                                                "Objekt se nachází v nejkrásnìjším koutu Šumavy, široké využití služeb, jak v létì tak i v zimì.";
            e3EN.FullDescription = "Chalupa - v pøízemí 2 velké místnosti( 1- zachován starodávný krkonošský ráz, vèetnì nábytku a kachlových kamen atd.,2 - novodobý ráz s krbem a malým barem) z této místnosti východ na velkou terasu, koupelna, WC, komory, ve velkém podkroví zaøízena 1 místnost - možnost pùdní dostavby.V chalupì zavedena voda a elektøina.";
            e3EN.AdditionalDescription = "Jedná se o dvou podlaží objekt vhodný k rekonstrukci, døíve sloužící jako penzion v blízkosti vodní nádrže Lipno, Ski areálu a Aqua parku.. Na pozemku plyn, voda, elektøina a možnost pøipojení na obecní kanalizaci cca 300m. Zastavìná plocha 650m2, pozemek 4672m2. Vhodné taky na apartmány, výhodná investice!\n" +
                                                        "\n" +
                                                        "Cena 8.290 000.-Kè";
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
            e4CZ.Name = "Byt 3+1 75m2, Praha 6 Øepy ul. Španielova";
            e4CZ.BasicDescription = "Praha 6 - Øepy, ulice Španielova, nabídka pronájmu bytové jednotky v dispozici 3+1+L, v panelovém patrovém bytovém domì s výtahem, tøetí patro ze sedmi, 75m2, neprùchozí pokoje, byt je vybaven pouze novou plnì funkèní kuchyòskou linkou, plovoucí podlahy, jedná se o prostorný byt ve velice dobrém stavu! Cena 12.000,- Kè bez sužeb.";
            e4CZ.Create();

            EstateCulture e4EN = new EstateCulture();
            e4EN.Estate = estate4;
            e4EN.Culture = cultureEn;
            e4EN.Name = "Byt 3+1 75m2, Praha 6 Øepy ul. Španielova";
            e4EN.BasicDescription = "Praha 6 - Øepy, ulice Španielova, nabídka pronájmu bytové jednotky v dispozici 3+1+L, v panelovém patrovém bytovém domì s výtahem, tøetí patro ze sedmi, 75m2, neprùchozí pokoje, byt je vybaven pouze novou plnì funkèní kuchyòskou linkou, plovoucí podlahy, jedná se o prostorný byt ve velice dobrém stavu! Cena 12.000,- Kè bez sužeb.";
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
            e5CZ.Name = "Prodej družstevního bytu 3+1+lodžie v Nymburce v ulici Božská.";
            e5CZ.BasicDescription = "Prodej družstevního bytu 3+1+lodžie v Nymburce v ulici Zbožská, cca 80m2, sklep, výtah, 4.patro, byt se nachází v panelovém 7.patrovém bytovém domì, k nastìhování od 01/08.";
            e5CZ.AdditionalDescription = "Bytová jednotka je v dobrém pùvodním stavu.\n" +
                                                        "Financování formou hypotéky možné!( zajištìno )\n" +
                                                        "Cena 1790000.-Kè";
            e5CZ.Create();

            EstateCulture e5EN = new EstateCulture();
            e5EN.Estate = estate5;
            e5EN.Culture = cultureEn;
            e5EN.Name = "Prodej penzionu u vodní nádrže Lipno.";
            e5EN.BasicDescription = "Lipno nad Vltavou\n" +
                                                "\n" +
                                                "Objekt se nachází v nejkrásnìjším koutu Šumavy, široké využití služeb, jak v létì tak i v zimì.";
            e5EN.FullDescription = "Chalupa - v pøízemí 2 velké místnosti( 1- zachován starodávný krkonošský ráz, vèetnì nábytku a kachlových kamen atd.,2 - novodobý ráz s krbem a malým barem) z této místnosti východ na velkou terasu, koupelna, WC, komory, ve velkém podkroví zaøízena 1 místnost - možnost pùdní dostavby.V chalupì zavedena voda a elektøina.";
            e5EN.AdditionalDescription = "Jedná se o dvou podlaží objekt vhodný k rekonstrukci, døíve sloužící jako penzion v blízkosti vodní nádrže Lipno, Ski areálu a Aqua parku.. Na pozemku plyn, voda, elektøina a možnost pøipojení na obecní kanalizaci cca 300m. Zastavìná plocha 650m2, pozemek 4672m2. Vhodné taky na apartmány, výhodná investice!\n" +
                                                        "\n" +
                                                        "Cena 8.290 000.-Kè";
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

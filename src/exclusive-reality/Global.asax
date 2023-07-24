<%@ Import Namespace="System.Globalization"%>
<%@ Application Language="C#" %>
<script runat="server"> 

    void Application_BeginRequest(object sender, EventArgs e)
    {
        //log4net.Config.XmlConfigurator.Configure(); 

        if (Request.Url.ToString().Contains("/admin_exclusivereal/") || Request.Url.ToString().Contains("/login/"))
        {
            System.Globalization.CultureInfo c = new System.Globalization.CultureInfo(1029);
            System.Threading.Thread.CurrentThread.CurrentCulture = c;
            System.Threading.Thread.CurrentThread.CurrentUICulture = c;
        }
        else
        {
            ExclusiveReality.Models.Page page = ExclusiveReality.Models.Page.GetPageByUrl(Request.Params["originalurl"], true);
            System.Globalization.CultureInfo c = null;


            ExclusiveReality.Models.Section section = page == null ? ExclusiveReality.Models.Section.GetSectionByUrl(Request.Params["originalurl"], true) : page.Section;


            if (page != null || section != null)
                c = new System.Globalization.CultureInfo((page != null ? page.GetEnsuredCulture(true).Id : section.GetEnsuredCulture(true).Id));
            else
            {
                ExclusiveReality.Models.Culture defaultCulture = ExclusiveReality.Models.Culture.GetDefaultCulture();
                if (defaultCulture != null)
                    c = new System.Globalization.CultureInfo(defaultCulture.Id);
            }

            if (c != null)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = c;
                System.Threading.Thread.CurrentThread.CurrentUICulture = c;
            }

            HttpContext.Current.Items.Add("Page", page);
            HttpContext.Current.Items.Add("Section", section);
        }
    }
    
    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup 

        Castle.ActiveRecord.Framework.IConfigurationSource source = ConfigurationManager.GetSection("activerecord") as Castle.ActiveRecord.Framework.IConfigurationSource; //this for .net 2.0
        Castle.ActiveRecord.ActiveRecordStarter.Initialize(
            source
            , typeof(ExclusiveReality.Models.Section)
            , typeof(ExclusiveReality.Models.Page)
            , typeof(ExclusiveReality.Models.PageTemplate)
            , typeof(ExclusiveReality.Models.Culture)
            , typeof(ExclusiveReality.Models.Actuality)
            , typeof(ExclusiveReality.Models.ActualityCulture)
             
            , typeof(ExclusiveReality.Models.DeveloperProject)
            , typeof(ExclusiveReality.Models.DeveloperProjectCulture)
            , typeof(ExclusiveReality.Models.Estate)
            , typeof(ExclusiveReality.Models.EstateCulture)
            , typeof(ExclusiveReality.Models.EstateProperties)
            , typeof(ExclusiveReality.Models.EstatePropertiesCulture)
             
            , typeof(ExclusiveReality.Models.EstateManCard)
            , typeof(ExclusiveReality.Models.CompanyInfo)
            , typeof(ExclusiveReality.Models.Region)
            , typeof(ExclusiveReality.Models.EstateType)
            , typeof(ExclusiveReality.Models.EstateTypeCulture)
            , typeof(ExclusiveReality.Models.EstateOfferType)
            , typeof(ExclusiveReality.Models.EstateOfferTypeCulture)
            , typeof(ExclusiveReality.Models.CurrencyType)
            , typeof(ExclusiveReality.Models.PriceType) 
            , typeof(ExclusiveReality.Models.PriceTypeCulture)
            , typeof(ExclusiveReality.Models.EstateImage)
            , typeof(ExclusiveReality.Models.EstateAttachment)
            , typeof(ExclusiveReality.Models.DeveloperProjectAttachment)
            , typeof(ExclusiveReality.Models.DeveloperProjectImage)
            , typeof(ExclusiveReality.Models.EstateMessage)
             
            //, typeof(ExclusiveReality.Models.Test)
            //, typeof(ExclusiveReality.Models.TestPerson)
            //, typeof(ExclusiveReality.Models.Customer)
            //, typeof(ExclusiveReality.Models.CustomerAddress)
            //, typeof(ExclusiveReality.Models.CustomerHobby)
        );

        //Castle.ActiveRecord.ActiveRecordStarter.CreateSchema();
        //ExclusiveReality.Models.SampleData sampleData = new ExclusiveReality.Models.SampleData();
        //sampleData.Create();
        
          
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>

<%@ Application Language="C#" %>
<%@ Import Namespace="ux_web_gx_pharmacyoffline" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
        log4net.Config.XmlConfigurator.Configure();

    }

</script>

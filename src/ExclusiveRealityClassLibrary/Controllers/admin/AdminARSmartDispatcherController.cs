namespace ExclusiveReality.Controllers.Admin
{
	using Castle.MonoRail.Framework;
    using Castle.MonoRail.ActiveRecordSupport;

    [ControllerDetails(Area = "admin_exclusivereal")]
    [Layout("default_admin"), Rescue("generalerror")]
    [Helper(typeof(ExclusiveReality.Helpers.NexusARFormHelper))]
    [Helper(typeof(Castle.MonoRail.ActiveRecordScaffold.Helpers.PresentationHelper))]
    public abstract class AdminARSmartDispatcherController : ARSmartDispatcherController
	{
        protected override void Initialize()
        {
            base.Initialize();

            PropertyBag["UserName"] = HttpContext.User.Identity.Name;
            PropertyBag["OffersCount"] = ExclusiveReality.Models.Estate.TotalCount();
        }
    }
}

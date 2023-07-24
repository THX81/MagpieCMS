namespace ExclusiveReality.Controllers.Admin
{
	using System;
    using Castle.MonoRail.Framework;
    using ExclusiveReality.Crud;
    using ExclusiveReality.Models;

    [ControllerDetails(Area = "admin_exclusivereal/estates")]
    [Crud(typeof(PriceType))]
    [DynamicActionProvider(typeof(CrudProvider))]
    public class PriceTypesController : AdminARSmartDispatcherController
	{
	}
}

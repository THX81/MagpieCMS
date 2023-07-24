namespace ExclusiveReality.Controllers.Admin
{
	using System;
    using Castle.MonoRail.Framework;
    using ExclusiveReality.Crud;
    using ExclusiveReality.Models;

    [ControllerDetails(Area = "admin_exclusivereal/estates")]
    [Crud(typeof(EstateOfferType))]
    [DynamicActionProvider(typeof(CrudProvider))]
    public class EstateOfferTypesController : AdminARSmartDispatcherController
	{
	}
}

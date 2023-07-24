namespace ExclusiveReality.Controllers.Admin
{
	using System;
    using Castle.MonoRail.Framework;
    using ExclusiveReality.Crud;
    using ExclusiveReality.Models;

    [ControllerDetails(Area = "admin_exclusivereal/estates")]
    [Crud(typeof(CurrencyType))]
    [DynamicActionProvider(typeof(CrudProvider))]
    public class CurrencyTypesController : AdminARSmartDispatcherController
	{
	}
}

namespace ExclusiveReality.Controllers.Admin
{
	using System;
    using Castle.MonoRail.Framework;
    using ExclusiveReality.Models;
    using ExclusiveReality.Crud;

    [Crud(typeof(Culture))]
    [DynamicActionProvider(typeof(CrudProvider))]
    public class CulturesController : AdminARSmartDispatcherController
	{
	}
}

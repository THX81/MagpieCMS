namespace ExclusiveReality.Controllers.Admin
{
    using System;
    using Castle.MonoRail.Framework;
    using ExclusiveReality.Models;
    using ExclusiveReality.Crud;
    using Castle.MonoRail.ActiveRecordSupport;

    //[Scaffolding(typeof(Test))]
    [Crud(typeof(Test))]
    [DynamicActionProvider(typeof(CrudProvider))]
    public class TestController : AdminARSmartDispatcherController
	{
    }
}

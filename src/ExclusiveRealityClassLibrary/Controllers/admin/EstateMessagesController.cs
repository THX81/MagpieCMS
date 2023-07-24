namespace ExclusiveReality.Controllers.Admin
{
    using System;
    using Castle.MonoRail.Framework;
    using ExclusiveReality.Models;
    using ExclusiveReality.Crud;

    [Crud(typeof(EstateMessage))]
    [DynamicActionProvider(typeof(CrudProvider))]
    public class EstateMessagesController : AdminARSmartDispatcherController
	{
    }
}

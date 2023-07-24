using Castle.MonoRail.ActiveRecordScaffold.Helpers;
using Castle.MonoRail.Framework;
using ExclusiveReality.Helpers;

namespace ExclusiveReality.Controllers
{
    [Layout("default_web_cs"), Rescue("generalerror")]
    [Helper(typeof (NexusARFormHelper))]
    [Helper(typeof (PresentationHelper))]
    public abstract class BaseController : SmartDispatcherController
    {
    }
}
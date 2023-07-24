using System;
using System.Collections.Generic;
using System.Text;
using ExclusiveReality.Models;
using Castle.MonoRail.Framework;
using Castle.ActiveRecord.Queries;
using System.Threading;
using Castle.ActiveRecord.Framework.Internal;

namespace ExclusiveReality.ViewComponents
{
    public class LocalizedPropertiesForm : ViewComponent
    {
        public override void Initialize()
        {
            PropertyBag["LocalizationModel"] = ComponentParams["LocalizationModel"];
            PropertyBag["LocalizationPrefix"] = ComponentParams["LocalizationPrefix"];
            base.Initialize();
        }

        public override void Render()
        {
            base.Render();
        }
    }
}

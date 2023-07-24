using System;
using System.Collections.Generic;
using System.Text;
using ExclusiveReality.Models;
using Castle.MonoRail.Framework;
using Castle.ActiveRecord.Queries;
using System.Threading;
using System.Collections.ObjectModel;

namespace ExclusiveReality.ViewComponents
{
    public class DeveloperProjectDetail : ViewComponent
    {
        public override void Render()
        {
            int projectId = 0;
            if (Int32.TryParse(Params["detail"], out projectId))
            {
                DeveloperProject developerProject = DeveloperProject.FindOne(NHibernate.Expression.Expression.Eq("Id", projectId));
                PropertyBag["SelectedProject"] = developerProject;


                EstateType[] estateTypes = EstateType.FindAll();
                PropertyBag["EstateTypes"] = estateTypes;
                base.Render();
            }
        }
    }
}

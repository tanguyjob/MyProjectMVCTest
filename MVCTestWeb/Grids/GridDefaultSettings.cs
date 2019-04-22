using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTestWeb.Grids
{


    [ModelBinder(typeof(GridDefaultSettingsModelBinder))]
    public class GridDefaultSettings
    {
        public string Search { get; set; }
        public int Draw { get; set; }
        public int Length { get; set; }
        public int SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int Start { get; set; } 


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTestWeb.Grids
{
    public class GridDefaultSettingsModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)

        {
            try
            {
                var request = controllerContext.HttpContext.Request;

                return new GridDefaultSettings
                {
                    Search = Convert.ToString(request["search[value]"]),
                    Draw = int.Parse(request["draw"] ?? "1"),
                    Length = int.Parse(request["length"] ?? "10"),
                    Start = int.Parse(request["start"] ?? "1"),
                    SortOrder = Convert.ToString(request["order[0][dir]"]),
                    SortColumn = int.Parse(request["order[0][column]"] ?? "0")

                };


            }

            catch
            {
                return null;
            }
        } }
}
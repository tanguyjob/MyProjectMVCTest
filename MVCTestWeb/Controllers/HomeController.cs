using MVCTestWeb.Grids;
using MVCTestWeb.Helper;
using MVCTestWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTestWeb.Controllers
{
    public class HomeController : Controller
    {


        [HttpGet]
        public ActionResult Overview()
        {

            return View(new List<Utilisateur>());
            //var lstUser = Util.GetUtilisateur();
            //return View(lstUser);
            }

        [HttpGet]
        public ActionResult OverviewBook()
        {

            return View(new List<Livre>());
            //var lstUser = Util.GetUtilisateur();
            //return View(lstUser);
        }


        public ActionResult GetOverviewBook(GridDefaultSettings grid)
        {
            try
            {
                var list = Util.GetLivre();
                if (!string.IsNullOrEmpty(grid.Search))
                    list = list.FindAll(x => x.LivreName.Contains(grid.Search) || x.DateParution.Contains(grid.Search)).ToList();
                list = OrderLivre(grid.SortColumn, grid.SortOrder, list).ToList();

                var totalRecords = list.Count;
                var totalPages = (int)Math.Ceiling(totalRecords / (float)grid.Length);
                list = list.Skip(grid.Start).Take(grid.Length).ToList();
                var jsonData = new
                {
                    draw = grid.Draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecords,
                    start = grid.Start,
                    length = totalPages,
                    data = Util.GetDataTableContentBook(list, Url.Action("EditBook", "Home"))
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public JsonResult GetOverview(GridDefaultSettings grid)
        {
            try
            { 
            var list = Util.GetUtilisateur();
            if (!string.IsNullOrEmpty(grid.Search))
                list = list.FindAll(x => x.Utilisateurfirstname.Contains(grid.Search) || x.UtilisateurName.Contains(grid.Search)).ToList();

            //ne pas oublier de modifier l'order par après
            list = Order(grid.SortColumn, grid.SortOrder, list).ToList();


            var totalRecords = list.Count;
            var totalPages = (int)Math.Ceiling(totalRecords / (float)grid.Length);
                list = list.Skip(grid.Start).Take(grid.Length).ToList();
             var jsonData = new
            {
                draw = grid.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                start = grid.Start,
                length = totalPages,
                data = Util.GetDatatablesContentUtilisateur(list, Url.Action("Edit", "Home"), Url.Action("Delete", "Home"))
                

             };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
            catch(Exception ex)
            {
                throw;
            }

    }

        [HttpGet]
        public ActionResult EditBook(int id)
        {
            var lstBook = Util.GetLivre();
            if (lstBook.Exists(x => x.LivreID == id))
            {
                var bookToEdit = lstBook.Find(x => x.LivreID == id);
                return View(bookToEdit);
            }
            return RedirectToAction("OverviewBook");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBook([Bind(Include = "LivreID, LivreName, DateParution")] Livre model)
        {

            if (ModelState.IsValid)
            {

                var context = new DataBaseFirstDemoEntities();
                context.Entry(model).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("OverviewBook");

            }
            return View(model);


        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var lstUser = Util.GetUtilisateur();
            if (lstUser.Exists(x => x.UtilisateurID == id))
            {
                var userToEdit = lstUser.Find(x => x.UtilisateurID == id);
                return View(userToEdit);
            }
            return RedirectToAction("Overview");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "UtilisateurID, Utilisateurfirstname, UtilisateurName")] Utilisateur utilisateur)
        {

            if (ModelState.IsValid)
            {

                var context = new DataBaseFirstDemoEntities();
                context.Entry(utilisateur).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Overview");

            }
            return View(utilisateur);




            //String firstname = Utilisateur

            //if (ModelState.IsValid)
            //{
            //    var context = new DataBaseFirstDemoEntities();

            //    Utilisateur utlr = new Utilisateur()
            //    {
            //        Utilisateurfirstname = model.Utilisateurfirstname,
            //        UtilisateurName = model.UtilisateurName
            //    };
            //    context.Utilisateur.Add(utlr);
            //    context.SaveChanges();
            //    RedirectToAction("Overview");
            //}
            //return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            Livre livre = new Livre();
            return View(livre);
        }

        [HttpPost]
        public ActionResult Create(Livre model)
        {

            var list = Util.GetLivreToCreate();
            if (ModelState.IsValid)
            {
                model.LivreID = list.Max(x => x.LivreID) + 1;
                Util.AddBookDataBase(model);
                return RedirectToAction("OverviewBook");
            }
            return View(model);
        }

        public List<Utilisateur> Order(int column, string order, List<Utilisateur> list)
        {
            switch (column)
            {
                case 0:
                    return order != "desc" ? list.OrderBy(x => x.UtilisateurID).ToList() : list.OrderByDescending(x => x.UtilisateurID).ToList();
                case 1:
                    return order != "desc" ? list.OrderBy(x => x.Utilisateurfirstname).ToList() : list.OrderByDescending(x => x.Utilisateurfirstname).ToList();
                case 2:
                    return order != "desc" ? list.OrderBy(x => x.UtilisateurName).ToList() : list.OrderByDescending(x => x.UtilisateurName).ToList();
                default: return list;


            }
        }

        public List<LivreModel> OrderLivre(int column, string order, List<LivreModel> list)
        {
            switch (column)
            {
                case 0:
                    return order != "desc" ? list.OrderBy(x => x.LivreID).ToList() : list.OrderByDescending(x => x.LivreID).ToList();
                case 1:
                    return order != "desc" ? list.OrderBy(x => x.LivreName).ToList() : list.OrderByDescending(x => x.LivreName).ToList();
                case 2:
                    return order != "desc" ? list.OrderBy(x => x.DateParution).ToList() : list.OrderByDescending(x => x.DateParution).ToList();


                default: return list;


            }
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}       
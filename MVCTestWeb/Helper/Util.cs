using MVCTestWeb.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MVCTestWeb.Helper
{
    public static class Util
    {
        public static List<LivreModel> GetLivre()
        {
            using (DataBaseFirstDemoEntities context = new DataBaseFirstDemoEntities())
            {
                var livre = context.GetAllLivre();

                List<LivreModel> maliste = new List<LivreModel>();


                foreach (var livres in livre)
                {
                    if (livres.DateParution.HasValue)
                    {
                        maliste.Add(new LivreModel()
                        {

                                LivreID = livres.LivreID,
                                LivreName = livres.LivreName,
                                DateParution = livres.DateParution.Value.ToString("d", new CultureInfo("pt-BR"))
                        });
                    }
                    else {
                        maliste.Add(new LivreModel()
                        {
                            LivreID = livres.LivreID,
                            LivreName= livres.LivreName,
                            DateParution = " "
                        });
                          }
                }
                return maliste;
            }
        }

        public static List <Livre> GetLivreToCreate()
        {
            using (DataBaseFirstDemoEntities context = new DataBaseFirstDemoEntities())
            {
                var livre = context.GetAllLivre();

                List<Livre> maliste = new List<Livre>();


                foreach (var livres in livre)
                {
                   
                        maliste.Add(new Livre()
                        {

                            LivreID = livres.LivreID,
                            LivreName = livres.LivreName,
                            DateParution = livres.DateParution
                        });
                   
                }
                return maliste;
            }
        }

            public static List<Utilisateur> GetUtilisateur()
            {
                using (DataBaseFirstDemoEntities context = new DataBaseFirstDemoEntities())
                {
                    var utilisateur = context.GetAllUtilisateurs();

                    List<Utilisateur> maliste = new List<Utilisateur>();


                    foreach (var utilisateurs in utilisateur)
                    {
                        maliste.Add(new Utilisateur()
                        {

                            UtilisateurID = utilisateurs.UtilisateurID,
                            UtilisateurName = utilisateurs.UtilisateurName,
                            Utilisateurfirstname = utilisateurs.Utilisateurfirstname

                        });

                    }
                    return maliste;
                }
            }

            public static string[][] GetDataTableContentBook(List<LivreModel> listOverview, string editLink)
            {
                if (listOverview == null || !listOverview.Any())
                {
                    return (from i in new List<Livre>()
                            select new string[]
                            {}
                            ).ToArray();
                }

                var resultToReturn = (from i in listOverview
                                      select new String[]
                                      {
                                      i.LivreID.ToString(),
                                      i.LivreName,
                                      i.DateParution,
                                        $"<div><a href='{editLink + "/" + i.LivreID}' class='btn btn-primary'>Edit</a></div>"
                                      }
                                      ).ToArray();
                return resultToReturn;
            }





            public static string[][] GetDatatablesContentUtilisateur(List<Utilisateur> listOverview, string editLink, string editLink2)
            {
                if (listOverview == null || !listOverview.Any())
                {
                    return (from i in new List<Utilisateur>()
                            select new string[]
                            {
                            }).ToArray();
                }

                var resultToReturn = (from i in listOverview
                                      select new String[]
                                      {
                                      i.UtilisateurID.ToString(),
                                      i.UtilisateurName,
                                      i.Utilisateurfirstname,
                                      $"<div><a href='{editLink + "/" + i.UtilisateurID}' class='btn btn-primary'>Edit</a></div>",
                                      $"<div><a href='{editLink2 + "/" + i.UtilisateurID}' class='btn btn-primary'>Delete</a></div>"

                                      }).ToArray();
                return resultToReturn;
            }

        public static void AddBookDataBase(Livre model)
        {
            using (DataBaseFirstDemoEntities context = new DataBaseFirstDemoEntities())
            {
                Livre livre = new Livre()
                {
                    LivreID = model.LivreID,
                    LivreName = model.LivreName,
                    DateParution = model.DateParution
                };

                context.Livre.Add(livre);
                context.SaveChanges();


                //    SqlParameter ClientIdParameter = new SqlParameter("@PostId", 1);

                //    var result = context.Database.SqlQuery<Posts>("GetPostFromId", ClientIdParameter).ToList();


                //}

                //Posts post = new Posts()
                //{
                //    Body = "body",
                //    DatePublished = DateTime.Now,
                //    Title = "Title",
                //    PostID = 1
                //};
                //context.Posts.Add(post);
                //context.SaveChanges();
            }
        }

    }
    }
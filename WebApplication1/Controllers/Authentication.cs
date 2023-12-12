﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class Authentication : Controller
    {
        public ActionResult Authenticate(FormCollection fc)
        {
            string email = fc["email"];
            string password = fc["password"];

            databaseEntities2 authenticateduser = new databaseEntities2();

            // Assuming there is a 'users' DbSet in your database context
            user authenticatedUser = authenticateduser.users.FirstOrDefault(u => u.password == password && u.email == email);

            // Check if the user was found
            if (authenticatedUser != null)
            {
                if (authenticatedUser.role_id == 1)
                {
                    Session["first_name"] = authenticatedUser.first_name;
                    Session["last_name"] = authenticatedUser.last_name;
                    Session["user_id"] = authenticatedUser.user_id;
                  //  ViewData["admin"] = authenticatedUser;
                    return RedirectToAction("Admin");
                }
                else if (authenticatedUser.role_id == 2)
                {
                    Session["first_name"] = authenticatedUser.first_name;
                    Session["last_name"] = authenticatedUser.last_name;
                    Session["user_id"] = authenticatedUser.user_id;
                    // ViewData["user"] = authenticatedUser;
                    return RedirectToAction("UserPage");
                }

               
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid credentials. Please try again.";
            }

            // Always return the "Login" view, whether authentication succeeds or fails
            return View("Login");
        }
    }
}

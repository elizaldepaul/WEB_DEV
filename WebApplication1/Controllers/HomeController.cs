using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Layout()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AddActivities()
        {
            friendsEntities1 user = new friendsEntities1();

            var userList = (from a in user.users select a).ToList();

            ViewData["UserList"] = userList;
            return View();
        } 

       



        public ActionResult AddUserToDatabase(FormCollection fc)
        {
            String firstName = fc["firstname"];
            String lastName = fc["lastname"];
            String email = fc["email"];
            int age = Convert.ToInt16(fc["age"]);
            String address = fc["address"]; 
            String gender = fc["gender"];
            String password = fc["password"];

            user use = new user();
            use.first_name = firstName;
            use.last_name = lastName;
            use.age = age;
            use.address = address;
            use.gender = gender;
            use.email = email;
            use.password = password;
            use.role_id = 1;



            friendsEntities1 fe = new friendsEntities1();
            fe.users.Add(use);
            fe.SaveChanges();

            //insert the code that will save these information to the DB

            return RedirectToAction("ShowUser");
        }


      //  [HttpPost]
        public ActionResult UserUpdate()
        {
            //friendsEntities1 update_users = new friendsEntities1();
            // user u = update_users.users.FirstOrDefault(a => a.user_id == id);

           // int x = id;

            // if (u != null)
            // {
            //    u.first_name = newFirstName;
            // update_users.SaveChanges();
            //  }
            return View();
            // return RedirectToAction("UserUpdate"); // Redirect to the appropriate action or view
        }

        public ActionResult UserDelete()
        {
            friendsEntities1 rdbe = new friendsEntities1();
            user u = (from a in rdbe.users
                      where a.user_id == 2
                      select a).FirstOrDefault();
            rdbe.users.Remove(u);
            rdbe.SaveChanges();

            return View();
        }
        public ActionResult ShowUser()
        {
            friendsEntities1 user = new friendsEntities1();

            var userList = (from a in user.users select a).ToList();

            return View();
        } 



    }
}
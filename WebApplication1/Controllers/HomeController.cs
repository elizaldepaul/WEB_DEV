using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Authentication
    {
        public ActionResult Layout()
        {
            return View();
        }
        public ActionResult UserDashboard()
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
        public ActionResult Admin()
        {
            databaseEntities1 user = new databaseEntities1();

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
            use.role_id = 2;



            databaseEntities1 fe = new databaseEntities1();
            fe.users.Add(use);
            fe.SaveChanges();


            Session["user_id"] = use.users_id;
            Session["role_id"] = use.role_id;
            Session["first_name"] = use.first_name;
            Session["last_name"] = use.last_name;

            //insert the code that will save these information to the DB

            return RedirectToAction("User");
        }


        [HttpPost]
        public ActionResult UserUpdate(int id)
        {
            int x = id;



            databaseEntities1 user = new databaseEntities1();

            var selectedUser = (from a in user.users where a.users_id == x select a).ToList();
            ViewData["User"] = selectedUser;

            return View();
            //  return RedirectToAction("UserUpdate");  // Redirect to the appropriate action or view
        }

        public ActionResult Update(FormCollection fc, int id)
        {
            databaseEntities1 rdbe = new databaseEntities1();
            user u = (from a in rdbe.users
                      where a.users_id == id
                      select a).FirstOrDefault();

            String new_first_name = fc["new_first_name"];
            String new_last_name = fc["new_last_name"];
            int new_age = Convert.ToInt16(fc["new_age"]);
            String new_email = fc["new_email"];
            String new_gender = fc["new_gender"];
            String new_address = fc["new_address"];

            u.first_name = new_first_name;
            u.last_name = new_last_name;
            u.age = new_age;
            u.email = new_email;
            u.gender = new_gender;
            u.address = new_address;

            rdbe.SaveChanges();

            return RedirectToAction("Admin");
        }


        public ActionResult UserDelete(int id)
        {
            databaseEntities1 rdbe = new databaseEntities1();
            user u = (from a in rdbe.users
                      where a.users_id == id
                      select a).FirstOrDefault();
            rdbe.users.Remove(u);
            rdbe.SaveChanges();

            return RedirectToAction("Admin");
        }

        public ActionResult AddActivities(FormCollection activity)
        {
            String activity_name = activity["activity_name"];
            String activity_date = activity["activity_date"];
            String activity_time = activity["activity_time"];
            String activity_location = activity["activity_location"];
            String activity_ootd = activity["activity_ootd"];
            String date_created = activity["date_created"];

            activity act = new activity();

            // tiwason ni nako ugma daun HAHAHA para makatabang nako sa mag patabang mga
            // activity ni sya dre in case makalimot ko hehe HAHHAH 

            databaseEntities1 fe = new databaseEntities1();
            fe.users.Add(use);
            fe.SaveChanges();
            return View();
        }

    }
}
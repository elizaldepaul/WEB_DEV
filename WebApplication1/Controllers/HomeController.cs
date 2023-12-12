using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

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
            databaseEntities2 user = new databaseEntities2();

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



            databaseEntities2 fe = new databaseEntities2();
            fe.users.Add(use);
            fe.SaveChanges();


            Session["user_id"] = use.user_id;
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



            databaseEntities2 user = new databaseEntities2();

            var selectedUser = (from a in user.users where a.user_id == x select a).ToList();
            ViewData["User"] = selectedUser;

            return View();
            //  return RedirectToAction("UserUpdate");  // Redirect to the appropriate action or view
        }
        public ActionResult Update(FormCollection fc, int id)
        {
            databaseEntities2 rdbe = new databaseEntities2();
            user u = (from a in rdbe.users
                      where a.user_id == id
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
            databaseEntities2 rdbe = new databaseEntities2();
            user u = (from a in rdbe.users
                      where a.user_id == id
                      select a).FirstOrDefault();
            rdbe.users.Remove(u);
            rdbe.SaveChanges();

            return RedirectToAction("Admin");
        }
        public ActionResult AddActivities(FormCollection activity)
        {
            String activity_name = activity["activity_name"];

            // Parse only the date part from the string
            DateTime activity_date = Convert.ToDateTime(activity["activity_date"]);
            var actdate = activity_date.Date;

            // Convert the string to TimeSpan
            TimeSpan activity_time;
            if (!TimeSpan.TryParse(activity["activity_time"], out activity_time))
            {
                // Handle invalid time format (e.g., show an error message to the user)
                return View("Error");
            }

            String activity_location = activity["activity_location"];
            String activity_ootd = activity["activity_ootd"];
            int id = Convert.ToInt16(activity["user_id"]);

            activity act = new activity();

            act.activity_name = activity_name;
            act.activity_date = actdate; // Assign the parsed date
            act.activity_time = activity_time;
            act.activity_location = activity_location;
            act.activity_ootd = activity_ootd;
            act.user_id = id;

            //DateTime activity_date = DateTime.Parse("12/12/2023");
            // string formattedDate = activity_date.ToString("yyyy-MM-dd");

            // Now, 'formattedDate' contains the formatted date "2023-12-12"


            // tiwason ni nako ugma daun HAHAHA para makatabang nako sa mag patabang mga
            // activity ni sya dre in case makalimot ko hehe HAHHAH 

            databaseEntities2 fe = new databaseEntities2();
            fe.activities.Add(act);
            fe.SaveChanges();
            return View();
        }
        [HttpPost]
        public ActionResult Activity(int id)
        {
            int x = id;
            databaseEntities2 act = new databaseEntities2();
            var activity = (from a in act.activities where a.activity_id == x select a).ToList();
            ViewData["Activity"] = activity;
            return View();

        }
        public ActionResult ActivityUpdate(FormCollection activity, int id)
        {
            String activity_name = activity["activity_name"];

            // Parse only the date part from the string
            DateTime activity_date = Convert.ToDateTime(activity["activity_date"]);

            // Parse only the date part from the string




            // Convert the string to TimeSpan
            TimeSpan activity_time;
            if (!TimeSpan.TryParse(activity["activity_time"], out activity_time))
            {
                // Handle invalid time format (e.g., show an error message to the user)
                return View("Error");
            }

            String activity_location = activity["activity_location"];
            String activity_ootd = activity["activity_ootd"];

            activity act = new activity();

            act.activity_name = activity_name;
            act.activity_date = activity_date.Date; // Assign the parsed date
            act.activity_time = activity_time;
            act.activity_location = activity_location;
            act.activity_ootd = activity_ootd;
            databaseEntities2 act1 = new databaseEntities2();

            act1.SaveChanges();

            return RedirectToAction("User");
        } 
        public ActionResult UserPage()
        {
            // Retrieve the user ID from the session
            int userId = (int)Session["user_id"];

            databaseEntities2 act = new databaseEntities2();

            // Select all activities based on the user ID
            var userActivities = (from a in act.activities
                                  where a.user_id == userId
                                  select a).ToList();

            ViewData["ActivityList"] = userActivities;

            return View();
        }
        public ActionResult SetActivity(int id)
        {

            databaseEntities2 act = new databaseEntities2();

            // Select all activities based on the user ID
            var activity = (from a in act.activities
                                  where a.activity_id == id
                                  select a).ToList();

            ViewData["Activity"] = activity;

            return View();
        }

        public ActionResult MarkAsDone(int id)
        {
            int x = id;
            databaseEntities2 act = new databaseEntities2();
            var done = (from a in act.activities where a.activity_id == x select a).ToList();
            ViewData["Activity"] = done
                ;
            return View();

        }
    }


}
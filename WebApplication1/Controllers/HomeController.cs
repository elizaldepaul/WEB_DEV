using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
            databaseEntities1 db = new databaseEntities1();

            var announcementList = (from a in db.announcements select a).ToList();
            ViewData["announcementList"] = announcementList;

           

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

            var announcementList = (from a in user.announcements select a).ToList();

            ViewData["announcementList"] = announcementList;

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


            Session["user_id"] = use.user_id;
            Session["role_id"] = use.role_id;
            Session["first_name"] = use.first_name;
            Session["last_name"] = use.last_name;

            //insert the code that will save these information to the DB

            return RedirectToAction("UserDashboard");
        }
        [HttpPost]
        public ActionResult UserUpdate(int id)
        {
            int x = id;



            databaseEntities1 user = new databaseEntities1();

            var selectedUser = (from a in user.users where a.user_id == x select a).ToList();
            ViewData["User"] = selectedUser;

            return View();
            //  return RedirectToAction("UserUpdate");  // Redirect to the appropriate action or view
        }

        public ActionResult Update(FormCollection fc, int id)
        {
            databaseEntities1 rdbe = new databaseEntities1();
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
            databaseEntities1 rdbe = new databaseEntities1();
            user u = (from a in rdbe.users
                      where a.user_id == id
                      select a).FirstOrDefault();
            rdbe.users.Remove(u);
            rdbe.SaveChanges();

            return RedirectToAction("Admin");
        }

        public ActionResult AddActivities(FormCollection actvs)
        {
            String activity_name = actvs["activity_name"];
            String activity_date = actvs["activity_date"];
            String activity_status = "Pending";
            TimeSpan activity_time;
            if (!TimeSpan.TryParse(actvs["activity_time"], out activity_time))
            {
                return View("Error");
            }
            String activity_location = actvs["activity_location"];
            String activity_ootd = actvs["activity_ootd"];
            int id = Convert.ToInt16(actvs["user_id"]);
            activity act = new activity();
            act.activity_name = activity_name;
            act.activity_date = activity_date; // Assign the parsed date
            act.activity_time = activity_time;
            act.activity_status = activity_status;
            act.activity_location = activity_location;
            act.activity_ootd = activity_ootd;
            act.user_id = id;
            databaseEntities1 fe = new databaseEntities1();
            fe.activities.Add(act);
            fe.SaveChanges();
            return RedirectToAction("UserPage");
        }
        [HttpPost]
        public ActionResult Activity(int id)
        {
            using (databaseEntities1 actv = new databaseEntities1())
            {
                List<activity> pendingActivities = actv.activities
                    .Where(a => a.activity_id == id)
                    .ToList();
                return View(pendingActivities);
            }
        }

        public ActionResult ActivityUpdate(FormCollection act, int id)
        {
            databaseEntities1 rdbe = new databaseEntities1();
            activity acts = (from a in rdbe.activities
                             where a.activity_id == id
                             select a).FirstOrDefault();



            string activity_name = act["activity_name"];
            string activity_date = act["activity_date"];
            TimeSpan activity_time;
            if (!TimeSpan.TryParse(act["activity_time"], out activity_time))
            {
                // Handle invalid time format (e.g., show an error message to the user)
                return View("Error");
            }



            string activity_ootd = act["activity_ootd"];
            string activity_location = act["activity_location"];


            acts.activity_name = activity_name;
            acts.activity_date = activity_date;
            acts.activity_time = activity_time;
            acts.activity_ootd = activity_ootd;
            acts.activity_location = activity_location;


            rdbe.SaveChanges();

            return RedirectToAction("UserPage");
        }

        public ActionResult UserPage()
        {
            // Retrieve the user ID from the session
            int userId = (int)Session["user_id"];

            databaseEntities1 act = new databaseEntities1();

            // Select all activities based on the user ID
            var userActivities = (from a in act.activities
                                  where a.user_id == userId
                                  select a).ToList();

            ViewData["ActivityList"] = userActivities;

            return View();
        }

        public ActionResult SetActivity(int id)
        {

            databaseEntities1 act = new databaseEntities1();

            // Select all activities based on the user ID
            var activity = (from a in act.activities
                            where a.activity_id == id
                            select a).ToList();

            ViewData["Activity"] = activity;

            return View();
        }

        public ActionResult SetAct(FormCollection set)
        {
            int id = Convert.ToInt32(set["id"]);
            String activity_remarks = set["activity_remarks"];
            String activity_status = set["activity_status"];

            using (databaseEntities1 actv = new databaseEntities1())
            {
                activity existingActivity = actv.activities.Find(id);
                if (existingActivity != null)
                {
                    existingActivity.activity_status = activity_status;
                    existingActivity.remarks = activity_remarks;
                    actv.SaveChanges();
                }
            }

            return RedirectToAction("UserPage");
        }

        public ActionResult GetDoneActivities()
        {
            using (databaseEntities1 actv = new databaseEntities1())
            {
                var user_id = (int)Session["user_id"];
                // Assuming "activities" is the DbSet property in your database context
                List<activity> doneActivities = actv.activities
                     .Where(a => a.user_id == user_id)
                    .ToList();

                return View(doneActivities);
            }
        }

        public ActionResult GetCancelledActivities()
        {
            using (databaseEntities1 actv = new databaseEntities1())
            {
                var user_id = (int)Session["user_id"];
                // Assuming "activities" is the DbSet property in your database context
                List<activity> cancelledActivities = actv.activities
                     .Where(a => a.user_id == user_id)
                    .ToList();

                return View(cancelledActivities);
            }
        }
        public ActionResult Logout()
        {
            // Sign out the user
            FormsAuthentication.SignOut();

            // Clear session variables if needed
            Session.Clear();
            Session.Abandon();

            // Redirect to the login page or any other page after logout
            return RedirectToAction("Layout"); // Change "Login" and "Account" to your actual login page and controller
        }


        public ActionResult AddAnnouncement(FormCollection ann)
        {
            
                string announcementText = ann["announcement"];
                string announcementTitle = ann["announcement_title"];

                announcement announce = new announcement();
                announce.announcement1 = announcementText;
                announce.announcement_title = announcementTitle;

                using (databaseEntities1 fe = new databaseEntities1())
                {
                    fe.announcements.Add(announce);
                    fe.SaveChanges();
                }

                // Redirect to the Announcements page (assuming you have an Announcements controller with an Index action)
                return RedirectToAction("Admin" );
          
        }

    }

}



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

        public ActionResult AddUserToDatabase(FormCollection fc)
        {
            String firstName = fc["firstname"];
            String lastName = fc["lastname"];
            String email = fc["email"];
            int age = int.Parse(fc["age"]);
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



            final_projectEntities fe = new final_projectEntities();
            fe.users.Add(use);
            fe.SaveChanges();

            //insert the code that will save these information to the DB

            return RedirectToAction("Layout");
        }


        public ActionResult UserUpdate()
        {
            final_projectEntities update_users = new final_projectEntities();
            user u = (from a in update_users.users where a.user_id == 2 select a).FirstOrDefault();

            u.first_name = "Paul HEnry";
            u.last_name = "Elizalde";
            u.email = "paul@gmail.com";
            u.address = "bantayan";
            u.age = 20;
            u.gender = "male";
            u.password = "123";
            u.role_id = 1;

            update_users.SaveChanges();

            return View();

        }
        public ActionResult UserDelete()
        {
            final_projectEntities rdbe = new final_projectEntities();
            user u = (from a in rdbe.users
                      where a.user_id == 2
                      select a).FirstOrDefault();
            rdbe.users.Remove(u);
            rdbe.SaveChanges();

            return View();
        }
        public ActionResult ShowUser()
        {
            final_projectEntities user = new final_projectEntities();

            var userList = (from a in user.users select a).ToList();

            return View();
        } 



    }
}
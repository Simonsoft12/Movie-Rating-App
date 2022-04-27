using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLibrary;
using WebApp.Models;
using static DataLibrary.BusinessLogic.UserProcessor;
using System.Configuration;
using System.Data;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index(Models.UserModel model)
        {
            ViewBag.Message = "Users List";

            var data = LoadUsers();
            List<Models.UserModel> users = new List<Models.UserModel>();

            foreach (var row in data)
            {
                users.Add(new Models.UserModel
                {
                    id = row.id,
                    name = row.name,
                    surname = row.surname
                });
            }

            return View(users);
        }

        // Users/AddUser
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(Models.UserModel model)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = CreateUser(
                    model.name,
                    model.surname);
                return RedirectToAction("Index");
            }
            return View();
        }


        // Users/Edit/1
        public ActionResult Edit(int id)
        {
            var data = LoadUsers(id);
            List<Models.UserModel> users = new List<Models.UserModel>();

            foreach (var row in data)
            {
                users.Add(new Models.UserModel
                {
                    id = row.id,
                    name = row.name,
                    surname = row.surname
                });
            }
            UserModel user = new UserModel();
            user = users.First();

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Models.UserModel model)
        {
            if (model == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                int recordsCreated = UpdateUser(
                    model.id,
                    model.name,
                    model.surname);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            DeleteUser(id);

            return RedirectToAction("Index");
        }
    }
}
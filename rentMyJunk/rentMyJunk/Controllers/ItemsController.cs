﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using rentMyJunk.Models;
using rentMyJunk.ViewModels;
using System.Threading.Tasks;

namespace rentMyJunk.Controllers
{
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ItemRepository repo = new ItemRepository();

        // GET: Items
        // by cat - ano, user
        // all - ano, user
        // my stuff only - user

        public ActionResult Index(string category, string userId)
        {
            var items = repo.GetItemsAsync().Result;

            if (items != null)
            {
                if (string.IsNullOrEmpty(category))
                {
                    if (string.IsNullOrEmpty(userId))
                    {
                        ViewBag.header = "Available Junk";
                        return View(items.ToList());
                    }

                    ViewBag.header = "Your Junk";
                    items = items.Where(i => i.ownerId == userId).ToList();
                    return View(items.ToList());
                }
                else 
                {
                    items = items.Where(i => i.category == category).ToList();
                    if (string.IsNullOrEmpty(userId))
                    {
                        ViewBag.header = "All Junk in the " + category + " category";
                        return View(items.ToList());
                    }

                    ViewBag.header = "All your Junk in the " + category + " category";
                    items = items.Where(i => i.ownerId == userId).ToList();
                    return View(items.ToList());
                }
            }
            return HttpNotFound();
        }

        // GET: Items/Details/5
        public ActionResult Details(string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.urlReferrer = Request.UrlReferrer; 

            Item item = repo.GetItemAsync(id).Result;
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }


      [HttpPost]
        public ActionResult Create(ItemViewModel item)
        {
            if (ModelState.IsValid)
            {
                item.ownerId = User.Identity.Name;

                if (item.ImgFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        item.ImgFile.InputStream.CopyTo(ms);
                        Task t  = repo.SaveItem(item, ms);
                        t.Wait();
                    }
                }

                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = this.repo.GetItemAsync(id).Result;
           
            if (item == null)
            {
                return HttpNotFound();
            }
            var itemvm = new ItemViewModel()
            {
                category = item.category,
                description = item.description,
                ownerId = item.ownerId,
                id = item.id,
                imageUri = item.imageUri,
                isAvailable = item.isAvailable,
                ratePerDay = item.ratePerDay
            };
            return View(itemvm);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(ItemViewModel item)
        {
            if (ModelState.IsValid)
            {
                item.ownerId = User.Identity.Name;

                if (item.ImgFile != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        item.ImgFile.InputStream.CopyTo(ms);
                        Task t = repo.UpdateItemAsync(item, ms);
                        t.Wait();
                    }
                }

                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
       
    }
}

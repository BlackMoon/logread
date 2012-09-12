using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fix.Models;
using System.IO;
using fix.Helpers;

namespace fix.Controllers
{
    public enum eOrder              // направление сортировки
    {
        ordASC,
        ordDESC
    }    
    
    public class HomeController : Controller
    {
        private fixdb DB = new fixdb();        

        public ActionResult Index(string query, string param, eOrder? ord)
        {   
            List<fx_doc> docs = (from fx_doc in DB.fx_docs select fx_doc).ToList();
            if (!string.IsNullOrEmpty(query))
                docs = docs.Where(f => (f.name.Contains(query) || f.author.Contains(query)) ).ToList();

            if (!string.IsNullOrEmpty(param))
            {
                docs = docs.OrderBy(param + ((ord == eOrder.ordASC) ? " ASC" : " DESC")).ToList();

                ViewData["sort"] = param;

                if (param == "name")
                    ViewData["ord1"] = 1 - ord;
                else if (param == "date")
                    ViewData["ord2"] = 1 -ord;
                else if (param == "author")
                    ViewData["ord3"] = 1 - ord;
                
            }
            return View(docs);
        }        

        public ActionResult Create()
        {
            var doc = new fx_doc();
            return View(doc);
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase f)
        {
            fx_doc doc = new fx_doc();
            try
            {
                if (f == null) throw new Exception("Файл не задан");
                DB.createNew(f.FileName, User.Identity.Name);                

                using (FileStream fs = System.IO.File.Create((string)System.Web.HttpContext.Current.Application["docs"] + f.FileName))
                {
                    f.InputStream.CopyTo(fs);
                    fs.Close();
                }
                
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View(doc);
        }

        public ActionResult Delete(int id)
        {
            var doc = (from fx_doc in DB.fx_docs where fx_doc.id == id select fx_doc).First();
            return View(doc);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var doc = (from fx_doc in DB.fx_docs where fx_doc.id == id select fx_doc).First();
            
            try
            {
                DB.DeleteObject(doc);
                DB.SaveChanges();
                System.IO.File.Delete((string)System.Web.HttpContext.Current.Application["docs"] + doc.name);   
            }
            catch
            {
                return View(doc);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Sort(string param, eOrder ord)
        {   
            List<fx_doc> docs = (from fx_doc in DB.fx_docs select fx_doc).ToList();
            docs = docs.OrderBy(param + ((ord == eOrder.ordASC) ? " ASC" : " DESC")).ToList();  

            return RedirectToAction("Index");
        }        
    }
}

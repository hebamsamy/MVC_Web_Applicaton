using Ecomaerce.Models;

using Ecomaerce.repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


using System.Globalization;
using System.Security.Claims;



using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ecomaerce.Models;
using System.IO;

namespace Ecomaerce.Controllers
{
    [Authorize(Roles= "Admain")]
    public class AdminController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.Name });
            }
            return list;
        }
        public List<SelectListItem> GetRole()
        {
            ApplicationDbContext db = new ApplicationDbContext();
          
           
            
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = db.Roles.ToList();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.Name.ToString(), Text = item.Name });
            }
            return list;
        }
        public ActionResult Dashboard()
        {
            return View();
        }


        public ActionResult Categories()
        {
            List<Category> allcategories = _unitOfWork.GetRepositoryInstance<Category>().GetAllRecordsIQueryable().Where(i => i.IsDeleted == false).ToList();
            return View(allcategories);
        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            _unitOfWork.GetRepositoryInstance<Category>().Add(category);
            return RedirectToAction("Categories");
        }

        public ActionResult UpdateCategory(int categoryid)
        {
            Category cd;
            if (categoryid != 0)
            {
                cd = JsonConvert.DeserializeObject<Category>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Category>().GetFirstorDefault(categoryid)));
            }
            else
            {
                cd = new Category();
            }
            return View("UpdateCategory", cd);
        }

        public ActionResult EditCategory(int categoryid)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Category>().GetFirstorDefault(categoryid));
        }
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            _unitOfWork.GetRepositoryInstance<Category>().Update(category);
            return RedirectToAction("Categories");
        }



        public ActionResult Products()
        {
            return View(_unitOfWork.GetRepositoryInstance<Product>().GetProduct());
        }

        public ActionResult EditProducts(int productid)
        {
            ViewBag.CategoryList = GetCategory();
            return View(_unitOfWork.GetRepositoryInstance<Product>().GetFirstorDefault(productid));
        }
        [HttpPost]
        public ActionResult EditProducts(Product product, HttpPostedFileBase ufile)
        {
            //ufile = Request.Files[0];
            string pic = null;
            if (ufile != null && ufile.ContentLength > 0)
            {
                pic = Path.GetFileName(ufile.FileName);
                string path = Path.Combine(Server.MapPath("~/productimages/"), pic);
                ufile.SaveAs(path);
            }
            product.Image = ufile != null ? pic : product.Image;
            product.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Product>().Update(product);
            return RedirectToAction("Products");
        }


        public ActionResult AddNewProduct()
        {
            ViewBag.CategoryList = GetCategory();
            return View();
        }
        [HttpPost]
        public ActionResult AddNewProduct(Product product, HttpPostedFileBase ufile)
        {
            string pic = null;
            if (ufile != null)
            {
                pic = System.IO.Path.GetFileName(ufile.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/productimages/"), pic);
                ufile.SaveAs(path);
            }
            product.Image = ufile != null ? pic : product.Image;
            product.CreateDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Product>().Add(product);
            return RedirectToAction("Products");
        }
        public ActionResult Register()
        {
           // ViewBag.roleList = GetRole();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult Register(RegisterViewModel model)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var UsereManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
           

            var check = UsereManager.Create(user, model.Password);
            if (check.Succeeded)
            {
                UsereManager.AddToRole(user.Id, "Admain");
                return RedirectToAction("Index", "Home");
            }

          

            // If we got this far, something failed, redisplay form
            return View(model);
        }


    }
}
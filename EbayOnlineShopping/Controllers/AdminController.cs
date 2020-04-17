using EbayOnlineShopping.DAL;
using EbayOnlineShopping.Models;
using EbayOnlineShopping.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EbayOnlineShopping.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> catlist = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<tblCategory>().GetAllRecords();
            foreach (var item in cat)
            {
                catlist.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });

            }
            return catlist;
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<tblCategory> allcategories = _unitOfWork.GetRepositoryInstance<tblCategory>()
                             .GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }
        public ActionResult UpdateCategory(int CategoryId)
        {
            CategoryModel categoryModel;
            if (CategoryId != null)
            {
                categoryModel = JsonConvert.DeserializeObject<CategoryModel>
                    (JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<tblCategory>().GetFirstOrDefault(CategoryId)));
            }
            else
                categoryModel = new CategoryModel();

            return View("UpdateCategory", categoryModel);
        }
        [HttpGet]
        public ActionResult CategorytEdit(int categoryId)
        {

            return View(_unitOfWork.GetRepositoryInstance<tblCategory>().GetFirstOrDefault(categoryId)); ;
        }

        [HttpPost]
        public ActionResult CategoryEdit(tblCategory category)
        {

            _unitOfWork.GetRepositoryInstance<tblCategory>().Update(category);
            return RedirectToAction("Categories");
        }


        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<tblProduct>().GetProduct());
        }

        [HttpGet]
        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();

            return View(_unitOfWork.GetRepositoryInstance<tblProduct>().GetFirstOrDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(tblProduct product, HttpPostedFileBase photofile)
        {
            string picture = null;
            if (photofile != null)
            {
                picture = System.IO.Path.GetFileName(photofile.FileName);
                string filepath = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"), picture);
                //file is uploaded
                photofile.SaveAs(filepath);

            }
            product.ProductImage = photofile != null ? picture : product.ProductImage;
            product.ModifiedDate = DateTime.Now;

            _unitOfWork.GetRepositoryInstance<tblProduct>().Update(product);
            return RedirectToAction("Product");
        }

        [HttpGet]
        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();

            return View();
        }

        //file upload methods

        //string PhotoName = Path.GetFileNameWithoutExtension(photofile.FileName);
        //string PhotoExtension = Path.GetExtension(photofile.FileName);
        ////Add Current Date and time To Attached File Name  
        //PhotoName = DateTime.Now.ToString("yyyyMMddhhmmss") + "-" + PhotoName.Trim() + PhotoExtension;
        //string PhotoPath = Path.Combine(Server.MapPath("~/ProductImages"), PhotoName);
        //photofile.SaveAs(PhotoPath);

        // or this code for file upload


        //var photofile = Request.Files[0];
        //string picture = null;
        //if (photofile != null)
        //{
        //    picture = System.IO.Path.GetFileName(photofile.FileName);
        //    string filepath = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"), picture);
        //    //file is uploaded
        //    photofile.SaveAs(filepath);
        //}

        [HttpPost]
        public ActionResult ProductAdd(tblProduct model, HttpPostedFileBase photofile)
        {
            if (ModelState.IsValid)
            {
                // following code  is file upload control
                if (HttpContext.Request.Files.AllKeys.Any())
                {
                    var httpPostedFile = HttpContext.Request.Files[0];
                    if (photofile != null)
                    {
                        // validation uploaded images(optional)
                        //Get the complete file path
                        string filePath = HttpContext.Server.MapPath("~/ProductImages/");
                        //save the uploaded file to productimages folder
                        photofile.SaveAs(filePath);
                    }
                }

                ////model.ProductImage = PhotoName;
                model.ProductImage = photofile.ToString();
                //model.ProductImage = photofile.ToString();
                //model.CreatedDate = DateTime.Now;

            }
            _unitOfWork.GetRepositoryInstance<tblProduct>().Add(model);
            return RedirectToAction("Product");

        }

        public ActionResult Orders()
        {
            return View();
        }

    }
}
using dbFirst.DAL;
using dbFirst.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace dbFirst.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL _productDAL = new ProductDAL();

        // GET: Product
        public ActionResult Index ()
        {
            var productList = _productDAL.GetAllProducts();

            if (productList.Count == 0)
            {
                TempData["nodatamsg"] = "Currently Products Are Not Available.";
            }
            return View(productList);
        }

        // GET: Product/Details/5
        public ActionResult Details (int id)
        {
            try
            {
                var products = _productDAL.GetProductByID(id).FirstOrDefault();
                if (products == null)
                {
                    TempData["infoMsg"] = "Product Not Available !";
                    return RedirectToAction("Index");
                }
                return View(products);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Oh No ! Some Error Occure" + ex.Message;
                return View();
            }
        }

        // GET: Create
        public ActionResult Create ()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create (Product product)
        {

            bool IsInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = _productDAL.InsertProducts(product);

                    if (IsInserted)
                    {
                        TempData["SuccessMsg"] = "Product Saved Successfully.";
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Product Record Already Exist!";
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Oh No ! Some Error Occure" + ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit (int id)
        {
            var products = _productDAL.GetProductByID(id).FirstOrDefault();
            if (products == null)
            {
                TempData["infoMsg"] = "Product Not Available !";
                return RedirectToAction("Index");
            }

            return View(products);
        }

        // POST: Product/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateProduct (Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _productDAL.UpdateProduct(product);

                    if (IsUpdated)
                    {
                        TempData["SuccessMsg"] = "Product Updated Successfully.";
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Product Record Already Exist! / Failed To Update";
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMsg"] = "Oh No ! Some Error Occure" + ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete (int id)
        {
            try
            {
                var product = _productDAL.GetProductByID(id).FirstOrDefault();

                if (product == null)
                {
                    TempData["infoMsg"] = "Product Not Available !";
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Oh No ! Some Error Occure" + ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation (int id)
        {
            try
            {
                string result = _productDAL.DeleteProduct(id);
                if (result.Contains("Deleted"))
                {
                    TempData["SuccessMsg"] = result;
                }
                else
                {
                    TempData["ErrorMsg"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMsg"] = "Oh No ! Some Error Occure" + ex.Message;
                return View();
            }
        }
    }
}

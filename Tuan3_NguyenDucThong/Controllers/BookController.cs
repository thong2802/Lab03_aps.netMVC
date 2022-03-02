using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan3_NguyenDucThong.Models;

namespace Tuan3_NguyenDucThong.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        DataClasses1DataContext _data = new DataClasses1DataContext();
        public ActionResult Index()
        {
            var all_Book = from book in _data.Saches select book;
            return View(all_Book);
        }

        // Detail
        public ActionResult DeTail(int id)
        {
            var D_sach = _data.Saches.Where(m => m.masach == id).First();
            return View(D_sach);
        }
        // Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Sach sach)
        {
            var name = collection["sach"];
            var hinh = collection["hinh"];
            var maloai = collection["maloai"];
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(hinh) )
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                sach.maloai = Int32.Parse(maloai);
                _data.Saches.InsertOnSubmit(sach);
                sach.tensach = name;
                _data.Saches.InsertOnSubmit(sach);
                sach.hinh = hinh;
                _data.Saches.InsertOnSubmit(sach);
                _data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        // Edit
        public ActionResult Edit(int id)
        {
            var E_category = _data.Saches.First(m => m.maloai == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection collection, int id)
        {
            var sach = _data.Saches.First(m => m.maloai == id);
            var name = collection["sach"];
            var hinh = collection["hinh"];
            var maloai = collection["maloai"];
            sach.masach = id;
            if (string.IsNullOrEmpty(name))
            {

                ViewData["Error"] = "Don't empty";
            }
            else
            {
                sach.tensach = name;;
                UpdateModel(sach);
                sach.tensach = maloai; ;
                UpdateModel(sach);
                sach.tensach = hinh; ;
                UpdateModel(sach);
                _data.SubmitChanges();
                return RedirectToAction("Index");
            } 
            return this.Edit(id);
        }
        // Delete
        public ActionResult Delete(int id)
        {
            var D_theloai = _data.Saches.First(m => m.maloai == id);

            return View(D_theloai);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_sach = _data.TheLoais.Where(m => m.maloai == id).First();
            _data.TheLoais.DeleteOnSubmit(D_sach);
            _data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}
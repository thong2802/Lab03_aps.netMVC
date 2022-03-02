using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan3_NguyenDucThong.Models;

namespace Tuan3_NguyenDucThong.Controllers
{
    public class TheLoaiController : Controller
    {
        // GET: TheLoai 
        DataClasses1DataContext _data = new DataClasses1DataContext();
        public ActionResult Index()
        {
           var all_TheLoai = from tt in _data.TheLoais select tt;
            return View(all_TheLoai);
        }

        // Detail
        public ActionResult DeTail (int id)
        {
            var D_theLoai = _data.TheLoais.Where(m => m.maloai == id).First();
            return View(D_theLoai);
        }
        // Create
        public ActionResult Create ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, TheLoai tL)
        {
            var  name = collection["theloai"];
            if(string.IsNullOrEmpty(name))
            {
                ViewData["Error"] = "Don't empty";
            }
            else
            {
                tL.tenloai = name;
                _data.TheLoais.InsertOnSubmit(tL);
                _data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        // Edit
        public ActionResult Edit(int id)
        {
            var E_category = _data.TheLoais.First(m => m.maloai == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit (FormCollection collection, int id)
        {
            var theloai = _data.TheLoais.First (m => m.maloai == id);
            var E_tenLoai = collection["tenloai"];
            theloai.maloai = id;
            if(string.IsNullOrEmpty(E_tenLoai))
            {

                ViewData["Error"] = "Don't empty";
            }
            else
            {
                theloai.tenloai = E_tenLoai;
                UpdateModel(theloai);
                _data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        // Delete
        public ActionResult Delete(int id)
        {
            var D_theloai = _data.TheLoais.First(m=>m.maloai==id);  

            return View(D_theloai);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_theloai = _data.TheLoais.Where(m => m.maloai == id).First();
            _data.TheLoais.DeleteOnSubmit(D_theloai);
            _data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}
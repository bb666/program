using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BodyInfoManagement.Models;

namespace BodyInfoManagement.Controllers
{
    public class TeachersController : Controller
    {
        private BodyinfoModel db = new BodyinfoModel();

        // GET: Teachers
        public async Task<ActionResult> Index(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return View(await db.Teachers.Where(s => s.TeacherId.Contains(id)).ToListAsync());
            }
            return View(await db.Teachers.ToListAsync());
        }

        // GET: Teachers/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "TeacherId,SaltCode,TeacherPassword,ConfirmPassword,TeacherEmail")] Teacher teacher)
        {
            if (teacher.TeacherPassword != teacher.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "密码和确认密码不匹配");
            }

            teacher.StudentAuth = false;
            if (ModelState.IsValid)
            {
                teacher.SaltCode = Guid.NewGuid().ToString("N");
                teacher.TeacherPassword = HomeController.EncipherSha(teacher.SaltCode + teacher.TeacherPassword);
                db.Teachers.Add(teacher);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "TeacherId,SaltCode,TeacherPassword,TeacherEmail,StudentAuth")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacher).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = await db.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Teacher teacher = await db.Teachers.FindAsync(id);
            db.Teachers.Remove(teacher);
            await db.SaveChangesAsync();
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

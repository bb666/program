using BodyInfoManagement.Models;
using BodyInfoManagement.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BodyInfoManagement.Controllers
{
    public class HomeController : Controller
    {
        private BodyinfoModel db = new BodyinfoModel();
        public ActionResult Index()
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

        public ActionResult Administrators()
        {
            return View();
        }

        public ActionResult Teachers()
        {
            return View();
        }

        public ActionResult Students()
        {
            return View();
        }

        public async Task<ActionResult> BackTeachersAuth()
        {
            var todo = db.Teachers.Where(s => s.StudentAuth);
            foreach (var item in todo)
            {
                item.StudentAuth = false;
            }
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Teachers");
        }

        public async Task<ActionResult> SetTeachersAuth()
        {
            var todo = db.Teachers.Where(s => !s.StudentAuth);
            foreach (var item in todo)
            {
                item.StudentAuth = true;
            }
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Teachers");
        }

        public async Task<ActionResult> BackStudentsAuth()
        {
            var todo = db.Students.Where(s => s.StudentAuth);
            foreach (var item in todo)
            {
                item.StudentAuth = false;
            }
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Students");
        }
        public async Task<ActionResult> SetStudentsAuth()
        {
            var todo = db.Students.Where(s => !s.StudentAuth);
            foreach(var item in todo)
            {
                item.StudentAuth = true;
            }
            await db.SaveChangesAsync();

            return RedirectToAction("Index", "Students");
        }
        public static string EncipherSha(string sourceText)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();
            byte[] source = Encoding.Default.GetBytes(sourceText);
            return Convert.ToBase64String(sha256.ComputeHash(source));
        }

        public void WriteSession(int type, string id, bool auth)
        {
            Session["NowUserId"] = id;
            Session["NowUserType"] = type;
            Session["NowUserAuth"] = auth;
        }

        public ActionResult Logoff()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginBag)
        {
            switch ((int)loginBag.LoginType)
            {
                case 0:
                    {
                        var find = await db.Teachers.FindAsync(loginBag.LoginAccount);
                        if (find == null)
                        {
                            ModelState.AddModelError("LoginAccount", "用户不存在");
                            return View(loginBag);
                        }
                        if (find.TeacherPassword == EncipherSha(find.SaltCode + loginBag.LoginPassword))
                        {
                            WriteSession(0, find.TeacherId, find.StudentAuth);
                            return RedirectToAction("Teachers");
                        }
                        ModelState.AddModelError("LoginPassword", "密码错误");
                        return View(loginBag);
                    }
                case 1:
                    {
                        var find = await db.Students.FindAsync(loginBag.LoginAccount);
                        if (find == null)
                        {
                            ModelState.AddModelError("LoginAccount", "用户不存在");
                            return View(loginBag);
                        }
                        if (find.StudentPassword == EncipherSha(find.SaltCode + loginBag.LoginPassword))
                        {
                            WriteSession(0, find.StudentId, find.StudentAuth);
                            return RedirectToAction("Students");
                        }
                        ModelState.AddModelError("LoginPassword", "密码错误");
                        return View(loginBag);
                    }
                case 2:
                    {
                        var find = await db.Administrators.FindAsync(loginBag.LoginAccount);
                        if (find != null)
                        {
                            if (find.AdministratorPassword == loginBag.LoginPassword)
                            {
                                WriteSession(0, find.AdministratorId, true);
                                return RedirectToAction("Administrators");
                            }
                        }

                        ModelState.AddModelError("", "用户名或密码错误");
                        return View(loginBag);
                    }
                default: break;
            }
            return RedirectToAction("Index");
        }
    }
}
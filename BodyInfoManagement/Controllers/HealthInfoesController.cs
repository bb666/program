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
using System.Web.Helpers;

namespace BodyInfoManagement.Controllers
{
    public class HealthInfoesController : Controller
    {
        private BodyinfoModel db = new BodyinfoModel();

        // GET: HealthInfoes
        public async Task<ActionResult> Index(string id, int? year)
        {
            var healthInfoes = db.HealthInfoes.Include(h => h.Student);

            if (!String.IsNullOrEmpty(id))
            {
                healthInfoes = healthInfoes.Where(h => h.StudentId.Contains(id));
            }

            if (year != null)
            {
                healthInfoes = healthInfoes.Where(h => h.ExamDate.Year == year);
            }

            return View(await healthInfoes.ToListAsync());
        }

        // GET: HealthInfoes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthInfo healthInfo = await db.HealthInfoes.FindAsync(id);
            if (healthInfo == null)
            {
                return HttpNotFound();
            }

            List<string> address;
            List<double> Count;

            if (healthInfo.Student.StudentGender == ToolClass.MyEnumClass.EnumGender.男)
            {
                address = new List<string>() { "1000米", "引体向上", "100米", "立定跳远", "肺活量" };
                Count = GetBoyBodyInfo(healthInfo);
            }
            else
            {
                address = new List<string>() { "800米", "仰卧起坐", "50米", "立定跳远", "肺活量" };
                Count = GetGirlBodyInfo(healthInfo);
            }

            GeneratorRaderPicure(address, Count);
            GeneratorAreaPicure(address, Count);
            GeneratorColumnPicure(address, Count);

            return View(healthInfo);
        }

        private List<double> GetGirlBodyInfo(HealthInfo healthInfo)
        {
            return new List<double>
            {
                79,
                Math.Min(Math.Max(0,(healthInfo.Pull-10) * 2.5),100),
                76,
                Math.Min(Math.Max(0,(healthInfo.Jump-165)),100),
                Math.Min(Math.Max(0,(healthInfo.Breath - 1000) / 40),100)
            };
        }

        private List<double> GetBoyBodyInfo(HealthInfo healthInfo)
        {
            return new List<double>
            {
                80,
                Math.Min(Math.Max(0,(healthInfo.Pull - 1) * 4),100),
                80,
                Math.Min(Math.Max(0,(healthInfo.Jump-165)),100),
                Math.Min(Math.Max(0,(healthInfo.Breath - 1000) / 40),100)
            };
        }

        private static void GeneratorRaderPicure(List<string> address, List<double> Count)
        {
            new Chart(width: 1200, height: 600, theme: ChartTheme.Blue)
                .AddTitle("测试成绩雷达图")
                .AddSeries(name: "Radar", chartType: "Radar", xValue: address, yValues: Count)
                .Save("~/Content/ChartTemp/Radar.jpeg");
        }

        private static void GeneratorAreaPicure(List<string> address, List<double> Count)
        {
            new Chart(width: 1200, height: 600, theme: ChartTheme.Yellow)
                .AddTitle("测试成绩面积图")
                .AddSeries(name: "Area", chartType: "Area", xValue: address, yValues: Count)
                .Save("~/Content/ChartTemp/Area.jpeg");
        }

        private static void GeneratorColumnPicure(List<string> address, List<double> Count)
        {
            new Chart(width: 1200, height: 600, theme: ChartTheme.Green)
                .AddTitle("测试成绩柱形图")
                .AddSeries(name: "Column", chartType: "Column", xValue: address, yValues: Count)
                .Save("~/Content/ChartTemp/Column.jpeg");
        }

        // GET: HealthInfoes/Create
        public ActionResult Create()
        {
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentId");
            return View();
        }

        // POST: HealthInfoes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "HealthInfoId,StudentId,ExamDate,Height,Weigh,Jump,Breath,Seated,LongRun,ShortRun,Pull")] HealthInfo healthInfo)
        {
            if (ModelState.IsValid)
            {
                db.HealthInfoes.Add(healthInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentId", healthInfo.StudentId);
            return View(healthInfo);
        }

        // GET: HealthInfoes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthInfo healthInfo = await db.HealthInfoes.FindAsync(id);
            if (healthInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentId", healthInfo.StudentId);
            return View(healthInfo);
        }

        // POST: HealthInfoes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "HealthInfoId,StudentId,ExamDate,Height,Weigh,Jump,Breath,Seated,LongRun,ShortRun,Pull")] HealthInfo healthInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(healthInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "StudentId", healthInfo.StudentId);
            return View(healthInfo);
        }

        // GET: HealthInfoes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthInfo healthInfo = await db.HealthInfoes.FindAsync(id);
            if (healthInfo == null)
            {
                return HttpNotFound();
            }
            return View(healthInfo);
        }

        // POST: HealthInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            HealthInfo healthInfo = await db.HealthInfoes.FindAsync(id);
            db.HealthInfoes.Remove(healthInfo);
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

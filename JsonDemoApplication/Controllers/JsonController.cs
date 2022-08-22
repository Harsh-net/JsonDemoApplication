using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace JsonDemoApplication.Controllers
{
    public class JsonController : Controller
    {
        // GET: Json

        public ActionResult Index()
        //{
        //    return View();
        //}
        {

            List<Demojsondata> Demolist = new List<Demojsondata>();

            SampleDBEntities sd = new SampleDBEntities();
            {

                int fsd = Demolist.Count();

                string jsondata = (new WebClient()).DownloadString("https://jsonplaceholder.typicode.com/todos");
                //List<Demojsondata> Demolist = JsonConvert.DeserializeObject<List<Demojsondata>>(jsondata);
                Demolist = JsonConvert.DeserializeObject<List<Demojsondata>>(jsondata);

                if (sd.Demojsondatas.Any()) return View(sd.Demojsondatas.ToList());


                foreach (var item in Demolist)
                {
                    @item.id.ToString();
                    @item.userId.ToString();
                    @item.title.ToString();
                    @item.completed.ToString();
                    sd.Demojsondatas.Add(item);
                    sd.SaveChanges();
                }

            }
            return View(Demolist);
        }

        public ActionResult Edit(int id)
        {


            Demojsondata ObjDemojsondata = new Demojsondata();


            SampleDBEntities sd = new SampleDBEntities();

            ObjDemojsondata = sd.Demojsondatas.Where(x => x.id == id).FirstOrDefault();
            ObjDemojsondata.id = id;

            return View(ObjDemojsondata);
        }

        [HttpPost]
        public ActionResult Edit(Demojsondata ObjDemojsondata)
        {

            SampleDBEntities sd = new SampleDBEntities();
            //Int32 id = (int)TempData["id"];

            var JsonData = sd.Demojsondatas.Where(x => x.id == ObjDemojsondata.id).FirstOrDefault();
            if (JsonData != null)
            {
                JsonData.userId = ObjDemojsondata.userId;
                JsonData.title = ObjDemojsondata.title;
                JsonData.completed = ObjDemojsondata.completed;

                sd.Entry(JsonData).State = EntityState.Modified;
                sd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
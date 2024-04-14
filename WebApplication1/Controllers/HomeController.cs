using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private List<Marker> markers;
        private int markerId;

        public HomeController()
        {
            markers = new List<Marker>();
            markerId = 0;
        }

        public IActionResult Index()
        {
            return View(markers);
        }

        [HttpPost]
        public IActionResult SaveMarker()
        {
            var center = new { lat = Request.Form["lat"], lng = Request.Form["lng"] };
            var marker = new Marker { Id = markerId++, Lat = Convert.ToDouble(center.lat), Lng = Convert.ToDouble(center.lng), Datetime = DateTime.UtcNow };
            markers.Add(marker);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetMarkers()
        {
            return Json(markers);
        }

        public IActionResult DownloadMarkers()
        {
            var json = JsonConvert.SerializeObject(markers);
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);
            return File(bytes, "application/json", "markers.json");
        }
    }

}
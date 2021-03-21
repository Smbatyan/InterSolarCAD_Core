using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InterSolarCAD_Core.Models;
using Microsoft.AspNetCore.Authorization;
using InterSolarCAD_Core.Models.Web;
using InterSolarCAD_Core.Data;
using System.Net.Http;
using System.Text;
using InterSolarCAD_Core.Models.Admin.Entity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InterSolarCAD_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext context)
        {
            _db = context;
        }

        public IActionResult Index()
        {
            ViewModel vm = new ViewModel(_db);
            
            return View(vm);
        }

        [Authorize]
        public IActionResult Admin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.Client, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm([Bind("FirstName,LastName,Email,PhoneNumber,Country,City,ZIP,Message,Id")]ContactForm contact)
        {
            using var client = new HttpClient();
            string rresponse = Request.Form["g-recaptcha-response"];
            HttpRequestMessage requestMessage = new HttpRequestMessage(new HttpMethod("post"), "https://www.google.com/recaptcha/api/siteverify");
            List<KeyValuePair<string, string>> param = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("secret", "6LdY2eQUAAAAAN4hfoi8n1_sreStu2ikUWF012RF"),
                new KeyValuePair<string, string>("response", rresponse)
            };

            requestMessage.Content = new FormUrlEncodedContent(param);   // This is where your content gets added to the request body


            HttpResponseMessage response = client.SendAsync(requestMessage).Result;

            string apiResponse = response.Content.ReadAsStringAsync().Result;

            CaptchResponse res = new CaptchResponse();
            try
            {
                if (apiResponse != "")
                {
                    res = JsonConvert.DeserializeObject<CaptchResponse>(apiResponse);
                    if (res.success)
                    {
                        contact.Date = DateTime.Now;
                        _db.ContactForm.Add(contact);
                        _db.SaveChanges();
                        return RedirectToAction("Index#Contacts");
                    }
                    else
                    {
                        return Error();
                    }
                }
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error ocurred while calling the API. It responded with the following message: {response.StatusCode} {response.ReasonPhrase} : {ex}");
            }
        }
    }
}

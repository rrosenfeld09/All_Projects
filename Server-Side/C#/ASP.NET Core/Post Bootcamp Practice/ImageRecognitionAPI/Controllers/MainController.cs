using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using APITest2.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace APITest2.Controllers
{
    public class MainController : Controller 
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("show_result")]
        public async Task<ActionResult> RunAsync(string url)
        {
            string apiKey = "acc_b885b81f84493ad";
            string apiSecret = "3e857713f1655ae0406a0e8bc70541d6";
            string imageUrl = $"{url}";

            string basicAuthValue = System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes (String.Format ("{0}:{1}", apiKey, apiSecret)));

            using (var client = new HttpClient ()) {
                client.BaseAddress = new Uri ("https://api.imagga.com/v2/");
                client.DefaultRequestHeaders.Accept.Clear ();
                client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
                client.DefaultRequestHeaders.Add ("Authorization", String.Format("Basic {0}", basicAuthValue));

                HttpResponseMessage response = await client.GetAsync (String.Format("tags?image_url={0}", imageUrl));

                HttpContent content = response.Content;

                string result = await content.ReadAsStringAsync();

                return RedirectToAction("Result", new{result = result});
            }             
        }

        [HttpGet("get_result")]
        public IActionResult Result(string result)
        { 
            ViewModel viewModel = new ViewModel();
            viewModel.resultData = result;
            return View(viewModel);
        }
    }
}
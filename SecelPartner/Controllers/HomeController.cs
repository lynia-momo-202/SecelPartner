using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Models;
using System.Diagnostics;

namespace SecelPartner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
     

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs(SendEmail sendEmail)
        {
            sendEmail.Subject = "venant de secelpartner";
            sendEmail.ToEmail="secelpartner001@gmail.com";
            try
           {
                _unitOfWork.Emails.EmailSend(sendEmail);
                await _unitOfWork.Emails.Add(sendEmail);
                _unitOfWork.Complete();
                TempData["successMessage"] = "Mail send successfully !!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(sendEmail);
            }
        }
        public IActionResult Service()
        {
            return View();
        }
        [Authorize]
        public IActionResult Documentation()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
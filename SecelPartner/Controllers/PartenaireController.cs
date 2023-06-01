using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.infrastructure.Services;
using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Interfaces;
using SecelPartner.UI.Migrations;

namespace SecelPartner.UI.Controllers
{
    [Authorize]
    public class PartenaireController : Controller
    {
        #region membres prives
        private readonly IUnitOfWork _unitOfWork;
        private readonly FichierService _fichierService;
        private readonly IGerantRepository _gerantRepository;
        private readonly UserManager<SecelPartnerUIUser> _userManager;
        #endregion

        #region constructeur
        public PartenaireController(IUnitOfWork unitOfWork, FichierService fichierService, IGerantRepository gerantRepository, UserManager<SecelPartnerUIUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _fichierService = fichierService;
            _userManager = userManager;
            _gerantRepository = gerantRepository;
        }
        #endregion
        // GET: Partenaire
        public async Task<IActionResult> Index()
        {
            var partenaires= await _unitOfWork.Partenaires.GetAll();
            return View(partenaires);
        }
        [Authorize(Roles = "Chef de partenariat")]
        public async Task<IActionResult> IndexGerant()
        {
            var Id = _userManager.GetUserId(User);
            var contrats = await _unitOfWork.Contrats.GetAll();
            var Partenaires =await _unitOfWork.Partenaires.GetAll();
            var PartenairesGerant = _gerantRepository.ListPartenaireGerant(Id, contrats,Partenaires);
            return View(PartenairesGerant.Distinct());
        }

        // GET: Partenaire/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var partenaire = await _unitOfWork.Partenaires.GetById(id);
                if (partenaire != null)
                {
                    return View(partenaire);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"partenaire with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Administrateur")]
        // GET: Partenaire/Create
        [Authorize(Roles = "Administrateur,Super Administrateur")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Partenaire/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur,Super Administrateur")]
        public async Task<IActionResult> Create(Partenaire partenaire)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (partenaire.Logo != null)
                    {
                        partenaire.LogoName = partenaire.Logo.FileName;
                        partenaire.LogoPath = await _fichierService.UploadAsync(partenaire.Logo);
                    }
                    await _unitOfWork.Partenaires.Add(partenaire);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Partener create successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["errorMessage"] = "Model state is Invalid ";
                    return View();
                }
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Partenaire/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var partenaire = await _unitOfWork.Partenaires.GetById(id);
            if (partenaire != null)
            {
                return View(partenaire);
            }
            else
            {
                TempData["errorMessage"] = $"Partenaire details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Partenaire/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Partenaire partenaire)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (partenaire.Logo != null)
                    {
                        partenaire.LogoName = partenaire.Logo.FileName;
                        partenaire.LogoPath = await _fichierService.UploadAsync(partenaire.Logo);
                    }
                    await _unitOfWork.Partenaires.Update(partenaire);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Partner update successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["errorMessage"] = "Model state is Invalid ";
                    return View(partenaire);
                }
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        // GET: Partenaire/Delete/5
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var partenaire = await _unitOfWork.Partenaires.GetById(id);
                if (partenaire != null)
                {
                    return View(partenaire);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Partner with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // POST: Partenaire/Delete/5
        [Authorize(Roles = "Super Administrateur")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var partenaire = await _unitOfWork.Partenaires.GetById(id);
                if (null != partenaire.LogoPath)
                {
                    _fichierService.DeleteUploadFile(partenaire.LogoPath);
                }
                await _unitOfWork.Partenaires.Delete(id);
                    _unitOfWork.Complete();
                TempData["successMessage"] = "Partner Delete successfully !!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

    }
}

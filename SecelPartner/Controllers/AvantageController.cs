using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;
using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Data;
using SecelPartner.UI.Interfaces;

namespace SecelPartner.UI.Controllers
{
    [Authorize]
    public class AvantageController : Controller
    {
        #region membres prives
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<SecelPartnerUIUser> _userManager;
        private readonly IGerantRepository _gerantRepository;
        #endregion

        #region constructeur
        public AvantageController(IUnitOfWork unitOfWork, IGerantRepository gerantRepository, UserManager<SecelPartnerUIUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _gerantRepository = gerantRepository;
        }
        #endregion

        // GET: Avantage
        [Authorize(Roles = "Administrateur,Super Administrateur")]
        public async Task<IActionResult> Index()
        {
            var avantages = await _unitOfWork.Avantages.GetAll();
            return View(avantages);
        }
        // GET: Avantage_gerant
        [Authorize(Roles = "Chef de partenariat")]
        public async Task<IActionResult> IndexGerant()
        {
            var Id = _userManager.GetUserId(User);
            var contrats = await _unitOfWork.Contrats.GetAll();
            var avantage = await _unitOfWork.Avantages.GetAll();
            var avantages = _gerantRepository.ListAvantageGerant(Id, contrats, avantage);
            return View(avantages.Distinct());
        }
        // GET: Avantage/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var avantage = await _unitOfWork.Avantages.GetById(id);
                if (avantage != null)
                {
                    return View(avantage);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Avantage with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // GET: Avantage/Create
        public IActionResult Create()
        {
            try
            {
                if (_unitOfWork.Partenariats.ListPartenariats().Count == 0)
                {
                    TempData["warningMessage"] = "Vous devez enregistrer au moins un partenaire et un partenariat avant de creer un contrat de partenarat";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex;
                return RedirectToAction("Index");
            }
        }

        // POST: Avantage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Avantage avantage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Avantages.Add(avantage);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Avantage create successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                    TempData["errorMessage"] = "Une erreur c'est produite verifier vos champs ";
                    return View();
                }
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Avantage/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var avantage = await _unitOfWork.Avantages.GetById(id);
            if (avantage != null)
            {
                ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                return View(avantage);
            }
            else
            {
                TempData["errorMessage"] = $"Avantage details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Avantage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Avantage avantage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Avantages.Update(avantage);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Avantage update successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                    TempData["errorMessage"] = "Model state is Invalid ";
                    return View();
                }
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Avantage/Delete/5
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var avantage = await _unitOfWork.Avantages.GetById(id);
                if (avantage != null)
                {
                    return View(avantage);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Avantage with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // POST: Avantage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _unitOfWork.Avantages.Delete(id);
                _unitOfWork.Complete();
                TempData["successMessage"] = "Avantage Delete successfully !!";
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

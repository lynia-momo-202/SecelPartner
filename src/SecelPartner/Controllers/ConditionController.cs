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
using SecelPartner.UI.Interfaces;

namespace SecelPartner.UI.Controllers
{
    [Authorize]
    public class ConditionController : Controller
    {
        #region membres prives
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGerantRepository _gerantRepository;
        private readonly UserManager<SecelPartnerUIUser> _userManager;
        #endregion

        #region constructeur
        public ConditionController(
            IUnitOfWork unitOfWork,
            IGerantRepository gerantRepository,
            UserManager<SecelPartnerUIUser> userManager
        )
        {
            _gerantRepository = gerantRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        #endregion

        // GET: Condition
        [Authorize(Roles = "Administrateur,Super Administrateur")]
        public async Task<IActionResult> Index()
        {
            var conditions = await _unitOfWork.Conditions.GetAll();
            return View(conditions);
        }

        [Authorize(Roles = "Chef de partenariat")]
        public async Task<IActionResult> IndexGerant()
        {
            var Id = _userManager.GetUserId(User);
            var contrats = await _unitOfWork.Contrats.GetAll();
            var condition = await _unitOfWork.Conditions.GetAll();
            var conditions = _gerantRepository.ListConditionGerant(Id, contrats, condition);
            return View(conditions.Distinct());
        }

        // GET: Condition/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var conditions = await _unitOfWork.Conditions.GetById(id);
                if (conditions != null)
                {
                    return View(conditions);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Condition with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // GET: Condition/Create
        public IActionResult Create()
        {
            try
            {
                if (_unitOfWork.Partenariats.ListPartenariats().Count == 0)
                {
                    TempData["warningMessage"] =
                        "Vous devez enregistrer au moins un partenaire et un partenariat avant de creer un contrat de partenarat";
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

        // POST: Condition/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Condition condition)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Conditions.Add(condition);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Condition create successfully !!";
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

        // GET: Condition/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var condition = await _unitOfWork.Conditions.GetById(id);
            if (condition != null)
            {
                ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                return View(condition);
            }
            else
            {
                TempData["errorMessage"] = $"Condition details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Condition/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Condition condition)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Conditions.Update(condition);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Condition update successfully !!";
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
                return RedirectToAction("Index");
            }
        }

        // GET: Condition/Delete/5
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var condition = await _unitOfWork.Conditions.GetById(id);
                if (condition != null)
                {
                    return View(condition);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Condition with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // POST: Condition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _unitOfWork.Conditions.Delete(id);
                _unitOfWork.Complete();
                TempData["successMessage"] = "Condition Delete successfully !!";
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

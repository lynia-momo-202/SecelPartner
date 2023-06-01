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
    public class ConditionRenouvController : Controller
    {
        #region membres prives
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGerantRepository _gerantRepository;
        private readonly UserManager<SecelPartnerUIUser> _userManager;
        #endregion

        #region constructeur
        public ConditionRenouvController(IUnitOfWork unitOfWork, IGerantRepository gerantRepository, UserManager<SecelPartnerUIUser> userManager)
        {
            _gerantRepository = gerantRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        #endregion

        // GET: ConditionRenouv
        [Authorize(Roles = "Administrateur,Super Administrateur")]
        public async Task<IActionResult> Index()
        {
            var conditionsRenouv = await _unitOfWork.ConditionsRenouv.GetAll();
            return View(conditionsRenouv);
        }
        [Authorize(Roles = "Chef de partenariat")]
        public async Task<IActionResult> IndexGerant()
        {
            var Id = _userManager.GetUserId(User);
            var contrats = await _unitOfWork.Contrats.GetAll();
            var conditionRenouv = await _unitOfWork.ConditionsRenouv.GetAll();
            var conditionsRenouv = _gerantRepository.ListConditionRenouvGerant(Id, contrats, conditionRenouv);
            return View(conditionsRenouv.Distinct());
        }
        // GET: ConditionRenouv/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var conditionsRenouv = await _unitOfWork.ConditionsRenouv.GetById(id);
                if (conditionsRenouv != null)
                {
                    return View(conditionsRenouv);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"condition Renouvellement with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // GET: ConditionRenouv/Create
        public IActionResult Create()
        {
            try
            {
                if (_unitOfWork.Partenariats.ListPartenariats().Count == 0)
                {
                    TempData["warningMessage"] = "Vous devez enregistrer au moins un partenariat avant de creer les avantages lies";
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

        // POST: ConditionRenouv/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConditionRenouv conditionRenouv)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.ConditionsRenouv.Add(conditionRenouv);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Condition Renouvellement create successfully !!";
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

        // GET: ConditionRenouv/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var conditionRenouv = await _unitOfWork.ConditionsRenouv.GetById(id);
            if (conditionRenouv != null)
            {
                ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                return View(conditionRenouv);
            }
            else
            {
                TempData["errorMessage"] = $"Partenaire details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ConditionRenouv/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ConditionRenouv conditionRenouv)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.ConditionsRenouv.Update(conditionRenouv);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Condition Renouvellement update successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                    TempData["errorMessage"] = "Model state is Invalid ";
                    return View(conditionRenouv);
                }
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: ConditionRenouv/Delete/5
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var conditionRenouv = await _unitOfWork.ConditionsRenouv.GetById(id);
                if (conditionRenouv != null)
                {
                    return View(conditionRenouv);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"condition Renouvellement with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // POST: ConditionRenouv/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _unitOfWork.ConditionsRenouv.Delete(id);
                _unitOfWork.Complete();
                TempData["successMessage"] = "condition Renouvellement Delete successfully !!";
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.UI.Data;
using SecelPartner.UI.Interfaces;
using SecelPartner.UI.Migrations;
using SecelPartner.UI.Models;

namespace SecelPartner.UI.Controllers
{
    [Authorize(Roles = "Super Administrateur")]
    public class GerantController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IGerantRepository _gerantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GerantController(
            SecelPartnerUIContext context,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IGerantRepository gerantRepository
        )
        {
            _userRepository = userRepository;
            _gerantRepository = gerantRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: Gerant
        public async Task<IActionResult> Index()
        {
            var secelPartnerUIContext = await _gerantRepository.GetAll();
            return View(secelPartnerUIContext);
        }

        // GET: Gerant/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var gerant = await _gerantRepository.GetById(id);
                if (gerant != null)
                {
                    return View(gerant);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Niveau gerant with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // GET: Gerant/Create
        public IActionResult Create()
        {
            try
            {
                if (
                    _unitOfWork.Contrats.ListItems().Count == 0
                    || _gerantRepository.ListItems().Count == 0
                )
                {
                    TempData["warningMessage"] =
                        "Vous devez enregistrer au moins un contrat de partenariat avant d'y attribuer un Administrateur";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ContratPartenariatList = _unitOfWork.Contrats.ListItems();
                    ViewBag.UserList = _gerantRepository.ListItems();
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex;
                return RedirectToAction("Index");
            }
        }

        // POST: Gerant/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Gerant gerant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _gerantRepository.Add(gerant);
                    TempData["successMessage"] = "Chef Partenariat create successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ContratPartenariatList = _unitOfWork.Contrats.ListItems();
                    ViewBag.UserList = _gerantRepository.ListItems();
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

        // GET: Gerant/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.ContratPartenariatList = _unitOfWork.Contrats.ListItems();
            ViewBag.UserList = _userRepository.ListItems();
            var gerant = await _gerantRepository.GetById(id);
            if (gerant != null)
            {
                return View(gerant);
            }
            else
            {
                TempData["errorMessage"] = $"gerant details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Gerant/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Gerant gerant)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _gerantRepository.Update(gerant);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "gerant update successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ContratPartenariatList = _unitOfWork.Contrats.ListItems();
                    ViewBag.UserList = _gerantRepository.ListItems();
                    TempData["errorMessage"] = "Une erreur cest produite verifier vos champ ";
                    return View(gerant);
                }
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Gerant/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var gerant = await _gerantRepository.GetById(id);
                if (gerant != null)
                {
                    return View(gerant);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"gerant with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // POST: Gerant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _gerantRepository.Delete(id);
                _unitOfWork.Complete();
                TempData["successMessage"] = "gerant Delete successfully !!";
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

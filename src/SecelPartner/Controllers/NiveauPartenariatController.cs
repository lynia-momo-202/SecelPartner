using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.UI.Controllers
{
    public class NiveauPartenariatController : Controller
    {
        #region membres prives
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region constructeur
        public NiveauPartenariatController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        // GET: NiveauPartenariat
        public async Task<IActionResult> Index()
        {
            var niveauxPartenariat = await _unitOfWork.NiveauxPartenariat.GetAll();
            return View(niveauxPartenariat);
        }

        // GET: NiveauPartenariat/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var niveauPartenariat = await _unitOfWork.NiveauxPartenariat.GetById(id);
                if (niveauPartenariat != null)
                {
                    return View(niveauPartenariat);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Niveau Partenariat with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // GET: NiveauPartenariat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NiveauPartenariat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Designation")] NiveauPartenariat niveauPartenariat
        )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.NiveauxPartenariat.Add(niveauPartenariat);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Niveau Partenariat create successfully !!";
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

        // GET: NiveauPartenariat/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var niveauPartenariat = await _unitOfWork.NiveauxPartenariat.GetById(id);
            if (niveauPartenariat != null)
            {
                return View(niveauPartenariat);
            }
            else
            {
                TempData["errorMessage"] = $"Niveau Partenariat details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: NiveauPartenariat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("Id,Designation")] NiveauPartenariat niveauPartenariat
        )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.NiveauxPartenariat.Update(niveauPartenariat);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Niveau Partenariat update successfully !!";
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

        // GET: NiveauPartenariat/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var niveauPartenariat = await _unitOfWork.NiveauxPartenariat.GetById(id);
                if (niveauPartenariat != null)
                {
                    return View(niveauPartenariat);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Niveau Partenariat with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // POST: NiveauPartenariat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _unitOfWork.NiveauxPartenariat.Delete(id);
                _unitOfWork.Complete();
                TempData["successMessage"] = "Niveau Partenariat Delete successfully !!";
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

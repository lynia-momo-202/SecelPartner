using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.Infrastructure.DefaultContext;

namespace SecelPartner.UI.Controllers
{
    public class TypePartenariatController : Controller
    {
        #region membres prives
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region constructeur
        public TypePartenariatController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        // GET: TypePartenariat
        public async Task<IActionResult> Index()
        {
            var typesPartenariat = await _unitOfWork.TypesPartenariat.GetAll();
            return View(typesPartenariat);
        }

        // GET: TypePartenariat/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var typePartenariat = await _unitOfWork.TypesPartenariat.GetById(id);
                if (typePartenariat != null)
                {
                    return View(typePartenariat);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Type Partenariat with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // GET: TypePartenariat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypePartenariat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom")] TypePartenariat typePartenariat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.TypesPartenariat.Add(typePartenariat);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Type Partenariat create successfully !!";
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

        // GET: TypePartenariat/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var typePartenariat = await _unitOfWork.TypesPartenariat.GetById(id);
            if (typePartenariat != null)
            {
                return View(typePartenariat);
            }
            else
            {
                TempData["errorMessage"] = $"Type Partenariat details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: TypePartenariat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Nom")] TypePartenariat typePartenariat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.TypesPartenariat.Update(typePartenariat);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Type Partenariat update successfully !!";
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

        // GET: TypePartenariat/Delete/5
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var typePartenariat = await _unitOfWork.TypesPartenariat.GetById(id);
                if (typePartenariat != null)
                {
                    return View(typePartenariat);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Type Partenariat with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // POST: TypePartenariat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _unitOfWork.TypesPartenariat.Delete(id);
                _unitOfWork.Complete();
                TempData["successMessage"] = "Type Partenariat Delete successfully !!";
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

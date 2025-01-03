using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Interfaces;

namespace SecelPartner.UI.Controllers
{
    public class PartenariatController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGerantRepository _gerantRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<SecelPartnerUIUser> _userManager;

        public PartenariatController(
            IUnitOfWork unitOfWork,
            IGerantRepository gerantRepository,
            IUserRepository userRepository,
            UserManager<SecelPartnerUIUser> userManager
        )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userRepository = userRepository;
            _gerantRepository = gerantRepository;
        }

        // GET: Partenariat
        public async Task<IActionResult> Index()
        {
            var partenariat = await _unitOfWork.Partenariats.GetAll();
            return View(partenariat);
        }

        [Authorize(Roles = "Chef de partenariat")]
        // GET: Partenariat
        public async Task<IActionResult> IndexGerant()
        {
            var Id = _userManager.GetUserId(User);
            var contrats = await _unitOfWork.Contrats.GetAll();
            var partenariats = await _unitOfWork.Partenariats.GetAll();
            var partenariatGerant = _gerantRepository.ListPartenariatGerant(
                Id,
                contrats,
                partenariats
            );
            return View(partenariatGerant.Distinct());
        }

        // GET: Partenariat/Details/5
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var partenariat = await _unitOfWork.Partenariats.GetById(id);
                if (partenariat != null)
                {
                    return View(partenariat);
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

        // GET: Partenariat/Create
        [Authorize(Roles = "Administrateur,Super Administrateur")]
        public IActionResult Create()
        {
            ViewBag.NiveauPartenariatList = _unitOfWork.NiveauxPartenariat.ListItems();
            ViewBag.TypePartenariatList = _unitOfWork.TypesPartenariat.ListItems();
            return View();
        }

        // POST: Partenariat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrateur,Super Administrateur")]
        public async Task<IActionResult> Create(Partenariat partenariat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Partenariats.Add(partenariat);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = " Partenariat create successfully !!";
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

        // GET: Partenariat/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.NiveauPartenariatList = _unitOfWork.NiveauxPartenariat.ListItems();
            ViewBag.TypePartenariatList = _unitOfWork.TypesPartenariat.ListItems();
            var partenariat = await _unitOfWork.Partenariats.GetById(id);
            if (partenariat != null)
            {
                return View(partenariat);
            }
            else
            {
                TempData["errorMessage"] = $"Partenariat details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Partenariat/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Partenariat partenariat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Partenariats.Update(partenariat);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Partenariat update successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["errorMessage"] = "Une erreur cest produite verifier vos champ ";
                    return View();
                }
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Partenariat/Delete/5
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var partenariat = await _unitOfWork.Partenariats.GetById(id);
                if (partenariat != null)
                {
                    return View(partenariat);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Partenariat with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // POST: Partenariat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _unitOfWork.Partenariats.Delete(id);
                _unitOfWork.Complete();
                TempData["successMessage"] = "Partenariat Delete successfully !!";
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

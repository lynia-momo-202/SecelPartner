using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Interfaces;

namespace SecelPartner.UI.Controllers
{
    [Authorize]
    public class ContratPartenariatController : Controller
    {
        #region membres prives
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGerantRepository _gerantRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<SecelPartnerUIUser> _userManager;
        #endregion

        #region constructeur
        public ContratPartenariatController(
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
        #endregion
        [Authorize(Roles = "Administrateur ,Super Administrateur")]
        // GET: ContratPartenariat
        public async Task<IActionResult> Index()
        {
            var contratsPartenariat = await _unitOfWork.Contrats.GetAll();
            return View(contratsPartenariat);
        }

        [Authorize(Roles = "Chef de partenariat")]
        // GET: ContratPartenariat_gerant
        public async Task<IActionResult> IndexGerant()
        {
            var Id = _userManager.GetUserId(User);
            var contrats = await _unitOfWork.Contrats.GetAll();
            var contratsPartenariat = _gerantRepository.ListContratGerant(Id, contrats);
            return View(contratsPartenariat.Distinct());
        }

        // GET: ContratPartenariat/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var contratPartenariat = await _unitOfWork.Contrats.GetById(id);
                if (contratPartenariat != null)
                {
                    return View(contratPartenariat);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Contrat Partenariat with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // GET: ContratPartenariat/Create
        public IActionResult Create()
        {
            try
            {
                if (
                    _unitOfWork.Partenaires.ListItems().Count == 0
                    || _unitOfWork.Partenariats.ListPartenariats().Count == 0
                )
                {
                    TempData["warningMessage"] =
                        "Vous devez enregistrer au moins un partenaire et un partenariat avant de creer un contrat de partenarat";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                    ViewBag.PartenaireList = _unitOfWork.Partenaires.ListItems();
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex;
                return RedirectToAction("Index");
            }
        }

        // POST: ContratPartenariat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContratPartenariat contratPartenariat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Contrats.Add(contratPartenariat);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Contrat Partenariat create successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                    ViewBag.PartenaireList = _unitOfWork.Partenaires.ListItems();
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

        // GET: ContratPartenariat/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contratPartenariat = await _unitOfWork.Contrats.GetById(id);
            if (contratPartenariat != null)
            {
                ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                ViewBag.PartenaireList = _unitOfWork.Partenaires.ListItems();
                return View(contratPartenariat);
            }
            else
            {
                TempData["errorMessage"] = $"Contrat Partenariat details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ContratPartenariat/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            [Bind("Id,DateSign,Duree,Montant,Titre")] ContratPartenariat contratPartenariat
        )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Contrats.Update(contratPartenariat);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Contrat Partenariat update successfully !!";
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
                return RedirectToAction("Index");
            }
        }

        // GET: ContratPartenariat/Delete/5
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var contratPartenariat = await _unitOfWork.Contrats.GetById(id);
                if (contratPartenariat != null)
                {
                    return View(contratPartenariat);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Contrat Partenariat with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // POST: ContratPartenariat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _unitOfWork.Contrats.Delete(id);
                _unitOfWork.Complete();
                TempData["successMessage"] = "Contrat Partenariat Delete successfully !!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> RapportPartner(int id)
        {
            try
            {
                TempData["infoMessage"] = "cliquez sur print pour imprimer le rapport";
                var gerant = _gerantRepository.ListGerant(id);
                List<SecelPartnerUIUser>? users = new List<SecelPartnerUIUser>();
                if (gerant != null)
                {
                    foreach (var g in gerant)
                    {
                        var user = await _userRepository.GetById(g.UserId);
                        users.Add(user);
                    }
                }
                ViewBag.Users = users;
                ViewBag.ContactList = _unitOfWork.Contacts.ListItems();
                ViewBag.ConditionList = _unitOfWork.Conditions.ListItems();
                ViewBag.ConditionRenouvList = _unitOfWork.ConditionsRenouv.ListItems();
                ViewBag.AvantageList = _unitOfWork.Avantages.ListItems();
                ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                ViewBag.PartenaireList = _unitOfWork.Partenaires.ListItems();
                ViewBag.NiveauPartenariatList = _unitOfWork.NiveauxPartenariat.ListItems();
                ViewBag.TypePartenariatList = _unitOfWork.TypesPartenariat.ListItems();

                var contratPartenariat = await _unitOfWork.Contrats.GetById(id);
                if (contratPartenariat != null)
                {
                    return View(contratPartenariat);
                }
                TempData["errorMessage"] = $"Contrat Partenariat with Id = {id} not found";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> RapportPrint(int id)
        {
            try
            {
                var gerant = _gerantRepository.ListGerant(id);
                List<SecelPartnerUIUser>? users = new List<SecelPartnerUIUser>();
                if (gerant != null)
                {
                    foreach (var g in gerant)
                    {
                        var user = await _userRepository.GetById(g.UserId);
                        users.Add(user);
                    }
                }
                ViewBag.Users = users;
                ViewBag.ContactList = _unitOfWork.Contacts.ListItems();
                ViewBag.ConditionList = _unitOfWork.Conditions.ListItems();
                ViewBag.ConditionRenouvList = _unitOfWork.ConditionsRenouv.ListItems();
                ViewBag.AvantageList = _unitOfWork.Avantages.ListItems();
                ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
                ViewBag.PartenaireList = _unitOfWork.Partenaires.ListItems();
                ViewBag.NiveauPartenariatList = _unitOfWork.NiveauxPartenariat.ListItems();
                ViewBag.TypePartenariatList = _unitOfWork.TypesPartenariat.ListItems();
                var contratPartenariat = await _unitOfWork.Contrats.GetById(id);
                if (contratPartenariat != null)
                {
                    return View(contratPartenariat);
                }
                TempData["errorMessage"] = $"Contrat Partenariat with Id = {id} not found";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecelPartner.Core.Entities;
using SecelPartner.Core.Interfaces;
using SecelPartner.infrastructure.Services;
using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Interfaces;

namespace SecelPartner.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<SecelPartnerUIUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly FichierService _fichierService;

        public UserController(
            IUserRoleRepository userRoleRepository,
            FichierService fichierService,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            RoleManager<IdentityRole> roleManager,
            UserManager<SecelPartnerUIUser> userManager
        )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _fichierService = fichierService;
        }

        // GET: UserController
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Index()
        {
            var user = await _userRepository.GetAll();
            ViewBag.UserRoles = await _userRoleRepository.GetAll();
            ViewBag.Roles = _roleManager.Roles.ToList();
            return View(user);
        }

        // GET: UserController/Details/5
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                if (user != null)
                {
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"User with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Edit/5
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetById(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                TempData["errorMessage"] = $"user details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Edit(SecelPartnerUIUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (user.Profil != null)
                    {
                        user.ProfilName = user.Profil.FileName;
                        user.ProfilPath = await _fichierService.UploadAsync(user.Profil);
                    }
                    await _userRepository.Update(user);
                    TempData["successMessage"] = "Contact update successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.RoleList = _roleManager.Roles.ToList();
                    TempData["errorMessage"] = "Model state is Invalid ";
                    return View(user);
                }
            }
            catch (Exception? ex)
            {
                ViewBag.PartenaireList = _unitOfWork.Partenaires.ListItems();
                TempData["errorMessage"] = ex.Message;
                return View(user);
            }
        }

        [Authorize(Roles = "Super Administrateur")]
        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                if (user != null)
                {
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"Contact with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        // POST: UserController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Deleteconfirmed(string id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                if (null != user.ProfilPath)
                {
                    _fichierService.DeleteUploadFile(user.ProfilPath);
                }
                await _userRepository.Delete(id);
                TempData["successMessage"] = "user Delete successfully !!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> MailUser(string id)
        {
            try
            {
                if (_userRepository.ListItems().Count == 0)
                {
                    TempData["warningMessage"] = "Vous devez enregistrer au moins un utilisateur";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.UserList = _userRepository.ListItems();
                    if (id == null)
                    {
                        return View();
                    }
                    var user = await _userRepository.GetById(id);
                    if (user != null)
                    {
                        ViewBag.user = user;
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
            TempData["errorMessage"] = $"user with Id = {id} not found";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> MailUser(SendEmail sendEmail)
        {
            try
            {
                ViewBag.userList = _userRepository.ListItems();
                var user = await _userManager.GetUserAsync(User);
                sendEmail.FromEmail = user.Email;
                sendEmail.Name = user.FirstName + " " + user.LastName;
                if (user.PhoneNumber == null)
                {
                    TempData["warningMessage"] =
                        "Votre numero de telephone est requit ... MyAccount>Profil";
                    return RedirectToAction("Index");
                }
                sendEmail.Tel = Int32.Parse(user.PhoneNumber);
                _unitOfWork.Emails.EmailSend(sendEmail);
                await _unitOfWork.Emails.Add(sendEmail);
                _unitOfWork.Complete();
                TempData["successMessage"] = $"Mail send successfully to {sendEmail.ToEmail}";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}

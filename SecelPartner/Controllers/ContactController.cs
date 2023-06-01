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
    public class ContactController : Controller
    {
        #region membres prives
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGerantRepository _gerantRepository;
        private readonly FichierService _fichierService;
        private readonly UserManager<SecelPartnerUIUser> _userManager;
        #endregion

        #region constructeur
        public ContactController(IUnitOfWork unitOfWork, FichierService fichierService, UserManager<SecelPartnerUIUser> userManager, IGerantRepository gerantRepository)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _fichierService = fichierService;
            _gerantRepository = gerantRepository;
        }
        #endregion
        [Authorize(Roles = "Administrateur,Super Administrateur")]
        // GET: Contact
        public async Task<IActionResult> Index()
        {
            var contacts = await _unitOfWork.Contacts.GetAll();
            return View(contacts);
        }
        [Authorize(Roles = "Chef de partenariat")]
        public async Task<IActionResult> IndexGerant()
        {
            var Id = _userManager.GetUserId(User);
            var contrats = await _unitOfWork.Contrats.GetAll();
            var contact = await _unitOfWork.Contacts.GetAll();
            var contacts = _gerantRepository.ListContactGerant(Id, contrats, contact);
            return View(contacts.Distinct());
        }
        // GET: Contact/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var contacts = await _unitOfWork.Contacts.GetById(id);
                if (contacts != null)
                {
                    return View(contacts);
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

        // GET: Contact/Create
        public IActionResult Create()
        {
            try
            {
                if (_unitOfWork.Partenaires.ListItems().Count == 0)
                {
                    TempData["warningMessage"] = "Vous devez enregistrer au moins un partenaire avant de creer un contact";
                    return RedirectToAction("Index");
                }
                else
                {
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

        // POST: Contact/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (contact.Photo != null)
                    {
                        contact.PhotoName = contact.Photo.FileName;
                        contact.PhotoPath = await _fichierService.UploadAsync(contact.Photo);
                    }
                    await _unitOfWork.Contacts.Add(contact);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Contact create successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
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

        // GET: Contact/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var contacts = await _unitOfWork.Contacts.GetById(id);
                ViewBag.PartenaireList = _unitOfWork.Partenaires.ListItems();
            if (contacts != null)
            {
                return View(contacts);
            }
            else
            {
                TempData["errorMessage"] = $"Contact details not found with Id : {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Contact contact)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (contact.Photo != null)
                    {
                        contact.PhotoName = contact.Photo.FileName;
                        contact.PhotoPath = await _fichierService.UploadAsync(contact.Photo);
                    }
                    await _unitOfWork.Contacts.Update(contact);
                    _unitOfWork.Complete();
                    TempData["successMessage"] = "Contact update successfully !!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.PartenaireList = _unitOfWork.Partenaires.ListItems();
                    TempData["errorMessage"] = "Model state is Invalid ";
                    return View(contact);
                }
            }
            catch (Exception? ex)
            {
                ViewBag.PartenaireList = _unitOfWork.Partenaires.ListItems();
                TempData["errorMessage"] = ex.Message;
                return View(contact);
            }
        }

        // GET: Contact/Delete/5
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var contacts = await _unitOfWork.Contacts.GetById(id);
                if (contacts != null)
                {
                    return View(contacts);
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

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super Administrateur")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var contact = await _unitOfWork.Contacts.GetById(id);
                if (null != contact.PhotoPath)
                {
                    _fichierService.DeleteUploadFile(contact.PhotoPath);
                }
                await _unitOfWork.Contacts.Delete(id);
                _unitOfWork.Complete();
                TempData["successMessage"] = "Contact Delete successfully !!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception? ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> MailContact(int id)
        {
            try
            {
                if (_unitOfWork.Contacts.ListItems().Count == 0)
                {
                    TempData["warningMessage"] = "Vous devez enregistrer au moins un contact";
                    return RedirectToAction("Index");
                }
                else
                {
                        ViewBag.ContactList = _unitOfWork.Contacts.ListItems();
                    if (id == 0)
                    {
                        return View();
                    }
                    var contact = await _unitOfWork.Contacts.GetById(id);
                    if (contact != null)
                    {
                        ViewBag.Contact = contact;
                        return View();
                    }
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
        [HttpPost]
        public async Task<IActionResult> MailContact(SendEmail sendEmail)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                sendEmail.FromEmail = user.Email;
                sendEmail.Name = user.FirstName + " "+user.LastName;
                if(user.PhoneNumber == null)
                {
                    TempData["warningMessage"] = "Votre numero de telephone est requit ... MyAccount>Profil";
                    return RedirectToAction("Index");
                }
                sendEmail.Tel = int.Parse(user.PhoneNumber);
                _unitOfWork.Emails.EmailSend(sendEmail);
                await _unitOfWork.Emails.Add(sendEmail);
                _unitOfWork.Complete();
                TempData["successMessage"] = $"Mail send successfully to {sendEmail.ToEmail}";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ContactList =  _unitOfWork.Contacts.ListItems();
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

    }
}

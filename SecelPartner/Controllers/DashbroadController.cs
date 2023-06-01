using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecelPartner.Core.Interfaces;
using SecelPartner.UI.Areas.Identity.Data;
using SecelPartner.UI.Data;
using SecelPartner.UI.Interfaces;

namespace SecelPartner.UI.Controllers
{
    public class DashbroadController : Controller
    {
        #region membres prives
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IGerantRepository _gerantRepository;
        private readonly UserManager<SecelPartnerUIUser> _userManager;
        #endregion

        #region constructeur
        public DashbroadController(UserManager<SecelPartnerUIUser> userManager,IUnitOfWork unitOfWork, SecelPartnerUIContext context, IUserRepository userRepository, IGerantRepository gerantRepository)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _gerantRepository = gerantRepository;
            _userManager = userManager;
        }
        #endregion
        // GET: DashbroadAdministrateur
        [Authorize(Roles = "Administrateur,Super Administrateur")]
        public async Task<IActionResult> DashbroadAdmin()
        {
            ViewBag.UserList = _userRepository.ListItems();
            ViewBag.PartenariatList = _unitOfWork.Partenariats.ListPartenariats();
            ViewBag.PartenaireList = _unitOfWork.Partenaires.ListItems();
            ViewBag.ContactList = _unitOfWork.Contacts.ListItems();
            ViewBag.ContratList = _unitOfWork.Contrats.ListItems();
            ViewBag.NiveauPartenariatList = _unitOfWork.NiveauxPartenariat.ListItems();
            ViewBag.TypePartenariatList = _unitOfWork.TypesPartenariat.ListItems();
            var user = await _userManager.GetUserAsync(User);
            ViewBag.Roles = await _userManager.GetRolesAsync(user);
            return View();
        }
        [Authorize(Roles = "Chef de partenariat")]
        // GET: DashbroadChef_Partenariat
        public async Task<IActionResult> DashbroadChef()
        {
            var user = await _userManager.GetUserAsync(User);
            var contrats = await _unitOfWork.Contrats.GetAll();
            var partenariats= await _unitOfWork.Partenariats.GetAll();
            var partenaires = await _unitOfWork.Partenaires.GetAll();
            var contacts = await _unitOfWork.Contacts.GetAll();
            ViewBag.ContratList = _gerantRepository.ListContratGerant(user.Id,contrats).Distinct();
            ViewBag.PartenariatList = _gerantRepository.ListPartenariatGerant(user.Id, contrats,partenariats).Distinct();
            ViewBag.PartenaireList = _gerantRepository.ListPartenaireGerant(user.Id,contrats,partenaires).Distinct();
            ViewBag.ContactList = _gerantRepository.ListContactGerant(user.Id,contrats, contacts).Distinct();
            ViewBag.NiveauPartenariatList = _unitOfWork.NiveauxPartenariat.ListItems();
            ViewBag.TypePartenariatList = _unitOfWork.TypesPartenariat.ListItems();
            ViewBag.Roles = await _userManager.GetRolesAsync(user);
            return View();
        }
    }
}

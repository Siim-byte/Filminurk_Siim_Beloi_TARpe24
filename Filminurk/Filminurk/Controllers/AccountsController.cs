using Filminurk.Core.Domain;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Filminurk.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly FilminurkTARpe24Context _context;
        private readonly IEmailServices _emailServices;

        public AccountsController
        (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            FilminurkTARpe24Context context
        )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.DisplayName,
                    Email = model.Email,
                    ProfileType = model.ProfileType,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Accounts", new { userID = user.Id, token = token }, Request.Scheme);
                    //homework task: koosta email kasutajalt kasutajalt pärineva aadressile saatmiseks, kasutaja saab oma postkastist kätte emaili, kinnituslingiga
                    // mille jaoks kasutatakse tokenit, siin tuleb välja kutsuda vastav, uus, emaili saatmise meetod, mis saadab õige sisuga kirja
                }
                return BadRequest();
            }
        }
    }
}


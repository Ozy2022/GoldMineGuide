using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using GoldMineGuide.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GoldMineGuide.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<GoldMineGuideUser> _signInManager;
        private readonly UserManager<GoldMineGuideUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;


       public RegisterModel(
            UserManager<GoldMineGuideUser> userManager,
            SignInManager<GoldMineGuideUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;

    }

        public SelectList RoleSelectList = new SelectList(
             new List<SelectListItem>
             {
                 new SelectListItem { Selected =true, Text = "Select Role", Value = ""},
                 new SelectListItem { Selected =true, Text = "Manager", Value = "Manager"},
                 new SelectListItem { Selected =true, Text = "Staff", Value = "Staff"},
             }, "Value", "Text", 1);


        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Name is required in this form")]

            [StringLength(255, ErrorMessage = "The length of your name should be between 5 - 256 chars!", MinimumLength = 5)]
            [Display(Name = "Your Full Name")]
            public string StuffFullName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }


            [Required]
            [StringLength(255, ErrorMessage = "The length of your name should be between 5 - 256 chars!", MinimumLength = 5)]
            [Display(Name = "Your Home Address")]
            public string StuffAddress { get; set; }


            [Required]
            [Display(Name = "Your Date Of Birth")]
            [DataType(DataType.Date)]
            public DateTime StuffDOB { get; set; }


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "User Role")]
            public string MangerRole { get; set; }


        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new GoldMineGuideUser {
                    UserName = Input.StuffFullName,
                    Email = Input.Email,
                    StuffFullName = Input.StuffFullName,
                    StuffAddress = Input.StuffAddress,
                    StuffDOB = Input.StuffDOB,
                    EmailConfirmed = true,
                    MangerRole = Input.MangerRole
                    
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {

                    bool roleresult = await _roleManager.RoleExistsAsync("Manager");
                    if (!roleresult)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Manager"));
                    }
                    roleresult = await _roleManager.RoleExistsAsync("Staff");
                    if (!roleresult)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Staff"));
                    }
                    await _userManager.AddToRoleAsync(user, Input.MangerRole);


                    //_logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //"/Account/ConfirmEmail",
                    //pageHandler: null,
                    //values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        //return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        return RedirectToPage("login");
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}

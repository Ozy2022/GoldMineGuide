using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GoldMineGuide.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoldMineGuide.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<GoldMineGuideUser> _userManager;
        private readonly SignInManager<GoldMineGuideUser> _signInManager;

        public IndexModel(
            UserManager<GoldMineGuideUser> userManager,
            SignInManager<GoldMineGuideUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(255, ErrorMessage = "The length of your name should be between 5 - 256 chars!", MinimumLength = 5)]
            [Display(Name = "Your Full Name")]
            public string StuffFullName { get; set; }

            [Required]
            [StringLength(255, ErrorMessage = "The length of your name should be between 5 - 256 chars!", MinimumLength = 5)]
            [Display(Name = "Your Home Address")]
            public string StuffAddress { get; set; }


            [Required]
            [Display(Name = "Your Date Of Birth")]
            [DataType(DataType.Date)]
            public DateTime StuffDOB { get; set; }
        }

        private async Task LoadAsync(GoldMineGuideUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                StuffFullName = user.StuffFullName,
                StuffAddress = user.StuffAddress,
                StuffDOB = user.StuffDOB
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.StuffFullName != user.StuffFullName)
            {
                user.StuffFullName = Input.StuffFullName;
            }
            if (Input.StuffDOB != user.StuffDOB)
            {
                user.StuffDOB = Input.StuffDOB;
            }
            if (Input.StuffAddress != user.StuffAddress)
            {
                user.StuffAddress = Input.StuffAddress;
            }

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) 
            {
                StatusMessage = "Unexpected error when trying to set data.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

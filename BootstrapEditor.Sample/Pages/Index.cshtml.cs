using BootstrapEditor.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BootstrapEditor.Sample.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required]
        [Display(Name = "First Name", Prompt = "First Name")]
        [StringLength(20)]
        public string FirstName { get; set; } = default!;

        [BindProperty]
        [Required]
        [Display(Name = "Last Name", Prompt = "Last Name")]
        [StringLength(20)]
        public string LastName { get; set; } = default!;

        [BindProperty]
        [Required]
        [EmailAddress]
        [Display(Name = "Email", Prompt = "Email")]
        [StringLength(50)]
        public string EmailAddress { get; set; } = default!;

        [BindProperty]
        [Display(Name = "State")]
        [BootstrapColumnWidth(6)]
        [UIHint(nameof(StateList))]
        public string? State { get; set; } = default!;

        [BindProperty]
        [Display(Name = "Hobby")]
        [BootstrapColumnWidth(6)]
        public string? Hobby { get; set; } = default!;

        [BindProperty]
        [Display(Name = "Interest")]
        [BootstrapColumnWidth(6)]
        public string? Interest { get; set; } = default!;

        [BindProperty]
        [StringLength(125)]
        [UIHint("textarea")]
        [Display(Prompt = "Description (Optional)")]
        public string? Comment { get; set; }

        [BindProperty]
        [HiddenInput]
        public string HiddenTracker { get; set; } = default!;

        public IEnumerable<SelectListItem> StateList { get; set; } = new List<SelectListItem>
        {
            new("VA", "VA"),
            new("MD", "MD"),
            new("PA", "PA"),
            new("CA", "CA"),
            new("NY", "NY")
        };
    }
}

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BootstrapEditor;

public static class MvcBootstrapHtmlEditorExtensions
{
    public static IHtmlContent BootstrapEditorForModel(this IHtmlHelper htmlHelper)
    {
        return BootstrapForm.GenerateForm(htmlHelper);
    }
}

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq.Expressions;

namespace BootstrapEditor;

public static class MvcBootstrapHtmlEditorExtensions
{
    private static readonly BootstrapForm form = new();

    public static IHtmlContent BootstrapEditorForModel(this IHtmlHelper htmlHelper)
    {
        return BootstrapForm.GenerateForm(htmlHelper);
    }
}

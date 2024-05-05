using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;

namespace BootstrapEditor;

internal class BootstrapEditorHtmlContent : IEditorHtmlContent
{
    private readonly IHtmlContent _html;

    public BootstrapEditorHtmlContent(IHtmlContent html)
    {
        _html = html;
    }

    public bool IsFormGroupRequired { get; set; } = true;

    public bool IsValidationMessageRequired { get; set; } = true;

    public bool IsLabelRequired { get; set; } = true;

    public void WriteTo(TextWriter writer, HtmlEncoder encoder) => _html.WriteTo(writer, encoder);
}

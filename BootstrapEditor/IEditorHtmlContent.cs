using Microsoft.AspNetCore.Html;

namespace BootstrapEditor;

public interface IEditorHtmlContent : IHtmlContent
{
    bool IsFormGroupRequired { get; }

    bool IsValidationMessageRequired { get; }

    bool IsLabelRequired { get; }
}

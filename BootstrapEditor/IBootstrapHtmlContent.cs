using Microsoft.AspNetCore.Html;

namespace BootstrapEditor;

public interface IBootstrapHtmlContent : IHtmlContent
{
    bool IsFormGroupRequired { get; }

    bool IsValidationMessageRequired { get; }

    bool IsLabelRequired { get; }
}

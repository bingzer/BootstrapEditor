using BootstrapEditor;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor.Editors;

internal class InputDate : IBootstrapEditor
{
    internal static readonly IReadOnlySet<Type> DateOnlyTypes = new HashSet<Type> {
        typeof(DateTime),
        typeof(DateTimeOffset),
        typeof(DateOnly)
    };

    public bool AcceptModel(ModelExplorer modelExplorer)
    {
        var dataTypeName = modelExplorer.Metadata.DataTypeName?.ToLowerInvariant();
        if (dataTypeName?.Equals("DateTime", StringComparison.InvariantCultureIgnoreCase) == true)
        {
            return true;
        }

        if (IsTypeDateOnly(modelExplorer.ModelType))
        {
            return true;
        }

        return false;
    }

    public IHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        string? value = modelExplorer.Model switch
        {
            DateTimeOffset dateTimeOffset => dateTimeOffset.ToString("yyyy-MM-dd"),
            DateOnly dateOnly => dateOnly.ToString("yyyy-MM-dd"),
            DateTime dateTime => dateTime.ToString("yyyy-MM-dd"),
            _ => null
        };

        var editor = htmlHelper.TextBox(modelExplorer.Metadata.PropertyName,
            value,
            new { type = "date", @class = "form-control" }
        );

        return new BootstrapEditorHtmlContent(editor);
    }

    private static bool IsTypeDateOnly(Type? type)
    {
        if (type is null)
        {
            return false;
        }

        return DateOnlyTypes.Any(n => n == type || Nullable.GetUnderlyingType(type) != null &&
            IsTypeDateOnly(Nullable.GetUnderlyingType(type)));
    }
}
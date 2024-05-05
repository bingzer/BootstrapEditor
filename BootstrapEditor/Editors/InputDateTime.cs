using BootstrapEditor;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor.Editors;

internal class InputDateTime : IBootstrapEditor
{
    private static readonly IReadOnlySet<Type> DateTypes = new HashSet<Type> {
        typeof(DateTime),
        typeof(DateTimeOffset)
    };

    public bool AcceptModel(ModelExplorer modelExplorer)
    {
        var dataTypeName = modelExplorer.Metadata.DataTypeName?.ToLowerInvariant();
        if (dataTypeName?.Equals("DateTime", StringComparison.InvariantCultureIgnoreCase) == true)
        {
            return true;
        }

        if (IsTypeDateTime(modelExplorer.ModelType))
        {
            return true;
        }

        return false;
    }

    public IEditorHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        string? value = modelExplorer.Model switch
        {
            DateTimeOffset dateTimeOffset => dateTimeOffset.ToString("yyyy-MM-ddThh:mm"),
            DateOnly dateOnly => dateOnly.ToString("yyyy-MM-ddThh:mm"),
            DateTime dateTime => dateTime.ToString("yyyy-MM-ddThh:mm"),
            _ => null
        };

        var editor = htmlHelper.TextBox(modelExplorer.Metadata.PropertyName,
            value,
            new { type = "datetime-local", @class = "form-control" }
        );

        return new BootstrapEditorHtmlContent(editor);
    }

    private static bool IsTypeDateTime(Type? type)
    {
        if (type is null)
        {
            return false;
        }

        return DateTypes.Any(n => n == type || Nullable.GetUnderlyingType(type) != null &&
            IsTypeDateTime(Nullable.GetUnderlyingType(type)));
    }
}
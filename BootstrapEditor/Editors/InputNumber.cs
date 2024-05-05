using BootstrapEditor;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor.Editors;

public class InputNumber : IBoostrapEditor
{

    private static readonly IReadOnlySet<Type> WholeNumbers = new HashSet<Type>
    {
        typeof(byte),
        typeof(sbyte),
        typeof(short),
        typeof(ushort),
        typeof(int),
        typeof(uint),
        typeof(long),
        typeof(ulong)
    };

    private static readonly IReadOnlySet<Type> Decimals = new HashSet<Type>
    {
        typeof(float),
        typeof(double),
        typeof(decimal),
    };

    private static readonly IReadOnlySet<Type> NumericTypes = WholeNumbers.Union(Decimals).ToHashSet();

    public bool AcceptModel(ModelExplorer modelExplorer)
    {
        var modelType = modelExplorer.ModelType;

        return IsTypeNumeric(modelType);
    }

    public BootstrapEditorHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        var isDecimal = IsTypeDecimal(modelExplorer.ModelType);

        var editor = htmlHelper.TextBox(modelExplorer.Metadata.PropertyName,
            modelExplorer.Model,
            new { type = "number", @class = "form-control", step = isDecimal ? "0.01" : "1" }
        );

        return new BootstrapEditorHtmlContent(editor);
    }

    private static bool IsTypeDecimal(Type? type)
    {
        if (type is null)
        {
            return false;
        }

        return Decimals.Any(n => n == type || Nullable.GetUnderlyingType(type) != null &&
            IsTypeNumeric(Nullable.GetUnderlyingType(type)));
    }

    private static bool IsTypeNumeric(Type? type)
    {
        if (type is null)
        {
            return false;
        }

        return NumericTypes.Any(n => n == type || Nullable.GetUnderlyingType(type) != null &&
            IsTypeNumeric(Nullable.GetUnderlyingType(type)));
    }
}

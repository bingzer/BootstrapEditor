using BootstrapEditor;
using BootstrapEditor.Attributes;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor.Editors;

internal class TextArea : IBootstrapEditor
{
    public bool AcceptModel(ModelExplorer modelExplorer)
    {
        var templateHint = modelExplorer.Metadata.TemplateHint;

        return templateHint?.Equals("textarea", StringComparison.InvariantCultureIgnoreCase) == true;
    }

    public IEditorHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        var (rows, cols) = FindRowAndColumn(modelExplorer);

        var editor = htmlHelper.TextArea(modelExplorer.Metadata.PropertyName,
            new { @class = "form-control", rows, cols }
        );

        return new BootstrapEditorHtmlContent(editor);
    }

    private static (int? row, int? column) FindRowAndColumn(ModelExplorer modelExplorer)
    {
        if (modelExplorer.Metadata is not DefaultModelMetadata metadata)
        {
            return (null, null);
        }

        var textarea = metadata.Attributes
            ?.PropertyAttributes
            ?.OfType<TextAreaAttribute>()
            ?.FirstOrDefault();
        if (textarea is null)
        {
            return (null, null);
        }

        return (textarea.Rows, textarea.Columns);
    }
}

using BootstrapEditor;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor.Editors;

internal class Checkbox : IBootstrapEditor
{
    public bool AcceptModel(ModelExplorer modelExplorer)
    {
        var modelType = modelExplorer.ModelType;

        return modelType == typeof(bool) || modelType == typeof(bool?);
    }

    public IEditorHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        var builder = new HtmlContentBuilder();

        var editor = htmlHelper.CheckBox(modelExplorer.Metadata.PropertyName,
            modelExplorer.Model as bool? == true,
            new { @class = "form-check-input" });

        var label = htmlHelper.Label(modelExplorer.Metadata.PropertyName,
            null,
            new { @class = "form-check-label" });

        builder.AppendHtmlLine(@"<div class=""form-check"">");
        builder.AppendHtml(editor);
        builder.AppendHtml(label);
        builder.AppendHtml("</div>");

        return new BootstrapEditorHtmlContent(builder)
        {
            IsFormGroupRequired = true,
            IsLabelRequired = false,
            IsValidationMessageRequired = true
        };
    }
}
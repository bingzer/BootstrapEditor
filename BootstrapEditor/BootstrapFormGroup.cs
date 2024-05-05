using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using BootstrapEditor.Attributes;
using BootstrapEditor.Editors;

namespace BootstrapEditor;

internal static class BootstrapFormGroup
{

    public static IHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer? modelExplorer)
    {
        if (modelExplorer == null)
        {
            return HtmlString.Empty;
        }

        var editor = BootstrapEditorRegistrations.Editors.First(e => e.AcceptModel(modelExplorer));

        var htmlContent = editor.GenerateHtmlContent(htmlHelper, modelExplorer);
        if (htmlContent is not IBootstrapHtmlContent editorHtmlContent)
        {
            return htmlContent;
        }

        var isLabelRequired = editorHtmlContent.IsLabelRequired;
        var columnWidth = GetColumnWidth(modelExplorer);

        var builder = new HtmlContentBuilder();

        if (editorHtmlContent.IsFormGroupRequired)
        {
            builder.AppendHtmlLine($@"<div class=""{columnWidth}"">");
        }

        // label
        if (editorHtmlContent.IsLabelRequired)
        {
            builder.AppendHtml(Label(htmlHelper, modelExplorer));
        }

        // editor
        builder.AppendHtml(editorHtmlContent);

        // validation message
        if (editorHtmlContent.IsValidationMessageRequired)
        {
            builder.AppendHtml(ValidationMessage(htmlHelper, modelExplorer));
        }

        if (editorHtmlContent.IsFormGroupRequired)
        {
            builder.AppendHtmlLine($@"</div>");
        }

        return builder;
    }

    internal static string GetColumnWidth(ModelExplorer modelExplorer)
    {
        var columnWidth = "col-12";

        if (modelExplorer.Metadata is not DefaultModelMetadata metadata)
        {
            return columnWidth;
        }

        var bootstrapColumnWidth = metadata.Attributes.PropertyAttributes?
            .OfType<BootstrapColumnWidthAttribute>()?
            .FirstOrDefault();
        if (bootstrapColumnWidth is not null)
        {
            return bootstrapColumnWidth.ColumnWidth ?? columnWidth;
        }

        return columnWidth ?? "col-12";
    }

    private static IHtmlContent Label(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        var propertyName = modelExplorer.Metadata.PropertyName;

        var label = htmlHelper.Label(propertyName, null, new { @class = "form-label" });

        return label;
    }

    private static IHtmlContent ValidationMessage(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        var propertyName = modelExplorer.Metadata.PropertyName;

        var validation = htmlHelper.ValidationMessage(propertyName);

        return validation;
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BootstrapEditor;

internal class BootstrapForm
{
    public static IHtmlContent GenerateForm(IHtmlHelper htmlHelper)
    {
        var builder = new HtmlContentBuilder();

        var properties = GetEditableProperties(htmlHelper.ViewData.ModelExplorer);

        builder.AppendHtmlLine(@"<div class=""row g-2"">");
        foreach (var property in properties)
        {
            var htmlContent = BootstrapFormGroup.GenerateHtmlContent(htmlHelper, property);
            builder.AppendHtml(htmlContent);
            builder.AppendLine();
        }
        builder.AppendHtmlLine("</div>");

        return builder;
    }

    private static List<ModelExplorer> GetEditableProperties(ModelExplorer modelExplorer)
    {
        var properties = modelExplorer
            .Properties
            .Where(p => p.Metadata.IsBindingAllowed &&
                p.Metadata.ShowForEdit &&
                !p.Metadata.IsReadOnly &&
                !p.Metadata.IsComplexType &&
                HasBindPropertyAttribute(p.Metadata))
            .ToList();

        return properties;
    }

    private static bool HasBindPropertyAttribute(ModelMetadata modelMetadata)
    {
        if (modelMetadata is not DefaultModelMetadata metadata)
        {
            return false;
        }

        // check itself
        var bindPropertyAttribute = metadata.Attributes.PropertyAttributes?.OfType<BindPropertyAttribute>();
        if (bindPropertyAttribute?.Any() == true)
        {
            return true;
        }

        // check 
        if (metadata.ContainerMetadata is not DefaultModelMetadata containerMetadata)
        {
            return false;
        }

        var bindPropertiesAttributes = containerMetadata.Attributes.Attributes?.OfType<BindPropertiesAttribute>();
        if (bindPropertiesAttributes?.Any() == true)
        {
            return true;
        }

        return false;
    }
}

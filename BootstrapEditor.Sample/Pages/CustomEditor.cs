using BootstrapEditor.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor.Sample.Pages;

public class CustomEditor : IBootstrapEditor
{
    public bool AcceptModel(ModelExplorer modelExplorer)
    {
        return modelExplorer.GetTemplateHint() == "CustomEditor";
    }

    public IHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        var builder = new HtmlContentBuilder();
        builder.AppendHtml($"""
<div class="col-12 border bg-secondary">
    This is a CustomEditor for {modelExplorer.Metadata.Name}
</div>
""");

        return builder;
    }
}

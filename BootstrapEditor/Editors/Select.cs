using BootstrapEditor.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor.Editors;

internal class Select : IBootstrapEditor
{
    public bool AcceptModel(ModelExplorer modelExplorer)
    {
        var templateHint = modelExplorer.GetTemplateHint();
        if (templateHint is null)
        {
            return false;
        }

        var templateModelExplorer = modelExplorer.Container.GetExplorerForProperty(templateHint);
        if (templateModelExplorer is null)
        {
            return false;
        }

        if (templateModelExplorer.Model is IEnumerable<string> || templateModelExplorer.Model is IEnumerable<SelectListItem>)
        {
            return true;
        }

        var modelType = modelExplorer.ModelType;
        if (modelType.IsEnum)
        {
            return true;
        }

        return false;
    }

    public IHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        var selectList = CreateSelectList(modelExplorer);

        var editor = htmlHelper.DropDownList(modelExplorer.Metadata.PropertyName,
            selectList,
            new { @class = "form-select" }
        );

        return new BootstrapEditorHtmlContent(editor);
    }

    private static IEnumerable<SelectListItem> CreateSelectList(ModelExplorer modelExplorer)
    {
        var templateHint = modelExplorer.GetTemplateHint();
        var templateModelExplorer = modelExplorer.Container.GetExplorerForProperty(templateHint);
        var templateModel = templateModelExplorer.Model;

        if (templateModel is IEnumerable<SelectListItem> selectList)
        {
            return selectList;
        }

        if (templateModel is IEnumerable<string> strings)
        {
            return strings.Select(s => new SelectListItem { Text = s, Value = s });
        }

        var modelType = modelExplorer.ModelType;
        if (modelType.IsEnum)
        {
            // TODO: enum?
            var enumSelectList = new List<SelectListItem>();
            foreach (var e in Enum.GetValues(modelType))
            {
                enumSelectList.Add(new SelectListItem { Text = e.ToString(), Value = ((int)e).ToString() });
            }

            return enumSelectList;
        }

        return Array.Empty<SelectListItem>();
    }
}
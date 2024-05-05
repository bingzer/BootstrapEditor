using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace BootstrapEditor.Editors;

internal class Select : IBoostrapEditor
{
    public bool AcceptModel(ModelExplorer modelExplorer)
    {
        var templateHint = GetTemplateHint(modelExplorer);
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

    public BootstrapEditorHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
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
        var templateHint = GetTemplateHint(modelExplorer);
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

    /// <summary>
    /// Returns TemplateHint if available otherwise look for UIHint property and get it from there
    /// </summary>
    /// <param name="modelExplorer"></param>
    /// <returns></returns>
    private static string? GetTemplateHint(ModelExplorer modelExplorer)
    {
        var templateHint = modelExplorer.Metadata.TemplateHint;
        if (templateHint is not null)
        {
            return templateHint;
        }

        if (modelExplorer.Metadata is DefaultModelMetadata dm)
        {
            var uiHint = dm.Attributes
                .PropertyAttributes
                .OfType<UIHintAttribute>()
                .Where(att => att.UIHint != null)
                .FirstOrDefault();

            return uiHint?.UIHint;

        }

        return null;
    }
}
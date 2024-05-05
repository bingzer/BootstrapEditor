using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor;

public interface IBootstrapEditor
{
    /// <summary>
    /// True if accepting the meodel
    /// </summary>
    /// <param name="modelExplorer"></param>
    /// <returns></returns>
    bool AcceptModel(ModelExplorer modelExplorer);

    /// <summary>
    /// Generates IHtmlContent
    /// </summary>
    /// <param name="htmlHelper"></param>
    /// <param name="modelExplorer"></param>
    /// <returns></returns>
    IEditorHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer);
}

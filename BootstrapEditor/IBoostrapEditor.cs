using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor;

public interface IBoostrapEditor
{
    bool AcceptModel(ModelExplorer modelExplorer);

    BootstrapEditorHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer);
}

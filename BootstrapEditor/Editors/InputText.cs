using BootstrapEditor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor.Editors;

public class InputText : IBoostrapEditor
{
    public bool AcceptModel(ModelExplorer modelExplorer) => true;

    public BootstrapEditorHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        var inputType = modelExplorer.Metadata.DataTypeName ?? "text";

        var editor = htmlHelper.TextBox(modelExplorer.Metadata.PropertyName,
            modelExplorer.Model,
            new { type = inputType, @class = "form-control" }
        );

        return new BootstrapEditorHtmlContent(editor);
    }
}

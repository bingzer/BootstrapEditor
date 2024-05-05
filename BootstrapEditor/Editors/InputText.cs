using BootstrapEditor;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor.Editors;

internal class InputText : IBootstrapEditor
{
    public bool AcceptModel(ModelExplorer modelExplorer) => true;

    public IHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        var inputType = modelExplorer.Metadata.DataTypeName ?? "text";

        var editor = htmlHelper.TextBox(modelExplorer.Metadata.PropertyName,
            modelExplorer.Model,
            new { type = inputType, @class = "form-control" }
        );

        return new BootstrapEditorHtmlContent(editor);
    }
}

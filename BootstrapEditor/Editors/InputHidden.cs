﻿using BootstrapEditor;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BootstrapEditor.Editors;

internal class InputHidden : IBootstrapEditor
{
    public bool AcceptModel(ModelExplorer modelExplorer)
    {
        var dataTypeName = modelExplorer.Metadata.DataTypeName?.ToLowerInvariant();
        var uiHint = modelExplorer.Metadata.TemplateHint?.ToLowerInvariant();

        // type == hidden
        return dataTypeName == "hidden" || uiHint == "hiddeninput";
    }

    public IEditorHtmlContent GenerateHtmlContent(IHtmlHelper htmlHelper, ModelExplorer modelExplorer)
    {
        var editor = htmlHelper.TextBox(modelExplorer.Metadata.PropertyName,
            modelExplorer.Model,
            new { type = "hidden" }
        );

        return new BootstrapEditorHtmlContent(editor)
        {
            IsFormGroupRequired = false,
            IsLabelRequired = false,
            IsValidationMessageRequired = false
        };
    }
}

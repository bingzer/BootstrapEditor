using BootstrapEditor.Editors;

namespace BootstrapEditor;

public static class BootstrapEditorRegistrations
{
    private static readonly List<IBootstrapEditor> editors = new()
    {
        new InputDate(),
        new InputDateTime(),
        new InputHidden(),
        new InputNumber(),
        new Select(),
        new TextArea(),
        new Checkbox(),
        new InputText()
    };

    public static void RegistorEditor(IBootstrapEditor editor)
    {
        editors.Insert(0, editor);
    }

    public static readonly IReadOnlyList<IBootstrapEditor> Editors = editors;
}

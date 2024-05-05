using System.ComponentModel.DataAnnotations;

namespace BootstrapEditor.Annotations;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class TextAreaAttribute : UIHintAttribute
{
    public TextAreaAttribute() : base("textarea")
    {

    }

    public TextAreaAttribute(int rows) : this(rows, -1)
    {
    }

    public TextAreaAttribute(int rows, int columns) : this()
    {
        Rows = rows > 0 ? rows : null;
        Columns = columns > 0 ? rows : null;
    }

    public int? Rows { get; }
    public int? Columns { get; }
}
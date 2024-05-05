using System.ComponentModel.DataAnnotations;

namespace BootstrapEditor.Annotations;

/// <summary>
/// Bootstrap's Layout/Grid system
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class BootstrapColumnWidthAttribute : UIHintAttribute
{
    public BootstrapColumnWidthAttribute()
        : base(default!, "Bootstrap")
    {
    }

    public BootstrapColumnWidthAttribute(int width) : this($"col-{width}")
    {
    }

    public BootstrapColumnWidthAttribute(string columnWidth) : this()
    {
        ColumnWidth = columnWidth;
    }

    public string? ColumnWidth { get; }
}

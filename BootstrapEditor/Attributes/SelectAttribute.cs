﻿using System.ComponentModel.DataAnnotations;

namespace BootstrapEditor.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class SelectAttribute : UIHintAttribute
{
    public SelectAttribute(string selectListName): base(selectListName)
    {
        SelectListName = selectListName;
    }

    public string SelectListName { get; }
}

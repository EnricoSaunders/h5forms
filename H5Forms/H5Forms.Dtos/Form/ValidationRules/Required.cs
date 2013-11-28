﻿using System.Security.AccessControl;

namespace H5Forms.Dtos.Form.ValidationRules
{
    public class Required: ValidationRule
    {
        public bool IsRequired { get; set; }
        public override bool IsValid(string value)
        {
            return !IsRequired || !string.IsNullOrEmpty(value);
        }
    }
}

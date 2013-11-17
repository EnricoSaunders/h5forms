﻿using System;

namespace H5Forms.Entities.Form.ValidationRules
{
    public class Required: ValidationRule
    {
        public override bool IsValid(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}

﻿using System.Diagnostics.CodeAnalysis;

namespace FXExchange.Models
{
    [ExcludeFromCodeCoverage]
    public class FXValidationResult
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
    }
}

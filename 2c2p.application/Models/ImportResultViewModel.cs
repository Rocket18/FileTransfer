using System;
using System.Collections.Generic;
using System.Linq;

namespace _2c2p.application.Models
{
    public class ValidationResult<T>
    {
        public ValidationResult(T model)
        {
            Payload = model;
        }

        public bool IsError { get; set; }
        public string Message { get; set; } = string.Empty;

        public List<FieldError> FieldErrors { get; set; } = new List<FieldError>();
        public T Payload { get;}

        public void AddFieldError(string fieldName, string fieldMessage)
        {
            if (string.IsNullOrWhiteSpace(fieldName))
                throw new ArgumentException("Empty field name");

            if (string.IsNullOrWhiteSpace(fieldMessage))
                throw new ArgumentException("Empty field message");

            // appending error to existing one, if field already contains a message
            var existingFieldError = FieldErrors.FirstOrDefault(e => e.FieldName.Equals(fieldName));
            if (existingFieldError == null)
                FieldErrors.Add(new FieldError { FieldName = fieldName, FieldMessage = fieldMessage });
            else
                existingFieldError.FieldMessage = $"{existingFieldError.FieldMessage}. {fieldMessage}";

            IsError = true;
        }

        public void AddEmptyFieldError(string fieldName, string contextInfo = null)
        {
            AddFieldError(fieldName, $"No value provided for field. Context info: {contextInfo}");
        }
    }

    public class FieldError
    {
        public string FieldName { get; set; }

        public string FieldMessage { get; set; }

       
    }
}

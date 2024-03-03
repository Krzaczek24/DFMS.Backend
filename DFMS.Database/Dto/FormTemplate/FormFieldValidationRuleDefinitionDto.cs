using System.Collections.Generic;

namespace DFMS.Database.Dto.FormTemplate
{
    public class FormFieldValidationRuleDefinitionDto
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Value { get; set; }
        public bool EditableMessage { get; set; }
        public bool EditableValue { get; set; }
        public IReadOnlyCollection<string> ValueTypes { get; set; }
    }
}

using System.Collections.Generic;

namespace DFMS.Database.Dto.FormTemplate
{
    public class FormFieldDefinitionDto
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public IReadOnlyCollection<string> ValueTypes { get; set; }
    }
}

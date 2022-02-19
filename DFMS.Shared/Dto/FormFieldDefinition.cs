using System.Collections.Generic;

namespace DFMS.Shared.Dto
{
    public class FormFieldDefinition
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public IReadOnlyCollection<string> ValueTypes { get; set; }
    }
}

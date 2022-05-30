using DocumentWorkflow.Core.Services;

namespace DocumentWorkflow.Core.Models
{
    public class NewDocument
    {
        public int CategoryId { get; set; }
        public List<ReplaceField> Fields { get; set; }
    }
}

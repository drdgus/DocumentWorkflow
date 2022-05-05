namespace DocumentWorkflow.Core.Services;

public class TemplateField
{
    public string Name { get; }
    public string NameForUser => NormalizeName();
    public InputTypes Type { get; }
    public int Order { get; }
    public string Value { get; set; }
    public bool IsDisabled { get; set; }
    public bool VisibleForUser { get; set; }
    public IEnumerable<Element> RequiredElements { get; set; } = new List<Element>();

    public TemplateField(string name, InputTypes type, int order, string value = "")
    {
        Name = name;
        Type = type;
        Value = value;
        Order = order;
    }

    public enum InputTypes
    {
        Text,
        Number,
        Datetime,
    }

    public enum Element
    {
        Students,
        Employees
    }

    private string NormalizeName()
    {
        return Name.Replace("$", "").Replace("_", " ");
    }

    public override string ToString()
    {
        return Name;
    }
}
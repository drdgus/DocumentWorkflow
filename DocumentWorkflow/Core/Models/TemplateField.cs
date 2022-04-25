namespace DocumentWorkflow.Core.Services;

public class TemplateField
{
    public string Name { get; set; }
    public string NameForUser => NormalizeName();
    public InputTypes Type { get; set; }
    public bool IsDisabled { get; set; }
    public bool VisibleForUser { get; set; }
    public string Value { get; set; }

    public enum InputTypes
    {
        text,
        number,
        datetime
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
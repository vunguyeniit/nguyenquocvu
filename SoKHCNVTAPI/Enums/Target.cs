namespace SoKHCNVTAPI.Enums;

public class Target
{
    private Target(string value) { Value = value; }

    public string Value { get; private set; }

    public static Target User   { get { return new Target("User"); } }
    public static Target Mission   { get { return new Target("Mission"); } }
    public static Target Organization    { get { return new Target("Organization"); } }
    public static Target OrganizationPartner    { get { return new Target("OrganizationPartner"); } }
    public static Target OrganizationStaff    { get { return new Target("OrganizationStaff"); } }

    public static Target Officer { get { return new Target("Officer"); } }
    public static Target Expert   { get { return new Target("Expert"); } }

    public override string ToString()
    {
        return Value;
    }
}
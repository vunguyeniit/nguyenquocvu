namespace SoKHCNVTAPI.Enums;

public enum WorkflowStatus
{
    Activate = 1,
    DeActivate = 0,
    Locked = 2,
    Completed = 3
}

public enum WorkflowStepStatus
{
    KhoiTao = 0,
    ChoDuyet = 1,
    Duyet = 2,
    DaKy = 3, // Kem theo MaChuKy
    TamDung= 4
}

//public enum WorkflowPermission
//{
//    ADD = "Add",
//    UPDATE = "Update",
//    APPROVE = "Approve",
//    SIGN = "Sign"
//}

public sealed class WorkflowPermission
{
    public static readonly WorkflowPermission ADD = new WorkflowPermission("Add");
    public static readonly WorkflowPermission UPDATE = new WorkflowPermission("Update");

    public static readonly WorkflowPermission APPROVE = new WorkflowPermission("Approve");
    public static readonly WorkflowPermission SIGN = new WorkflowPermission("Sign");

    private WorkflowPermission(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }
}
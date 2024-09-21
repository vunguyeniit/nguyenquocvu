using System;
using System.Runtime.Serialization;

namespace SoKHCNVTAPI.Enums;

public enum ActionTypeEnum
{
    Manage,
    Create,
    Show,
    Edit,
    Delete,
    Apporve,
    Notification,
    Denied,
    Upload,
};

public enum ModuleEnum
{
    Organization,
    Workflow,
    Role,
    User,
    Document
};
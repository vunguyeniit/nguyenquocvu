using System;
using System.ComponentModel.DataAnnotations;

namespace SoKHCNVTAPI.Models;

public class StepHasStatusModel
{
    [Required(ErrorMessage = "{0} không được để trống")]
    public required long StepId { get; set; }

    [Required(ErrorMessage = "{0} không được để trống")]
    public required string Name { get; set; }

    public short Status { get; set; } = 1;
}


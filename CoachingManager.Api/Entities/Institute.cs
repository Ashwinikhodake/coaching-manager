using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoachingManager.Api.Entities;

public class Institute
{
    public int Id { get; set; }
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(150)]
    public string OwnerName { get; set; } = string.Empty;
    [MaxLength(10)]
    public string Phone { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<User> Users { get; set; } = new();
    // public List<Batch> Batches { get; set; } = new();
    // public List<Student> Students { get; set; } = new();
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Academy.Models;

public partial class Group
{
    [Key]
    [Column("group_id")]
    [Display(Name = "ID")]
    public int GroupId { get; set; }

    [Column("group_name")]
    [StringLength(10, ErrorMessage = "Не более 10 символов")]
    [Required(ErrorMessage = "Укажите название группы")]
    [Display(Name = "Группа")]
    public string GroupName { get; set; } = null!;

    [Column("direction")]
    [Display(Name = "Направление")]
    public byte Direction { get; set; }

    [Column("start_date")]
    [Display(Name = "Дата начала")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
    public DateOnly? StartDate { get; set; }

    [Column("start_time")]
    [Display(Name = "Время начала")]
    [DisplayFormat(DataFormatString = "{0:HH\\:mm}")]
    public TimeOnly? StartTime { get; set; }

    [Column("learning_days")]
    [Display(Name = "Учебные дни")]
    public byte? LearningDays { get; set; }

    [ForeignKey("Direction")]
    [InverseProperty("Groups")]
    public virtual Direction DirectionNavigation { get; set; } = null!;

    [InverseProperty("GroupNavigation")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    [InverseProperty("GroupNavigation")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    [ForeignKey("Group")]
    [InverseProperty("Groups")]
    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
}

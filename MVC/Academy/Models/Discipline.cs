using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Academy.Models;

public partial class Discipline
{
    [Key]
    [Column("discipline_id")]
    [Display(Name = "ID")]
    public short DisciplineId { get; set; }

    [Column("discipline_name")]
    [StringLength(150, ErrorMessage = "Не более 150 символов")]
    [Required(ErrorMessage = "Укажите название дисциплины")]
    [Display(Name = "Дисциплина")]
    public string DisciplineName { get; set; } = null!;

    [Column("number_of_lessons")]
    [Range(1, 255, ErrorMessage = "От 1 до 255 занятий")]
    [Display(Name = "Количество занятий")]
    public byte NumberOfLessons { get; set; }

    [InverseProperty("DisciplineNavigation")]
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    [InverseProperty("DisciplineNavigation")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    [ForeignKey("Discipline")]
    [InverseProperty("Disciplines")]
    public virtual ICollection<Discipline> DependentDiscipline1s { get; set; } = new List<Discipline>();

    [ForeignKey("Discipline")]
    [InverseProperty("Disciplines")]
    public virtual ICollection<Direction> Directions { get; set; } = new List<Direction>();

    [ForeignKey("DependentDiscipline1")]
    [InverseProperty("DependentDiscipline1s")]
    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();

    [ForeignKey("RequiredDiscipline1")]
    [InverseProperty("RequiredDiscipline1s")]
    public virtual ICollection<Discipline> DisciplinesNavigation { get; set; } = new List<Discipline>();

    [ForeignKey("Discipline")]
    [InverseProperty("Disciplines")]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    [ForeignKey("Discipline")]
    [InverseProperty("DisciplinesNavigation")]
    public virtual ICollection<Discipline> RequiredDiscipline1s { get; set; } = new List<Discipline>();

    [ForeignKey("Discipline")]
    [InverseProperty("Disciplines")]
    public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
}

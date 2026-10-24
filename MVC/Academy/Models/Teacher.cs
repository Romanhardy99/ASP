using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Academy.Models;

public partial class Teacher
{
    [Key]
    [Column("teacher_id")]
    [Display(Name = "ID")]
    public short TeacherId { get; set; }

    [Column("last_name")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    [Display(Name = "Фамилия")]
    public string? LastName { get; set; }

    [Column("first_name")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    [Display(Name = "Имя")]
    public string? FirstName { get; set; }

    [Column("middle_name")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    [Display(Name = "Отчество")]
    public string? MiddleName { get; set; }

    [Column("birth_date")]
    [Display(Name = "Дата рождения")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
    public DateOnly? BirthDate { get; set; }

    [Column("email")]
    [StringLength(50)]
    [EmailAddress(ErrorMessage = "Неверный формат e-mail")]
    [Display(Name = "E-mail")]
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(16)]
    [Phone(ErrorMessage = "Неверный формат телефона")]
    [Display(Name = "Телефон")]
    public string? Phone { get; set; }

    [Column("photo", TypeName = "image")]
    [Display(Name = "Фото")]
    public byte[]? Photo { get; set; }

    [Column("work_since")]
    [Display(Name = "Работает с")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
    public DateOnly? WorkSince { get; set; }

    [Column("rate", TypeName = "smallmoney")]
    [Display(Name = "Ставка")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal? Rate { get; set; }

    [InverseProperty("TeacherNavigation")]
    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    [ForeignKey("Teacher")]
    [InverseProperty("Teachers")]
    public virtual ICollection<Discipline> Disciplines { get; set; } = new List<Discipline>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Academy.Models;

public partial class Student
{
    [Key]
    [Column("stud_id")]
    [Display(Name = "ID")]
    public int StudId { get; set; }

    [Column("last_name")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    [Required(ErrorMessage = "Укажите фамилию")]
    [Display(Name = "Фамилия")]
    public string LastName { get; set; } = null!;

    [Column("first_name")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    [Required(ErrorMessage = "Укажите имя")]
    [Display(Name = "Имя")]
    public string FirstName { get; set; } = null!;

    [Column("middle_name")]
    [StringLength(50, ErrorMessage = "Не более 50 символов")]
    [Display(Name = "Отчество")]
    public string? MiddleName { get; set; }

    [Column("birth_date")]
    [Display(Name = "Дата рождения")]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
    public DateOnly BirthDate { get; set; }

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

    [Column("group")]
    [Display(Name = "Группа")]
    public int? Group { get; set; }

    [InverseProperty("StudentNavigation")]
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    [InverseProperty("StudentNavigation")]
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    [InverseProperty("StudentNavigation")]
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    [ForeignKey("Group")]
    [InverseProperty("Students")]
    public virtual Group? GroupNavigation { get; set; }
}

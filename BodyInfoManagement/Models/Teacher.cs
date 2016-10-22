using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BodyInfoManagement.Models
{
    public class Teacher
    {
        [Key]
        [Required]
        [DisplayName("职工号")]
        [StringLength(50, ErrorMessage = "职工格式错误", MinimumLength = 5)]
        public string TeacherId { get; set; }

        [DisplayName("用户密钥")]
        public string SaltCode { get; set; }

        [DisplayName("密码")]
        [StringLength(50, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required]
        public string TeacherPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string TeacherEmail { get; set; }

        [DisplayName("是否可以修改数据")]
        public bool StudentAuth { get; set; }
    }
}
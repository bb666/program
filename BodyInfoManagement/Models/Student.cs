using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static BodyInfoManagement.ToolClass.MyEnumClass;

namespace BodyInfoManagement.Models
{
    public class Student
    {
        [Key]
        [Required]
        [DisplayName("学号")]
        [StringLength(10, ErrorMessage = "学号格式错误", MinimumLength = 10)]
        public string StudentId { get; set; }

        [Required]
        [DisplayName("学生性别")]
        public EnumGender StudentGender { get; set; }

        [DisplayName("用户密钥")]
        public string SaltCode { get; set; }

        [DisplayName("密码")]
        [StringLength(50, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Required]
        public string StudentPassword { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        public string ConfirmPassword { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string StudentEmail { get; set; }

        [DisplayName("是否可以修改数据")]
        public bool StudentAuth { get; set; }

        public virtual ICollection<HealthInfo> HealthInfoes { get; set; }
    }
}
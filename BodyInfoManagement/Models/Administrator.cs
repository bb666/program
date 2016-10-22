using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BodyInfoManagement.Models
{
    public class Administrator
    {
        [Key]
        [Required]
        [DisplayName("管理员ID")]
        public string AdministratorId { get; set; }

        [DisplayName("密码")]
        [Required]
        public string AdministratorPassword { get; set; }
    }
}
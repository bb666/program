using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static BodyInfoManagement.ToolClass.MyEnumClass;

namespace BodyInfoManagement.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string LoginAccount { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string LoginPassword { get; set; }

        [Required]
        public EnumTyoe LoginType { get; set; }
    }
}
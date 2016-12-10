using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BodyInfoManagement.Models
{
    //男：身高  体重  1000米  引体向上 100米 立定跳远 肺活量 坐位体前屈 
    //女：身高 体重  800米 仰卧起坐 50米 立定跳远 肺活量 坐位体前屈
    public class HealthInfo
    {
        [Key]
        public int HealthInfoId { get; set; }

        public string StudentId { get; set; }
        public virtual Student Student { get; set; }

        [Required]
        [DisplayName("测量日期")]
        public DateTime ExamDate { get; set; }

        [Required]
        [DisplayName("身高（cm）")]
        public double Height { get; set; }

        [Required]
        [DisplayName("体重（kg）")]
        public double Weigh { get; set; }

        [Required]
        [DisplayName("立定跳远（cm）")]
        public double Jump { get; set; }

        [Required]
        [DisplayName("肺活量")]
        public double Breath { get; set; }

        [Required]
        [DisplayName("坐位体前屈（cm）")]
        public double Seated { get; set; }

        [Required]
        [DisplayName("1000米/800米")]
        public string LongRun { get; set; }

        [Required]
        [DisplayName("100米/50米")]
        public string ShortRun { get; set; }

        [Required]
        [DisplayName("引体向上/仰卧起坐")]
        public int Pull { get; set; }
    }
}
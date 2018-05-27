using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GymPlannerWeb
{
    [MetadataType(typeof(UsersMD))]
    partial class Users
    {
        public class UsersMD
        {
            [Required(ErrorMessage = "Це поле обов'язкове")]
            [Display(Name = "Логін")]
            public string Login { get; set; }

            [Required(ErrorMessage = "Це поле обов'язкове")]
            [DataType(DataType.Password)]
            [StringLength(16, MinimumLength = 4, ErrorMessage = "Пароль мусить містити від 4 до 16 символів")]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [Display(Name = "Ім'я")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public string Name { get; set; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Wed_Do_An.Models;

namespace Wed_Do_An.ViewModel
{
    public class loginVM
    {
        MyDataDataContext db = new MyDataDataContext();
        [Required(ErrorMessage = "Username can't be blank")]
        public string tentk { get; set; }
        [Required(ErrorMessage = "Password can't be plank")]
        public string mk { get; set; }
    }
}
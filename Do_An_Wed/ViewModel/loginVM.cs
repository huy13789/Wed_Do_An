using Do_An_Wed.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Do_An_Wed.ViewModel
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
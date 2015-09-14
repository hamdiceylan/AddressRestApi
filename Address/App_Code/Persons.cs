using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Address.App_Code
{
    public class Persons
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public Nullable<int> Gender { get; set; }
        public string Email { get; set; }
    }
}
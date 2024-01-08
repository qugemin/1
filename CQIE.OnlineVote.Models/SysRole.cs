 //﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQIE.OnlineVote.Models
{
    public class SysRole

    {  
        //回复正常
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public string? Describe { get; set;}

        public  ICollection<USerRole>?  USerRoles { get; set; }
        public ICollection<MenuRole>? MenuRoles {  get; set; } 
    
    }
}

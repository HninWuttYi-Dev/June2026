using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace June2026.EntityFrameworkCore
{
    [Table("Tbl_User")]
    public class UserEntity
    {
        [Key]
        [Column("userId")]
        public int id {get; set;}
        public string name {get; set;}
    }
}
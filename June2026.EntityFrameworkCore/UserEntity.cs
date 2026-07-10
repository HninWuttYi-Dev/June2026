using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace June2026.EntityFrameworkCore
{
    [Table("Tbl_Staff")]
    public class UserEntity
    {
        [Key]
        [Column("Id")]
        public int userId {get; set;}
        public string name {get; set;}
    }
}
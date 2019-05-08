using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Code_Sample.Dots
{
    public class RoleDto
    {
        public int Id { get; set; }

        
        [Required]
        public string Name { get; set; }
    }
}

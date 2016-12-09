using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Entities
{
    public class Role : IdentityRole<int>
    {
        public string Description { get; set; }
    }
}

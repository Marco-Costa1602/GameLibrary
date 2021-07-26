using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibrary.Models
{
    public class Client : IdentityUser
    {
        public double Funds { get; set; }
        public List<Game> GameLibrary { get; set; }
    }
}

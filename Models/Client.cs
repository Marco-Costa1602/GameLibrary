using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GameLibrary.Models
{
    /// <summary>
    /// Default Identity Model
    /// </summary>
    public class Client : IdentityUser
    {
        /// <summary>
        /// Client Funds - used to buy games
        /// </summary>
        public double Funds { get; set; }

        /// <summary>
        /// List of games owned by the Client
        /// </summary>
        public List<Game> GameLibrary { get; set; }
    }
}

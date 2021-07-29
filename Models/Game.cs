using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameLibrary.Models
{
    /// <summary>
    /// Game Model
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Game Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Description of the Game
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Price of the game
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// List of Clients that own that game
        /// </summary>
        public List<Client> Clients { get; set; }

        /// <summary>
        /// System Requirements for the game
        /// </summary>
        public SystemRequirements GameRequirements { get; set; }
    }
}

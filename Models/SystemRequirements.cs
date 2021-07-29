using System.ComponentModel.DataAnnotations.Schema;

namespace GameLibrary.Models
{
    /// <summary>
    /// Requirements of the Game
    /// </summary>
    [NotMapped]
    public class SystemRequirements
    {
        /// <summary>
        /// Operational System
        /// </summary>
        public string Os { get; set; }

        /// <summary>
        /// Processor 
        /// </summary>
        public string Cpu { get; set; }

        /// <summary>
        /// Required amount of memory
        /// </summary>
        public int Memory { get; set; }

        /// <summary>
        /// Required amount of storage
        /// </summary>
        public int Storage { get; set; }
    }
}

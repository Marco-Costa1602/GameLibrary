using System.ComponentModel.DataAnnotations.Schema;

namespace GameLibrary.Models
{
    [NotMapped]
    public class SystemRequirements
    {
        public string Os { get; set; }
        public string Cpu { get; set; }
        public int Memory { get; set; }
        public int Storage { get; set; }
    }
}

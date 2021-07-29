using System;
using System.Collections.Generic;

namespace GameLibrary.Models
{
    /// <summary>
    /// Sale Model
    /// </summary>
    public class Sale
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sum of the games in the sale
        /// </summary>
        public double TotalPrice { get; set; }

        /// <summary>
        /// Date of the sale
        /// </summary>
        public DateTime Datetime { get; set;}
        
        /// <summary>
        /// Id of the Client who made the sale
        /// </summary>
        public string ClientId { get; set; }
        
        /// <summary>
        /// Client object who made the sale
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// List of games added to the sale
        /// </summary>
        public List<Game> Games { get; set; }
    }
}

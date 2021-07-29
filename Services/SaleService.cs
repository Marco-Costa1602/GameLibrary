using GameLibrary.Data;
using GameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.Services
{
    /// <summary>
    /// Service for managing sales
    /// </summary>
    public class SaleService
    {
        #region// DEPENDENCIES
        GLContext _context;
        /// <summary>
        /// Constructor made to import the context
        /// </summary>
        /// <param name="context"></param>
        public SaleService(GLContext context)
        {
            _context = context;
        }
        #endregion

        #region// GET
        /// <summary>
        /// Uses a SQL Query to select all sales made by the current Client
        /// </summary>
        /// <param name="clientId">Current Client ID</param>
        /// <returns>Returns a list of sales of the user</returns>
        public List<Sale> Get(string clientId)
        {
            var query = from s in _context.Set<Sale>()
                        from u in _context.Set<Client>()
                        where s.ClientId == clientId
                        && u.Id == clientId
                        select new Sale()
                        {
                            Id = s.Id,
                            Datetime = s.Datetime,
                            ClientId = s.ClientId,
                            Games = s.Games,
                        };

            return query.ToList();
        }

        /// <summary>
        /// Function to get all sales made
        /// </summary>
        /// <returns>Return all sales in the context</returns>
        public List<Sale> Get()
        {
            var query = from s in _context.Set<Sale>()
                        select new Sale()
                        {
                            Id = s.Id,
                            Datetime = s.Datetime,
                            ClientId = s.ClientId,
                            Games = s.Games,
                        };

            return query.ToList();
        }
        #endregion

        #region// CREATE
        /// <summary>
        /// Creates a new sale (Cant buy same itens as before)
        /// </summary>
        /// <param name="sale">Sale Object</param>
        /// <param name="userId">Current user Id</param>
        /// <returns>Returns a boolean</returns>
        public bool Create(Sale sale, string userId)
        {
            try
            {
                if (sale.Games.Count == 0) throw new InvalidOperationException("There are no products in the sale");

                var user = _context.Clients.FirstOrDefault(c => c.Id == userId);
                List<Game> shopList = new();
                double salePrice = 0;

                foreach(var p in sale.Games)
                {
                    salePrice += getGamePrice(p.Name).Price;
                    var product = _context.Games.FirstOrDefault(g => g.Name == p.Name);
                    if (product == null) throw new InvalidOperationException("There are invalid products in your order");
                    shopList.Add(product);
                }

                sale.Games = shopList;
                sale.Datetime = DateTime.Now;
                sale.TotalPrice = salePrice;

                if (user.Funds < salePrice) return false;
                user.Funds -= salePrice;
                user.GameLibrary = shopList;

                _context.Add(sale);
                _context.Clients.Update(user);

                _context.SaveChanges();

                return true;
            }
            catch { return false; }

        }


        private Game getGamePrice(string gameName)
        {
            var query = from g in _context.Set<Game>()
                        where g.Name == gameName
                        select new Game()
                        {
                            Price = g.Price
                        };
            return query.First();

        }
        #endregion
    }
}

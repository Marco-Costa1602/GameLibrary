using GameLibrary.Data;
using GameLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.Services
{
    /// <summary>
    /// Service for managing Game objects in the Context
    /// </summary>
    public class GameService
    {

        #region// CONTEXT
        GLContext _context;
        /// <summary>
        /// Constructor for importing the context
        /// </summary>
        /// <param name="context">Context type</param>
        public GameService(GLContext context)
        {
            _context = context;
        }
        #endregion

        #region// GET
        /// <summary>
        /// Gets all Games in the context
        /// </summary>
        /// <returns>Returns a list of containing all games</returns>
        public List<Game> Get() => _context.Games.ToList();

        /// <summary>
        /// Gets a specific Game by its ID
        /// </summary>
        /// <param name="id">Id of the Game</param>
        /// <returns>Return a Game Object</returns>
        public Game Get(int id) => _context.Games.FirstOrDefault(g => g.Id == id);

        /// <summary>
        /// Get a specific Game by its Name
        /// </summary>
        /// <param name="name">Name of the object</param>
        /// <returns>Returns a Game object with the required name</returns>
        public Game Get(string name) => _context.Games.FirstOrDefault(g => g.Name == name);

        /// <summary>
        /// Gets all games owned by the current Client
        /// </summary>
        /// <param name="userId">Id of the current Client</param>
        /// <returns>Returns a List of Games owned by the current Client</returns>
        public List<Game> getCurrent(string userId)
        {
            var query = from c in _context.Set<Client>()
                        from g in _context.Set<Game>()
                        where c.Id == userId
                        select new Client()
                        {
                            Id = c.Id,
                            GameLibrary = c.GameLibrary,
                        };
            
            var library = query.First();
            return library.GameLibrary;
        }
        #endregion

        #region// CREATE
        /// <summary>
        /// Creates a New game object in the context
        /// </summary>
        /// <param name="game">Game object</param>
        /// <returns>Returns a boolean</returns>
        public bool Create(Game game)
        {
            try
            {
                var exists = _context.Games.FirstOrDefault(g => g.Name == game.Name) != null;
                if (exists) return false;

                _context.Games.Add(game);
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        #endregion

        #region// UPDATE
        /// <summary>
        /// Updates an existing game
        /// </summary>
        /// <param name="game">Game Object</param>
        /// <returns>Returns a boolean</returns>
        public bool Update(Game game)
        {
            try
            {
                var exists = Get(game.Id);
                var valid = game != null;
                if (exists == null || !valid) return false;

                exists.Name = game.Name;
                exists.Description = game.Description;
                exists.Price = game.Price;
                exists.GameRequirements = game.GameRequirements;

                _context.Games.Update(exists);
                _context.SaveChanges();

                game = exists;

                return true;
            }
            catch { return false; }
        }
        #endregion

        #region// DELETE
        /// <summary>
        /// Deletes an existing game
        /// </summary>
        /// <param name="id">Game ID</param>
        /// <returns>Returns a boolean</returns>
        public bool Delete(int id)
        {
            try
            {
                var exists = Get(id) != null;
                if (!exists) return false;

                _context.Games.Remove(Get(id));
                _context.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        #endregion
    }
}

using GameLibrary.Data;
using GameLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameLibrary.Services
{
    public class GameService
    {

        #region// CONTEXT
        GLContext _context;
        public GameService(GLContext context)
        {
            _context = context;
        }
        #endregion

        #region// GET
        public List<Game> Get() => _context.Games.ToList();
        public Game Get(int id) => _context.Games.FirstOrDefault(g => g.Id == id);
        public Game Get(string name) => _context.Games.FirstOrDefault(g => g.Name == name);
        #endregion

        #region// CREATE
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

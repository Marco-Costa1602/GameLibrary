using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace GameLibrary.API
{
    /// <summary>
    /// Add the RoleTypes to the existing roles
    /// </summary>
    public class RoleAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// Constructor of the RoleAuth
        /// </summary>
        /// <param name="r">Roles types Array</param>
        public RoleAuthorize(params RoleTypes [] r)
        {
            var roles = r.Select(x => Enum.GetName(typeof(RoleTypes), x));
            Roles = string.Join(",", roles);
        }
    }
}

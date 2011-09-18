using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

using TaskTracker.Models.Domain;
using TaskTracker.Models.ViewModels;

namespace TaskTracker.Models.Mapper
{
    public class ModelMapper<S, D> where S : class where D : class
    {
        public ModelMapper()
        {
            
        }

        public D Map(S source)
        {
            if (typeof(S) == typeof(User))
            {
                User user = source as User;
                if (typeof(D) == typeof(UserDetailViewModel))
                {
                    return this.MapUserToUserDetailViewModel(user) as D;
                }
            }
            throw new MappingException("Unknown mapping, " + typeof(S).ToString() + " to " + typeof(D).ToString());
        }

        private UserDetailViewModel MapUserToUserDetailViewModel(User user)
        {
            UserDetailViewModel model = new UserDetailViewModel();
            model.UserName = user.UserName;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;

            /*List<Role> roles = (from r in user.Roles
                                orderby r.Name
                                select r).ToList();
            model.Roles = roles;
            model.Id = user.Id;
            model.Active = user.Active;
            model.Creator = user.Creator;
            model.Modifier = user.Modifier;*/

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZone);
            model.TimeZone = tzi.DisplayName;

            return model;
        }
    }

    public class MappingException : Exception
    {
        public MappingException(string message)
            : base(message) { }
    }
}
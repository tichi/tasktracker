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
            if (typeof(S) == typeof(IUser))
            {
                IUser user = source as IUser;
                if (typeof(D) == typeof(UserDetailViewModel))
                {
                    return this.MapIUserToUserDetailViewModel(user) as D;
                }
            }
            throw new MappingException("Unknown mapping, " + typeof(S).ToString() + " to " + typeof(D).ToString());
        }

        private UserDetailViewModel MapIUserToUserDetailViewModel(IUser user)
        {
            UserDetailViewModel model = new UserDetailViewModel();
            model.UserName = user.UserName;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Id = user.Id;

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
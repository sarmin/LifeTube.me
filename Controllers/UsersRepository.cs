using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeTube.Models
{
    public class UsersRepository
    {
        internal List<User> FindPeople(string searchString, int maxResult)
        {
            List<User> names = new List<User>();

            var result = from n in names
                         where n.firstName.Contains(searchString)
                         orderby n.firstName
                         select n;

            return result.Take(maxResult).ToList();
        }
    }
}
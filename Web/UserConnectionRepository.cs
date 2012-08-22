using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Web
{
    public interface IUserConnectionRepository
    {
        string GetConnectionId(string userName);
        void Register(string userName, string connectionId);
        void Remove(string connectionId);
    }

    public class UserConnectionRepository : IUserConnectionRepository
    {
        /// <summary>
        /// I really don't like using this data structure. Initially, I was using a dictionary,
        /// however on the disconnect all that I have is the connectionId (the value of the dictionary)
        /// to remove the user from the list. I was unable to find a data structure that was going to do
        /// exactly what I wanted so I settled on the following, which honestly I despite.
        /// </summary>
        private readonly List<KeyValuePair<string, string>> _list;

        public UserConnectionRepository()
        {
            _list = new List<KeyValuePair<string, string>>();
        }

        public void Register(string userName, string connectionId)
        {
            RemoveExisting(userName);

            _list.Add(new KeyValuePair<string, string>(userName, connectionId));
        }

        public string GetConnectionId(string userName)
        {
            return _list.Single(x => x.Key == userName).Value;
        }

        public void Remove(string userName)
        {
            RemoveExisting(userName);
        }

        private void RemoveExisting(string userName)
        {
            var existingItem = _list.SingleOrDefault(x => x.Key == userName);
            if (existingItem.Equals(default(KeyValuePair<string, string>)))
            {
                _list.Remove(existingItem);
            }
        }
    }
}

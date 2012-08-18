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
    }

    public class UserConnectionRepository : IUserConnectionRepository
    {
        private readonly Dictionary<string, string> _dictionary;

        public UserConnectionRepository()
        {
            _dictionary = new Dictionary<string, string>();
        }

        public void Register(string userName, string connectionId)
        {
            _dictionary[userName] = connectionId;
        }

        public string GetConnectionId(string userName)
        {
            return _dictionary[userName];
        }
    }
}

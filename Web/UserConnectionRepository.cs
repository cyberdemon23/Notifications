using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notifications.Web
{
    public interface IUserConnectionRepository
    {
        IEnumerable<string> GetConnectionIds(string userName);
        void Register(string userName, string connectionId);
        void Deregister(string connectionId);
    }

    public class UserConnectionRepository : IUserConnectionRepository
    {
        private readonly List<KeyValuePair<string, string>> _list;

        public UserConnectionRepository()
        {
            _list = new List<KeyValuePair<string, string>>();
        }

        public void Register(string userName, string connectionId)
        {
            _list.Add(new KeyValuePair<string, string>(userName, connectionId));
        }

        public IEnumerable<string> GetConnectionIds(string userName)
        {
            return _list.Where(x => x.Key == userName).Select(x => x.Value).Distinct();
        }

        public void Deregister(string connectionId)
        {
            _list.RemoveAll(x => x.Value == connectionId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer.Routing
{
    class RoutingPath : IEquatable<RoutingPath>
    {
        public string route;
        public string method;

        public RoutingPath(string route, string method)
        {
            this.route = route;
            this.method = method;
        }

        public static implicit operator RoutingPath((string route, string method) data)
        {
            return new RoutingPath(data.route, data.method);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RoutingPath);
        }

        public bool Equals(RoutingPath other)
        {
            return other != null &&
                   route == other.route &&
                   method == other.method;
        }

        public override int GetHashCode()
        {
            var hashCode = 1576634622;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(route);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(method);
            return hashCode;
        }

        public static bool operator ==(RoutingPath path1, RoutingPath path2)
        {
            return EqualityComparer<RoutingPath>.Default.Equals(path1, path2);
        }

        public static bool operator !=(RoutingPath path1, RoutingPath path2)
        {
            return !(path1 == path2);
        }
    }
}

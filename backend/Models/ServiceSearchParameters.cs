using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubWatchApi.Models
{
    public class ServiceSearchParameters
    {
        public string Name { get; set; }

        public float? PriceMin { get; set; }

        public float? PriceMax { get; set; }

        public float? PriceExact { get; set; }

        public string Url { get; set; }

        public IEnumerable<ServiceInfo> Search(IQueryable<ServiceInfo> serviceDb)
        {
            return serviceDb.Where(TestService);
        }

        public bool TestService(ServiceInfo serviceInfo)
        {
            // maybe add property for doing fuzzy testing (like only testing if strings contain search terms
            // vs only finding exact matches)
            if (Name != null && Name.Length > 0 && !serviceInfo.Name.Contains(Name)) return false;
            if (PriceMin != null && PriceMin >= 0 && serviceInfo.Price < PriceMin) return false;
            if (PriceMax != null && PriceMax > 0 && serviceInfo.Price > PriceMax) return false;
            if (PriceExact >= 0 && serviceInfo.Price != PriceExact) return false;
            if (Url != null && Url.Length > 0 && serviceInfo.Url != Url) return false;

            return true;
        }
    }
}

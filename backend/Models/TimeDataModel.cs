using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SubWatchApi.Models
{
    public class TimeData
    {
        public string Username { get; set; }
        
        public string ServiceName { get; set; }

        [ForeignKey("ServiceName")]
        public virtual ServiceInfo Service { get; set; }

        public TimeSpan UsedTime { get
            {
                return EndTime - StartTime;
            }
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class TimeDataExtensionDTO
    {
        public string Url;
        [JsonProperty("start")]
        public DateTime StartTime;
        [JsonProperty("end")]
        public DateTime EndTime;
    }
}

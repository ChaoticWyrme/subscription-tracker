using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SubWatchApi.Models
{
    public class TimeData
    {
        [Key]
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Username { get; set; }
        
        public string ServiceName { get; set; }

        public long ServiceId { get; set; }

        [ForeignKey("ServiceId")]
        public virtual ServiceInfo Service { get; set; }

        public TimeSpan UsedTime { get
            {
                return EndTime - StartTime;
            }
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeData() { }
        public TimeData(SubWatchContext context, string username, TimeDataExtensionDTO sourceData)
        {

            var services = context.GetSubscribedServices(username);
            
            var service = services.Where(service => service.Url == sourceData.Url).FirstOrDefault();

            
        }

        public TimeData(string username, TimeDataExtensionDTO sourceData, ServiceInfo service)
        {
            Username = username;
            StartTime = sourceData.StartTime;
            EndTime = sourceData.EndTime;

        }
    }

    public class TimeDataExtensionDTO
    {
        public string Url { get; set; }
        [JsonProperty("start")]
        public DateTime StartTime { get; set; }
        [JsonProperty("end")]
        public DateTime EndTime { get; set; }
    }
}

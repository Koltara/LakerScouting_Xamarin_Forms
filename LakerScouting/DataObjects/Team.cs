using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakerScouting.DataObjects
{
    public class Team
    {
        string id;
        int teamId;
        string teamName;
        DateTime rookieYear;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "teamId")]
        public int TeamId
        {
            get { return teamId; }
            set { teamId = value; }
        }

        [JsonProperty(PropertyName = "teamName")]
        public string TeamName
        {
            get { return teamName; }
            set { teamName = value; }
        }

        [JsonProperty(PropertyName = "rookieYear")]
        public DateTime RookieYear
        {
            get { return rookieYear; }
            set { rookieYear = value; }
        }

        [Version]
        public string Version { get; set; }

    }
}

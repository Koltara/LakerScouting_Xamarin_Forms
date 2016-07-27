using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakerScouting.DataObjects
{
    public class Competition
    {
        string id;
        int competitionId;
        string location;
        List<Team> teamList;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "competitionId")]
        public int CompetitionId
        {
            get { return competitionId; }
            set { competitionId = value; }
        }

        [JsonProperty(PropertyName = "location")]
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        [JsonProperty(PropertyName = "teamList")]
        public List<Team> TeamList
        {
            get { return teamList; }
            set { teamList = value; }
        }

        [Version]
        public string Version { get; set; }

    }
}

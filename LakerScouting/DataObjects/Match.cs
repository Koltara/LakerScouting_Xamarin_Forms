using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakerScouting.DataObjects
{
    public class Match
    {
        string id;
        int matchNumber;
        Competition competitionId;
        Team teamId;

        int highGoalsScored;
        int highGoalsAttempted;
        bool crossLowBar;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "matchNumber")]
        public int MatchNumber
        {
            get { return matchNumber; }
            set { matchNumber = value; }
        }

        [JsonProperty(PropertyName = "competitionId")]
        public Competition CompetitionId
        {
            get { return competitionId; }
            set { competitionId = value; }
        }

        [JsonProperty(PropertyName = "teamId")]
        public Team TeamId
        {
            get { return teamId; }
            set { teamId = value; }
        }

        [JsonProperty(PropertyName = "highGoalsScored")]
        public int HighGoalsScored
        {
            get { return highGoalsScored; }
            set { highGoalsScored = value; }
        }

        [JsonProperty(PropertyName = "highGoalsAttempted")]
        public int HighGoalsAttempted
        {
            get { return highGoalsAttempted; }
            set { highGoalsAttempted = value; }
        }

        [JsonProperty(PropertyName = "crossLowBar")]
        public bool CrossLowBar
        {
            get { return crossLowBar; }
            set { crossLowBar = value; }
        }

        [Version]
        public string Version { get; set; }
    }
}

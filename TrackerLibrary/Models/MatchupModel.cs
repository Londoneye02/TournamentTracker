using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
   public class MatchupModel
    {
        /// <summary>
        /// The unique identifier for the matchup
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The set of teams that were involved in this match
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();
        /// <summary>
        /// The winner of the match
        /// </summary>
        public TeamModel    Winner{ get; set; } 
        /// <summary>
        /// Wich round this match is a part of
        /// </summary>
        public int MatchupRound { get; set; }
        
    }
}

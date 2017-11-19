using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary
{
   public static class TournamentLogic
    {
        //Order our list randomly of teams
        //Check if it is big enoug - if not, ad in byes 2*2*2*2   2^4
        //Create oour first round of matchups
        //Create every round after that 8 matchups - 4 matchups - 2 matchups - 1 matchups

        public static void CreateRounds (TournamentModel model)
        {
            List<TeamModel> randomizedTeams = RandomizeTeamOrder(model.EnteredTeams);
            int rounds = FindNumberOfRounds(randomizedTeams.Count);
            int byes = NumberOfByes(rounds, randomizedTeams.Count);

            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams));
            CreateOtherRounds(model, rounds);
        }

        private static void CreateOtherRounds (TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currRound = new List<MatchupModel>();
            MatchupModel currMatchup = new MatchupModel();
           while (round<=rounds)
            {
                foreach (MatchupModel match in previousRound)
                {
                    currMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = match });

                    if (currMatchup.Entries.Count>1)
                    {
                        currMatchup.MatchupRound = round;
                        currRound.Add(currMatchup);
                        currMatchup = new MatchupModel();

                    }
                }

                model.Rounds.Add(currRound);
                previousRound = currRound;
                currRound = new List<MatchupModel>();
                round += 1;
            }
        }
        private static List<MatchupModel> CreateFirstRound (int byes, List<TeamModel> teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel curr = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                curr.Entries.Add(new MatchupEntryModel { TeamCompeting = team });
                if (byes>0 || curr.Entries.Count>1)
                {
                    curr.MatchupRound = 1;
                    output.Add(curr);
                    curr = new MatchupModel();
                    if (byes>0)
                    {
                        byes -= 1;
                    }
                }

            }
            return output;
        } 
        private static int NumberOfByes (int rounds, int numberOfTeams)
        {
            int output = 0;
            int totalTeams = 1;

            for (int i = 1; i <= rounds; i++)
            {
                //hacemos un bucle desde 1 hasta el numero de rounds. 
                //Se podria utilizar math.pow, pero esta utiliza dobles y no le gusta a tim.
                totalTeams *= 2;
            }

            output = totalTeams - numberOfTeams;

            return output;
        }
        private static int FindNumberOfRounds (int teamCount)
        {
            int output = 1;
            int val = 2;
            while (val<teamCount)
            {
                output += 1;
                val *= 2;
            }
            return output;
        }
        private static List<TeamModel> RandomizeTeamOrder (List<TeamModel> teams)
        {
            //cards.orderby (a=>guid.newGuid()).ToList();
            //guid Es un identificador unico que c# le da a la lista. No es puramente random, pero es suficiente para esto

            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
    }
}

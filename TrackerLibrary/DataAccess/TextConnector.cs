using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection


    {

        private const string PrizesFile = "PrizeModels.csv";
        private const string PeopleFile = "PersonModels.csv";
        private const string TeamFile = "TeamModels.csv";
        private const string TournamentFile = "TournamentModels.csv";
        private const string MatchupFile = "MatchupModels.csv";
        private const string MatchupEntryFile = "MatchupEntryModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if (people.Count>0)
            {
                currentId= people.OrderByDescending(x => x.Id).First().Id + 1;
            }
            model.Id = currentId;

            people.Add(model);

            people.SaveToPeopleFile(PeopleFile);

            return model;
        }

        //TODO -  Wire up the Create Prize for text file
        public PrizeModel CreatePrize(PrizeModel model)

        {
            //Load the text file and  convert the text to List<PrizeModel>
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();
             
            //find the max ID
            int currentId = 1;
            if (prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1; //lo ordena por orden inverso de IDs y le añade 1
            }

            model.Id = currentId;
            // add the new record with the new ID (max+1)
            prizes.Add(model);
            //Convert the prizes to list<string>
            //Save the list<string> to the text file
            prizes.SaveToPrizeFile(PrizesFile);
            return model;
        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }

        public TeamModel CreateTeam(TeamModel model)  
        {
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);

            //find the max ID
            int currentId = 1;
            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1; //lo ordena por orden inverso de IDs y le añade 1
            }
            model.Id = currentId;
            teams.Add(model);

            teams.SaveToTeamFile(TeamFile);
            return model;
        }

        public List<TeamModel> GetTeam_All()
        {
          return TeamFile.FullFilePath().LoadFile().ConvertToTeamModels(PeopleFile);
        }

        public void CreateTournament(TournamentModel model)
        {
            List<TournamentModel> Tournaments = TournamentFile
                .FullFilePath()
                .LoadFile()
                .ConvertToTournamentModels(TeamFile, PeopleFile,PrizesFile);

            int currentId = 1;
            if (Tournaments.Count > 0)
            {
                currentId = Tournaments.OrderByDescending(x => x.Id).First().Id + 1; //lo ordena por orden inverso de IDs y le añade 1
            }
            model.Id = currentId;
            model.SaveRoundsToFile(MatchupFile, MatchupEntryFile);
            Tournaments.Add(model);
            Tournaments.SaveToTournamentFile (TournamentFile);
        }

       
    }
}

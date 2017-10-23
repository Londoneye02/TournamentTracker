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
        //TODO -  Wire up the Create Prize for text file
        public PrizeModel CreatePrize(PrizeModel model)

        {
            //Load the text file
            //Convert the text to List<PrizeModel>
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


    }
}

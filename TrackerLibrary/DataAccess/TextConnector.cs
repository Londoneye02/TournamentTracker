using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        //TODO -  Wire up the Create Prize for text file
        public PrizeModel CreatePrize(PrizeModel model)
        {  
            model.Id = 1;

            return model;
        }
    }
}

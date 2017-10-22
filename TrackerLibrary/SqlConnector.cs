using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class SqlConnector : IDataConnection
    {
        ///TODO-Make the CreatePrize method actually save to the database
        ///<summary>
        /// Saves a new prize to the database
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The prize information including the unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            //throw new NotImplementedException(); //El codigo lo compila, pero me tira una excepción de no implementado. Es sólo para que complie

            model.Id = 1;

            return model;
        }

         
    }
}
  
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

	//@PlaceNumber int,
	//@ nvarchar(50),
	//@PrizeAmount money,
 //   @PrizePercentage float,
	//@id int=0 output

namespace TrackerLibrary.DataAccess
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

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString("Tournaments")))
                //Utilizamos la instrucción using para que la connecxion se cierre correctamente si o sí.
                //Lo malo es que hay que abrir la conexion cada vez
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", 0, dbType: DbType.Int32,direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert",p,commandType:CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");
                 
                return model;
            }
        }

    }
}

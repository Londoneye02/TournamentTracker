using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig // Es visible desde cualquier sitio
    {
        //Esta lista es de IDataconnection, que es una interfaz, y que puede albergar cualquier cosa que implemente el contrato de la interfazç
        //ya sea SQL, text, MySQL.... 
        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnections(DatabaseType DB)
        {
            if (DB== DatabaseType.Sql ) 
            {
                //TODO - Set up the SQL connector properly
                SqlConnector sql = new SqlConnector();
                Connection= sql;
            }

            else if (DB == DatabaseType.TextFile)
            {
                //TODO - Create the Text Connection
                TextConnector text = new TextConnector();
                Connection=text;

            }
        }

        public static string CnnString(string name)

        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}

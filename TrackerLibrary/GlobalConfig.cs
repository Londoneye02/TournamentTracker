using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig // Es visible desde cualquier sitio
    {
        //Esta lista es de IDataconnection, que es una interfaz, y que puede albergar cualquier cosa que implemente el contrato de la interfazç
        //ya sea SQL, text, MySQL....
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();

        public static void InitializeConnections(bool database, bool textFiles)
        {
            if (database)
            {
                //TODO - Set up the SQL connector properly
                SqlConnector sql = new SqlConnector();
                Connections.Add(sql);
            }

            if (textFiles)
            {
                //TODO - Create the Text Connection
                TextConnection text = new TextConnection();
                Connections.Add(text);
                 
            }
        }
    }
}

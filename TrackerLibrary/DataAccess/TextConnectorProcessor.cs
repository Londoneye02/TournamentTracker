using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    //*Load the text file
          //Convert the text to List<PrizeModel>
         //find the max ID
         // add the new record with the new ID (max+1)
         //Save the list<string> to the text file

using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelpers // No quiero que todas las clases de TrackerLibrary.DataAcces vean los metodos de esta clase, solo utilizando un using
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)  //completes the full name to a path
        {
            //c:\.......\data\filename.csv
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }

        public static List<String> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                //si no existe devuelve una lista vacia
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModels( this List<string> lines) //Extension method. Añade una funcionalidad a string
        {  
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');//genera un array que esta separado por comas
                PrizeModel p = new PrizeModel();
                p.Id = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PrizeAmount = decimal.Parse(cols[3]);
                p.PrizePercentage = double.Parse(cols[4]);

                output.Add(p);
            }
            return output;
        }
        public static void SaveToPrizeFile(this List<PrizeModel> models, string filename) //Extension method. Añade una funcionalidad a List<PrizeModel>
        {
            List<string> lines = new List<string>();  
            foreach (PrizeModel p  in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }

            File.WriteAllLines(filename.FullFilePath(), lines);
        }
    }
}

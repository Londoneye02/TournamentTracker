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
        public static List<PersonModel> ConvertToPersonModels (this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();
            foreach (string line in lines)  
            {
                string[] cols = line.Split(',');

                PersonModel p = new PersonModel();
                p.Id = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.Lastname = cols[2];
                p.EmailAddress = cols[3];
                p.CellPhoneNumber = cols[4];
                output.Add(p);
                
            }
            return output;
        }
        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName) //Extension method. Añade una funcionalidad a List<PrizeModel>
        {
            List<string> lines = new List<string>();  
            foreach (PrizeModel p  in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string peopleFileName)
        {
            //id, team name, list of ids separated by the pipe
            //3, Tim's team, 1|3|5

            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = peopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach (string line in lines)
            {
                string [] cols = line.Split(',');

                TeamModel t = new TeamModel();
                t.Id = int.Parse(cols[0]);
                t.TeamName = cols[1];

                string[] personIds = cols[2].Split('|');

                foreach (string id in personIds)
                {
                    t.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }
                output.Add(t);
            }
            return output;
        }
        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id},{p.FirstName},{p.Lastname},{p.EmailAddress},{p.CellPhoneNumber}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
        public static void SaveToTeamFile (this List<TeamModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel t in models)
            {
                lines.Add($"{t.Id},{t.TeamName},{ConvertPeopleListToString(t.TeamMembers)}");
            }

            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        private static string ConvertPeopleListToString (List<PersonModel> people)
        {
            string output = "";

            if( people.Count==0)
            {
                return "";
            }

            foreach (PersonModel p in people)
            {
                output += $"{p.Id}|";

            }
            output = output.Substring(0, output.Length - 1);

            return output;
        }

    }
}

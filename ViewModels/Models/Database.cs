using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WeatherApp.ViewModels.Models
{
    public class Database
    {
        private SQLiteConnection dbConnection;
        public Database()
        {
            //Cesta k lokálnímu úložišti pro DB
            dbConnection = new SQLiteConnection(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.Personal),
            "history.db3")); //cesta ke složce

            dbConnection.CreateTable<HistoryData>();
        }

        public void PridatDoHistorie(HistoryData data)
        {
            dbConnection.Insert(data); //Zápis dat 
        }

        public List<HistoryData> CelaHistorie()
        {
            var data = dbConnection.Table<HistoryData>().ToList();
            return data; //vracení celé historie
        } 

    }
}

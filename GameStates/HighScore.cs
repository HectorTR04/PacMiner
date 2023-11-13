using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMiner.Managers;
using PacMiner.GameObjects;
using System;
using System.Collections.Generic;
using System.IO;
using PacMiner.Game_Objects;
using System.Data.SqlClient;
using PacMiner.BaseClasses;
using Microsoft.Xna.Framework.Input;
using static PacMiner.GameStates.InputSelect;
using static PacMiner.Managers.GameStateManager;
using System.Windows.Forms.Design.Behavior;
using PacMiner.GameStates;

namespace PacMiner.GameStates
{
    internal class HighScore
    {
        List<int> topScorers = new List<int>();
        private bool scoreInserted = false;

        public SqlConnection Connect()
        {
            string connectionString;
            SqlConnection cnn;
            connectionString = @"Server=localhost; Database=Score; User Id=HecticSwede; Password=123; TrustServerCertificate=True; Encrypt=False;";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            Console.WriteLine(cnn);
            cnn.Close();
            return cnn;
        }

        public void InsertScore()
        {
            if (!scoreInserted) 
            {
                ShowResults(); 
                scoreInserted = true; 
            }
        }
            public List<int> ShowResults()
            {
            SqlConnection cnn = Connect();
            cnn.Open();
            string query = "INSERT INTO High_Scores (HighScore)";
            query += " VALUES (@HighScore)";

            SqlCommand myCommand = new SqlCommand(query, cnn);
            myCommand.Parameters.AddWithValue("@HighScore", Level.scoreManager.Score);


            myCommand.ExecuteNonQuery();

            SqlCommand command = new SqlCommand("SELECT TOP 5 * FROM High_Scores ORDER BY HighScore DESC", cnn);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read() && topScorers.Count < 5)
                {
                    topScorers.Add(((int)reader["HighScore"]));
                }
            }
            cnn.Close();
            return topScorers;
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            List<int> topScorers = ShowResults(); 

            for (int i = 0; i < topScorers.Count; i++)
            {
               string text = $"{i + 1}: {topScorers[i]}";
               Vector2 position = new Vector2(100, 100 + i * 30); 
               spriteBatch.DrawString(TextureManager.highScore, text, position, Color.White);
            }
        }
    }

   
}


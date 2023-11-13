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
using System.Diagnostics;

namespace PacMiner.GameStates
{
    internal class Level
    {
        public static Tile[,] tileArray;
        public static int tileSize = 32;
        private Vector2 pauseMenuPos = new Vector2(113,160);

        public PacMan pacMan;
        Beermug beerMug;
        Pickaxe pickaxe;
        RedSugar redSugar;
        List<Gold> golds = new List<Gold>();
        Gold gold;
        HUD hud;
        Driller driller;
        Scout scout;
        Engineer engineer;

        //Score
        public static ScoreManager scoreManager;
        private HighScore highScore = new HighScore();

        enum LevelState
        {
            Playing,
            Paused,
        }
        LevelState currentLevelState { get; set; }

        

        internal void Setup()
        {     
            CreateLevel("pacmanlevel");          
            hud = new HUD();
            scoreManager = new ScoreManager();
            pacMan.health = 3;
            currentLevelState = LevelState.Playing;
        }
      
       

        internal void Update(GameTime gameTime)
        {   
            if (currentLevelState == LevelState.Playing)
            {
                foreach (Gold g  in golds)
                {
                    pacMan.CollisionGold(g);
                }            
                pacMan.Update(gameTime);
                beerMug.CollisionPacMan(pacMan);
                pickaxe.CollisionPacMan(pacMan);
                redSugar.CollisionPacMan(pacMan);
                driller.CollisionPacMan(pacMan);
                scout.CollisionPacMan(pacMan);
                engineer.CollisionPacMan(pacMan);
                driller.Update(gameTime);    
                scout.Update(gameTime);
                engineer.Update(gameTime);
                PauseGame();    
            }
            if (currentLevelState == LevelState.Paused)
            {
                PauseMenuNavigation();
            }
            CheckIfLost();
            CheckIfWin();
        }
        private void CheckIfLost()
        {
            if (pacMan.health == 0)
            {
                state = GameState.ResultsMenu;
            }
        }

        private void CheckIfWin()
        {
            for (int i = golds.Count - 1; i >= 0; i--)
            {
                Gold gold = golds[i];
                if (gold.isCollected)
                {
                    golds.RemoveAt(i);
                }
            }
            if (golds.Count == 0)
            {
                state = GameState.ResultsMenu;
                SoundManager.pacManWin.Play();
                highScore.InsertScore();
            }
        }
        private void PauseGame()
        {
            KeyMouseReader.Update();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (selectedInput == Input.Keyboard)
            {
                if (KeyMouseReader.KeyPressed(Keys.Escape))
                {
                    currentLevelState = LevelState.Paused;
                }
            }
            if (selectedInput == Input.Controller)
            {
                if (gamePadState.IsButtonDown(Buttons.Start))
                {
                    currentLevelState = LevelState.Paused;
                }
            }
        }

        private void PauseMenuNavigation()
        {
            KeyMouseReader.Update();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (selectedInput == Input.Keyboard)
            {
                if (KeyMouseReader.KeyPressed(Keys.Q))
                {
                    state = GameState.MainMenu;
                }
                if (KeyMouseReader.KeyPressed(Keys.Space))
                {
                    currentLevelState = LevelState.Playing;
                }
            }
            if (selectedInput == Input.Controller)
            {
                if (gamePadState.IsButtonDown(Buttons.A))
                {
                    currentLevelState = LevelState.Playing;
                }
                if (gamePadState.IsButtonDown(Buttons.B))
                {
                    state = GameState.MainMenu;
                }
            }
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Tile tile in tileArray)
            {
                tile.Draw(spriteBatch);
            }
            foreach (Gold g in golds)
            {
                g.Draw(spriteBatch, gameTime);
            }
            hud.Draw(spriteBatch, pacMan);
            pacMan.Draw(spriteBatch);
            beerMug.Draw(spriteBatch, gameTime);
            pickaxe.Draw(spriteBatch, gameTime);
            redSugar.Draw(spriteBatch, gameTime);
            driller.Draw(spriteBatch);
            scout.Draw(spriteBatch);
            engineer.Draw(spriteBatch);
            //Move this to a class at some point
            spriteBatch.Draw(TextureManager.announceTex, new Vector2(325,5), Color.White);
            spriteBatch.DrawString(TextureManager.announceText, " Alright Miners \n A glyphid has got \n into the spacerig. \n " +
            "Take care of it \n for a nice bonus.", new Vector2(427, 29), Color.White);

            if (currentLevelState == LevelState.Paused)
            {
                spriteBatch.Draw(TextureManager.pauseMenuTex, pauseMenuPos, Color.White);
            }
        }

        private List<string> ReadFromFile(string fileName)
        {
            StreamReader streamReader = new StreamReader("pacmanlevel.txt");
            List<string> result = new List<string>();

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                result.Add(line);
            }
            streamReader.Close();
            return result;
        }

        private void CreateLevel(string fileName)
        {
            List<string> list = ReadFromFile("pacmanlevel.txt");

            tileArray = new Tile[list[0].Length, list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[0].Length; j++)
                {
                    if (list[i][j] == 'w')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureManager.pacWallTex, true);
                    }
                    else if (list[i][j] == 'h')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureManager.hudBackgroundTex, true);
                    }
                    else if (list[i][j] == 'b')
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureManager.barrelTex, true);
                    }
                    else
                    {
                        tileArray[j, i] = new Tile(new Vector2(j * tileSize, i * tileSize), TextureManager.pacFloorTex, false);
                    }
                    if (list[i][j] == 'p')
                    {
                        pacMan = new PacMan(new Vector2(j * tileSize, i * tileSize), TextureManager.startTex, 100);
                    }
                    if ((list[i][j]) == 'm')
                    {
                        beerMug = new Beermug(new Vector2(j * tileSize, i * tileSize), TextureManager.beerMugTex);
                    }
                    if ((list[i][j]) == 'x')
                    {
                        pickaxe = new Pickaxe(new Vector2(j * tileSize, i * tileSize), TextureManager.pickaxeTex);
                    }
                    if ((list[i][j]) == '-')
                    {
                        gold = new Gold(new Vector2(j * tileSize, i * tileSize), TextureManager.goldTex);
                        golds.Add(gold);
                    }
                    if (((list[i][j]) == 'y'))
                    {
                        driller = new Driller(new Vector2(j * tileSize, i * tileSize), TextureManager.drillerTex, 100);
                    }
                    if (((list[i][j]) == 'z'))
                    {
                        scout = new Scout(new Vector2(j * tileSize, i * tileSize), TextureManager.scoutTex, 100);
                    }
                    if (((list[i][j]) == 'q'))
                    {
                        engineer = new Engineer(new Vector2(j * tileSize, i * tileSize), TextureManager.engineerTex, 100);
                    }
                    if (((list[i][j]) == 'u'))
                    {
                        redSugar = new RedSugar(new Vector2(j * tileSize, i * tileSize),TextureManager.redSugarTex);
                    }
                }
            }
        }
       
        internal static bool GetTileAtPosition(Vector2 pos)
        {
            return tileArray[(int)pos.X / tileSize, (int)pos.Y / tileSize].notWalkable;
        }

        
    }
}
    


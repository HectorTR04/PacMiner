using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PacMiner.GameStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PacMiner.Managers
{
    internal class GameStateManager
    {
        public static GameState state;

        public enum GameState
        {          
            InputSelect,
            MainMenu, 
            HighScore,
            LevelSelect,
            Level,
            ResultsMenu,

        }

        private InputSelect inputSelect = new InputSelect();
        private MainMenu mainMenu = new MainMenu();
        private ResultsMenu ResultsMenu = new ResultsMenu();
        private Level level = new Level();      
        private HighScore highScore = new HighScore();

        public void LoadContent(ContentManager content)
        {
            TextureManager.MenuScreenTextures(content);
            TextureManager.LoseScreenTextures(content);
            TextureManager.LevelTextures(content);
            SoundManager.LoadSounds(content);
        }

        internal void Update(GameTime gameTime)
        {
            switch (state)
            {
                case GameState.InputSelect:
                    inputSelect.Update(gameTime);
                    break;
                case GameState.MainMenu:
                    level.Setup();
                    mainMenu.Update(gameTime);
                    break;               
                case GameState.ResultsMenu:
                    ResultsMenu.Update(gameTime);
                    break;               
                case GameState.Level:
                    level.Update(gameTime);                    
                    break;
                

            }
        }
       
        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (state)
            {
                case GameState.InputSelect:
                    inputSelect.Draw(spriteBatch);
                    break;
                case GameState.MainMenu:
                    mainMenu.Draw(spriteBatch);
                    break;
                case GameState.HighScore:
                    highScore.Draw(spriteBatch);
                    break;
                case GameState.ResultsMenu:
                    ResultsMenu.Draw(spriteBatch);
                    break;              
                case GameState.Level:
                    level.Draw(spriteBatch, gameTime);    
                    break;
               
            }

        }
    }
}

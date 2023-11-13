using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMiner.Managers;
using PacMiner.BaseClasses;
using static PacMiner.Managers.GameStateManager;
using static PacMiner.GameStates.InputSelect;


namespace PacMiner.GameStates
{
    internal class ResultsMenu : Menu
    {
        private Vector2 loseScreenPos = Vector2.Zero;
        private Vector2 loseStatePos = new Vector2(250, 400);
        private bool menuCooldown;
        HighScore highScore = new HighScore();

        private enum ResultState
        {
            ResultState1,
            ResultState2,
        }
        ResultState CurrentResultState { get; set; }

        internal override void Update(GameTime gameTime)
        {
            NavigateMenu();
            ChangeGameState();
        }

        
        protected override void NavigateMenu()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (selectedInput == Input.Keyboard)
            {
                if (!menuCooldown)
                {
                    switch (CurrentResultState)
                    {
                        case ResultState.ResultState1:
                            if (keyboardState.IsKeyDown(Keys.Down))
                            {
                                CurrentResultState = ResultState.ResultState2;
                                menuCooldown = true;
                            }
                            break;
                        case ResultState.ResultState2:
                            if (keyboardState.IsKeyDown(Keys.Up))
                            {
                                CurrentResultState = ResultState.ResultState1;
                                menuCooldown = true;
                            }
                            break;
                    }
                }
                else if (keyboardState.IsKeyUp(Keys.Down) && keyboardState.IsKeyUp(Keys.Up))
                {
                    menuCooldown = false;
                }
            }
            if (selectedInput == Input.Controller)
            {
                if (!menuCooldown)
                {
                    switch (CurrentResultState)
                    {
                        case ResultState.ResultState1:
                            if (gamePadState.IsButtonDown(Buttons.DPadDown))
                            {
                                CurrentResultState = ResultState.ResultState2;
                                menuCooldown = true;
                            }
                            break;
                        case ResultState.ResultState2:
                            if (gamePadState.IsButtonDown(Buttons.DPadUp))
                            {
                                CurrentResultState = ResultState.ResultState1;
                                menuCooldown = true;
                            }
                            break;
                    }
                }
                else if (gamePadState.IsButtonUp(Buttons.DPadUp) && gamePadState.IsButtonUp(Buttons.DPadDown))
                {
                    menuCooldown = false;
                }
            }
        }
        protected override void ChangeGameState()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            if (selectedInput == Input.Keyboard)
            {
                switch (CurrentResultState)
                {
                    case ResultState.ResultState1:
                        if (keyboardState.IsKeyDown(Keys.Enter))
                        {
                            state = GameState.MainMenu;
                        }
                        break;
                    case ResultState.ResultState2:
                        if (keyboardState.IsKeyDown(Keys.Space))
                        {
                            System.Environment.Exit(0);
                        }
                        break;
                }
            }
            if (selectedInput == Input.Controller)
            {        
                switch (CurrentResultState)
                {
                    case ResultState.ResultState1:
                        if (gamePadState.IsButtonDown(Buttons.B))
                        {
                            state = GameState.MainMenu;
                        }
                        break;
                    case ResultState.ResultState2:
                        if (gamePadState.IsButtonDown(Buttons.B))
                        {
                            System.Environment.Exit(0);
                        }
                        break;
                }            
            }                
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.loseScreenTex, loseScreenPos, Color.White);

            switch (CurrentResultState)
            {
                case ResultState.ResultState1:
                    spriteBatch.Draw(TextureManager.loseState1Tex, loseStatePos, Color.White);
                    break;
                case ResultState.ResultState2:
                    spriteBatch.Draw(TextureManager.loseState2Tex, loseStatePos, Color.White);
                    break;
            }
        }
    }
}

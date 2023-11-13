using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PacMiner.Managers;
using PacMiner.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PacMiner.Managers.GameStateManager;
using static PacMiner.GameStates.InputSelect;

namespace PacMiner.GameStates
{
    internal class MainMenu : Menu
    {

        private Vector2 titleScreenPos = Vector2.Zero;
        private Vector2 menuStatePos = new Vector2(275, 400);
        private bool menuCooldown;

        enum MainMenuState
        {
            MenuState1,
            MenuState2,
            MenuState3,
        }
        MainMenuState CurrentMenuState { get; set; }

        public MainMenu()
        {
            CurrentMenuState = MainMenuState.MenuState1;
        }

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

                    switch (CurrentMenuState)
                    {
                        case MainMenuState.MenuState1:
                            if (keyboardState.IsKeyDown(Keys.Down))
                            {
                                CurrentMenuState = MainMenuState.MenuState2;
                                menuCooldown = true;
                            }
                            break;
                        case MainMenuState.MenuState2:
                            if (keyboardState.IsKeyDown(Keys.Down))
                            {
                                CurrentMenuState = MainMenuState.MenuState3;
                                menuCooldown = true;

                            }
                            else if (keyboardState.IsKeyDown(Keys.Up))
                            {
                                CurrentMenuState = MainMenuState.MenuState1;
                                menuCooldown = true;

                            }
                            break;
                        case MainMenuState.MenuState3:
                            if (keyboardState.IsKeyDown(Keys.Up))
                            {
                                CurrentMenuState = MainMenuState.MenuState2;
                                menuCooldown = true;

                            }
                            break;

                    }
                }
                else if ((keyboardState.IsKeyUp(Keys.Down) && keyboardState.IsKeyUp(Keys.Up)))
                {
                    menuCooldown = false;
                }
            }
            if (selectedInput == Input.Controller)
            {
                if (!menuCooldown)
                {
                    switch (CurrentMenuState)
                    {
                        case MainMenuState.MenuState1:
                            if (gamePadState.IsButtonDown(Buttons.DPadDown))
                            {
                                CurrentMenuState = MainMenuState.MenuState2;
                                menuCooldown = true;
                            }
                            break;
                        case MainMenuState.MenuState2:
                            if (gamePadState.IsButtonDown(Buttons.DPadDown))
                            {
                                CurrentMenuState = MainMenuState.MenuState3;
                                menuCooldown = true;

                            }
                            else if (gamePadState.IsButtonDown(Buttons.DPadUp))
                            {
                                CurrentMenuState = MainMenuState.MenuState1;
                                menuCooldown = true;

                            }
                            break;
                        case MainMenuState.MenuState3:
                            if (gamePadState.IsButtonDown(Buttons.DPadUp))
                            {
                                CurrentMenuState = MainMenuState.MenuState2;
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
                switch (CurrentMenuState)
                {
                    case MainMenuState.MenuState1:
                        if (keyboardState.IsKeyDown(Keys.Space))
                        {
                            state = GameState.Level;
                        }
                        break;
                    case MainMenuState.MenuState2:
                        if (keyboardState.IsKeyDown(Keys.Space))
                        {
                            state = GameState.HighScore;
                        }
                        break;
                    case MainMenuState.MenuState3:
                        if (keyboardState.IsKeyDown(Keys.Space))
                        {
                            System.Environment.Exit(0);
                        }
                        break;
                }
            }
            if (selectedInput == Input.Controller)
            {
                switch (CurrentMenuState)
                {
                    case MainMenuState.MenuState1:
                        if (gamePadState.IsButtonDown(Buttons.A))
                        {
                            state = GameState.Level;
                        }
                        break;
                    case MainMenuState.MenuState2:
                        if (gamePadState.IsButtonDown(Buttons.A))
                        {
                            state = GameState.HighScore;
                        }
                        break;
                    case MainMenuState.MenuState3:
                        if (gamePadState.IsButtonDown(Buttons.A))
                        {
                            System.Environment.Exit(0);
                        }
                        break;
                }
            }
        }

        internal override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.titleScreenTex, titleScreenPos, Color.White);

            switch (CurrentMenuState)
            {
                case MainMenuState.MenuState1:
                    spriteBatch.Draw(TextureManager.menuState1Tex, menuStatePos, Color.White);
                    break;
                case MainMenuState.MenuState2:
                    spriteBatch.Draw(TextureManager.menuState2Tex, menuStatePos, Color.White);
                    break;
                case MainMenuState.MenuState3:
                    spriteBatch.Draw(TextureManager.menuState3Tex, menuStatePos, Color.White);
                    break;
               
            }
        }

    }
}

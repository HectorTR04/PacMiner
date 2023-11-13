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
using PacMiner.Game_Objects;
using PacMiner.Managers;
using static PacMiner.Managers.GameStateManager;


namespace PacMiner.GameStates
{
    internal class InputSelect
    {
        private Vector2 inputsPos = new Vector2(100,250);

        public static Input selectedInput;
        public enum Input
        {
            Keyboard,
            Controller,
        }

        internal void Update(GameTime gameTime)
        {
            KeyMouseReader.Update();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (KeyMouseReader.KeyPressed(Keys.Enter))
            {
                selectedInput = Input.Keyboard;
                state = GameState.MainMenu;
                SoundManager.start.Play();
            }
            if (gamePadState.IsButtonDown(Buttons.X))
            {
                selectedInput = Input.Controller;
                state = GameState.MainMenu;
                SoundManager.start.Play();
            }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.inputTex, inputsPos, Color.White);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PacMiner.BaseClasses
{
    internal abstract class Menu
    {
        protected enum MenuState
        {

        }
        protected MenuState State { get; set; }

        protected abstract void NavigateMenu();
        protected abstract void ChangeGameState();       
        internal abstract void Update(GameTime gameTime);
        internal abstract void Draw(SpriteBatch spriteBatch);
    }
}

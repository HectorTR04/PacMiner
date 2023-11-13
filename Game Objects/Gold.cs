using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PacMiner.BaseClasses;
using PacMiner.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacMiner.GameObjects;

namespace PacMiner.Game_Objects
{
    internal class Gold : Collectibles
    {
         
        public Gold(Vector2 pos, Texture2D tex) : base(pos, tex)
        {
            tex = TextureManager.goldTex;
            this.pos = pos;
        }


        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!isCollected)
            {
                spriteBatch.Draw(TextureManager.goldTex, pos, Color.White);
            }
        }

    }
}

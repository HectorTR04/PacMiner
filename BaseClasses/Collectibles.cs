using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PacMiner.Managers;
using PacMiner.GameObjects;
using PacMiner.GameStates;
namespace PacMiner.BaseClasses

{
    internal class Collectibles
    {
        protected Vector2 pos;
        protected Texture2D tex;
        protected Rectangle itemRect;
        internal bool isCollected = false;
        protected double showItemScore = 0;
        protected double stopItemScore = 1;
        protected int collectibleScore = 100;

        public Collectibles(Vector2 pos, Texture2D tex)
        {
            this.pos = pos;
            this.tex = tex;
        }

        public Rectangle ItemRect
        {
            get
            {
                itemRect.X = (int)pos.X;
                itemRect.Y = (int)pos.Y;
                itemRect.Width = (int)tex.Width;
                itemRect.Height = (int)tex.Height;
                return itemRect;
            }
        }

        public virtual void CollisionPacMan(PacMan pacMan)
        {
            if (pacMan.PlayerRect.Intersects(ItemRect) && !isCollected)
            {
                Level.scoreManager.Score += collectibleScore;
                isCollected = true;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (isCollected)
            {
                showItemScore += gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (!isCollected)
            {
                spriteBatch.Draw(tex, pos, Color.White);
            }

            if (isCollected && showItemScore <= stopItemScore)
            {
                spriteBatch.Draw(TextureManager.floatingScoreTex, pos, Color.White);
            }
        }
    }
}


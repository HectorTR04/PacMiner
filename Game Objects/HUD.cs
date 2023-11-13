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
using PacMiner.Managers;
using PacMiner.GameObjects;

namespace PacMiner.Game_Objects
{
    internal class HUD
    {
        private Vector2 healthBarPos = new Vector2(0,20);
        private Vector2 scoreIconPos = new Vector2(675, 30);
        private Vector2 scoreNumberPos = new Vector2(735, 50);
       
        internal void Draw(SpriteBatch spriteBatch, PacMan pacMan)
        {
            HealthBar(spriteBatch,pacMan);
            Score(spriteBatch, pacMan);
        }

        private void HealthBar(SpriteBatch spriteBatch, PacMan pacMan)
        {
            if (pacMan.health == 3)
            {
                spriteBatch.Draw(TextureManager.healthBarTex1, healthBarPos, Color.White);
            }
            if (pacMan.health == 2)
            {
                spriteBatch.Draw(TextureManager.healthBarTex2, healthBarPos, Color.White);
            }
            if (pacMan.health == 1)
            {
                spriteBatch.Draw(TextureManager.healthBarTex3, healthBarPos, Color.White);
            }
        }

        private void Score(SpriteBatch spriteBatch, PacMan pacMan)
        {
            spriteBatch.Draw(TextureManager.scoreIconTex, scoreIconPos, Color.White);
            spriteBatch.DrawString(TextureManager.scoreText, " " + Level.scoreManager.Score, scoreNumberPos, Color.White);
        }
    }
}

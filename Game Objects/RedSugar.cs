using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PacMiner.BaseClasses;
using PacMiner.GameObjects;
using PacMiner.GameStates;
using PacMiner.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMiner.Game_Objects
{
    internal class RedSugar : Collectibles
    {
        public RedSugar(Vector2 pos, Texture2D tex) : base(pos, tex)
        {
            tex = TextureManager.redSugarTex;
            this.pos = pos;
        }

        public override void CollisionPacMan(PacMan pacMan)
        {
            if (pacMan.PlayerRect.Intersects(ItemRect) && !isCollected)
            {
                Level.scoreManager.Score += collectibleScore;
            if (pacMan.health != 3)
            {
                pacMan.health++;
            }
                isCollected = true;
            }
        }
    }
    
}

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PacMiner.BaseClasses;
using PacMiner.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacMiner.GameStates;

namespace PacMiner.Game_Objects
{
    internal class Driller : GameObject
    {

        public Driller(Vector2 pos, Texture2D tex, float speed) : base (pos, tex, speed)
        {
            tex = TextureManager.drillerTex;
            this.pos = pos;
            this.speed = speed;
        }
                 
    }
}

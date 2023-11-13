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
using PacMiner.GameObjects;

namespace PacMiner.Game_Objects
{
    internal class Engineer : GameObject
    {

        public Engineer(Vector2 pos, Texture2D tex, float speed) : base(pos, tex, speed)
        {
            tex = TextureManager.engineerTex;
            this.pos = pos;
            this.speed = speed;
        }
                    
    }
}

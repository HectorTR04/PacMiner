using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using PacMiner.BaseClasses;
using PacMiner.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMiner.Game_Objects
{
    internal class Beermug : Collectibles
    {
        public Beermug(Vector2 pos, Texture2D tex) : base (pos, tex) 
        {
            tex = TextureManager.beerMugTex;
            this.pos = pos;
        }
    }
}

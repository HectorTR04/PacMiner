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
using Microsoft.Xna.Framework.Audio;

namespace PacMiner.Managers
{
    internal class SoundManager
    {
        public static SoundEffect start;
        public static SoundEffect eatSound;
        public static SoundEffect pacManWin;


        internal static void LoadSounds(ContentManager content)
        {
            start = content.Load<SoundEffect>("pacmanstart");
            eatSound = content.Load<SoundEffect>("eatsound");
            pacManWin = content.Load<SoundEffect>("pacmanwin");
        }
    }
}

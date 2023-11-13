using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacMiner.GameStates;

namespace PacMiner.Managers
{
    internal class TextureManager
    {

        //Menu Textures
        public static Texture2D inputTex;
        public static Texture2D titleScreenTex;
        public static Texture2D menuState1Tex;
        public static Texture2D menuState2Tex;
        public static Texture2D menuState3Tex;
        public static Texture2D pauseMenuTex;

        //Restart Textures
        public static Texture2D loseScreenTex;
        public static Texture2D loseState1Tex;
        public static Texture2D loseState2Tex;

        //Level

        //HUD
        public static Texture2D hudBackgroundTex;
        public static Texture2D announceTex;
        public static SpriteFont announceText;
        public static Texture2D healthBarTex1;
        public static Texture2D healthBarTex2;
        public static Texture2D healthBarTex3;
        public static Texture2D scoreIconTex;
        public static SpriteFont scoreText;
        public static SpriteFont highScore;


        //Items
        public static Texture2D goldTex;
        public static Texture2D pickaxeTex;
        public static Texture2D beerMugTex;
        public static Texture2D redSugarTex;
        public static Texture2D floatingScoreTex;


        //Map
        public static Texture2D barrelTex;
        public static Texture2D pacWallTex;
        public static Texture2D pacFloorTex;

        //Characters
        public static Texture2D startTex;
        public static Texture2D pacXTex;
        public static Texture2D pacYTex;
        public static Texture2D drillerTex;
        public static Texture2D scoutTex;
        public static Texture2D engineerTex;

               
        internal static void MenuScreenTextures(ContentManager content)
        {
            inputTex = content.Load<Texture2D>("MenuScreen\\inputs");
            titleScreenTex = content.Load<Texture2D>("MenuScreen\\titlescreen");
            menuState1Tex = content.Load<Texture2D>("MenuScreen\\menu1");
            menuState2Tex = content.Load<Texture2D>("MenuScreen\\menu2");
            menuState3Tex = content.Load<Texture2D>("MenuScreen\\menu3");
            pauseMenuTex = content.Load<Texture2D>("MenuScreen\\pausemenu");
        }

        internal static void LoseScreenTextures(ContentManager content)
        {
            loseScreenTex = content.Load<Texture2D>("RestartScreen\\restartscreen");
            loseState1Tex = content.Load<Texture2D>("RestartScreen\\restartstate1");
            loseState2Tex = content.Load<Texture2D>("RestartScreen\\restartstate2");
        }

        internal static void LevelTextures(ContentManager content)
        {
            //HUD
            hudBackgroundTex = content.Load<Texture2D>("Level\\HUD\\hudback");
            announceTex = content.Load<Texture2D>("Level\\HUD\\missioncontrol");
            announceText = content.Load<SpriteFont>("Level\\HUD\\announce");
            healthBarTex1 = content.Load<Texture2D>("Level\\HUD\\healthbar1");
            healthBarTex2 = content.Load<Texture2D>("Level\\HUD\\healthbar2");
            healthBarTex3 = content.Load<Texture2D>("Level\\HUD\\healthbar3");
            scoreIconTex = content.Load<Texture2D>("Level\\HUD\\scoreicon");
            scoreText = content.Load<SpriteFont>("Level\\HUD\\score");
            highScore = content.Load<SpriteFont>("Level\\HUD\\highscore");

            //Items
            goldTex = content.Load<Texture2D>("Level\\gold");
            pickaxeTex = content.Load<Texture2D>("Level\\pickaxe");
            beerMugTex = content.Load<Texture2D>("Level\\beermug");
            redSugarTex = content.Load<Texture2D>("Level\\redsugar");
            floatingScoreTex = content.Load<Texture2D>("Level\\floatscore");

            //Map
            barrelTex = content.Load<Texture2D>("Level\\barrel");
            pacWallTex = content.Load<Texture2D>("Level\\pacwalltest");
            pacFloorTex = content.Load<Texture2D>("Level\\pacfloor");

            //Characters
            startTex = content.Load<Texture2D>("Level\\temppac");
            pacXTex = content.Load<Texture2D>("Level\\pacphyidX");
            pacYTex = content.Load<Texture2D>("Level\\pacphyidY");
            drillerTex = content.Load<Texture2D>("Level\\driller");
            scoutTex = content.Load<Texture2D>("Level\\scout");
            engineerTex = content.Load<Texture2D>("Level\\engineer");

        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public static class ContentManager
    {
        public static string MarioFileName { get; private set; }
        public static string LaneFileName { get; private set; }
        public static List<Texture2D> Particles { get; private set; }
        public static Texture2D Dot { get; private set; }
        public static Texture2D BasicCharacter { get; private set; }
        public static Texture2D BasicPlatform { get; private set; }
        public static Texture2D MarioSheet { get; private set; }
        public static Texture2D ShroomPlatform { get; private set; }
        public static Texture2D MenuBackground { get; private set; }
        public static Texture2D LaneBackground { get; private set; }
        public static Texture2D LaneTexture { get; private set; }
        public static Texture2D PlatformSheet { get; private set; }
        public static SpriteFont Font { get; private set; }
        public static Texture2D CircleBody { get; private set; }
        public static Texture2D Castle { get; private set; }
        public static Texture2D Platformer_Character_Smiley { get;  private set; }

        public static void Load(Microsoft.Xna.Framework.Content.ContentManager _Content)
        {
            Particles = new List<Texture2D>();
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire1"));
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire2"));
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire3"));

            Dot = _Content.Load<Texture2D>("Dot");
            BasicCharacter = _Content.Load<Texture2D>("BasicCharacter");
            MarioSheet = _Content.Load<Texture2D>("SpriteSheets\\MarioSheet");
            ShroomPlatform = _Content.Load<Texture2D>("Platform\\Shrooms");
            PlatformSheet = _Content.Load<Texture2D>("Platform\\PlatformTextures16");
            BasicPlatform = _Content.Load<Texture2D>("BasicPlatform");
            MenuBackground = _Content.Load<Texture2D>("MenuBackground");
            LaneBackground = _Content.Load<Texture2D>("LaneBackground");
            LaneTexture = _Content.Load<Texture2D>("LaneTexture");
            Font = _Content.Load<SpriteFont>("PixelFont");
            CircleBody = _Content.Load<Texture2D>("CircleBody");
            Castle = _Content.Load<Texture2D>("Castle");
            MarioFileName = "MarioMap.txt";
            LaneFileName = "LaneMap.txt";




            Platformer_Character_Smiley = _Content.Load<Texture2D>("Platformer\\Characters\\Smiley");
        } 
    }
}
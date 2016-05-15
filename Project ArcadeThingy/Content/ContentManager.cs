using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    public static class ContentManager
    {
        public static List<Texture2D> Particles { get; private set; }
        public static Texture2D Dot { get; private set; }
        public static SpriteFont Font { get; private set; }
        public static Texture2D MenuBackground { get; private set; }
        public static Texture2D PlatformSheet { get; private set; }
        public static Texture2D Platformer_Character_Smiley { get;  private set; }
        public static Texture2D Platformer_PowerUps_Coin { get;  private set; }

        public static void Load(Microsoft.Xna.Framework.Content.ContentManager _Content)
        {
            Particles = new List<Texture2D>();
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire1"));
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire2"));
            Particles.Add(_Content.Load<Texture2D>("Particles\\Fire3"));

            Dot = _Content.Load<Texture2D>("Dot");
            Font = _Content.Load<SpriteFont>("PixelFont");
            MenuBackground = _Content.Load<Texture2D>("MenuBackground");

            PlatformSheet = _Content.Load<Texture2D>("Platform\\PlatformTextures16");
            Platformer_Character_Smiley = _Content.Load<Texture2D>("Platformer\\Characters\\Smiley");
            Platformer_PowerUps_Coin = _Content.Load<Texture2D>("Platformer\\PowerUps\\CoinSpriteSheet");
        } 
    }
}
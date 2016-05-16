using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    public static class ContentManager
    {
        public static List<Texture2D> FireParticles { get; private set; }
        public static List<Texture2D> CuteParticles { get; private set; }
        public static Texture2D Dot { get; private set; }
        public static SpriteFont Font { get; private set; }
        public static Texture2D MenuBackground { get; private set; }
        public static Texture2D Platformer_UI { get; private set; }
        public static Texture2D PlatformSheet { get; private set; }
        public static Texture2D Platformer_Character_Smiley { get;  private set; }
        public static Texture2D Platformer_PowerUps_Coin { get;  private set; }

        public static void Load(Microsoft.Xna.Framework.Content.ContentManager _Content)
        {
            FireParticles = new List<Texture2D>();
            FireParticles.Add(_Content.Load<Texture2D>("Particles\\Fire1"));
            FireParticles.Add(_Content.Load<Texture2D>("Particles\\Fire2"));
            FireParticles.Add(_Content.Load<Texture2D>("Particles\\Fire3"));

            CuteParticles = new List<Texture2D>();
            CuteParticles.Add(_Content.Load<Texture2D>("Particles\\Heart"));
            CuteParticles.Add(_Content.Load<Texture2D>("Particles\\Star"));

            Dot = _Content.Load<Texture2D>("Dot");
            Font = _Content.Load<SpriteFont>("PixelFont");
            MenuBackground = _Content.Load<Texture2D>("MenuBackground");
            Platformer_UI = _Content.Load<Texture2D>("Platformer\\UI");

            PlatformSheet = _Content.Load<Texture2D>("Platform\\PlatformTextures16");
            Platformer_Character_Smiley = _Content.Load<Texture2D>("Platformer\\Characters\\Smiley");
            Platformer_PowerUps_Coin = _Content.Load<Texture2D>("Platformer\\PowerUps\\CoinSpriteSheet");
        } 
    }
}
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_ArcadeThingy
{
    public enum SoundEffectName
    {
        Pickup_Coin,
        Pickup_PowerUp,
        Movement_Jump,
    }

    public enum Songs
    {
        First
    }

    public static class AudioManager
    {
        private static SoundEffect mPickupCoin;
        private static SoundEffect mPickupPowerUp;
        private static SoundEffect mMovementJump;

        private static SoundEffect mBGMusicOne;
        private static SoundEffectInstance mCurrentBGMusic;


        public static float MasterVolume = 0.5f;

        public static void Load(Microsoft.Xna.Framework.Content.ContentManager _Content)
        {
            mPickupCoin = _Content.Load<SoundEffect>("Platformer\\Audio\\Pickup_Coin");
            mPickupPowerUp = _Content.Load<SoundEffect>("Platformer\\Audio\\Pickup_PowerUpOne");
            mMovementJump = _Content.Load<SoundEffect>("Platformer\\Audio\\Movement_Jump");

            mBGMusicOne = _Content.Load<SoundEffect>("Platformer\\Audio\\Music_One");
        }

        public static void PlayEffect(SoundEffectName _Input)
        {
            switch (_Input)
            {
                case SoundEffectName.Pickup_Coin:
                    mPickupCoin.Play(0.1f, 0.0f, 0.0f);
                    break;
                case SoundEffectName.Pickup_PowerUp:
                    mPickupPowerUp.Play(1.0f, 0.0f, 0.0f);
                    break;
                case SoundEffectName.Movement_Jump:
                    mMovementJump.Play(0.3f, 0.0f, 0.0f);
                    break;
                default:
                    break;
            }
        }

        public static void PlaySong(Songs _Input)
        {
            if (mCurrentBGMusic != null)
            {
                mCurrentBGMusic.Stop();
                mCurrentBGMusic.Dispose();
            }
            switch (_Input)
            {
                case Songs.First:
                    mCurrentBGMusic = mBGMusicOne.CreateInstance();
                    mCurrentBGMusic.IsLooped = true;
                    mCurrentBGMusic.Volume = 0.4f;
                    mCurrentBGMusic.Play();
                    break;
                default:
                    break;
            }
        }








    }
}

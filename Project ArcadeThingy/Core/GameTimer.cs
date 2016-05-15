using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project_ArcadeThingy
{
    class GameTimer
    {
        public bool IsCounting { get; private set; }
        public bool IsFinnished { get; private set; }
        public bool IsLooping { get; set; }
        public double CurrentTime { get; private set; }
        public double TotalTime { get; set; }
        public Action OnFinnished { get; set; }

        public GameTimer(double _TotalTime = 0.0f)
        {
            if (_TotalTime > 0.0f)
            {
                CurrentTime = TotalTime = _TotalTime;
                Start();
            }
        }

        public void Start()
        {
            IsCounting = true;
            IsFinnished = false;
        }

        public void Stop()
        {
            IsCounting = false;
        }

        public void Restart(double _NewTotalTime = 0.0f)
        {
            if (_NewTotalTime > 0.0f)
                TotalTime = _NewTotalTime;

            CurrentTime = TotalTime;
            Start();
        }

        public void Update(GameTime _GT)
        {
            if (IsCounting)
            {
                CurrentTime -= _GT.ElapsedGameTime.TotalSeconds;

                if (CurrentTime <= 0.0f)
                {
                    CurrentTime = 0.0f;

                    if (OnFinnished != null)
                        OnFinnished();

                    if (IsLooping)
                        CurrentTime = TotalTime;
                    else
                    {
                        IsCounting = false;
                        IsFinnished = true;
                    }
                }
            }
        }

        private string TimerToText(double _Timer, bool _Hours, bool _Minutes, bool _Seconds, bool _MilliSeconds)
        {
            string result = "";

            int h = (int)(CurrentTime / 3600);
            int m = (int)(CurrentTime / 60);
            int s = (int)(CurrentTime);
            int ms = (int)(CurrentTime * 1000);

            if (_Hours)
            {
                m = m % 60;
                result += h + ":";
            }

            if (_Minutes)
            {
                s = s % 60;
                result += m + ":";
            }

            if (_Seconds)
            {
                ms = ms % 1000;
                result += s + ":";
            }

            if (_MilliSeconds)
                result += ms / 10;

            return result;
        }

        public void Draw(SpriteBatch _SB, SpriteFont _Font, Vector2 _Position, Color _Color)
        {
            string text = TimerToText(CurrentTime, false, false, true, true);
            Vector2 origin = _Font.MeasureString(text) / 2;

            _SB.DrawString(_Font, text, _Position, _Color, 0.0f, origin, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}

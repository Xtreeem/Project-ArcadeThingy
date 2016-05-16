using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project_ArcadeThingy
{
    class GameTimer
    {
        public bool IsCounting { get; private set; }
        public bool IsFinished { get; private set; }
        public bool IsLooping { get; set; }
        public double CurrentTime { get; private set; }
        public double TotalTime { get; set; }
        public Action OnFinished { get; set; }

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
            IsFinished = false;
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

                    if (OnFinished != null)
                        OnFinished();

                    if (IsLooping)
                        CurrentTime = TotalTime;
                    else
                    {
                        IsCounting = false;
                        IsFinished = true;
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

            if (_Hours)
            {
                m = m % 60;
                result += h;
            }

            if (_Minutes)
            {
                s = s % 60;
                if (m > 10)
                    result += m;
                else
                    result += "0" + m;

                result += ":";
            }

            if (_Seconds)
            {
                if (s >= 10)
                    result += s;
                else
                    result += "0" + s;
            }

            return result;
        }

        public void Draw(SpriteBatch _SB, SpriteFont _Font, Vector2 _Position, Color _Color)
        {
            string text = "";
            if (CurrentTime >= 60)
                text = TimerToText(CurrentTime, false, true, true, false);
            else
                text = TimerToText(CurrentTime, false, false, true, true);

            Vector2 origin = _Font.MeasureString(text) / 2;

            if (CurrentTime <10)
                _Color = Color.Red;

            _SB.DrawString(_Font, text, _Position, _Color, 0.0f, origin, 1.0f, SpriteEffects.None, 1.0f);
        }
    }
}
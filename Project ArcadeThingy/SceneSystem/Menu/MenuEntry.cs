using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project_ArcadeThingy
{
    class EntryDesc
    {
        // text attributes
        public Color SelectedColor { get; set; } = Color.White;
        public Color Color { get; set; } = Color.Black;

        public Vector2 StartPosition { get; set; } = Vector2.Zero;
        public Vector2 EndPosition { get; set; } = Vector2.Zero;

        public float FadeSpeed { get; set; } = 0.1f;
        public float GlowSpeed { get; set; } = 0.1f;

        public SpriteFont Font { get; set; }

        public EntryDesc(SpriteFont _Font, Vector2 _StartPosition, Vector2? _EndPosition = null)
        {
            Font = _Font;
            StartPosition = _StartPosition;
            EndPosition = (_EndPosition.HasValue) ? _EndPosition.Value : _StartPosition;
        }
        public EntryDesc(EntryDesc _Desc) : this(_Desc.Font, _Desc.StartPosition, _Desc.EndPosition)
        {
            SelectedColor = _Desc.SelectedColor;
            Color = _Desc.Color;
            FadeSpeed = _Desc.FadeSpeed;
            GlowSpeed = _Desc.GlowSpeed;
        }
    }

    class MenuEntry
    {
        public Action BoundAction { get; set; }
        public bool IsSelected { get; set; }
        Color mColor;
        string mText;
        Vector2 mPosition;
        Rectangle mBounds;
        Vector2 mOrigin;
        float mScale;
        float mRotation;
        float mLayerDepth;
        EntryDesc mDesc;

        public MenuEntry(string _Text, EntryDesc _Desc, Action _Action = null)
        {
            mText = _Text;
            mDesc = new EntryDesc(_Desc);
            BoundAction = _Action;
            mPosition = mDesc.StartPosition;
            mScale = 1.0f;
            mRotation = 0.0f;
            mLayerDepth = 0.0f;

            Vector2 size = mDesc.Font.MeasureString(mText) * mScale;
            mOrigin = size / 2.0f;
            mBounds = new Rectangle((int)(mPosition.X - mOrigin.X), (int)(mPosition.Y - mOrigin.Y), (int)(size.X), (int)(size.Y));
        }

        public void DoAction()
        {
            if (BoundAction != null)
                BoundAction();
        }

        public void HandleTransition(SceneState _State, float _TransitionStatus)
        {
            mColor = mDesc.Color * _TransitionStatus;
            mPosition = Vector2.Lerp(mDesc.StartPosition, mDesc.EndPosition, _TransitionStatus);
        }

        public void Update(GameTime _GT)
        {
            if (IsSelected)
                mColor = Color.Lerp(mColor, mDesc.SelectedColor, mDesc.GlowSpeed);
            else
                mColor = Color.Lerp(mColor, mDesc.Color, mDesc.FadeSpeed);
        }

        public void Draw(SpriteBatch _SB)
        {
            _SB.DrawString(mDesc.Font, mText, mPosition, mColor, mRotation, mOrigin, mScale, SpriteEffects.None, mLayerDepth);
        }
    }
}
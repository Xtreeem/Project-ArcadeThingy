using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Project_ArcadeThingy
{
    abstract class MenuScene : Scene
    {
        protected MenuEntry mSelectedEntry { get { return mEntries[mSelectedIndex]; } }
        public EntryDesc Desc { get; private set; }

        protected List<MenuEntry> mEntries;
        int mSelectedIndex;

        /// <summary>
        /// If you do not send a description as parameter you MUST use Set_Desc!
        /// </summary>
        /// <param name="_Desc"></param>
        public MenuScene(EntryDesc _Desc = null) : base(false)
        {
            mTransitionOnTime = 0.5f;
            mTransitionOffTime = 0.5f;
            Desc = (_Desc == null) ? null : new EntryDesc(_Desc);
            mEntries = new List<MenuEntry>();
        }

        public void Set_Desc(EntryDesc _Desc) { Desc = new EntryDesc(_Desc); }

        public void AddEntry(string _Text, Action _Action = null)
        {
            if(Desc == null) throw new Exception("No description specified!");

            EntryDesc desc = new EntryDesc(Desc);
            desc.StartPosition += new Vector2(0.0f, Desc.Font.LineSpacing * mEntries.Count);
            desc.EndPosition += new Vector2(0.0f, Desc.Font.LineSpacing * mEntries.Count);

            mEntries.Add(new MenuEntry(_Text, desc, _Action));

            if (mEntries.Count == 1)
                Set_SelectedIndex(0);
        }

        private void Set_SelectedIndex(int index)
        {
            mSelectedEntry.IsSelected = false;
            mSelectedIndex = (index < 0) ? mEntries.Count - 1 : (index >= mEntries.Count) ? 0 : index;
            mSelectedEntry.IsSelected = true;
        }

        public override bool HandleTransition(GameTime _GT)
        {
            for (int i = 0; i < mEntries.Count; ++i)
                mEntries[i].HandleTransition(mState, mTransitionStatus);

            return base.HandleTransition(_GT);
        }

        public override void HandleInput()
        {
            if (mEntries.Count <= 0) return;

            if (InputManager.IsKeyClicked(Keys.Down))
                Set_SelectedIndex(mSelectedIndex + 1);
            else if (InputManager.IsKeyClicked(Keys.Up))
                Set_SelectedIndex(mSelectedIndex - 1);
            else if (InputManager.IsKeyClicked(Keys.Enter))
                mSelectedEntry.DoAction();
        }

        public override void Update(GameTime _GT)
        {
            for (int i = 0; i < mEntries.Count; ++i)
                mEntries[i].Update(_GT);
        }

        public override void Draw(SpriteBatch _SB)
        {
            for (int i = 0; i < mEntries.Count; ++i)
                mEntries[i].Draw(_SB);
        }
    }
}
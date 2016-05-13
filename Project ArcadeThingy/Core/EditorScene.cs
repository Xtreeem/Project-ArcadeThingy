using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace Project_ArcadeThingy
{
    // TODO: 
    // -collision
    class EditorScene : Scene
    {
        int mTileSize = BasePlatform.TILE_SIZE;
        List<BasePlatform> mPlatforms = new List<BasePlatform>();
        World mWorld = new World(new Vector2(0, 9.8f));
        BasePlatform mPlatform;
        BasePlatform mSoonPlatform;
        bool mCreatingPlatform = false;
        bool mShowGrid = false;
        Vector2 mStartPosition;
        Vector2 mStartPlatPos;
        ShroomType mShroomType;
        int mType;
        StreamWriter mWriter;
        StreamReader mReader;
        string mShroomCode = "Shroom";
        string mSuperCode = "Super";
        Spline mTopSpline;
        Spline mMidSpline;
        Spline mBotSpline;
        Spline mCurrSpline;
        bool mPlacingSpline = true;

        public EditorScene()
        {
            mShroomType = ShroomType.Yellow;
            mPlatform = new ShroomPlatform(mShroomType, new Vector2(32, 16), InputManager.MousePosition(), ref mWorld);
            mType = 0;
            mTopSpline = new Spline(1.0f);
            mMidSpline = new Spline(1.0f);
            mBotSpline = new Spline(1.0f);
            mCurrSpline = mTopSpline;
        }

        private string WriteVector(Vector2 _VectorToWrite) { return ((int)_VectorToWrite.X).ToString() + "," + ((int)_VectorToWrite.Y).ToString(); }

        private void SaveToFile()
        {
            if (mPlacingSpline)
            {
                mWriter = new StreamWriter(ContentManager.LaneFileName);

                for (int i = 0; i < mTopSpline.mBasePositions.Count; ++i)
                    mWriter.WriteLine("top," + WriteVector(mTopSpline.mBasePositions[i]));
                for (int i = 0; i < mMidSpline.mBasePositions.Count; ++i)
                    mWriter.WriteLine("mid," + WriteVector(mMidSpline.mBasePositions[i]));
                for (int i = 0; i < mBotSpline.mBasePositions.Count; ++i)
                    mWriter.WriteLine("bot," + WriteVector(mBotSpline.mBasePositions[i]));

                mWriter.Flush();
                mWriter.Close();
                return;
            }

            mWriter = new StreamWriter(ContentManager.MarioFileName);
            for (int i = 0; i < mPlatforms.Count; ++i)
            {
                BasePlatform platform = mPlatforms[i];
                string line = "";
                if (platform is ShroomPlatform)
                    line = mShroomCode + "," + (int)(platform as ShroomPlatform).Type + ",";
                else if (platform is SuperPlatform)
                    line = mSuperCode + "," + (platform as SuperPlatform).Type + ",";

                line += WriteVector(platform.Body.Size) + ",";
                line += WriteVector(platform.Body.Body.Position.UnitToPixels());

                mWriter.WriteLine(line);
            }
            mWriter.Flush();
            mWriter.Close();
        }

        int Int(string _Str) { return int.Parse(_Str); }

        private void ReadFromFile()
        {
            if (mPlacingSpline)
            {
                mTopSpline = new Spline(1.0f);
                mMidSpline = new Spline(1.0f);
                mBotSpline = new Spline(1.0f);

                mReader = new StreamReader(ContentManager.LaneFileName);
                while (!mReader.EndOfStream)
                {
                    string[] line = mReader.ReadLine().Split(',');

                    if (line[0] == "top")
                        mTopSpline.AddPosition(new Vector2(Int(line[1]), Int(line[2])));
                    if (line[0] == "mid")
                        mMidSpline.AddPosition(new Vector2(Int(line[1]), Int(line[2])));
                    if (line[0] == "bot")
                        mBotSpline.AddPosition(new Vector2(Int(line[1]), Int(line[2])));
                }

                mReader.Close();
                return;
            }

            mPlatforms.Clear();
            mReader = new StreamReader(ContentManager.MarioFileName);
            while (!mReader.EndOfStream)
            {
                string[] line = mReader.ReadLine().Split(',');
                if (line[0] == mShroomCode)
                    mPlatforms.Add(new ShroomPlatform((ShroomType)Int(line[1]), new Vector2(Int(line[2]), Int(line[3])), new Vector2(Int(line[4]), Int(line[5])), ref mWorld));
                else
                    mPlatforms.Add(new SuperPlatform(Int(line[1]), new Vector2(Int(line[2]), Int(line[3])), new Vector2(Int(line[4]), Int(line[5])), ref mWorld));
            }
            mReader.Close();
        }

        public override void Update(GameTime _GT)
        {
            if (mPlacingSpline)
            {


                return;
            }

            float mouseX = InputManager.MousePosition().X;
            float mouseY = InputManager.MousePosition().Y;
            float snapX = (mouseX + mTileSize / 2) - mouseX % mTileSize;
            float snapY = (mouseY + mTileSize * 1.5f) - mouseY % mTileSize;

            mWorld.Step((float)_GT.ElapsedGameTime.TotalSeconds);

            if (mPlatform != null)
            {
                mPlatform.Body.Body.Position = new Vector2(snapX, snapY).PixelsToUnits();

                if (mType == 6 && !(mPlatform is ShroomPlatform) || mPlatform is ShroomPlatform && (mPlatform as ShroomPlatform).Type != mShroomType)
                    mPlatform = new ShroomPlatform(mShroomType, new Vector2(32, 16), new Vector2(snapX, snapY), ref mWorld);
                else if (mType != 6 && !(mPlatform is SuperPlatform) || mPlatform is SuperPlatform && (mPlatform as SuperPlatform).Type != mType)
                    mPlatform = new SuperPlatform(mType, new Vector2(32, 16), new Vector2(snapX, snapY), ref mWorld);
            }

            if (mCreatingPlatform)
            {
                var currSize = new Vector2(Math.Max(mTileSize * (Math.Abs((mouseX - mStartPosition.X) / mTileSize)), mTileSize * 2),
                                                      Math.Max(mTileSize * (Math.Abs((mouseY - mStartPosition.Y) / mTileSize)), mTileSize));
                currSize.X = currSize.X - currSize.X % mTileSize;
                var currPos = new Vector2(mStartPlatPos.X - mStartPlatPos.X % mTileSize + currSize.X / 2, mStartPlatPos.Y - mStartPlatPos.Y % mTileSize + currSize.Y / 2);
                if (mType == 6)
                {
                    currSize.Y = mTileSize;
                    currPos.Y = mStartPlatPos.Y - mStartPlatPos.Y % mTileSize + currSize.Y / 2;
                    mSoonPlatform = new ShroomPlatform(mShroomType, currSize, currPos, ref mWorld);
                }
                else
                    mSoonPlatform = new SuperPlatform(mType, currSize, currPos, ref mWorld);
            }
        }

        public override void HandleInput()
        {
            if (InputManager.IsKeyClicked(Keys.Tab))
                mShowGrid = !mShowGrid;
            if (InputManager.IsKeyClicked(Keys.S))
                SaveToFile();
            if (InputManager.IsKeyClicked(Keys.L))
                ReadFromFile();

            if (mPlacingSpline)
            {
                float mX = InputManager.MousePosition().X;
                float mY = InputManager.MousePosition().Y;
                float sX = mX + mTileSize - mX % mTileSize - 5.0f - mTileSize / 2;
                float sY = mY + mTileSize - mY % mTileSize - 5.0f - mTileSize / 2;

                if (InputManager.LeftButtonClicked())
                    mCurrSpline.AddPosition(new Vector2(sX, sY));

                if (InputManager.IsKeyClicked(Keys.D1))
                    mCurrSpline = mTopSpline;
                if (InputManager.IsKeyClicked(Keys.D2))
                    mCurrSpline = mMidSpline;
                if (InputManager.IsKeyClicked(Keys.D3))
                    mCurrSpline = mBotSpline;

                if (InputManager.IsKeyPressed(Keys.LeftControl) && InputManager.IsKeyClicked(Keys.Z))
                    mCurrSpline.Pop();
                return;
            }

            if (InputManager.IsKeyClicked(Keys.Right))
                mType = (mType == 6) ? 0 : mType + 1;
            else if (InputManager.IsKeyClicked(Keys.Left))
                mType = (mType == 0) ? 6 : mType - 1;
            if (InputManager.IsKeyClicked(Keys.Space))
                mShroomType = (mShroomType == ShroomType.GreenYellow) ? ShroomType.Yellow : mShroomType + 1;
            if (InputManager.LeftButtonClicked())
            {
                mStartPosition = InputManager.MousePosition();
                mStartPlatPos = mPlatform.Body.Body.Position.UnitToPixels();
                mCreatingPlatform = true;
                mPlatform = null;
            }
            if (InputManager.IsLeftButtonReleased())
            {
                mCreatingPlatform = false;
                mPlatforms.Add(mSoonPlatform);
                mSoonPlatform = null;
                if (mType == 7)
                    mPlatform = new ShroomPlatform(mShroomType, new Vector2(32, 16), InputManager.MousePosition(), ref mWorld);
                else
                    mPlatform = new SuperPlatform(mType, new Vector2(32, 16), InputManager.MousePosition(), ref mWorld);
            }

            if (InputManager.IsKeyPressed(Keys.LeftControl) && InputManager.IsKeyClicked(Keys.Z))
                if (mPlatforms.Count > 0) mPlatforms.RemoveAt(mPlatforms.Count - 1);
        }

        int position = 0;

        public override void Draw(SpriteBatch _SB)
        {
            if (mShowGrid)
            {
                for (int x = 0; x < SceneManager.Width / mTileSize; ++x)
                {
                    Vector2 start = new Vector2(x * mTileSize, 0.0f);
                    Vector2 end = new Vector2(start.X, SceneManager.Height);
                    _SB.DrawLine(start, end, 1.0f, Color.Black, 2);
                }
                for (int y = 0; y < SceneManager.Height / mTileSize; ++y)
                {
                    Vector2 start = new Vector2(0.0f, y * mTileSize);
                    Vector2 end = new Vector2(SceneManager.Width, start.Y);
                    _SB.DrawLine(start, end, 1.0f, Color.Black, 2);
                }
            }

            if (mPlacingSpline)
            {
                mTopSpline.DrawSpline(_SB, ContentManager.Dot, Color.Red, 10.0f);
                mMidSpline.DrawSpline(_SB, ContentManager.Dot, Color.Green, 10.0f);
                mBotSpline.DrawSpline(_SB, ContentManager.Dot, Color.Blue, 10.0f);

                mTopSpline.DrawPositions(_SB, ContentManager.Dot, Color.DarkRed, 10.0f);
                mMidSpline.DrawPositions(_SB, ContentManager.Dot, Color.DarkGreen, 10.0f);
                mBotSpline.DrawPositions(_SB, ContentManager.Dot, Color.DarkBlue, 10.0f);

                if (position <= mTopSpline.Positions.Count - 1)
                {
                    string text = "HEJ HUGO :D";
                    _SB.DrawString(ContentManager.Font, text, mTopSpline.Positions[position], Color.Black, 0.0f, ContentManager.Font.MeasureString(text) / 2, 1.0f, SpriteEffects.None, 1.0f);
                    position += 1;
                }
                else
                    position = 0;
                return;
            }

            for (int i = 0; i < mPlatforms.Count; ++i)
                mPlatforms[i].Draw(_SB);

            if (mPlatform != null)
                mPlatform.Draw(_SB);

            if (mSoonPlatform != null)
                mSoonPlatform.Draw(_SB);


        }
    }
}
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

namespace Project_ArcadeThingy
{
    public static class FileUtils
    {
        private static string mShroom = "Shroom";
        private static string mSuper = "Super";
        public static string Spline_Top { get { return "top"; } }
        public static string Spline_Mid { get { return "mid"; } }
        public static string Spline_Bot { get { return "bot"; } }

        private static string VectorToString(Vector2 _VectorToWrite)
        {
            return ((int)_VectorToWrite.X).ToString() + "," + ((int)_VectorToWrite.Y).ToString();
        }

        public static List<Vector2> GetSpline(string _Name)
        {
            List<Vector2> result = new List<Vector2>();

            StreamReader reader = new StreamReader(ContentManager.LaneFileName);
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(',');
                string name = line[0];
                Vector2 position = new Vector2(int.Parse(line[1]), int.Parse(line[2]));

                if (name == _Name)
                    result.Add(position);
            }

            reader.Close();
            return result;
        }

        public static void SaveSpline(string _Name, Spline _Spline)
        {
            StreamWriter mWriter = new StreamWriter(ContentManager.LaneFileName);

            for (int i = 0; i < _Spline.mBasePositions.Count; ++i)
                mWriter.WriteLine(_Name + ',' + VectorToString(_Spline.mBasePositions[i]));

            mWriter.Flush();
            mWriter.Close();
        }

        public static List<BasePlatform> GetPlatforms(ref World _World)
        {
            List<BasePlatform> result = new List<BasePlatform>();

            StreamReader reader = new StreamReader(ContentManager.MarioFileName);
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(',');
                string name = line[0];
                int type = int.Parse(line[1]);
                Vector2 size = new Vector2(int.Parse(line[2]), int.Parse(line[3]));
                Vector2 position = new Vector2(int.Parse(line[4]), int.Parse(line[5]));

                if (name == mShroom)
                    result.Add(new ShroomPlatform((ShroomType)type, size, position, ref _World));
                else if (name == mSuper)
                    result.Add(new SuperPlatform(type, size, position, ref _World));
            }

            reader.Close();
            return result;
        }

        public static void SavePlatforms(List<BasePlatform> _Platforms)
        {
            StreamWriter mWriter = new StreamWriter(ContentManager.MarioFileName);

            for (int i = 0; i < _Platforms.Count; ++i)
            {
                BasePlatform platform = _Platforms[i];
                string line = "";

                if (platform is ShroomPlatform)
                    line = mShroom + ',' + (int)(platform as ShroomPlatform).Type + ',';
                else if (platform is SuperPlatform)
                    line = mSuper + ',' + (platform as SuperPlatform).Type + ',';

                line += VectorToString(platform.Body.Size) + ',';
                line += VectorToString(platform.Body.Body.Position.UnitToPixels());

                mWriter.WriteLine(line);
            }

            mWriter.Flush();
            mWriter.Close();
        }
    }
}
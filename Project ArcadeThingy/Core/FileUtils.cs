using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

namespace Project_ArcadeThingy
{
    public static class FileUtils
    {
        private static string MarioFileName = "MarioMap.txt";
        private static string mShroom = "Shroom";
        private static string mSuper = "Super";

        private static string VectorToString(Vector2 _VectorToWrite)
        {
            return ((int)_VectorToWrite.X).ToString() + "," + ((int)_VectorToWrite.Y).ToString();
        }

        public static List<PF_Platform_Base> GetPlatforms(World _World)
        {
            List<PF_Platform_Base> result = new List<PF_Platform_Base>();

            StreamReader reader = new StreamReader(MarioFileName);
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(',');
                string name = line[0];
                int type = int.Parse(line[1]);
                Vector2 size = new Vector2(int.Parse(line[2]), int.Parse(line[3]));
                Vector2 position = new Vector2(int.Parse(line[4]), int.Parse(line[5]));

                if (name == mShroom)
                    result.Add(new PF_Platform_Shroom(size, position, _World, (Platform_Type_Shroom)type));
                else if (name == mSuper)
                    result.Add(new PF_Platform_Super(size, position, _World, (Platform_Type_Super)type));
            }

            reader.Close();
            return result;
        }

        public static void SavePlatforms(List<PF_Platform_Base> _Platforms)
        {
            StreamWriter mWriter = new StreamWriter(MarioFileName);

            for (int i = 0; i < _Platforms.Count; ++i)
            {
                PF_Platform_Base platform = _Platforms[i];
                string line = "";

                if (platform is PF_Platform_Shroom)
                    line = mShroom + ',' + (int)(platform as PF_Platform_Shroom).Type + ',';
                else if (platform is PF_Platform_Super)
                    line = mSuper + ',' + (platform as PF_Platform_Super).Type + ',';

                line += VectorToString(platform.Body.Size) + ',';
                line += VectorToString(platform.Body.Position);

                mWriter.WriteLine(line);
            }

            mWriter.Flush();
            mWriter.Close();
        }
    }
}
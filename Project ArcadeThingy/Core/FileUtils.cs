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
        private static string mCoin = "Coin";

        private static string VectorToString(Vector2 _VectorToWrite)
        {
            return ((int)_VectorToWrite.X).ToString() + "," + ((int)_VectorToWrite.Y).ToString();
        }

        public static List<PF_GameObj> GetPlatforms(World _World)
        {
            List<PF_GameObj> result = new List<PF_GameObj>();

            StreamReader reader = new StreamReader("..//..//..//..//Content//" + MarioFileName);
            while (!reader.EndOfStream)
            {
                string[] line = reader.ReadLine().Split(',');
                string name = line[0];
                int type = int.Parse(line[1]);
                Vector2 position = new Vector2(int.Parse(line[2]), int.Parse(line[3]));
                Vector2 size = new Vector2(int.Parse(line[4]), int.Parse(line[5]));

                if (name == mShroom)
                    result.Add(new PF_Platform_Shroom(position, size / PF_Platform_Base.TILE_SIZE, _World, (Platform_Type_Shroom)type));
                else if (name == mSuper)
                    result.Add(new PF_Platform_Super(position, size / PF_Platform_Base.TILE_SIZE, _World, (Platform_Type_Super)type));
                else if (name == mCoin)
                    result.Add(new PF_PowerUps_Coin(_World, position, size, 0, BodyType.Static));
            }

            reader.Close();
            return result;
        }

        public static void SavePlatforms(List<PF_GameObj> _Objects)
        {
            StreamWriter mWriter = new StreamWriter("..//..//..//..//Content//" + MarioFileName);

            mWriter.WriteLine("Super,4,974,101,21,8");

            for (int i = 1; i < _Objects.Count; ++i)
            {
                PF_GameObj obj = _Objects[i];
                string line = "";

                if (obj is PF_Platform_Shroom)
                    line = mShroom + ',' + (int)(obj as PF_Platform_Shroom).Type + ',';
                else if (obj is PF_Platform_Super)
                    line = mSuper + ',' + (int)(obj as PF_Platform_Super).Type + ',';
                else if (obj is PF_PowerUps_Coin)
                    line = mCoin + ',' + 0 + ',';

                line += VectorToString(obj.Body.Position) + ',';
                line += VectorToString(obj.Body.Size);

                mWriter.WriteLine(line);
            }

            mWriter.Flush();
            mWriter.Close();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace MyLib 
{
    public partial class Util
    {
        public static int FramePerSecond = 20;
        public static int GameToNetNumber = 100;
        public static int GameVecToNet(float v)
        {
            return (int)(v * GameToNetNumber);
        }
        public static int GameTimeToNet(float t)
        {
            return (int)(t * FramePerSecond);
        }
        
        public static void Log(string msg)
        {
            //Console.WriteLine(msg);
        }


        public class Pair
        {
            public byte moduleId;
            public byte messageId;

            public Pair(byte a, byte b)
            {
                moduleId = a;
                messageId = b;
            }
        }



        public static Pair GetMsgID(string name)
        {
            return SaveGame.saveGame.GetMsgID(name);
        }

        public static Pair GetMsgID(string moduleName, string name)
        {
            Debug.Log("moduleName " + moduleName + " " + name);
            var mId = SaveGame.saveGame.msgNameIdMap[moduleName]["id"].AsInt;
            var pId = SaveGame.saveGame.msgNameIdMap[moduleName][name].AsInt;
            return new Pair((byte)mId, (byte)pId);
        }

        public static string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();
            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);
                exception = exception.InnerException;
            }
            return stringBuilder.ToString();
        }

        public static string PrintStackTrace()
        {
            var st = new StackTrace(true);
            StringBuilder sb = new StringBuilder();
            for (var i = 0; i < st.FrameCount; i++)
            {
                var sf = st.GetFrame(i);
                var s = sf.ToString();
                Debug.Log(s);
                sb.Append(s);
            }
            return sb.ToString();
        }


        private static Dictionary<int, UnitData> monsterData = new Dictionary<int, UnitData>();
        public static UnitData GetUnitData(bool isPlayer, int mid, int level)
        {
            //玩家才需要level 怪物的level都是0， 因此mid为玩家的job的时候*10足够了
            int key = Convert.ToInt32(isPlayer)*1000000 + mid*10 + level;
            if (monsterData.ContainsKey(key))
            {
                return monsterData[key];
            }

            UnitData ud = new UnitData(isPlayer, mid, level);
            monsterData[key] = ud;
            return ud;
        }

        public static int RangeInt(int a, int b)
        {
            var rd = new Random();
            return rd.Next(a, b);
        }

        public static float RangeFloat(float a, float b)
        {
            var rd = new Random();
            return (float)(rd.NextDouble())*(b-a)+a;
        }



        public static List<List<float>> ParseConfig(string config)
        {
            var ret = new List<List<float>>();
            var g = config.Split(char.Parse("|"));
            foreach (var s in g)
            {
                var c = s.Split(char.Parse("_"));
                var c1 = new List<float>();
                ret.Add(c1);
                foreach (var c2 in c)
                {
                    var f = Convert.ToSingle(c2);
                    c1.Add(f);
                }
            }
            return ret;
        }

        public static double GetTimeNow()
        {
	        return DateTime.UtcNow.Ticks/10000000.0;
        }

        /// <summary>
        /// 服务器上距离2016年1月1日过去的时间
        /// </summary>
        /// <returns></returns>
        public static int GetServerTime()
        {
            /*
            var gameBegin = new DateTime(2016, 1, 1);
            var now = DateTime.Now;
            //var passTicks = now.Ticks - gameBegin.Ticks;
            var passTicks = now.Ticks;
            var sec = passTicks/10000000.0f;
            return (int) sec;
            */
            var span = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            return (int)span.TotalSeconds;
        }
        public static string GetBinPath()
        {
            var path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            return path.Substring(6);
        }
    }

    public static class Log
    {
        public static void AI(string s)
        {
            LogHelper.Log("Info", s);
        }

        public static void Important(string s)
        {
            LogHelper.Log("Info", s);
        }

        public static void Sys(string s)
        {
            LogHelper.Log("Info", s);
        }

        public static void Critical(string s)
        {
            LogHelper.Log("Info", s);
        }
    }
    public class Debug{
		private static StringBuilder sb = new StringBuilder();

        [Conditional("DEBUG_LOG")]
        public static void Log(string msg) {
			//Console.WriteLine(msg);
            WriteFile(msg);
        }
        [Conditional("DEBUG_LOG")]
        public static void LogError(string msg) {
            //Console.WriteLine("Error:"+msg);
            var sb2 = Util.PrintStackTrace();
            WriteFile(msg+sb2);
        }
        [Conditional("DEBUG_LOG")]
        public static void LogWarning(string msg){
            //Console.WriteLine(msg);
            WriteFile(msg);
        }
         
        private static readonly  object Locker = new object();
        [Conditional("DEBUG_LOG")]
        private static void WriteFile(string msg)
        {
            LogHelper.Log("Info", msg);
            /*
            lock (Locker)
            {
                sb.Append(msg);
                if (sb.Length > 1000)
                {
                    File.AppendAllText("log.txt", sb.ToString());
                    sb.Clear();
                }
            }
            */
        }
    }
}


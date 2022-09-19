namespace Shootout
{
    using SFDGameScriptInterface;
    using System.Linq;
    using System;
    using System.Collections.Generic;

    public class SE_Debug
    {
        public string Name { get; set; }
        public bool Enable { get; set; }

        public static bool HaveWarnings { get; set; }
        public static bool HaveErrors { get; set; }
        
        public static int Warnings { get; set; }

        List<Tuple<string, Color>> DebugLog = new List<Tuple<string, Color>>();
        
        public SE_Debug(string Name)
        {
            this.Name = Name;
        }

        public void AddToLog(string str, Color color)
        {
            DebugLog.Add(new Tuple<string, Color>(str, color));
        }

        public void AddToLog(Color color, params string[] str)
        {
            foreach (string s in str)
                DebugLog.Add(new Tuple<string, Color>(s, color));
        }
        
        public void AddToLogSuccessful (string str)
        {
            DebugLog.Add(new Tuple<string, Color>(str, Color.Green));
        }

        public void AddToLogSuccessful (params string[] str)
        {
            foreach (string s in str)
            {
                DebugLog.Add(new Tuple<string, Color>(s, Color.Green));
            }
        }
        
        public void AddToLogWarning (string str)
        {
            DebugLog.Add(new Tuple<string, Color>(str, Color.Yellow));
            Warnings++;
            HaveWarnings = true;
        }

        public void AddToLogWarning (params string[] str)
        {
            foreach (string s in str)
            {
                DebugLog.Add(new Tuple<string, Color>(s, Color.Yellow));
                Warnings++;
            }

            HaveWarnings = true;
        }

        public void OutputToUser(IUser user)
        {
            SEE_Game.SendMessageToPlayer(user, Color.Green, "DEBUG OUTPUT: " + Name);

            foreach (Tuple<string, Color> tuple in DebugLog)
            {
                SEE_Game.SendMessageToPlayer(user, tuple.Item2, tuple.Item1);
            }
        }

        public void OutputToHost()
        {
            SEE_Game.SendMessageToHost(Color.Green, "DEBUG OUTPUT: " + Name);

            foreach (Tuple<string, Color> tuple in DebugLog)
            {
                SEE_Game.SendMessageToHost(tuple.Item2, tuple.Item1);
            }
        }
    }
}

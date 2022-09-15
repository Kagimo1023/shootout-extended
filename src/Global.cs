namespace Shootout
{
    using SFDGameScriptInterface;
    using System.Collections.Generic;
    using System.Linq;
    using System;

    public static class Global
    {
        /* SHOOTOUT MODE: EXTENDED SETTINGS */
        public static bool showDialogues = true;
        public static bool showDescriptions = true;
        public static bool shuffleCharacters = true;
        public static bool debugMode = true;
        public static bool cloneableChars = false;
        public static bool oneAndOnlyMode = false;
        public static bool unstableBuild = true;
        public static bool botsSupport = true;

        public static readonly string version = "v0.40 Pre-Alpha";
        public static readonly string [] buildType =
        {
            "(Release ",
            "(Unstable)"
        };

        public static readonly string gameName = "SHOOTOUT: EXTENDED MODE " + version;

        public static readonly string features =
            gameName + "\n" +
            "- New plugin Doors Generator!\n" +
            "- New plugin Spectating Mode!\n" +
            "- Fixed many bugs in script!\n\n";

        public static Random random = new Random ();

        public static List <Vector2> _spawnPoints = new List <Vector2> ();
        public static int [] _users;

        // Original author: NearHuscarl
        // https://github.com/NearHuscarl/BotExtended/blob/master/src/BotExtended/Library/SharpHelper.cs
        #region NearHuscarlCode
        public static T StringToEnum<T>(string str) => (T)Enum.Parse(typeof(T), str);
        public static T[] EnumToArray<T>() => (T[])Enum.GetValues(typeof(T));
        public static string EnumToString<T>(T enumVal) => Enum.GetName(typeof(T), enumVal);
        
        public static void Timeout(Action callback, uint interval) => Events.UpdateCallback.Start(e => callback.Invoke(), interval, 1);
        
        public static bool TryParseEnum<T>(string str, out T result) where T : struct, IConvertible
        {
            result = default(T);

            if (!typeof(T).IsEnum)
            {
                return false;
            }

            int index = -1;
            if (int.TryParse(str, out index))
            {
                if (Enum.IsDefined(typeof(T), index))
                {
                    // https://stackoverflow.com/questions/10387095/cast-int-to-generic-enum-in-c-sharp
                    result = (T)(object)index;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (!Enum.TryParse(str, ignoreCase: true, result: out result))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        public static void OnStartMessages ()
        {
            GameScriptInterface.Game.ShowPopupMessage (features, Color.Green);

            Events.UpdateCallback.Start((x) => GameScriptInterface.Game.HidePopupMessage(), 7000u, 1);
        }
    
        public static void Shuffle<T> (this Random rng, T[] array)
        {
            int n = array.Length;
            while (n > 1) 
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static Int32 GetPercent(Int32 b, Int32 a)
        {
            if (b == 0) return 0;

            return (Int32)( a / (b / 100M));
        }

        public static void SendMessageToAll (Color color, params string [] message)
        {
            foreach (string msg in message)
                GameScriptInterface.Game.ShowChatMessage (msg, color);
        }

        public static Vector2 GetCenterOfObject (IObject obj)
        {
            Vector2 tmp = new Vector2 ();
            Area area = obj.GetAABB ();
            tmp = area.Center;
            return tmp;
        }

        public static void SendMessageToPlayer (IUser user, Color color, params string [] message)
        {
            UserMessageCallbackArgs args = new UserMessageCallbackArgs (user, "");

            foreach (string msg in message)
            {
                if (!user.IsBot)
                    GameScriptInterface.Game.ShowChatMessage (msg, color, args.User.UserIdentifier);
            }
        }

        public static void SendMessageToHost (Color color, params string [] message)
        {
            foreach(IUser user in GameScriptInterface.Game.GetActiveUsers())
            {
                UserMessageCallbackArgs args = new UserMessageCallbackArgs (user, "");
                foreach (string msg in message)
                {
                    if (user.IsHost)
                        GameScriptInterface.Game.ShowChatMessage (msg, color, args.User.UserIdentifier);
                }
            }
        }

        public static void OnStartDescriptions (bool show)
        {
            foreach (Character character in Characters.List.Playable)
                character.SetOnStartShowDescription (show);
        }

        public static void OnStartDialogues (bool show)
        {
            foreach (Character character in Characters.List.Playable)
                character.SetOnStartShowDialogue (show);
        }

        public static IDialogue CreateDialogue (string text, Color color, Vector2 pos, string name, float duration, bool showInChat)
        {
            return GameScriptInterface.Game.CreateDialogue (text, color, pos, name, duration, showInChat);
        }

        public static void ShowCFTX (string text, IObject obj)
        {
            GameScriptInterface.Game.PlayEffect ("CFTXT", obj.GetWorldPosition (), text);
        }

        public static void ShowGameEffect (string gameEffectName, IObject obj)
        {
            GameScriptInterface.Game.PlayEffect (gameEffectName, obj.GetWorldPosition ());
        }

        public static int[] GenerateRandomArrayNonDuplicate (int length)
        {
            return Enumerable.Range (0, length).OrderBy(c => random.Next()).ToArray();
        }

        public static void SetSpawnPoints ()
        {
            Global._users = Global.GenerateRandomArrayNonDuplicate (GameScriptInterface.Game.GetActiveUsers ().Length);

            foreach (IObject obj in GameScriptInterface.Game.GetObjectsByName("SpawnPlayer"))
            {
                _spawnPoints.Add (obj.GetWorldPosition ());
            }
        }

        public static float GetPercentByValue (float current, float maximum)
        {
            float result = (float) Math.Round ((double) (100 * current) / maximum);

            if (debugMode == true) GameScriptInterface.Game.ShowPopupMessage ("Percent by value: " + result.ToString ("N2") + "%");

            return result;
        }

        public static double GetPercentByValue (double current, double maximum)
        {
            double result = Math.Round ((double) (100 * current) / maximum);

            if (debugMode == true) GameScriptInterface.Game.ShowPopupMessage ("Percent by value: " + result.ToString ("N2") + "%");

            return result;
        }

        public static int GetPercentByValue (int current, int maximum)
        {
            int result = (int) Math.Round ((double) (100 * current) / maximum);

            if (debugMode == true) GameScriptInterface.Game.ShowPopupMessage ("Percent by value: " + result + "%");

            return result;
        }

        public static Vector2 GetSpawnPoint (int id) => _spawnPoints [id];
    }
}

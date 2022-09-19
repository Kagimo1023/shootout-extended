namespace Shootout {
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using SFDGameScriptInterface;
    
    public class SEE_Game : GameScriptInterface {
        // public static IGame Game;
        public string Name = "Unnamed";
        
        public static bool EnableToShowInformation;
        public static bool BotsSupport = true;
        public static bool ShowDialogues = true;
        public static bool ShowDescriptions = true;
        public static bool ShuffleCharacters = true;
        public static Vector2 [] SpawnPoints;

        public string [] Description = null;
        public string [] Authors = null;

        public static int [] Users;
        public static readonly string Features;

        public string License = "License: GNU LGPL v3";
        
        public Mode [] Modes = null;
        
        public Mode CurrentMode = null;
        public static Random random = new Random ();

        public SEE_Game (string Name) : base(null)
        {
            this.Name = Name;
        }

        public void TryToStartMode (Mode mode)
        {
            if (mode != null)
            {
                if (CurrentMode.OnStartup)
                {
                    CurrentMode.StartUp(CurrentMode.OnStartup);
                }
            }
            else
            {
                GameScriptInterface.Game.ShowChatMessage("Game not contain a mode. Game started with default SFD map type mode.", Color.Green);
            }
        }

        public void SetRandomMode (Mode [] Modes)
        {
            if (Modes != null)
            {
                Random random = new Random ();
                CurrentMode = Modes[random.Next (0, Modes.Length)];
            }
        }

        public static void OnStartMessages ()
        {
            GameScriptInterface.Game.ShowPopupMessage (Features, Color.Green);

            Events.UpdateCallback.Start((x) => GameScriptInterface.Game.HidePopupMessage(), 7000u, 1);
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

        public static Vector2 GetSpawnPoint (int id) {
             return SpawnPoints [id];
        }

        public void OnStartDescriptions (bool show, Mode mode)
        {
            foreach (Character character in GetCurrentMode().GetPlayableCharacters())
                character.SetOnStartShowDescription (show);
        }

        public void OnStartDialogues (bool show)
        {
            foreach (Character character in CurrentMode.GetPlayableCharacters())
                character.SetOnStartShowDialogue (show);
        }

        public IDialogue CreateDialogue (string text, Color color, Vector2 pos, string name, float duration, bool showInChat)
        {
            return GameScriptInterface.Game.CreateDialogue (text, color, pos, name, duration, showInChat);
        }

        public void ShowCFTX (string text, IObject obj)
        {
            GameScriptInterface.Game.PlayEffect ("CFTXT", obj.GetWorldPosition (), text);
        }

        public void ShowGameEffect (string gameEffectName, IObject obj)
        {
            GameScriptInterface.Game.PlayEffect (gameEffectName, obj.GetWorldPosition ());
        }

        public static void SetSpawnPoints ()
        {
            Users = Global.GenerateRandomArrayNonDuplicate (GameScriptInterface.Game.GetActiveUsers ().Length);

            foreach (IObject obj in GameScriptInterface.Game.GetObjectsByName("SpawnPlayer"))
            {
                Global.Append (SpawnPoints,obj.GetWorldPosition ());
            }
        }

        public void SetDescription (params string [] Description) { this.Description = Description; }

        public void SetName (string Name) { this.Name = Name; }
        public void SetMode (Mode mode) { this.CurrentMode = mode; }
        public void SetAuthors (params string [] Authors) { this.Authors = Authors; }

        public Mode GetCurrentMode () { 
            return CurrentMode; 
        }
        public string GetName () { return this.Name; }
        public string [] GetAuthors () { return this.Authors; }
        public Mode [] GetModeList () { return this.Modes; }
    }
}
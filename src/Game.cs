namespace Shootout {
    using System;
    using SFDGameScriptInterface;
    
    public class SEE_Game {        
        public string Name = "Unnamed";
        
        public bool EnableToShowInformation;

        public string [] Description = null;
        public string [] Authors = null;
        public string License = "GNU GPL v3";
        
        public Mode [] Modes = null;
        
        public Mode CurrentMode;
        
        public SEE_Game (IGame Game, string Name, Mode mode)
        {
            this.Name = Name;
            this.CurrentMode = mode;

            if (CurrentMode != null && CurrentMode.OnStartup)
                CurrentMode.StartUp(CurrentMode.OnStartup);
            else
                Game.ShowChatMessage("Game was not set a mode. Game started with default SFD map type mode.", Color.Green);
        }

        public void SetName (string Name) => this.Name = Name;
        public void SetMode (Mode mode) => this.CurrentMode = mode;
        public void SetAuthors (params string [] Authors) => this.Authors = Authors;

        public Mode GetCurrentMode () => this.CurrentMode;
        public string GetName () => this.Name;
        public string [] GetAuthors () => this.Authors;
        public Mode [] GetModeList () => this.Modes;
        
        public void SetRandomMode (Mode [] Modes)
        {
            Random random = new Random ();
            CurrentMode = Modes[random.Next (0, Modes.Length)];
        }
    }
}
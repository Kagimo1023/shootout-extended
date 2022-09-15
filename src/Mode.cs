namespace Shootout {
    using System;
    using SFDGameScriptInterface;
    
    public abstract class Mode {
        public static Callbacks CallbackList;
        
        private IGame Game;
        
        private string Name;
        
        private string [] Authors;
        private string [] Description;
        private object [] Settings;
        
        private Character [] Playable;
        private Character [] NonPlayable;
        private Character [] Death;
        
        private Plugin [] Plugins;

        public bool OnStartup { get; set; }

        public Mode (string Name, bool OnStartup)
        {
            this.Name = Name;
            this.OnStartup = OnStartup;
        }

        public void StopAllPlugins (Plugin [] Plugins)
        {
            foreach (Plugin plugin in Plugins)
                plugin.Enable = false;
        }
        
        public void SetPlayableCharacterrs (params Character [] Characters) => this.Playable = Characters;
        public void SetNonPlayableCharacterrs (params Character [] Characters) => this.NonPlayable = Characters;
        public void SetDeathCharacterrs (params Character [] Characters) => this.Death = Characters;
        
        public void SetSettings (object [] Settings) => this.Settings = Settings;
        public void SetName (string Name) => this.Name = Name;
        public void SetAuthors (params string [] Authors) => this.Authors = Authors;
        public void SetDescription (params string [] Description) => this.Description = Description;

        public object [] GetSettings () => this.Settings;
        public string GetName () => this.Name;
        public string [] GetDescription () => this.Description;
        public object [] GetAuthors () => this.Authors;

        public abstract void StartUp(bool Start);
        public abstract bool CheckSettings (object [] Settings);
    }
}
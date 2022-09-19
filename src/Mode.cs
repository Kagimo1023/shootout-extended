namespace Shootout {
    using System;
    using SFDGameScriptInterface;
    
    public abstract class Mode {
        public static Callbacks CallbackList;
        
        private string Name;
        
        private string [] Authors = null;
        private string [] Description = null;
        private object [] Settings = null;
        
        private Character [] Playable = null;
        private Character [] NonPlayable = null;
        private Character [] Death = null;
        
        private Plugin [] Plugins = null;

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

        public void SetPlayableCharacterrs (params Character [] Characters) { 
            this.Playable = Characters;
        }
        
        public void SetNonPlayableCharacterrs (params Character [] Characters) {
            this.NonPlayable = Characters;
        }
        
        public void SetDeathCharacterrs (params Character [] Characters) {
            this.Death = Characters;
        }
        
        public void SetSettings (object [] Settings) {
            this.Settings = Settings;
        }
        public void SetName (string Name) { 
            this.Name = Name;
        }
        
        public void SetAuthors (params string [] Authors) { 
            this.Authors = Authors;
        }
        
        public void SetPlugins (params Plugin [] Plugins) {
            this.Plugins = Plugins;
        }

        public void SetDescription (params string [] Description) { 
            this.Description = Description;
        }

        public string GetName () { return this.Name; }
        public object [] GetSettings () {return this.Settings;} 
        public string [] GetDescription () { return this.Description; }
        public object [] GetAuthors () { return this.Authors; }
        public Plugin [] GetPlugins () { return this.Plugins; }
        public Callbacks GetCallbacks () { return Mode.CallbackList; }
        public Character [] GetPlayableCharacters () {return this.Playable; }

        public abstract void StartUp(bool Start);
        public abstract bool CheckSettings (object [] Settings);

        #region PluginManipulationRegion
        // Add some plugin on start. This function checking type of plugin and add to some category.

        public void RunOnStartupPlugins(Plugin [] ActionList)
        {
            if (ActionList != null && ActionList.Length != 0)
            {
                foreach (Plugin plg in ActionList)
                {
                    if (plg != null)
                        plg.OnStartup();
                }
            } else {
            }
        }

        public void RunAfterStartupPlugins(Plugin [] ActionList)
        {
            if (ActionList != null && ActionList.Length != 0)
            {
                foreach (Plugin plg in ActionList)
                {
                    if (plg != null)
                        plg.AfterStartup();
                }
            } else {
            }
        }

        public void RunOnShutdownPlugins(Plugin [] ActionList)
        {
            if (ActionList != null && ActionList.Length != 0)
            {
                foreach (Plugin plg in ActionList)
                {
                    if (plg != null)
                        plg.OnShutdown();
                }
            } else {
            }
        }

        public void RunOnUpdatePlugins(Plugin [] ActionList)
        {
            if (ActionList != null && ActionList.Length != 0)
            {
                foreach (Plugin plg in ActionList)
                {
                    if (plg != null)
                    {
                        Events.UpdateCallback.Start(plg.OnUpdate, (uint) plg.GetSleepOnUpdate(), plg.GetCyclesOnUpdate());
                    }
                }
            } else {
            }
        }

        public void RunOnGameoverPlugins(Plugin [] ActionList)
        {
            if (ActionList != null && ActionList.Length != 0)
            {
                foreach (Plugin plg in ActionList)
                {
                    if (plg != null)
                        plg.OnGameover();
                }
            } else {
                return;
            }
        }
        #endregion
    }
}
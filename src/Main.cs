namespace Shootout
{
    using SFDGameScriptInterface;
    using System.Collections.Generic;

    class Shootout
    {
        public static IGame Game;

        SE_Debug MainDebug;

        public Shootout (IGame game)
        {
            MainDebug = new SE_Debug("Main Debug Log");
            MainDebug.AddToLogSuccessful("Shootout() was called");
            Game = game;
            _cb = new Callbacks ();
            _cb.OnPlayerDamage = new PlayerDamageCallback ();
            _cb.OnPlayerDeath = new PlayerDeathCallback ();
            _cb.OnPlayerKeyInput = new PlayerKeyInputCallback ();
            _cb.OnObjectCreated = new UpdateCallback ();
            MainDebug.AddToLogSuccessful("Shootout() successful started!");
        }

        private void StartCallbacks ()
        {
            Events.PlayerDamageCallback.Start (((PlayerDamageCallback)_cb.OnPlayerDamage).Run);
            Events.PlayerDeathCallback.Start (((PlayerDeathCallback)_cb.OnPlayerDeath).Run);
            Events.PlayerKeyInputCallback.Start (((PlayerKeyInputCallback)_cb.OnPlayerKeyInput).Run);
            Events.UpdateCallback.Start (((UpdateCallback)_cb.OnObjectCreated).Run);
        }

        private void PluginSetup ()
        {
            MainDebug.AddToLogSuccessful("PluginSetup() was called");

            foreach (Plugin plugin in Plugins.List.PluginList)
                
            
            if (Global.debugMode == true && Plugins.List.PluginList.Length == 0)
                Global.SendMessageToHost (Color.Green, "Plugin list is empty!");
        }

        public void Start ()
        {
            StartCallbacks ();
            Global.SetSpawnPoints ();
            ShuffleCharacters (Global.shuffleCharacters);
            Global.OnStartMessages ();
            GenerateCharacters ();
            PluginSetup ();
        }
        
        private void ShuffleCharacters (bool shuffle) {
            if (shuffle) 
                Global.random.Shuffle (Characters.List.Playable);
        }
        
        public static Callbacks GetCallbacks () => _cb;
        public static IGame GetGame() => Game;
        
        private void NonSpawnCharacters (params Character [] characters)
        {
            foreach (Character chr in characters)
                chr.SetOnStartSpawn (false);
        }

        private void GenerateCharacters ()
        {
            int id = 0;
            int [] spw = Global.GenerateRandomArrayNonDuplicate (Global._spawnPoints.Count);

            if (Characters.List.Playable.Length == 0) return;
            
            foreach (Character character in Characters.List.Playable)
            {   
                if (id < Global._users.Length)
                {
                    if (character.GetOnStartSpawn ())
                    {
                        character.SetOldPlayer (Game.GetActiveUsers () [Global._users [id]].GetPlayer ());
                        character.GetOldPlayer ().Remove ();
                        character.SetUser (Game.GetActiveUsers () [Global._users [id]]);

                        if (id < Global._spawnPoints.Count)
                        {
                            character.CreateCharacter (Global.GetSpawnPoint (spw [id]));
                        }

                    }

                    id++;
                }
            }
        }

        #region PluginManipulationRegion
        public static Plugin GetPluginById (int id) => PluginList[id];
        // Get plugins list by type
        public static List <Plugin> GetPluginList () => PluginList;
        // Add some plugin on start. This function checking type of plugin and add to some category.
        public static void AddActionToList (Plugin plugin) => PluginList.Add(plugin);

        public void RunOnStartupPlugins(List<Plugin> ActionList)
        {
            if (ActionList != null && ActionList.Count != 0)
            {
                foreach (Plugin plg in ActionList)
                {
                    if (plg != null)
                        plg.OnStartup();
                }
            } else {
                if (Global.debugMode == true)
                    Global.SendMessageToHost (Color.Yellow, "OnStartup call failed! Plugin list is empty!");
            }
        }

        public void RunAfterStartupPlugins(List<Plugin> ActionList)
        {
            if (ActionList != null && ActionList.Count != 0)
            {
                foreach (Plugin plg in ActionList)
                {
                    if (plg != null)
                        plg.AfterStartup();
                }
            } else {
            }
        }

        public void RunOnShutdownPlugins(List<Plugin> ActionList)
        {
            if (ActionList != null && ActionList.Count != 0)
            {
                foreach (Plugin plg in ActionList)
                {
                    if (plg != null)
                        plg.OnShutdown();
                }
            } else {
            }
        }

        public void RunOnUpdatePlugins(List<Plugin> ActionList)
        {
            if (ActionList != null && ActionList.Count != 0)
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

        public void RunOnGameoverPlugins(List<Plugin> ActionList)
        {
            if (ActionList != null && ActionList.Count != 0)
            {
                foreach (Plugin plg in ActionList)
                {
                    if (plg != null)
                        plg.OnGameover();
                }
            } else {
            }
        }
        #endregion
    }
}

namespace BTFS
{
    using SFDGameScriptInterface;
    using System;
    using System.Text;

    namespace Modes
    {
        public class Default : Mode
        {
            public Default(string Name = "Default Mode", bool Enable = true) : base(Name, Enable)
            {
            }

            public override void StartUp(bool Enable)
            {
                if (Enable == true)
                {
                    SetPlugins(Modes.Plugins.List.Enabled);
                    BTFS_Game.OnStartMessages(string.Format("{0} {1}", Shootout_Extended.Name, Shootout_Extended.Version, Shootout_Extended.Features), Color.Cyan, 5000);
                    BTFS_Game.InitializeCallbacks();
                    BTFS_Game.random.Shuffle(Characters.List.Playable); // Shuffle characters list
                    BTFS_Game.SetSpawnPoints("SpawnPlayer");
                    GenerateCharacters(Characters.List.Playable);
                }
            }

            public override bool CheckSettings(object[] Settings)
            {
                throw new System.NotImplementedException();
            }

            private void GenerateCharacters(Character[] characters)
            {
                // GameScriptInterface.Game.ShowChatMessage("HERE");

                int id = 0;
                int[] spw = Global.GenerateRandomArrayNonDuplicate(BTFS_Game.SpawnPoints.Count);

                foreach (Character character in characters)
                {
                    if (id < BTFS_Game.Users.Length)
                    {
                        character.SetOldPlayer(GameScriptInterface.Game.GetActiveUsers()[BTFS_Game.Users[id]].GetPlayer());
                        character.GetOldPlayer().Remove();
                        character.SetUser(GameScriptInterface.Game.GetActiveUsers()[BTFS_Game.Users[id]]);

                        if (id < BTFS_Game.SpawnPoints.Count)
                        {
                            character.CreateCharacter(BTFS_Game.GetSpawnPoint(spw[id]));
                        }

                        id++;
                    }
                }
            }
        }
    }
}

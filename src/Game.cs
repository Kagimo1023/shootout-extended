namespace BTFS
{
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using SFDGameScriptInterface;

    public class BTFS_Game : GameScriptInterface
    {
        // public static IGame Game;
        public static string Name { get; set; }
        public static string Features { get; set; }
        public static string Version { get; set; }

        public static bool EnableToShowInformation;
        public static bool BotsSupport = true;
        public static bool ShowDialogues = true;
        public static bool ShowDescriptions = true;
        public static bool ShuffleCharacters = true;

        public static Callbacks cbs;

        public static List<Vector2> SpawnPoints = new List<Vector2>();

        public static string[] Description = null;
        public static string[] Authors = null;

        public static int[] Users;

        public readonly string License = "License: GNU LGPL v3";

        public Mode[] Modes = null;

        public static Mode CurrentMode = null;
        public static Random random = new Random();

        public static Events.UserMessageCallback uargs = null;

        public enum ModifierType
        {
            MaxHealth,
            MaxEnergy,
            CurrentHealth,
            CurrentEnergy,
            EnergyConsumption,
            ExplosionDamageTaken,
            ProjectileDamageTaken,
            ProjectileCritChanceTaken,
            FireDamageTaken,
            MeleeDamageTaken,
            ImpactDamageTaken,
            ProjectileDamageDealt,
            ProjectileCritChanceDealt,
            MeleeDamageDealt,
            MeleeForce,
            MeleeStunImmunity,
            CanBurn,
            RunSpeed,
            SprintSpeed,
            EnergyRecharge,
            Size,
            InfiniteAmmo,
            ItemDropMode
        }

        public BTFS_Game(string _Name) : base(null)
        {
            Name = _Name;
        }

        public static void InitializeCallbacks()
        {
            cbs = new Callbacks();

            cbs.OnPlayerDamage = new PlayerDamageCallback();
            cbs.OnPlayerDeath = new PlayerDeathCallback();
            cbs.OnPlayerKeyInput = new PlayerKeyInputCallback();
            cbs.OnObjectCreated = new UpdateCallback();

            Events.PlayerDamageCallback.Start(((PlayerDamageCallback)cbs.OnPlayerDamage).Run);
            Events.PlayerDeathCallback.Start(((PlayerDeathCallback)cbs.OnPlayerDeath).Run);
            Events.PlayerKeyInputCallback.Start(((PlayerKeyInputCallback)cbs.OnPlayerKeyInput).Run);
            Events.UpdateCallback.Start(((UpdateCallback)cbs.OnObjectCreated).Run);

            uargs = Events.UserMessageCallback.Start(OnUserMessage);
        }

        public static Callbacks GetCallbacks()
        {
            return cbs;
        }

        public void TryToStartMode(Mode mode)
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

        public static void OnUserMessage (UserMessageCallbackArgs args)
        {
            // foreach (CommandShell Command in )
        }

        public static float GetModifiersByTypes(IPlayer plr, params ModifierType[] mt)
        {
            PlayerModifiers pm = plr.GetModifiers();

            foreach (ModifierType modifiertype in mt)
            {
                switch (modifiertype)
                {
                    case ModifierType.MaxHealth:
                        return pm.MaxHealth;
                    case ModifierType.MaxEnergy:
                        return pm.MaxEnergy;
                    case ModifierType.CurrentHealth:
                        return pm.CurrentHealth;
                    case ModifierType.CurrentEnergy:
                        return pm.CurrentEnergy;
                    case ModifierType.EnergyConsumption:
                        return pm.EnergyConsumptionModifier;
                    case ModifierType.ExplosionDamageTaken:
                        return pm.ExplosionDamageTakenModifier;
                    case ModifierType.ProjectileDamageTaken:
                        return pm.ProjectileDamageTakenModifier;
                    case ModifierType.ProjectileCritChanceTaken:
                        return pm.ProjectileCritChanceTakenModifier;
                    case ModifierType.FireDamageTaken:
                        return pm.FireDamageTakenModifier;
                    case ModifierType.MeleeDamageTaken:
                        return pm.MeleeDamageTakenModifier;
                    case ModifierType.ImpactDamageTaken:
                        return pm.ImpactDamageTakenModifier;
                    case ModifierType.ProjectileDamageDealt:
                        return pm.ProjectileDamageDealtModifier;
                    case ModifierType.ProjectileCritChanceDealt:
                        return pm.ProjectileCritChanceDealtModifier;
                    case ModifierType.MeleeDamageDealt:
                        return pm.MeleeDamageDealtModifier;
                    case ModifierType.MeleeForce:
                        return pm.MeleeForceModifier;
                    case ModifierType.MeleeStunImmunity:
                        return pm.MeleeStunImmunity;
                    case ModifierType.CanBurn:
                        return pm.CanBurn;
                    case ModifierType.RunSpeed:
                        return pm.RunSpeedModifier;
                    case ModifierType.SprintSpeed:
                        return pm.SprintSpeedModifier;
                    case ModifierType.EnergyRecharge:
                        return pm.EnergyRechargeModifier;
                    case ModifierType.Size:
                        return pm.SizeModifier;
                    case ModifierType.InfiniteAmmo:
                        return pm.InfiniteAmmo;
                    case ModifierType.ItemDropMode:
                        return pm.ItemDropMode;
                }
            }

            return -1;
        }

        public void TryToStartMode()
        {
            if (CurrentMode != null)
            {
                if (CurrentMode.OnStartup)
                {
                    InitializeCallbacks();
                    CurrentMode.StartUp(CurrentMode.OnStartup);
                }
            }
            else
            {
                GameScriptInterface.Game.ShowChatMessage("Game not contain a mode. Game started with default SFD map type mode.", Color.Green);
            }
        }

        public void SetRandomMode(Mode[] Modes)
        {
            if (Modes != null)
            {
                Random random = new Random();
                CurrentMode = Modes[random.Next(0, Modes.Length)];
            }
        }

        public static Character[] GetActiveLiveCharacters()
        {
            List<Character> tmp = new List<Character>();

            foreach (Character character in CurrentMode.GetPlayableCharacters())
            {
                if ((character.GetPlayer().IsUser || character.GetPlayer().IsBot) && character.WasSpawned() && !character.GetPlayer().IsDead)
                {
                    tmp.Add(character);
                }
            }

            if (tmp.Count != 0)
                return tmp.ToArray();

            return tmp.ToArray();
        }

        public static void OnStartMessages(string str, Color color, uint delay)
        {
            GameScriptInterface.Game.ShowPopupMessage(str, color);

            Events.UpdateCallback.Start((x) => GameScriptInterface.Game.HidePopupMessage(), delay, 1);
        }

        public static void SendMessageToAll(Color color, params string[] message)
        {
            foreach (string msg in message)
                GameScriptInterface.Game.ShowChatMessage(msg, color);
        }

        public static Vector2 GetCenterOfObject(IObject obj)
        {
            Vector2 tmp = new Vector2();
            Area area = obj.GetAABB();
            tmp = area.Center;
            return tmp;
        }

        public static void SendMessageToPlayer(IUser user, Color color, params string[] message)
        {
            UserMessageCallbackArgs args = new UserMessageCallbackArgs(user, "");

            foreach (string msg in message)
            {
                if (!user.IsBot)
                    GameScriptInterface.Game.ShowChatMessage(msg, color, args.User.UserIdentifier);
            }
        }

        public static void SendMessageToHost(Color color, params string[] message)
        {
            foreach (IUser user in GameScriptInterface.Game.GetActiveUsers())
            {
                UserMessageCallbackArgs args = new UserMessageCallbackArgs(user, "");
                foreach (string msg in message)
                {
                    if (user.IsHost)
                        GameScriptInterface.Game.ShowChatMessage(msg, color, args.User.UserIdentifier);
                }
            }
        }

        public static Vector2 GetSpawnPoint(int id)
        {
            return SpawnPoints[id];
        }

        public void OnStartDescriptions(bool show, Mode mode)
        {
            foreach (Character character in GetCurrentMode().GetPlayableCharacters())
                character.SetOnStartShowDescription(show);
        }

        public void OnStartDialogues(bool show)
        {
            foreach (Character character in CurrentMode.GetPlayableCharacters())
                character.SetOnStartShowDialogue(show);
        }

        public IDialogue CreateDialogue(string text, Color color, Vector2 pos, string name, float duration, bool showInChat)
        {
            return GameScriptInterface.Game.CreateDialogue(text, color, pos, name, duration, showInChat);
        }

        public void DisableCollisionInteraction(bool Melee, bool Projectile, bool Explosion, params Character[] characters)
        {
            IObjectAlterCollisionTile obja = (IObjectAlterCollisionTile)Game.CreateObject("AlterCollision");

            foreach (Character chr in characters)
            {
                obja.AddTargetObject(chr.GetPlayer());
            }

            Events.UpdateCallback.Start((float x) =>
            {
                foreach (IPlayer obj in obja.GetTargetObjects())
                {
                    obja.SetDisablePlayerMelee(Melee);
                    obja.SetDisableProjectileHit(Projectile);

                    if (obj.IsDead && obj.IsRemoved)
                    {
                        obja.SetDisablePlayerMelee(false);
                        obja.SetDisableProjectileHit(false);
                        obja.Remove();
                        return;
                    }
                }
            }, 100, 0);
        }

        public static void ShowCFTX(string text, IObject obj)
        {
            GameScriptInterface.Game.PlayEffect("CFTXT", obj.GetWorldPosition(), text);
        }

        public static void ShowGameEffect(string gameEffectName, IObject obj)
        {
            GameScriptInterface.Game.PlayEffect(gameEffectName, obj.GetWorldPosition());
        }

        public static void SetSpawnPoints(string obj_name)
        {
            Users = Global.GenerateRandomArrayNonDuplicate(GameScriptInterface.Game.GetActiveUsers().Length);

            foreach (IObject obj in GameScriptInterface.Game.GetObjectsByName(obj_name))
            {
                SpawnPoints.Add(obj.GetWorldPosition());
            }
        }

        public void SetDescription(params string[] _Description) { Description = _Description; }

        public static void SetMode(Mode mode) { CurrentMode = mode; }
        public void SetAuthors(params string[] _Authors) { Authors = _Authors; }

        public static Mode GetCurrentMode()
        {
            return CurrentMode;
        }

        public string[] GetAuthors() { return Authors; }
        public Mode[] GetModeList() { return this.Modes; }
    }
}
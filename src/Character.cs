namespace BTFS
{
    using System;
    using SFDGameScriptInterface;

    public partial class Character
    {
        Random random = new Random();

        IUser User;
        IPlayer Player;
        IPlayer OldPlayer;

        IProfile Profile = new IProfile();
        PlayerModifiers Modifiers = new PlayerModifiers();

        // CharacterTeam Team;

        #region Character_Data
        string[] Description = null;
        Skill[] Skills = null;
        // IProfile[] ProfilePacks = null;
        WeaponItem[] Weapons = null;
        #endregion

        #region char_modifiers
        protected float _corpse_health = 100;
        #endregion

        #region char_options
        string _name = "Unnamed";
        float _time_on_spawn = 0;
        int _uniqueID = -1;
        int _dead_counter = 0;
        CameraFocusMode _focus_mode;
        bool _on_start = true;
        bool _hide_name = false;
        bool _hide_status = false;
        bool _wasSpawn = false;
        bool _showDialogueOnSpawn = true;
        bool _showCharDescription = true;
        bool _dead_spawn = false;

        PredefinedAIType _predefinedAIType = PredefinedAIType.None;
        #endregion

        public Character(bool OnStart,
                          bool ShowNickname,
                          bool ShowStatusBar,
                          string Name,
                          string[] Description,
                          IProfile Profile,
                          CameraFocusMode Mode,
                          PlayerModifiers Modifiers,
                          WeaponItem[] Weapons,
                          Skill[] Skills,
                          int CorpseHealth = 100,
                          bool DeadOnSpawn = false)
        {
            SetOnStartSpawn(OnStart);
            SetProfile(Profile);
            SetName(Name);
            SetCameraFocus(Mode);
            SetNametagVisible(ShowNickname);
            SetStatusBarVisible(ShowStatusBar);
            SetCharDescription(Description);
            SetPackModifiers(Modifiers);
            SetWeapons(Weapons);
            SetSkills(Skills);
            SetDeadOnSpawn(DeadOnSpawn);
            SetCorpseHealth(CorpseHealth);
        }

        public void OnCreateCharacter()
        {
            UpdateModifiers();
            UpdateProfile();

            HandleCameraFocus();
            HandleNametagVisible();
            HandleStatusBarVisible();
            HandleModifiers();
            HandleProfile();

            ApplySkills(Skills);

            ShowCharDescription(_showCharDescription);
            ShowCharDialogue(_showDialogueOnSpawn);

            HandleAIType();
            HandleUser();

            GiveWeapons();
            
            HandleDeadOnSpawn();
            
            _wasSpawn = true;
        }

        public void SetUser(IUser user) { User = user; }
        public void SetProfile(IProfile profile) { Profile = profile; }
        public void SetStatusBarVisible(bool hide_status) { _hide_status = hide_status; }
        public void SetNametagVisible(bool hide_name) { _hide_name = hide_name; }
        public void SetCorpseHealth(float corpse_health) { _corpse_health = corpse_health; }
        public void SetCameraFocus(CameraFocusMode focus_mode) { _focus_mode = focus_mode; }
        public void SetTimeOnSpawn(float time) { _time_on_spawn = time; }
        public void SetWeapons(WeaponItem[] weapons) { Weapons = weapons; }
        public void SetPlayer(IPlayer player) { Player = player; }
        public void SetSkills(Skill[] skills) { Skills = skills; }
        public void SetOnStartShowDescription(bool show) { _showCharDescription = show; }
        public void SetOnStartSpawn(bool spawn) { _on_start = spawn; }
        public void CreateCharDialogue(Color color, float duration) { GameScriptInterface.Game.CreateDialogue(GetName(), color, GetPlayer(), "", duration, false); }
        public void SetAIType(PredefinedAIType predefinedAIType) { _predefinedAIType = predefinedAIType; }
        public void SetUniqueID(int id) { _uniqueID = id; }
        public void SetName(string name) { _name = name; }
        public void SetOldPlayer(IPlayer player) { OldPlayer = player; }
        public void SetDeadOnSpawn(bool dead_spawn) { _dead_spawn = dead_spawn; }
        public void SetCharDescription(params string[] char_description) { this.Description = char_description; }
        public void SetOnStartShowDialogue(bool show) { _showDialogueOnSpawn = show; }
        public void SetPackModifiers(PlayerModifiers modifiers) { Modifiers = modifiers; }

        public void HandleStatusBarVisible() { Player.SetStatusBarsVisible(_hide_status); }
        public void HandleNametagVisible() { Player.SetNametagVisible(_hide_name); }
        public void HandleCameraFocus() { Player.SetCameraSecondaryFocusMode(_focus_mode); }
        public void HandleAIType() { Player.SetBotBehavior(new BotBehavior(true, _predefinedAIType)); }
        public void HandleProfile() { Player.SetProfile(Profile); }

        public bool GetDeadOnSpawn() { return _dead_spawn; }
        public bool CharacterIsDead() { return GetPlayer().IsDead; }
        public bool GetOnStartShowDialogue() { return _showDialogueOnSpawn; }
        public bool GetOnStartSpawn() { return _on_start; }
        public bool WasSpawned() { return _wasSpawn; }
        public bool GetOnStartShowDescription() { return _showCharDescription; }

        public string GetName() { return _name; }

        public float GetTimeOnSpawn() { return _time_on_spawn; }

        public int GetDeadCount() { return this._dead_counter; }
        public int GetUniqueID() { return _uniqueID; }

        public PredefinedAIType GetAIType() { return _predefinedAIType; }
        public IPlayer GetOldPlayer() { return this.OldPlayer; }
        public IPlayer GetPlayer() { return Player; }
        public PlayerModifiers GetModifiers() { return this.Modifiers; }
        public IUser GetUser() { return this.User; }
        public Skill[] GetSkills() { return Skills; }

        //Base character constructor
        public Character()
        {
            this.Profile = new IProfile();
            this.Modifiers = new PlayerModifiers();
        }

        //Using for creating player base on Character class
        public IPlayer CreateCharacter(Vector2 position)
        {
            SetPlayer(GameScriptInterface.Game.CreatePlayer(position));
            SetUniqueID(GetPlayer().UniqueID);
            OnCreateCharacter();
            return Player;
        }

        public void HandleModifiers()
        {
            Player.SetModifiers(Modifiers);
            Player.SetCorpseHealth(_corpse_health);
        }

        public void HandleDeadOnSpawn()
        {
            if (_dead_spawn == true)
                Player.Kill();
        }

        public void HandleUser()
        {
            Player.SetUser(User);

            if (BTFS_Game.BotsSupport && User.IsBot)
            {
                PredefinedAIType predefinedAIType = User.BotPredefinedAIType;
                Player.SetBotBehavior(new BotBehavior(true, predefinedAIType));
            }
        }

        //Send to user description about character
        public void ShowCharDescription(bool show)
        {
            if (show && Description == null && User != null)
                BTFS_Game.SendMessageToPlayer(User, Color.Red, "YOU ARE " + GetName().ToUpper(), "Character has not description");

            if (show && Description != null && User != null)
            {
                BTFS_Game.SendMessageToPlayer(User, Color.Green, "YOU ARE " + GetName().ToUpper());
                BTFS_Game.SendMessageToPlayer(User, Color.Green, Description);
            }
        }

        public void ShowCharDialogue(bool show)
        {
            if (show == true)
                CreateCharDialogue(Color.Red, 2000);
        }

        public void ApplySkills(Skill[] skills)
        {
            foreach (Skill s in skills)
            {
                s.SetCallbacks(BTFS_Game.GetCallbacks());
                Player.Apply(s);
            }
        }

        public void GiveWeapons()
        {
            if (Weapons == null)
                return;

            foreach (WeaponItem weapon in Weapons)
                Player.GiveWeaponItem(weapon);
        }

        public void GiveWeapons(WeaponItem[] weapons)
        {
            foreach (WeaponItem weapon in weapons)
                Player.GiveWeaponItem(weapon);
        }

        public IProfile CreateProfile(Gender gender,
                                       IProfileClothingItem skin,
                                       IProfileClothingItem head,
                                       IProfileClothingItem chestover,
                                       IProfileClothingItem chestunder,
                                       IProfileClothingItem hands,
                                       IProfileClothingItem waist,
                                       IProfileClothingItem legs,
                                       IProfileClothingItem feet,
                                       IProfileClothingItem accesory)
        {
            var profile = new IProfile()
            {
                Name = _name,
                Gender = gender,
                Skin = skin,
                Head = head,
                ChestOver = chestover,
                ChestUnder = chestunder,
                Hands = hands,
                Waist = waist,
                Legs = legs,
                Feet = feet,
                Accesory = accesory,
            };

            return profile;
        }

        public void UpdateProfile()
        {
            Profile = CreateProfile(Profile.Gender,
                                      Profile.Skin,
                                      Profile.Head,
                                      Profile.ChestOver,
                                      Profile.ChestUnder,
                                      Profile.Hands,
                                      Profile.Waist,
                                      Profile.Legs,
                                      Profile.Feet,
                                      Profile.Accesory);
        }

        public void UpdateModifiers()
        {
            Modifiers = CreateModifiers(Modifiers.MaxHealth,
                                          Modifiers.MaxEnergy,
                                          Modifiers.CurrentHealth,
                                          Modifiers.CurrentEnergy,
                                          Modifiers.EnergyConsumptionModifier,
                                          Modifiers.ExplosionDamageTakenModifier,
                                          Modifiers.ProjectileDamageTakenModifier,
                                          Modifiers.ProjectileCritChanceTakenModifier,
                                          Modifiers.FireDamageTakenModifier,
                                          Modifiers.MeleeDamageTakenModifier,
                                          Modifiers.ImpactDamageTakenModifier,
                                          Modifiers.ProjectileDamageDealtModifier,
                                          Modifiers.ProjectileCritChanceDealtModifier,
                                          Modifiers.MeleeDamageDealtModifier,
                                          Modifiers.MeleeForceModifier,
                                          Modifiers.MeleeStunImmunity,
                                          Modifiers.CanBurn,
                                          Modifiers.RunSpeedModifier,
                                          Modifiers.SprintSpeedModifier,
                                          Modifiers.EnergyRechargeModifier,
                                          Modifiers.SizeModifier,
                                          Modifiers.InfiniteAmmo,
                                          Modifiers.ItemDropMode);
        }

        public PlayerModifiers CreateModifiers(int max_health,
                                                int max_energy,
                                                float current_health,
                                                float current_energy,
                                                float energy_consumption,
                                                float explosion_damage_taken,
                                                float projectile_damage_taken,
                                                float projectile_crit_chance_taken,
                                                float fire_damage_taken,
                                                float melee_damage_taken,
                                                float impact_damage_taken,
                                                float projectile_damage_dealt,
                                                float projectile_crit_chance_dealt,
                                                float melee_damage_dealt,
                                                float melee_force,
                                                int melee_stun_immunity,
                                                int can_burn,
                                                float run_speed,
                                                float sprint_speed,
                                                float energy_recharge,
                                                float size_modifier,
                                                int infinite_ammo,
                                                int item_drop)
        {
            var modifiers = new PlayerModifiers()
            {
                MaxHealth = max_health,
                MaxEnergy = max_energy,
                CurrentHealth = current_health,
                CurrentEnergy = current_energy,
                EnergyConsumptionModifier = energy_consumption,
                ExplosionDamageTakenModifier = explosion_damage_taken,
                ProjectileDamageTakenModifier = projectile_damage_taken,
                ProjectileCritChanceTakenModifier = projectile_crit_chance_taken,
                FireDamageTakenModifier = fire_damage_taken,
                MeleeDamageTakenModifier = melee_damage_taken,
                ImpactDamageTakenModifier = impact_damage_taken,
                ProjectileDamageDealtModifier = projectile_damage_dealt,
                ProjectileCritChanceDealtModifier = projectile_crit_chance_dealt,
                MeleeDamageDealtModifier = melee_damage_dealt,
                MeleeForceModifier = melee_force,
                MeleeStunImmunity = melee_stun_immunity,
                CanBurn = can_burn,
                RunSpeedModifier = run_speed,
                SprintSpeedModifier = sprint_speed,
                EnergyRechargeModifier = energy_recharge,
                SizeModifier = size_modifier,
                InfiniteAmmo = infinite_ammo,
                ItemDropMode = item_drop
            };

            return modifiers;
        }
    }
}

namespace BTFS
{
    using SFDGameScriptInterface;

    namespace Modes.Characters.Data
    {
        public static class Modifiers 
        {
            public static PlayerModifiers Coleoptera = new PlayerModifiers ()
            {
                MaxHealth = 130,
                MaxEnergy = -1,
                CurrentHealth = 130,
                CurrentEnergy = -1,
                EnergyConsumptionModifier = -1,
                ExplosionDamageTakenModifier = 0.5f,
                ProjectileDamageTakenModifier = 0.5f,
                ProjectileCritChanceTakenModifier = 0.5f,
                FireDamageTakenModifier = 0.5f,
                MeleeDamageTakenModifier = 0.5f,
                ImpactDamageTakenModifier = -1,
                ProjectileDamageDealtModifier = 0.5f,
                ProjectileCritChanceDealtModifier = 0.5f,
                MeleeDamageDealtModifier = 0.5f,
                MeleeForceModifier = 0.5f,
                MeleeStunImmunity = -1,
                CanBurn = -1,
                RunSpeedModifier = -1,
                SprintSpeedModifier = -1,
                EnergyRechargeModifier = -1,
                SizeModifier = -1,
                InfiniteAmmo = -1,
                ItemDropMode = -1
            };
            
            public static PlayerModifiers Brute = new PlayerModifiers ()
            {
                MaxHealth = 130,
                MaxEnergy = -1,
                CurrentHealth = 130,
                CurrentEnergy = -1,
                EnergyConsumptionModifier = 2,
                ExplosionDamageTakenModifier = -1,
                ProjectileDamageTakenModifier = -1,
                ProjectileCritChanceTakenModifier = -1,
                FireDamageTakenModifier = -1,
                MeleeDamageTakenModifier = -1,
                ImpactDamageTakenModifier = 0.1f,
                ProjectileDamageDealtModifier = 0.8f,
                ProjectileCritChanceDealtModifier = -1,
                MeleeDamageDealtModifier = 2,
                MeleeForceModifier = 1.05f,
                MeleeStunImmunity = -1,
                CanBurn = -1,
                RunSpeedModifier = 0.75f,
                SprintSpeedModifier = 0.75f,
                EnergyRechargeModifier = 0.5f,
                SizeModifier = 1.25f,
                InfiniteAmmo = -1,
                ItemDropMode = -1
            };

            public static PlayerModifiers Critman = new PlayerModifiers ()
            {
                MaxHealth = -1,
                MaxEnergy = -1,
                CurrentHealth = -1,
                CurrentEnergy = -1,
                EnergyConsumptionModifier = 3,
                ExplosionDamageTakenModifier = 5,
                ProjectileDamageTakenModifier = -1,
                ProjectileCritChanceTakenModifier = -1,
                FireDamageTakenModifier = 5,
                MeleeDamageTakenModifier = -1,
                ImpactDamageTakenModifier = -1,
                ProjectileDamageDealtModifier = 0.9f,
                ProjectileCritChanceDealtModifier = 25,
                MeleeDamageDealtModifier = -1,
                MeleeForceModifier = -1,
                MeleeStunImmunity = -1,
                CanBurn = -1,
                RunSpeedModifier = 1.1f,
                SprintSpeedModifier = -1,
                EnergyRechargeModifier = -1,
                SizeModifier = -1,
                InfiniteAmmo = -1,
                ItemDropMode = -1
            };

            public static PlayerModifiers Cursed = new PlayerModifiers ()
            {
                MaxHealth = -1,
                MaxEnergy = -1,
                CurrentHealth = -1,
                CurrentEnergy = -1,
                EnergyConsumptionModifier = -1,
                ExplosionDamageTakenModifier = -1,
                ProjectileDamageTakenModifier = -1,
                ProjectileCritChanceTakenModifier = 0,
                FireDamageTakenModifier = -1,
                MeleeDamageTakenModifier = -1,
                ImpactDamageTakenModifier = 40,
                ProjectileDamageDealtModifier = 1.8f,
                ProjectileCritChanceDealtModifier = -1,
                MeleeDamageDealtModifier = -1,
                MeleeForceModifier = -1,
                MeleeStunImmunity = 0,
                CanBurn = -1,
                RunSpeedModifier = -1,
                SprintSpeedModifier = -1,
                EnergyRechargeModifier = 10,
                SizeModifier = -1,
                InfiniteAmmo = -1,
                ItemDropMode = -1
            };

            public static PlayerModifiers Exploder = new PlayerModifiers ()
            {
                MaxHealth = -1,
                MaxEnergy = -1,
                CurrentHealth = -1,
                CurrentEnergy = -1,
                EnergyConsumptionModifier = -1,
                ExplosionDamageTakenModifier = 0.1f,
                ProjectileDamageTakenModifier = 2,
                ProjectileCritChanceTakenModifier = 0.75f,
                FireDamageTakenModifier = -1,
                MeleeDamageTakenModifier = -1,
                ImpactDamageTakenModifier = -1,
                ProjectileDamageDealtModifier = -1,
                ProjectileCritChanceDealtModifier = -1,
                MeleeDamageDealtModifier = -1,
                MeleeForceModifier = -1,
                MeleeStunImmunity = -1,
                CanBurn = -1,
                RunSpeedModifier = -1,
                SprintSpeedModifier = -1,
                EnergyRechargeModifier = -1,
                SizeModifier = -1,
                InfiniteAmmo = -1,
                ItemDropMode = -1
            };

            public static PlayerModifiers Mauler = new PlayerModifiers ()
            {
                MaxHealth = 120,
                MaxEnergy = -1,
                CurrentHealth = 120,
                CurrentEnergy = -1,
                EnergyConsumptionModifier = -1,
                ExplosionDamageTakenModifier = -1,
                ProjectileDamageTakenModifier = -1,
                ProjectileCritChanceTakenModifier = 0.7f,
                FireDamageTakenModifier = -1,
                MeleeDamageTakenModifier = -1,
                ImpactDamageTakenModifier = -1,
                ProjectileDamageDealtModifier = 0.3f,
                ProjectileCritChanceDealtModifier = 0.5f,
                MeleeDamageDealtModifier = -1,
                MeleeForceModifier = 1.1f,
                MeleeStunImmunity = -1,
                CanBurn = -1,
                RunSpeedModifier = -1,
                SprintSpeedModifier = 1.1f,
                EnergyRechargeModifier = -1,
                SizeModifier = 1.05f,
                InfiniteAmmo = -1,
                ItemDropMode = -1
            };

            public static PlayerModifiers Soldier = new PlayerModifiers ()
            {
                MaxHealth = 110,
                MaxEnergy = -1,
                CurrentHealth = 110,
                CurrentEnergy = -1,
                EnergyConsumptionModifier = -1,
                ExplosionDamageTakenModifier = 0.5f,
                ProjectileDamageTakenModifier = 0.3f,
                ProjectileCritChanceTakenModifier = -1,
                FireDamageTakenModifier = -1,
                MeleeDamageTakenModifier = 2.5f,
                ImpactDamageTakenModifier = -1,
                ProjectileDamageDealtModifier = 1.5f,
                ProjectileCritChanceDealtModifier = -1,
                MeleeDamageDealtModifier = -1,
                MeleeForceModifier = -1,
                MeleeStunImmunity = -1,
                CanBurn = -1,
                RunSpeedModifier = -1,
                SprintSpeedModifier = -1,
                EnergyRechargeModifier = -1,
                SizeModifier = -1,
                InfiniteAmmo = -1,
                ItemDropMode = -1
            };

            public static PlayerModifiers Thief = new PlayerModifiers ()
            {
                MaxHealth = 80,
                MaxEnergy = 200,
                CurrentHealth = 80,
                CurrentEnergy = 200,
                EnergyConsumptionModifier = -1,
                ExplosionDamageTakenModifier = -1,
                ProjectileDamageTakenModifier = -1,
                ProjectileCritChanceTakenModifier = 1.5f,
                FireDamageTakenModifier = -1,
                MeleeDamageTakenModifier = -1,
                ImpactDamageTakenModifier = -1,
                ProjectileDamageDealtModifier = -1,
                ProjectileCritChanceDealtModifier = 1.2f,
                MeleeDamageDealtModifier = -1,
                MeleeForceModifier = -1,
                MeleeStunImmunity = -1,
                CanBurn = -1,
                RunSpeedModifier = 1.05f,
                SprintSpeedModifier = 1.05f,
                EnergyRechargeModifier = -1,
                SizeModifier = -1,
                InfiniteAmmo = -1,
                ItemDropMode = -1
            };

            public static PlayerModifiers Virus = new PlayerModifiers ()
            {
                MaxHealth = 75,
                MaxEnergy = -1,
                CurrentHealth = 75,
                CurrentEnergy = -1,
                EnergyConsumptionModifier = -1,
                ExplosionDamageTakenModifier = -1,
                ProjectileDamageTakenModifier = -1,
                ProjectileCritChanceTakenModifier = 1,
                FireDamageTakenModifier = -1,
                MeleeDamageTakenModifier = -1,
                ImpactDamageTakenModifier = -1,
                ProjectileDamageDealtModifier = -1,
                ProjectileCritChanceDealtModifier = -1,
                MeleeDamageDealtModifier = -1,
                MeleeForceModifier = -1,
                MeleeStunImmunity = -1,
                CanBurn = -1,
                RunSpeedModifier = -1,
                SprintSpeedModifier = 1.2f,
                EnergyRechargeModifier = -1,
                SizeModifier = -1,
                InfiniteAmmo = -1,
                ItemDropMode = -1
            };
        }
    }
}
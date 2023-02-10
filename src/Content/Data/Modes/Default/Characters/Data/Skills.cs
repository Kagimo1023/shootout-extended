namespace BTFS
{
    namespace Modes.Characters.Data
    {
        public static class Skills
        {
            public static Skill[] Coleoptera = {
                new Modes.Data.Skills.Actives.EatingObjects (50, null)
            };

            public static Skill[] Brute = {
                // new Modes.Data.Skills.Actives.ChargePunch (null, 0.5, 1.2, 5)
            };

            public static Skill[] Critman = {
            };

            public static Skill[] Cursed = {
                new Modes.Data.Skills.BodyTrap (null)
            };

            public static Skill[] Exploder = {
                new Modes.Data.Skills.Kamikaze (null, "EAT THAT'S BITCH!"),
                new Modes.Data.Skills.Actives.MineBody (50, null)
            };

            public static Skill [] Mauler = {
                new Modes.Data.Skills.DamageUp (null, 3, 0.001f),
                new Modes.Data.Skills.Actives.DevilArmor (80, null)
            };

            public static Skill[] Soldier = {
            };

            public static Skill[] Thief = {
                new Modes.Data.Skills.SpeedUp (null),
                // new Modes.Data.Skills.Compression (null),
                new Modes.Data.Skills.Actives.Steal (50, null),
                new Modes.Data.Skills.Actives.Hide (50, null, "Crate00", "Crate01", "Crate02")
            };

            public static Skill[] Virus = {
                new Modes.Data.Skills.Regeneration (null, 750, 1.25f, "TR_B"),
                new Modes.Data.Skills.Respawn (null, 10000)
            };
        }
    }
}
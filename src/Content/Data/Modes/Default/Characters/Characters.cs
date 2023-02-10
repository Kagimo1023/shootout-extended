namespace BTFS
{
    using SFDGameScriptInterface;

    namespace Modes.Characters
    {
        public class List
        {
            public static Character[] Playable = {
                new Character (true, false, false, "Brute", Data.Descriptions.Brute, Data.Profiles.Brute, CameraFocusMode.Ignore, Data.Modifiers.Brute,      Data.Weapons.Brute, Data.Skills.Brute),
                new Character (true, false, false, "Coleoptera", Data.Descriptions.Coleoptera, Data.Profiles.Coleoptera, CameraFocusMode.Ignore, Data.Modifiers.Coleoptera, Data.Weapons.Coleoptera, Data.Skills.Coleoptera),
                new Character (true, false, false, "Critman",    Data.Descriptions.Critman,    Data.Profiles.Critman, CameraFocusMode.Ignore, Data.Modifiers.Critman, Data.Weapons.Critman, Data.Skills.Critman),
                new Character (true, false, false, "Cursed",     Data.Descriptions.Cursed,     Data.Profiles.Cursed, CameraFocusMode.Ignore, Data.Modifiers.Cursed, Data.Weapons.Cursed, Data.Skills.Cursed),
                new Character (true, false, false, "Exploder",   Data.Descriptions.Exploder,   Data.Profiles.Exploder, CameraFocusMode.Ignore, Data.Modifiers.Exploder, Data.Weapons.Exploder, Data.Skills.Exploder),
                new Character (true, false, false, "Mauler",     Data.Descriptions.Mauler,     Data.Profiles.Mauler, CameraFocusMode.Ignore, Data.Modifiers.Mauler, Data.Weapons.Mauler, Data.Skills.Mauler),
                new Character (true, false, false, "Soldier",    Data.Descriptions.Soldier,    Data.Profiles.Soldier, CameraFocusMode.Ignore, Data.Modifiers.Soldier, Data.Weapons.Soldier, Data.Skills.Soldier),
                new Character (true, false, false, "Thief",      Data.Descriptions.Thief,      Data.Profiles.Thief, CameraFocusMode.Ignore, Data.Modifiers.Thief,      Data.Weapons.Thief, Data.Skills.Thief),
                new Character (true, false, false, "Virus",      Data.Descriptions.Virus,      Data.Profiles.Virus, CameraFocusMode.Ignore, Data.Modifiers.Virus,      Data.Weapons.Virus, Data.Skills.Virus)
            };

            public static Character[] NonSpawnCharacter = {
                new Character (true, false, false, "Blattodea", Data.Descriptions.Coleoptera, Data.Profiles.Coleoptera, CameraFocusMode.Ignore, Data.Modifiers.Coleoptera, Data.Weapons.Coleoptera, Data.Skills.Coleoptera),
            };
        }
    }
}

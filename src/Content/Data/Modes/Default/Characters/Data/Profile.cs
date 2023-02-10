namespace BTFS
{
    using SFDGameScriptInterface;

    namespace Modes.Characters.Data
    {
        public static class Profiles
        {
            // All profiles was generated with NearHuscarl utility https://superfighters.vercel.app/profile
            public static IProfile Coleoptera = new IProfile ()
            {
                Name = "Coleoptera",
                Gender = Gender.Male,
                Skin = new IProfileClothingItem ("Zombie", ""),
                Head = new IProfileClothingItem ("AviatorHat2", "ClothingDarkYellow", "ClothingLightYellow"),
                ChestOver = new IProfileClothingItem ("StuddedVest", "ClothingDarkYellow", "ClothingDarkYellow"),
                ChestUnder = new IProfileClothingItem ("BodyArmor", "ClothingDarkYellow"),
                Hands = new IProfileClothingItem ("SafetyGlovesBlack", "ClothingOrange"), 
                Waist = new IProfileClothingItem ("CombatBelt", "ClothingOrange"),
                Legs = new IProfileClothingItem ("TornPants", "ClothingDarkYellow"),
                Feet = new IProfileClothingItem ("RidingBootsBlack", "ClothingOrange"),
                Accesory = new IProfileClothingItem ("GasMask2", "ClothingDarkYellow", "ClothingLightYellow")
            };

            public static IProfile Brute = new IProfile ()
            {
                Name = "Brute",
                Gender = Gender.Male,
                Skin = new IProfileClothingItem("Normal", "Skin4", "ClothingLightGreen"),
                Waist = new IProfileClothingItem("CombatBelt", "ClothingRed"),
                Legs = new IProfileClothingItem("Pants", "ClothingDarkGray"),
                Feet = new IProfileClothingItem("BootsBlack", "ClothingBrown"),
                Accesory = new IProfileClothingItem("RestraintMask", "ClothingRed")
            };

            public static IProfile Critman = new IProfile ()
            {
                Name = "Critman",
                Gender = Gender.Male,
                Skin = new IProfileClothingItem ("Tattoos", "Skin3", "ClothingDarkRed"),
                ChestOver = new IProfileClothingItem ("KevlarVest", "ClothingDarkRed"),
                ChestUnder = new IProfileClothingItem ("LumberjackShirt2", "ClothingDarkGray", "ClothingDarkGray"),
                Hands = new IProfileClothingItem ("FingerlessGlovesBlack", "ClothingDarkGray"),
                Waist = new IProfileClothingItem ("Belt", "ClothingDarkGray", "ClothingDarkRed"),
                Legs = new IProfileClothingItem ("PantsBlack", "ClothingDarkGray"),
                Feet = new IProfileClothingItem ("Boots", "ClothingDarkGray"),
                Accesory = new IProfileClothingItem ("Balaclava", "ClothingDarkGray")
            };

            public static IProfile Cursed = new IProfile ()
            {
                Name = "Cursed",
                Gender = Gender.Male,
                Skin = new IProfileClothingItem ("Normal", "Skin5", "ClothingLightGreen"),
                ChestOver = new IProfileClothingItem ("VestBlack", "ClothingDarkRed", "ClothingDarkGray"),
                ChestUnder = new IProfileClothingItem ("SleevelessShirt", "ClothingDarkGray"),
                Hands = new IProfileClothingItem ("Gloves", "ClothingDarkGray"),
                Waist = new IProfileClothingItem ("AmmoBeltWaist", "ClothingRed"),
                Legs = new IProfileClothingItem ("CamoPants", "ClothingDarkGray", "ClothingGray"),
                Feet = new IProfileClothingItem("ShoesBlack", "ClothingBrown"),
                Accesory = new IProfileClothingItem ("StuddedLeatherMask", "ClothingDarkRed")
            };

            public static IProfile Exploder = new IProfile ()
            {
                Name = "Exploder",
                Gender = Gender.Male,
                Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGreen"),
                Head = new IProfileClothingItem("FLDisguise", "ClothingDarkGray", "ClothingLightGray"),
                ChestOver = new IProfileClothingItem("GrenadeBelt", ""),
                ChestUnder = new IProfileClothingItem("LumberjackShirt2", "ClothingDarkGreen", "ClothingGreen"),
                Hands = new IProfileClothingItem("Gloves", "ClothingGray"),
                Waist = new IProfileClothingItem("SatchelBelt", "ClothingGray"),
                Legs = new IProfileClothingItem("StripedPants", "ClothingBlue"),
                Feet = new IProfileClothingItem("BootsBlack", "ClothingGray"),
                Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkRed")
            };

            public static IProfile Mauler = new IProfile ()
            {
                Name = "Mauler",
                Gender = Gender.Male,
                Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGreen"),
                Head = new IProfileClothingItem("MetroLawGasMask", "ClothingDarkGray", "ClothingLightRed"),
                ChestOver = new IProfileClothingItem("Robe", "ClothingDarkGray"),
                ChestUnder = new IProfileClothingItem("StuddedLeatherSuit", "ClothingDarkGray"),
                Hands = new IProfileClothingItem("SafetyGlovesBlack", "ClothingDarkGray"),
                Legs = new IProfileClothingItem("PantsBlack", "ClothingDarkGray"),
                Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingDarkGray")
            };

            public static IProfile Soldier = new IProfile ()
            {
                Name = "Soldier",
                Gender = Gender.Male,
                Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGreen"),
                Head = new IProfileClothingItem("BaseballCap", "ClothingBlue", "ClothingDarkRed"),
                ChestOver = new IProfileClothingItem("KevlarVest", "ClothingDarkGray"),
                ChestUnder = new IProfileClothingItem("LeatherJacket", "ClothingBlue", "ClothingLightGray"),
                Hands = new IProfileClothingItem("GlovesBlack", "ClothingBrown"),
                Legs = new IProfileClothingItem("Pants", "ClothingBlue"),
                Feet = new IProfileClothingItem("BootsBlack", "ClothingBrown"),
                Accesory = new IProfileClothingItem("Balaclava", "ClothingDarkRed")
            };

            public static IProfile Thief = new IProfile ()
            {
                Name = "Thief",
                Gender = Gender.Male,
                Skin = new IProfileClothingItem("Normal", "Skin3", "ClothingLightGreen"),
                Head = new IProfileClothingItem("AviatorHat2", "ClothingYellow", "ClothingLightYellow"),
                ChestOver = new IProfileClothingItem("KevlarVest", "ClothingDarkGray"),
                ChestUnder = new IProfileClothingItem("Shirt", "ClothingGray"),
                Hands = new IProfileClothingItem("GlovesBlack", "ClothingYellow"),
                Waist = new IProfileClothingItem("Sash", "ClothingGray"),
                Legs = new IProfileClothingItem("Pants", "ClothingGray"),
                Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingYellow"),
                Accesory = new IProfileClothingItem("GasMask2", "ClothingYellow", "ClothingLightYellow")
            };

            public static IProfile Virus = new IProfile ()
            {
                Name = "Virus",
                Gender = Gender.Male,
                Skin = new IProfileClothingItem("Zombie", ""),
                ChestOver = new IProfileClothingItem("VestBlack", "ClothingDarkYellow", "ClothingDarkGray"),
                ChestUnder = new IProfileClothingItem("ShirtWithTie", "ClothingDarkYellow", "ClothingDarkRed"),
                Hands = new IProfileClothingItem("SafetyGloves", "ClothingDarkRed"),
                Waist = new IProfileClothingItem("None", "None"),
                Legs = new IProfileClothingItem("TornPants", "ClothingDarkYellow"),
                Feet = new IProfileClothingItem("Shoes", "ClothingDarkRed"),
                Accesory = new IProfileClothingItem("None", "None")
            };

            public static IProfile Blattodea = new IProfile()
            {
                Name = "Blattodea",
                Gender = Gender.Male,
                Skin = new IProfileClothingItem("Normal", "Skin1", "ClothingDarkBrown"),
                Head = new IProfileClothingItem("AviatorHat2", "ClothingDarkBrown", "ClothingLightGray"),
                ChestOver = new IProfileClothingItem("StuddedJacket", "ClothingDarkBrown", "ClothingBrown"),
                ChestUnder = new IProfileClothingItem("BodyArmor", "ClothingDarkBrown"),
                Hands = new IProfileClothingItem("SafetyGloves", "ClothingDarkBrown"),
                Waist = new IProfileClothingItem("CombatBelt", "ClothingDarkBrown"),
                Legs = new IProfileClothingItem("CamoPants", "ClothingBrown", "ClothingDarkBrown"),
                Feet = new IProfileClothingItem("RidingBootsBlack", "ClothingBrown"),
                Accesory = new IProfileClothingItem("GasMask", "ClothingBrown", "ClothingLightGray"),
            };
        }
    }
}

/* Copyright (C) 2022 Daniil Alexeev.

   This file is part of Shootout.

   Shootout is free software; you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation; either version 3, or (at your option)
   any later version.

   Shootout is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with Shootout; if not, see <https://www.gnu.org/licenses>.

   Additional permission under GNU GPL version 3 section 7

   If you modify Shootout, or any covered work, by linking or combining
   it with MythoLogic Interactive's Superfighters Deluxe, the licensors
   of this Program grant you additional permission to convey the resulting
   work.  */

namespace BTFS
{
    using SFDGameScriptInterface;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    
    namespace Modes.Data.Skills.Actives
    {
        public class EatingObjects : Active
        {
            public int EatCounter = 0;
            public int MaxEatCounter = 5;

            VirtualKey[] _key_binds =
            {
                VirtualKey.WALKING,
                VirtualKey.ATTACK,
                VirtualKey.AIM_CLIMB_DOWN
            };

            public EatingObjects(float energy_price, Callbacks cb) : base(energy_price, cb)
            {
                SetKeys(_key_binds);
                SetEnergyPrice(energy_price);
            }

            public override void TakeEnergy()
            {
                foreach (IPlayer plr in GameScriptInterface.Game.GetObjects<IPlayer>(GetOwner().GetAABB()))
                {
                    if (plr.UniqueID != GetOwner().UniqueID && plr.IsDead && plr.GetCharacter() != GetOwner().GetCharacter())
                    {
                        plr.Gib();

                        PlayerModifiers mod = GetOwner().GetModifiers();
                        mod.CurrentHealth += 25;
                        mod.CurrentEnergy -= GetEnergyPrice();
                        GetOwner().SetModifiers(mod);

                        if (EatCounter < MaxEatCounter)
                            EatCounter++;
                        else
                            EatCounter = 6; // Keep it Secret!

                        if (EatCounter < MaxEatCounter)
                            BTFS_Game.ShowCFTX(string.Format("NYAM {0}/{1}!", EatCounter, MaxEatCounter), GetOwner());
                        else
                            BTFS_Game.ShowCFTX(string.Format("???"), GetOwner()); //It's a secret!
                    }
                }
            }

            public int GetEatCounter()
            {
                return EatCounter;
            }
        }
    }
}

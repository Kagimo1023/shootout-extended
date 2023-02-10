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

    namespace Modes.Data.Skills.Actives
    {
        public class Steal : Active
        {
            VirtualKey[] _key_binds =
            {
                VirtualKey.WALKING,
                VirtualKey.ACTIVATE
            };

            public Steal(float energy_price, Callbacks cb) : base(energy_price, cb)
            {
                SetKeys(_key_binds);
                SetEnergyPrice(energy_price);
            }

            public override void TakeEnergy()
            {
                foreach (IPlayer plr in GameScriptInterface.Game.GetObjects<IPlayer>(GetOwner().GetAABB()))
                {
                    if (plr.UniqueID != GetOwner().UniqueID)
                    {
                        if (GetWeapon(plr) != WeaponItem.NONE)
                        {
                            BTFS_Game.ShowCFTX("STEAL!", GetOwner());

                            PlayerModifiers mod = GetOwner().GetModifiers();

                            mod.CurrentEnergy -= GetEnergyPrice();

                            GetOwner().SetModifiers(mod);
                        }
                    }
                }
            }

            private WeaponItem GetWeapon(IPlayer player)
            {
                WeaponItem weaponItem = WeaponItem.NONE;

                if ((player.CurrentMeleeMakeshiftWeapon).WeaponItem != WeaponItem.NONE)
                {
                    weaponItem = (player.CurrentMeleeMakeshiftWeapon).WeaponItem;
                    player.Disarm(Mapper.GetWeaponItemType(weaponItem));
                    return weaponItem;
                }

                if ((player.CurrentMeleeWeapon).WeaponItem != WeaponItem.NONE)
                {
                    weaponItem = (player.CurrentMeleeWeapon).WeaponItem;
                    player.Disarm(Mapper.GetWeaponItemType(weaponItem));
                    return weaponItem;
                }

                if ((player.CurrentPowerupItem).WeaponItem != WeaponItem.NONE)
                {
                    weaponItem = (player.CurrentPowerupItem).WeaponItem;
                    player.Disarm(Mapper.GetWeaponItemType(weaponItem));
                    return weaponItem;
                }

                if ((player.CurrentPrimaryRangedWeapon).WeaponItem != WeaponItem.NONE)
                {
                    weaponItem = (player.CurrentPrimaryRangedWeapon).WeaponItem;
                    player.Disarm(Mapper.GetWeaponItemType(weaponItem));
                    return weaponItem;
                }

                if ((player.CurrentPrimaryWeapon).WeaponItem != WeaponItem.NONE)
                {
                    weaponItem = (player.CurrentPrimaryWeapon).WeaponItem;
                    player.Disarm(Mapper.GetWeaponItemType(weaponItem));
                    return weaponItem;
                }

                if ((player.CurrentSecondaryRangedWeapon).WeaponItem != WeaponItem.NONE)
                {
                    weaponItem = (player.CurrentSecondaryRangedWeapon).WeaponItem;
                    player.Disarm(Mapper.GetWeaponItemType(weaponItem));
                    return weaponItem;
                }

                if ((player.CurrentThrownItem).WeaponItem != WeaponItem.NONE)
                {
                    weaponItem = (player.CurrentThrownItem).WeaponItem;
                    player.Disarm(Mapper.GetWeaponItemType(weaponItem));
                    return weaponItem;
                }

                return WeaponItem.NONE;
            }
        }
    }
}

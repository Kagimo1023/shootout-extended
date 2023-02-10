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
        public class DevilArmor : Active
        {
            bool k_DevilArmor = false;
            bool s_DevilArmor = false;

            VirtualKey[] _key_binds =
            {
                VirtualKey.WALKING,
                VirtualKey.BLOCK
            };

            PlayerModifiers buffer;

            public DevilArmor(float energy_price, Callbacks cb) : base(energy_price, cb)
            {
                SetKeys(_key_binds);
                SetEnergyPrice(energy_price);
            }

            void OnPlayerKeyInput(IPlayer player, VirtualKeyInfo[] keyEvents)
            {
                for (int i = 0; i < keyEvents.Length; i++)
                {
                    if (keyEvents[i].Event == VirtualKeyEvent.Pressed)
                    {
                        if (player.CurrentWeaponDrawn == WeaponItemType.Melee &&
                            (player.CurrentMeleeWeapon).WeaponItem == WeaponItem.KATANA &&
                            player.IsDead == false &&
                            player.KeyPressed(GetKeys()[0]) &&
                            player.KeyPressed(GetKeys()[1]) &&
                            GetModifiers().CurrentHealth <= 30)
                        {
                            if (k_DevilArmor == false)
                            {
                                TakeEnergy();
                                return;
                            }
                            else
                            {
                                BTFS_Game.ShowGameEffect("STM", player);
                                BTFS_Game.ShowCFTX("I CAN'T DO THIS AGAIN!", GetOwner());
                                return;
                            }
                        }
                    }
                }
            }

            public override void TakeEnergy()
            {
                if (CheckEnergy() == true && s_DevilArmor == false)
                {
                    k_DevilArmor = true;

                    IObjectEnableTrigger activate = (IObjectEnableTrigger)GameScriptInterface.Game.CreateObject("EnableTrigger");

                    buffer = GetModifiers();

                    GameScriptInterface.Game.PlayEffect("FIRE", GetOwner().GetWorldPosition(), 1, 25f, 2000f, true);
                    BTFS_Game.ShowCFTX("DEVIL ARMOR!", GetOwner());

                    PlayerModifiers mod = GetModifiers();

                    mod.CurrentEnergy -= GetEnergyPrice();

                    Events.UpdateCallback.Start((float x) =>
                    {
                        Random rnd = new Random();
                        int num = rnd.Next(0, 100);

                        GameScriptInterface.Game.PlayEffect("STM", GetOwner().GetWorldPosition(), 1, 50f, 2000f, true);
                        GameScriptInterface.Game.PlayEffect("STM", GetOwner().GetWorldPosition(), 1, 50f, 2000f, true);
                        GameScriptInterface.Game.PlayEffect("STM", GetOwner().GetWorldPosition(), 1, 50f, 2000f, true);

                        if (num > 0 && GetOwner().IsDead != true)
                        {
                            GameScriptInterface.Game.PlayEffect("CFTXT", GetOwner().GetWorldPosition(), "I'm ready die again and again. . .".ToUpper());
                            GetOwner().SetModifiers(buffer);
                            GetOwner().SetStrengthBoostTime(0);
                        }
                        else
                        {
                            if (GetOwner().IsRemoved != true)
                            {
                                GameScriptInterface.Game.PlayEffect("CFTXT", GetOwner().GetWorldPosition(), "...");
                                GetOwner().Kill();
                            }
                        }
                    }, 10000, 1);

                    mod.MeleeDamageDealtModifier += 1.5f;
                    mod.RunSpeedModifier = 1.8F;
                    mod.SprintSpeedModifier = 3F;
                    mod.ProjectileCritChanceTakenModifier = 0;
                    mod.ProjectileDamageTakenModifier = 0;
                    mod.FireDamageTakenModifier = 0;
                    mod.ImpactDamageTakenModifier = 0;
                    mod.ExplosionDamageTakenModifier = 0;
                    mod.MeleeDamageTakenModifier = 0;
                    mod.MaxEnergy *= 2;
                    mod.EnergyRechargeModifier = 40;
                    mod.MeleeStunImmunity = 0;
                    GetOwner().SetStrengthBoostTime(1000000);

                    GetOwner().SetModifiers(mod);

                    s_DevilArmor = false;

                    GetOwner().SetModifiers(mod);
                }
                else
                {
                    BTFS_Game.ShowCFTX(templateSkillStrings[0], GetOwner());
                }
            }

            public override void Activate(IPlayer player)
            {
                SetOwner(player);
                _cb.OnPlayerKeyInput.AddRoute(GetOwner(), (Action<IPlayer, VirtualKeyInfo[]>)OnPlayerKeyInput);
            }
        }
    }
}

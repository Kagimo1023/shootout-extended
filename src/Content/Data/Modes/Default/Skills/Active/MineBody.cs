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
    using System.Collections.Generic;
    using System;

    namespace Modes.Data.Skills.Actives
    {
        public class MineBody : Active
        {
            Events.PlayerKeyInputCallback ExplodeCallback = null;

            public List<IPlayer> MineBodys = new List<IPlayer>();
            private readonly int MaxMines = 3;

            VirtualKey[] MineKeys = {
                VirtualKey.WALKING,
                VirtualKey.SHEATHE,
                VirtualKey.AIM_CLIMB_DOWN
            };

            VirtualKey[] ExplodeKeys = {
                VirtualKey.WALKING,
                VirtualKey.AIM_CLIMB_DOWN,
                VirtualKey.RELOAD
            };

            public MineBody(float energy_price, Callbacks cb) : base(energy_price, cb)
            {
                SetKeys(MineKeys);
                SetEnergyPrice(energy_price);
            }

            public override void TakeEnergy()
            {
                int current_mines = GetMines().CurrentAmmo;

                foreach (IPlayer plr in GameScriptInterface.Game.GetObjects<IPlayer>(GetOwner().GetAABB()))
                {
                    if (plr.UniqueID != GetOwner().UniqueID && plr.IsDead == true)
                    {
                        if (GetMineBodys().Count != GetMaxMines() && current_mines > 0)
                        {
                            BTFS_Game.Game.PlayEffect("TR_S", plr.GetWorldPosition());
                            BTFS_Game.Game.PlaySound("", plr.GetWorldPosition());
                            
                            PlayerModifiers mod = GetOwner().GetModifiers();

                            AddToMines(plr);
                            GetOwner().SetCurrentThrownItemAmmo(current_mines--);

                            BTFS_Game.ShowCFTX("MINE " + GetMineBodys().Count + "/" + GetMaxMines(), plr);

                            mod.CurrentEnergy -= GetEnergyPrice();

                            GetOwner().SetModifiers(mod);
                        } else {
                            BTFS_Game.ShowCFTX("I CAN'T MINE ANYMORE!", plr);
                        }
                    }
                }

                ExplodeCallback = Events.PlayerKeyInputCallback.Start(ExplodeBody);
            }

            public void ExplodeBody(IPlayer player, VirtualKeyInfo[] keyEvents)
            {
                for (int i = 0; i < keyEvents.Length; i++)
                {
                    if (keyEvents[i].Event == VirtualKeyEvent.Pressed)
                    {
                        if (player.KeyPressed(ExplodeKeys[0]) && player.KeyPressed(ExplodeKeys[1]) && player.KeyPressed(ExplodeKeys[2]))
                        {
                            foreach (IPlayer plr in GetMineBodys())
                            {
                                CreateTriggerExplosion(plr.GetWorldPosition(), true);
                            }

                            GetMineBodys().Clear();
                            ExplodeCallback.Stop();
                        }
                    }
                }
            }

            public void AddToMines(IPlayer player)
            {
                MineBodys.Add(player);
            }

            public ThrownWeaponItem GetMines()
            {
                ThrownWeaponItem mines = new ThrownWeaponItem();

                if (GetOwner().CurrentThrownItem.WeaponItem == WeaponItem.MINES)
                    return mines = GetOwner().CurrentThrownItem;

                return mines;
            }

            IObjectExplosionTrigger CreateTriggerExplosion(Vector2 vector, bool enable)
            {
                IObjectExplosionTrigger exp = (IObjectExplosionTrigger)GameScriptInterface.Game.CreateObject("ExplosionTrigger", vector);

                exp.SetEnabled(enable);
                exp.SetActivateOnStartup(enable);

                return exp;
            }

            public List<IPlayer> GetMineBodys()
            {
                return MineBodys;
            }

            public int GetMaxMines()
            {
                return MaxMines;
            }
        }
    }
}


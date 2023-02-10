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
    using System.Linq;
    using System;

    namespace Modes.Data
    {
        public abstract class Active : Skill
        {
            protected readonly string[] templateSkillStrings =
            {
                "I DONT HAVE ENERGY!"
            };

            protected List<object> _conditions;
            protected float _energy_price;

            protected VirtualKey[] _keys;

            public override void Activate(IPlayer player)
            {
                SetOwner(player);
                _cb.OnPlayerKeyInput.AddRoute(player, (Action<IPlayer, VirtualKeyInfo[]>)ActivateSkill);
            }

            public Active(float EnergyPrice, Callbacks cb) : base(cb)
            {
                _energy_price = EnergyPrice;
            }

            public void SetEnergyPrice(float energy_price)
            {
                _energy_price = energy_price;
            }

            public bool CheckEnergy()
            {
                if (GetEnergyPrice() < GetCurrentEnergy())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public virtual void ActivateSkill(IPlayer player, VirtualKeyInfo[] keyEvents)
            {
                for (int i = 0; i < keyEvents.Length; i++)
                {
                    if (keyEvents[i].Event == VirtualKeyEvent.Pressed)
                    {
                        if (CheckKeysOnPressed(GetKeys(), player))
                        {
                            if (CheckEnergy())
                            {
                                TakeEnergy();
                            }
                        }
                    }
                }
            }

            public bool CheckKeysOnPressed (VirtualKey [] keys, IPlayer player)
            {
                List <bool> conditions = new List <bool> ();

                foreach (VirtualKey key in keys)
                {
                    if (player.KeyPressed(key))
                    {
                        conditions.Add (true);
                    }
                    else
                    {
                        conditions.Add (false);
                    }
                }

                foreach (bool cond in conditions)
                {
                    if (cond == false) return false; 
                }

                conditions.Clear();

                return true;
            }

            public float GetCurrentEnergy()
            {
                return GetModifiers().CurrentEnergy;
            }

            public float GetEnergyPrice()
            {
                return _energy_price;
            }

            public void SetKeys(params VirtualKey[] keys)
            {
                _keys = keys;
            }

            public VirtualKey[] GetKeys()
            {
                return _keys;
            }

            public abstract void TakeEnergy();
        }
    }
}

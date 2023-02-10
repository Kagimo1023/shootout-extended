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

    namespace Modes.Data.Skills.Actives
    {
        public class Hide : Active
        {
            bool WasHide = false;

            List<string> AcceptObjectsToHide = new List<string>();

            IObject CurrentHideInObject;

            VirtualKey[] _key_binds =
            {
                VirtualKey.WALKING,
                VirtualKey.RELOAD
            };

            public Hide(float energy_price, Callbacks cb, params string[] accept) : base(energy_price, cb)
            {
                foreach (string obj_name in accept)
                    AddToAcceptObjectList(obj_name);

                SetKeys(_key_binds);
                SetEnergyPrice(energy_price);
            }

            public void AddToAcceptObjectList(string obj_name)
            {
                AcceptObjectsToHide.Add(obj_name);
            }

            public override void TakeEnergy()
            {
                foreach (IObject obj in GameScriptInterface.Game.GetObjectsByArea(GetOwner().GetAABB()))
                {
                    foreach (string obj_name in AcceptObjectsToHide)
                    {
                        if (obj.Name == obj_name)
                        {
                            CurrentHideInObject = obj;
                            BTFS_Game.ShowCFTX("HIDE!", CurrentHideInObject);
                            SetHide(true);
                        }
                    }
                }
            }

            public void SetHide(bool hide)
            {
                WasHide = hide;

                Vector2 temp = new Vector2(CurrentHideInObject.GetWorldPosition().X, GameScriptInterface.Game.GetCameraMaxArea().TopLeft.Y + 100000);

                IObject obj = GameScriptInterface.Game.CreateObject("InvisibleBlock", temp);

                GetOwner().SetWorldPosition(new Vector2(temp.X, temp.Y + 10));
                GetOwner().SetInputMode(PlayerInputMode.ReadOnly);

                Events.PlayerKeyInputCallback.Start(HideManipulation);
                Events.ObjectDamageCallback.Start(OnObjectDamage);
                Events.ObjectTerminatedCallback.Start(OnObjectTerminated);
            }


            public void OnObjectDamage(IObject obj, ObjectDamageArgs args)
            {
                string[] RandomWords = { "OUCH!", "AHHH!!", "NOPE!", "I CAN'T HERE!" };
                Random random = new Random();
                if (obj == CurrentHideInObject && IsHide() == true)
                {
                    PlayerModifiers mod = GetOwner().GetModifiers();
                    mod.CurrentHealth -= args.Damage / 2;
                    GetOwner().SetModifiers(mod);
                    
                    if (mod.CurrentHealth < 35)
                        BTFS_Game.ShowCFTX(RandomWords[random.Next(0, RandomWords.GetLength(0))], CurrentHideInObject);

                    CurrentHideInObject.SetLinearVelocity(new Vector2 (0, 0.5f));

                    if (args.Damage > mod.CurrentHealth)
                        GetOwner().Kill();
                }
            }

            public void OnObjectTerminated(IObject[] objs)
            {
                foreach (IObject obj in objs)
                {
                    if (obj == CurrentHideInObject)
                    {
                        if (IsHide() == true)
                        {
                            WasHide = false;

                            GetOwner().SetInputMode(PlayerInputMode.Enabled);
                            GetOwner().SetWorldPosition(CurrentHideInObject.GetWorldPosition());

                            Events.UpdateCallback.Start((x) => GetOwner().SetInputEnabled(true), 0u, 1);
                        }
                    }
                }
            }

            public void HideManipulation(IPlayer player, VirtualKeyInfo[] keyEvents)
            {
                if (GetOwner() == player && IsHide() == true)
                {
                    for (int i = 0; i < keyEvents.Length; i++)
                    {
                        if (keyEvents[i].Event == VirtualKeyEvent.Pressed)
                        {
                            if (GetOwner().KeyPressed(VirtualKey.ACTIVATE))
                            {
                                WasHide = false;
                                GetOwner().SetInputMode(PlayerInputMode.Enabled);
                                GetOwner().SetWorldPosition(CurrentHideInObject.GetWorldPosition());
                            }

                            if (GetOwner().KeyPressed(VirtualKey.AIM_CLIMB_UP))
                                CurrentHideInObject.SetLinearVelocity(new Vector2(0, 3.5f));

                            if (GetOwner().KeyPressed(VirtualKey.AIM_RUN_LEFT))
                                CurrentHideInObject.SetAngle(-10);

                            if (GetOwner().KeyPressed(VirtualKey.AIM_RUN_RIGHT))
                                CurrentHideInObject.SetAngle(10);
                        }
                    }
                }
            }

            public bool IsHide()
            {
                return WasHide;
            }
        }
    }
}

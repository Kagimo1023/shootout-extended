/* Copyright (C) 2022 Daniil Alexeev

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
    using System.Threading;
    using System.Threading.Tasks;
    using System.Reflection;
    using System;

    namespace Modes.Plugins
    {
        public class Door
        {
            public Vector2 DoorPosition { get; set; }
            private List <List<IObject>> blocksGroup = new List<List<IObject>>();

            public Door ()
            {
                IObject InvisibleBlockRoof = GameScriptInterface.Game.CreateObject("InvisibleBlock", new Vector2(0, 0));
                IObject InvisibleBlockLeftBorder = GameScriptInterface.Game.CreateObject("InvisibleBlock", new Vector2 (0,0));
                IObject InvisibleBlockRightBorder = GameScriptInterface.Game.CreateObject("InvisibleBlock", new Vector2 (28,0));
                IObject InvisibleBlockFloor = GameScriptInterface.Game.CreateObject("InvisibleBlock", new Vector2 (0,-28));

                IObjectActivateTrigger ActivateTrigger = (IObjectActivateTrigger) GameScriptInterface.Game.CreateObject("ActivateTrigger", InvisibleBlockRoof.GetWorldPosition() * 2);

                InvisibleBlockRoof.SetSizeFactor(new Point(4,0));
                InvisibleBlockFloor.SetSizeFactor(new Point(4,0));
                InvisibleBlockLeftBorder.SetSizeFactor(new Point(0,4));
                InvisibleBlockRightBorder.SetSizeFactor(new Point(0,4));

                blocksGroup.Add(new List<IObject>());

                blocksGroup[0].Add(InvisibleBlockRoof);
                blocksGroup[0].Add(InvisibleBlockFloor);
                blocksGroup[0].Add(InvisibleBlockLeftBorder);
                blocksGroup[0].Add(InvisibleBlockRightBorder);
                blocksGroup[0].Add(ActivateTrigger);
            }

            public Vector2 GetPosition ()
            {
                return DoorPosition;
            }

            public void SetNameToActivateTrigger()
            {
                foreach (List <IObject> blocks in blocksGroup)
                {
                    blocks[4].CustomID = blocks[4].Name + "_" + blocksGroup.IndexOf(blocks);
                }
            }

//             public List <IObject> GetBlocksGroup(int id)
//             {
//                 return
//             }

            public IObjectActivateTrigger GetActivateTrigger()
            {
                IObjectActivateTrigger objact = null;

                foreach (List <IObject> blocks in blocksGroup)
                {
                    if (blocks[4] is IObjectActivateTrigger) return objact = (IObjectActivateTrigger) blocks[4];
                }

                return objact;
            }

            public void SetPosition (Vector2 vector2)
            {
                DoorPosition = vector2;

                foreach (List <IObject> blocks in blocksGroup)
                {
                    blocks[0].SetWorldPosition (DoorPosition);
                    blocks[1].SetWorldPosition (blocks[0].GetWorldPosition() - new Vector2(0, 40));
                    blocks[2].SetWorldPosition (blocks[0].GetWorldPosition() + new Vector2(0, -8));
                    blocks[3].SetWorldPosition (blocks[0].GetWorldPosition() + new Vector2(24, -8));
                    blocks[4].SetWorldPosition (blocks[0].GetWorldPosition() + new Vector2(12, -16));
                }
            }
        }

        public class DoorsGenerator : Plugin
        {
            private List <IObject> BGDoors = new List <IObject> ();
            private List <Vector2> DoorsPositions = new List <Vector2> ();
            private List <IObjectActivateTrigger> DoorsActivates = new List <IObjectActivateTrigger> ();
            private List <string> DoorsToDetect = new List <string> ();
            private List <Door> Doors = new List <Door> ();

            public DoorsGenerator (params string[] objs)
            {
                this.Enable = true;
                this.Name = "Doors Generator";
                SetDescription ("Generate doors on the maps");

                foreach (string str in objs)
                    DoorsToDetect.Add(str);
            }

            void DestroyDoor (int id)
            {
            }

            void InitializeDoors()
            {
                foreach (string str in DoorsToDetect)
                {
                    int c = 0;
                    foreach (IObject obj in Game.GetObjectsByName(str))
                    {
                        obj.CustomID = obj.Name + "_" + c;
                        BGDoors.Add(obj);
                        c++;
                    }
                }

                for (int i = 0; i < BGDoors.Count; i++)
                {
                    DoorsActivates.Add((IObjectActivateTrigger) Game.CreateObject("ActivateTrigger", BGDoors[i].GetWorldPosition()));
                }

//                 for (int i = 0; i < Doors.Count; i++)
//                 {
//                     Doors[i].SetPosition(new Vector2(BGDoors[i].GetWorldPosition().X, Game.GetCameraMaxArea().TopLeft.Y));
//                 }

                int count = 0;

                foreach (IObjectActivateTrigger objact in DoorsActivates)
                {
                    objact.SetEnabled(true);
                    objact.SetActivateOnStartup(true);
                    objact.SetHighlightObject(BGDoors[count]);
                    objact.CustomID = objact.Name + "_" + count;
                    count++;
                }
            }

            void GenerateDoors ()
            {
                int door_count = 0;
                int multiply = 2;

                foreach (IObject obj in BGDoors)
                {
                    Doors.Add(new Door());

                    if (door_count == 0)
                        Doors[door_count].SetPosition (Game.GetCameraMaxArea().TopLeft + new Vector2(-10000, 0));
                    else
                    {
                        Doors[door_count].SetPosition (new Vector2(Doors[0].GetPosition().X, Doors[0].GetPosition().Y * multiply + Doors[0].GetPosition().Y * door_count));
                        multiply++;
                    }

                    door_count++;
                }

                for (int i = 0; i < door_count; i++)
                {
                    Doors[i].SetNameToActivateTrigger();
                }
            }

            public void OnPlayerKeyInputHideIn (IPlayer player, VirtualKeyInfo[] keyEvents)
            {
                foreach (IObjectActivateTrigger objact in DoorsActivates)
                {
                    foreach (IObject obj in Game.GetObjectsByArea (objact.GetAABB ()))
                    {
                        if (obj is IPlayer && objact.IsEnabled)
                        {
                            player = (IPlayer) obj;

                            if (!player.IsDead)
                            {
                                for (int i = 0; i < keyEvents.Length; i ++)
                                {
                                    if (keyEvents[i].Event == VirtualKeyEvent.Pressed)
                                    {

                                        if (player.KeyPressed(VirtualKey.ACTIVATE) && !player.KeyPressed(VirtualKey.WALKING))
                                        {
                                            BTFS_Game.ShowCFTX ("HIDE", objact);
                                            BTFS_Game.ShowGameEffect("STM", objact);
                                            player.SetWorldPosition(Doors[DoorsActivates.IndexOf(objact)].GetPosition() + new Vector2(8, -8));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public void OnPlayerKeyInputHideOut (IPlayer player, VirtualKeyInfo[] keyEvents)
            {
                foreach (Door door in Doors)
                {
                    foreach (IObject obj in Game.GetObjectsByArea (door.GetActivateTrigger ().GetAABB ()))
                    {
                        if (obj is IPlayer && door.GetActivateTrigger().IsEnabled)
                        {
                            player = (IPlayer) obj;

                            if (!player.IsDead)
                            {
                                for (int i = 0; i < keyEvents.Length; i ++)
                                {
                                    if (keyEvents[i].Event == VirtualKeyEvent.Pressed)
                                    {
                                        if (player.KeyPressed(VirtualKey.ACTIVATE) && player.KeyPressed(VirtualKey.WALKING))
                                        {
                                            player.SetWorldPosition(DoorsActivates[Doors.IndexOf(door)].GetWorldPosition() + new Vector2(0, -12));
                                            BTFS_Game.SendMessageToPlayer(player.GetUser(), Color.Magenta, string.Format("You can leave from the door on {0} {1}", VirtualKey.ACTIVATE, VirtualKey.WALKING));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public override void OnStartup ()
            {
                Events.PlayerKeyInputCallback.Start(OnPlayerKeyInputHideIn);
                Events.PlayerKeyInputCallback.Start(OnPlayerKeyInputHideOut);

                InitializeDoors();
                GenerateDoors();
            }
        }
    }
}

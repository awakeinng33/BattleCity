using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;


namespace Battte_city_v._0._1
{
    class TankProcess
    {
    
        public void TankUpdate(Tank tank_obj,TankProcess tank_proc_ob,Map map_TankProc,Texture2D rocket,Rocket rocket_obj)
        {

            tank_obj.bTank.Min = new Vector3(tank_obj.tank_pos.X  , tank_obj.tank_pos.Y , 0);
            tank_obj.bTank.Max = new Vector3(tank_obj.bTank.Min.X +29.9f, tank_obj.bTank.Min.Y +29.9f, 0);
            //////Gesture
            while (TouchPanel.IsGestureAvailable)
            {

                GestureSample gesture = TouchPanel.ReadGesture();


                if (gesture.GestureType == GestureType.Tap)
                {
                    tank_obj.GetShoot(rocket,tank_obj,tank_proc_ob,rocket_obj);

                }
                else if (gesture.GestureType == GestureType.Flick)
                {

                    Single x = gesture.Delta.X, y = gesture.Delta.Y;

                    if (Math.Abs(x) > Math.Abs(y))
                    {
                        // left or right
                        if (x < 0)
                        {

                            if (((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                                tank_obj.current_direction = Direction.Left;// Налево
                            else
                            {
                                map_TankProc.count = 1;
                                tank_obj.next_direction = Direction.Left;
                            }

                        }
                        else
                        {

                            if (((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                                tank_obj.current_direction = Direction.Right; // Направо
                            else
                            {
                                map_TankProc.count = 2;
                                tank_obj.next_direction = Direction.Right;
                            }

                        }
                    }
                    else
                    {
                        // Вверх или вниз?
                        if (y < 0)
                        {

                            if (((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                                tank_obj.current_direction = Direction.Straight;// Вверх
                            else
                            {
                                map_TankProc.count = 3;
                                tank_obj.next_direction = Direction.Straight;
                            }

                        }
                        else
                        {

                            if (((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                            {
                                tank_obj.current_direction = Direction.Back;
                            }
                            else
                            {
                                map_TankProc.count = 4;

                                tank_obj.next_direction = Direction.Back;
                            }

                        }
                    }
                }
            }
        
            tank_obj.GetRotation(tank_obj);
           
            /////////////Move
            switch (tank_obj.current_direction)
            {
                case Direction.Left:


                    tank_obj.r = Direction.Left;
                    if (map_TankProc.count != 0)
                    {
                        if (map_TankProc.count == 3)
                        {
                            if (((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                            {

                                tank_obj.current_direction = tank_obj.next_direction;
                                map_TankProc.count = 0;


                            }
                            else
                                tank_obj.tank_pos.X -= tank_obj.speed;
                        }
                        if (map_TankProc.count == 4)
                        {
                            if (((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                            {

                                tank_obj.current_direction = tank_obj.next_direction;
                                map_TankProc.count = 0;

                            }
                            else
                                tank_obj.tank_pos.X -= tank_obj.speed;
                        }
                    }
                    else
                    {

                        if ((((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0))
                        {
                            if (map_TankProc.Main_Map[((int)((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) / map_TankProc.Delta), ((int)((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) / map_TankProc.Delta) - 1] == 0)
                                tank_obj.tank_pos.X -= tank_obj.speed;
                        }
                        else
                            tank_obj.tank_pos.X -= tank_obj.speed;
                    }

                    break;
                case Direction.Right:

                    tank_obj.r = Direction.Right;
                    if (map_TankProc.count != 0)
                    {
                        if (map_TankProc.count == 3)
                        {
                            if (((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                            {

                                tank_obj.current_direction = tank_obj.next_direction;
                                map_TankProc.count = 0;

                            }
                            else
                                tank_obj.tank_pos.X += tank_obj.speed;
                        }
                        if (map_TankProc.count == 4)
                        {
                            if (((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                            {

                                tank_obj.current_direction = tank_obj.next_direction;
                                map_TankProc.count = 0;

                            }
                            else
                                tank_obj.tank_pos.X += tank_obj.speed;
                        }
                    }
                    else
                    {

                        if ((((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0))
                        {
                            if (map_TankProc.Main_Map[((int)((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) / map_TankProc.Delta), ((int)((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) / map_TankProc.Delta) + 1] == 0)
                                tank_obj.tank_pos.X += tank_obj.speed;
                        }
                        else
                            tank_obj.tank_pos.X += tank_obj.speed;
                    }

                    break;
                case Direction.Straight:

                    tank_obj.r = Direction.Straight;
                    if (map_TankProc.count != 0)
                    {
                        if (map_TankProc.count == 1)
                        {
                            if (((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                            {

                                tank_obj.current_direction = tank_obj.next_direction;
                                map_TankProc.count = 0;

                            }
                            else
                                tank_obj.tank_pos.Y -= tank_obj.speed;
                        }
                        if (map_TankProc.count == 2)
                        {
                            if (((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                            {

                                tank_obj.current_direction = tank_obj.next_direction;
                                map_TankProc.count = 0;

                            }
                            else
                                tank_obj.tank_pos.Y -= tank_obj.speed;
                        }
                    }
                    else
                    {

                        if ((((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0))
                        {
                            if (map_TankProc.Main_Map[((int)((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) / map_TankProc.Delta) - 1, ((int)((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) / map_TankProc.Delta)] == 0)
                                tank_obj.tank_pos.Y -= tank_obj.speed;
                        }
                        else
                            tank_obj.tank_pos.Y -= tank_obj.speed;
                    }

                    break;
                case Direction.Back:

                    tank_obj.r = Direction.Back;
                    if (map_TankProc.count != 0)
                    {
                        if (map_TankProc.count == 1)
                        {
                            if (((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                            {

                                tank_obj.current_direction = tank_obj.next_direction;
                                map_TankProc.count = 0;

                            }
                            else
                                tank_obj.tank_pos.Y += tank_obj.speed;
                        }
                        if (map_TankProc.count == 2)
                        {
                            if (((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0)
                            {

                                tank_obj.current_direction = tank_obj.next_direction;
                                map_TankProc.count = 0;

                            }
                            else
                                tank_obj.tank_pos.Y += tank_obj.speed;
                        }
                    }
                    else
                    {
                        if ((((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) % map_TankProc.Delta == 0))
                        {
                            if (map_TankProc.Main_Map[((int)((tank_obj.tank_pos.Y- Game1.shift_y) - (float)map_TankProc.Delta / 2) / map_TankProc.Delta) + 1, ((int)((tank_obj.tank_pos.X- Game1.shift_x) - (float)map_TankProc.Delta / 2) / map_TankProc.Delta)] == 0)
                                tank_obj.tank_pos.Y += tank_obj.speed;
                        }
                        else
                            tank_obj.tank_pos.Y += tank_obj.speed;

                    }

                    break;

                default:
                    break;
            }         
        }           
    }
}

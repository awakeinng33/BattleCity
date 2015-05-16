using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;

namespace Battte_city_v._0._1
{
    class BotProcess
    {
        
        public int rand_direction;
        Random rand = new Random();
        float timer_game = 0;
        float timer_3 = 0;
        float timer_4 = 0;
        bool may_move = true;
     
        public void BotUpdate(Bot bot_obj,Map map_BotProc,GameTime gameTime)
        {
            timer_game += (float)gameTime.ElapsedGameTime.TotalSeconds;

            bot_obj.b_bot.Min = new Vector3(bot_obj.bot_pos.X, bot_obj.bot_pos.Y, 0);
            bot_obj.b_bot.Max = new Vector3(bot_obj.b_bot.Min.X + 29.9f, bot_obj.b_bot.Min.Y + 29.9f, 0);
          
            bot_obj.GetRotation(bot_obj);
            switch (bot_obj.bot_direction)
            {
                case Direction.Left:
                    bot_obj.r = Direction.Left;
                    if ((((bot_obj.bot_pos.X - Game1.shift_x)- (float)map_BotProc.Delta / 2) % map_BotProc.Delta == 0))
                    {
                        if (map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y - Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta), ((int)((bot_obj.bot_pos.X - Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) - 1] == 4 || map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y - Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta), ((int)((bot_obj.bot_pos.X - Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) - 1] == 2)
                        {
                            bot_obj.bot_direction =(Direction) rand.Next(2, 5);
                        }

                        else if (map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y- Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta), ((int)((bot_obj.bot_pos.X- Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) - 1] == 0)
                        {                        
                            rand_direction = rand.Next(0,8);
                            if ((rand_direction == 0) || (rand_direction == 1) || (rand_direction == 5) || (rand_direction == 6)|| (rand_direction == 7))
                            {
                                bot_obj.bot_pos.X -= bot_obj.speed_bot;
                            }
                            else bot_obj.bot_direction = (Direction) rand_direction;                          
                        }
                    }

                    else bot_obj.bot_pos.X -= bot_obj.speed_bot;

                    break;
                case Direction.Right:

                    bot_obj.r = Direction.Right;
                    if ((((bot_obj.bot_pos.X- Game1.shift_x) - (float)map_BotProc.Delta / 2) % map_BotProc.Delta == 0))
                    {
                        if (map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y- Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta), ((int)((bot_obj.bot_pos.X- Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) + 1] == 4 || map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y- Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta), ((int)((bot_obj.bot_pos.X- Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) + 1] == 2) 
                        {
                            rand_direction = rand.Next(1, 5);
                            if (rand_direction != 2) bot_obj.bot_direction = (Direction)rand_direction;                        
                        }

                        else if (map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y- Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta), ((int)((bot_obj.bot_pos.X- Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) + 1] == 0) 
                            bot_obj.bot_pos.X += bot_obj.speed_bot;
                    }
                    else
                        bot_obj.bot_pos.X += bot_obj.speed_bot;

                    break;
                case Direction.Straight:
                    timer_3 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    bot_obj.r = Direction.Straight;
                    if ((((bot_obj.bot_pos.Y- Game1.shift_y) - (float)map_BotProc.Delta / 2) % map_BotProc.Delta == 0))
                    {
                        if (map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y- Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) - 1, ((int)((bot_obj.bot_pos.X- Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta)] == 4 || map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y- Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) - 1, ((int)((bot_obj.bot_pos.X - Game1.shift_x)- (float)map_BotProc.Delta / 2) / map_BotProc.Delta)] == 2)
                        {
                            rand_direction = rand.Next(1, 5);
                            if (rand_direction != 3) bot_obj.bot_direction = (Direction)rand_direction;
                          
                        }

                        else if (map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y- Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) - 1, ((int)((bot_obj.bot_pos.X- Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta)] == 0)
                        {
                            if (timer_3 >= 2000)
                            {
                                bot_obj.bot_direction = (Direction) rand.Next(1, 3);
                                timer_3 = 0;
                            }
                            else bot_obj.bot_pos.Y -= bot_obj.speed_bot;
                        }
                    }
                    else
                    {                       
                            bot_obj.bot_pos.Y -= bot_obj.speed_bot;
                    }

                    break;
                case Direction.Back:
                    timer_4 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    bot_obj.r = Direction.Back;
                    if ((((bot_obj.bot_pos.Y- Game1.shift_y) - (float)map_BotProc.Delta / 2) % map_BotProc.Delta == 0))
                    {
                        if (map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y - Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) + 1, ((int)((bot_obj.bot_pos.X - Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta)] == 4 || map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y - Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) + 1, ((int)((bot_obj.bot_pos.X - Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta)] == 2)
                        {
                            bot_obj.bot_direction = (Direction)rand.Next(1, 4);

                        }

                        else if (map_BotProc.Main_Map[((int)((bot_obj.bot_pos.Y - Game1.shift_y) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta) + 1, ((int)((bot_obj.bot_pos.X - Game1.shift_x) - (float)map_BotProc.Delta / 2) / map_BotProc.Delta)] == 0)
                        {

                            if ((timer_4 >= 4000) && (may_move))
                            {
                                bot_obj.bot_direction = (Direction) rand.Next(1, 3);
                                timer_4 = 0;
                            }
                            

                            if (timer_game > 20)
                            {
                                may_move = false;
                                if ((map_BotProc.b_eagle.Max.Y - 15 - bot_obj.bot_pos.Y > 0))
                                {
                                    bot_obj.bot_pos.Y += bot_obj.speed_bot;

                                }
                                else if (map_BotProc.b_eagle.Max.Y - 15 - bot_obj.bot_pos.Y == 0)
                                {
                                    if (bot_obj.bot_pos.X > map_BotProc.b_eagle.Max.X - 15)
                                        bot_obj.bot_direction = Direction.Left;
                                    else bot_obj.bot_direction = Direction.Right;

                                }
                                else bot_obj.bot_pos.Y += bot_obj.speed_bot;
                            }
                            else bot_obj.bot_pos.Y += bot_obj.speed_bot;
                        }
                        else bot_obj.bot_pos.Y += 0;
                    }

                    else bot_obj.bot_pos.Y += bot_obj.speed_bot;                                      
                    
                    break;

                default:
                    break;

            }
           
        }
    
    }
}

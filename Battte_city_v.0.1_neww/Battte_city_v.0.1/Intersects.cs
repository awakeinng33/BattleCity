using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Battte_city_v._0._1
{
    class Intersects
    {
        public void RocketVsRocketBot(Rocket rocket_obj,RocketBot rocket_bot_obj)
        {
            foreach (var oneRocket in rocket_obj.rockets)
            {
                foreach (var oneBotRocket in rocket_bot_obj.rockets_bot)
                {
                    if (oneRocket.b_rocket.Intersects(oneBotRocket.b_bot_rocket))
                    {
                        oneRocket.is_visible = false;
                        oneBotRocket.is_visible = false;
                    }
                }
            }
        }
        public void RocketBotVsTank(Texture2D tank,Tank tank_game1_obj,RocketBot rocket_bot_obj,GameProcess game_process)
        {
            if (Game1.create_tank)
            {
                Tank new_tank = new Tank(new Vector2(225 + Game1.shift_x, 555 + Game1.shift_y));
                new_tank.tank_origin = new Vector2(tank.Width / 2, tank.Height / 2);
                new_tank.tank_appearance_vect = new Vector2(new_tank.tank_pos.X, new_tank.tank_pos.Y);
                tank_game1_obj.appeared_tank.Add(new_tank);
                Game1.create_tank = false;
            }

            foreach (var oneBotRocket in rocket_bot_obj.rockets_bot)
            {
                for (int k = 0; k < tank_game1_obj.my_tank_list.Count; k++)
                {
                    if (oneBotRocket.b_bot_rocket.Intersects(tank_game1_obj.tank_obj.bTank))
                    {
                        oneBotRocket.is_visible = false;
                        Tank.lives--;
                        tank_game1_obj.my_tank_list[k].tank_explotion_vect = tank_game1_obj.my_tank_list[k].tank_pos;
                        tank_game1_obj.exploded_tank.Add(tank_game1_obj.my_tank_list[k]);
                        tank_game1_obj.my_tank_list.RemoveAt(k);
                        if (Tank.lives <= 0)
                        {
                            game_process.LoseGame();

                        }
                    }
                }
            }
        }

        public void RocketVsBot(Texture2D bot,Bot bot_game1_obj,Rocket rocket_obj,GameProcess game_process)
        {
            if (Game1.create_bot)
            {

                Bot new_bot = new Bot(new Vector2(Game1.shift_x + 45 + (30 * new Random().Next(0, 13)), Game1.shift_y + 75), new Random().Next(1, 3), (Direction) new Random().Next(1, 5));
                new_bot.bot_origin = new Vector2(bot.Width / 2, bot.Height / 2);
                new_bot.bot_appearance_vect = new Vector2(new_bot.bot_pos.X, new_bot.bot_pos.Y);
                bot_game1_obj.appeared_bots.Add(new_bot);
                Game1.create_bot = false;

            }

            foreach (var oneRocket in rocket_obj.rockets)
            {
                for (int j = 0; j < bot_game1_obj.bots.Count; j++)
                {
                    if (oneRocket.b_rocket.Intersects(bot_game1_obj.bots[j].b_bot))
                    {
                        oneRocket.is_visible = false;
                        bot_game1_obj.bots[j].bot_explotion_vect = bot_game1_obj.bots[j].bot_pos;
                        bot_game1_obj.exploded_bots.Add(bot_game1_obj.bots[j]);
                        bot_game1_obj.bots.RemoveAt(j);
                        Bot.lives--;
                        if (Map.count_border_1 != 0)
                            Map.count_border_1--;
                        else if (Map.count_border_2 != 0)
                            Map.count_border_2--;
                        if (Bot.lives <= 0)
                        {
                            game_process.WinGame();

                        }

                    }
                }
            }
        }

        public void BotVsBotIntersects(Bot bot_obj)
        {

            for (int i = 0; i < bot_obj.bots.Count; i++)
            {
                for (int j = 0; j < bot_obj.bots.Count; j++)
                {
                    if (i != j)
                    {
                        if ((Math.Abs(bot_obj.bots[i].bot_pos.X - bot_obj.bots[j].bot_pos.X) <= 29) && (Math.Abs(bot_obj.bots[i].bot_pos.Y - bot_obj.bots[j].bot_pos.Y) <= 29))
                        {
                            if (bot_obj.bots[i].condition && bot_obj.bots[j].condition)
                            {
                                bot_obj.bots[i].condition = false;
                                bot_obj.bots[j].condition = false;

                                Direction cur_direction = bot_obj.bots[i].bot_direction;
                                bot_obj.bots[j].speed_bot = 0;
                                switch (cur_direction)
                                {
                                    case Direction.Left: bot_obj.bots[i].bot_direction = Direction.Right; break;
                                    case Direction.Right: bot_obj.bots[i].bot_direction = Direction.Left; break;
                                    case Direction.Straight: bot_obj.bots[i].bot_direction = Direction.Back; break;
                                    case Direction.Back: bot_obj.bots[i].bot_direction = Direction.Straight; break;
                                    default: break;

                                }

                                Direction cur_direction2 = bot_obj.bots[j].bot_direction;
                                switch (cur_direction2)
                                {
                                    case Direction.Left: bot_obj.bots[j].bot_direction = Direction.Right; break;
                                    case Direction.Right: bot_obj.bots[j].bot_direction = Direction.Left; break;
                                    case Direction.Straight: bot_obj.bots[j].bot_direction = Direction.Back; break;
                                    case Direction.Back: bot_obj.bots[j].bot_direction = Direction.Straight; break;
                                    default: break;
                                }


                            }

                        }
                        else
                        {
                            bot_obj.bots[i].condition = true;
                            bot_obj.bots[j].condition = true;
                            if (bot_obj.bots[j].speed_bot == 0)
                                bot_obj.bots[j].speed_bot = 1;
                            if (bot_obj.bots[i].speed_bot == 0)
                                bot_obj.bots[i].speed_bot = 1;

                        }

                    }
                }
            }
        }
    }
}

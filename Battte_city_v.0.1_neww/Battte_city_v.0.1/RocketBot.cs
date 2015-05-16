using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Battte_city_v._0._1
{
    class RocketBot:IRocket
    {
        public Vector2 rocket_bot_position;
        public Vector2 rocket_bot_origin;
        public Vector2 rocket_bot_velocity = new Vector2(0, 0);
        public bool is_visible = false ;
        public BoundingBox b_bot_rocket;
        public List<RocketBot> rockets_bot = new List<RocketBot>();
        BotProcess bot_proc_ob;
        public void RocketIntersects(Map map_obj,GameProcess game_process)
        {
            foreach (var oneRocket in rockets_bot)
            {
                oneRocket.rocket_bot_position += oneRocket.rocket_bot_velocity;
                oneRocket.b_bot_rocket.Min += new Vector3(oneRocket.rocket_bot_velocity.X, oneRocket.rocket_bot_velocity.Y, 0);
                oneRocket.b_bot_rocket.Max = new Vector3(oneRocket.b_bot_rocket.Min.X + 5, oneRocket.b_bot_rocket.Min.Y + 5, 0);
                for (int i = 0; i < 79; i++)
                {
                    if (i < 71 && oneRocket.b_bot_rocket.Intersects(map_obj.b_map[i]))
                    {
                        if (map_obj.Main_Map[(int)(map_obj.b_map[i].Min.Y - Game1.shift_y) / map_obj.Delta, (int)(map_obj.b_map[i].Min.X - Game1.shift_x) / map_obj.Delta] != 0)
                        {
                            map_obj.Main_Map[(int)(map_obj.b_map[i].Min.Y - Game1.shift_y) / map_obj.Delta, (int)(map_obj.b_map[i].Min.X - Game1.shift_x) / map_obj.Delta] = 0;
                            oneRocket.is_visible = false;
                        }
                    }

                    if (oneRocket.b_bot_rocket.Intersects(map_obj.b_map_2[i]))
                    {

                        oneRocket.is_visible = false;
                    }
                    if (i < 20 && oneRocket.b_bot_rocket.Intersects(map_obj.b_map_3[i]))
                    {

                        oneRocket.is_visible = false;
                    }
                    if (oneRocket.b_bot_rocket.Intersects(map_obj.b_eagle))
                    {
                        map_obj.Main_Map[(int)(map_obj.b_eagle.Min.Y - Game1.shift_y) / map_obj.Delta, (int)(map_obj.b_eagle.Min.X - Game1.shift_x) / map_obj.Delta] = 0;
                        oneRocket.is_visible = false;
                        game_process.LoseGame();
                    }
                }

            }

            for (int i = 0; i < rockets_bot.Count; i++)
            {
                if (!rockets_bot[i].is_visible)
                {
                    rockets_bot.RemoveAt(i);
                    i--;
                }
            }

        }

        public void GetImage(Texture2D rocket_bot,Object bot_proc_obj, SpriteBatch spriteBatch)
        {
            bot_proc_ob = bot_proc_obj as BotProcess;
            foreach (var oneRocket in rockets_bot) //ракеты бота
            {
                spriteBatch.Draw(rocket_bot, oneRocket.rocket_bot_position, null, Color.White, 0f, oneRocket.rocket_bot_origin, 1f, SpriteEffects.None, 0);
            }
        }

    }
}

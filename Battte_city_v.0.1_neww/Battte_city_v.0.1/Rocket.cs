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
    class Rocket:IRocket
    {
        public Vector2 rocket_position;
        public Vector2 rocket_origin;
        public Vector2 rocket_velocity = new Vector2(0, 0);
        public bool is_visible = false;
        public BoundingBox b_rocket;
        public List<Rocket> rockets = new List<Rocket>();
        TankProcess tank_proc_ob;
        public void RocketIntersects(Map map_obj,GameProcess game_process)
        {
            foreach (Rocket oneRocket in rockets)
            {
                oneRocket.rocket_position += oneRocket.rocket_velocity;
                oneRocket.b_rocket.Min += new Vector3(oneRocket.rocket_velocity.X, oneRocket.rocket_velocity.Y, 0);
                oneRocket.b_rocket.Max = new Vector3(oneRocket.b_rocket.Min.X + 5, oneRocket.b_rocket.Min.Y + 5, 0);


                for (int i = 0; i < 79; i++)
                {
                    if (i < 73 && oneRocket.b_rocket.Intersects(map_obj.b_map[i]))
                    {
                        if (map_obj.Main_Map[(int)(map_obj.b_map[i].Min.Y - Game1.shift_y) / map_obj.Delta, (int)(map_obj.b_map[i].Min.X - Game1.shift_x) / map_obj.Delta] != 0)
                        {
                            map_obj.Main_Map[(int)((map_obj.b_map[i].Min.Y - Game1.shift_y)) / map_obj.Delta, (int)((map_obj.b_map[i].Min.X - Game1.shift_x)) / map_obj.Delta] = 0;
                            oneRocket.is_visible = false;
                        }
                    }

                    if (oneRocket.b_rocket.Intersects(map_obj.b_map_2[i]))
                    {

                        oneRocket.is_visible = false;
                    }
                    if (i < 20 && oneRocket.b_rocket.Intersects(map_obj.b_map_3[i]))
                    {

                        oneRocket.is_visible = false;
                    }
                    if (oneRocket.b_rocket.Intersects(map_obj.b_eagle))
                    {
                        map_obj.Main_Map[(int)(map_obj.b_eagle.Min.Y - Game1.shift_y) / map_obj.Delta, (int)(map_obj.b_eagle.Min.X - Game1.shift_x) / map_obj.Delta] = 0;
                        oneRocket.is_visible = false;
                        game_process.LoseGame();
                    }
                }

            }
            for (int i = 0; i < rockets.Count; i++)
            {
                if (!rockets[i].is_visible)
                {
                    rockets.RemoveAt(i);
                    i--;
                }
            }

        }
        public void GetImage(Texture2D rocket,Object tank_proc_obj, SpriteBatch spriteBatch)
        {
            tank_proc_ob = tank_proc_obj as TankProcess;
            foreach (var oneRocket in rockets)
            {
                spriteBatch.Draw(rocket, oneRocket.rocket_position, null, Color.White, 0f, oneRocket.rocket_origin, 1f, SpriteEffects.None, 0);
            }
        }
    }
}

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
    class Tank:ITank,IAnimation
    {
        public Vector2 tank_pos;
        public Vector2 tank_origin;
        public Vector2 tank_appearance_vect;
        public Vector2 tank_explotion_vect;
        public  Tank tank_obj;
        Vector2 velocity = new Vector2(1, 1);
        public Direction next_direction;
        public Direction current_direction;
        public Direction r = Direction.Straight;
        public int speed = 1;
        public float rotation;
        public static int lives = 3;
        public BoundingBox bTank;
        public static int factor = 1;
        public List<Tank> my_tank_list = new List<Tank>();
        public List<Tank> exploded_tank = new List<Tank>();
        public List<Tank> appeared_tank = new List<Tank>();
        TankProcess tank_proc_obj;
        Rocket rocket_ob;
        public float timer_tank_appearance;
        public const int FRAME_WIDTH_2 = 63;
        public const int FRAME_HEIGHT_2 = 57;
        public const int FRAME_WIDTH = 30;
        public const int FRAME_HEIGHT = 30;
        public int current_frame;
        public int current_frame_2;
        public Vector2 origin_position;
        public Vector2 origin_position_2;
        public Rectangle rectangle;
        public Rectangle rectangle_2;
        public int pull_update;


        public Tank(Vector2 tank_pos)
        {
            this.tank_pos = tank_pos;
        }
        public void GetObject(Texture2D tank)
        {
            tank_obj = new Tank(new Vector2(Game1.shift_x + 225f, Game1.shift_y + 555f));
            tank_obj.tank_origin = new Vector2(tank.Width / 2, tank.Height / 2);
            my_tank_list.Add(tank_obj);
        }       
        public  void GetShoot(Texture2D rocket, Object tank_ob,Object tank_proc_ob,Object new_rocket_obj)
        {
            tank_obj = tank_ob as Tank;
            tank_proc_obj = tank_proc_ob as TankProcess;
            rocket_ob = new_rocket_obj as Rocket;
            Rocket rocket_obj = new Rocket();
            rocket_obj.rocket_origin = new Vector2(rocket.Width / 2, rocket.Height / 2);
            rocket_obj.rocket_velocity += new Vector2((float)Math.Sin(tank_obj.rotation) * 5f * factor, (float)Math.Cos(tank_obj.rotation + Math.PI) * 5f * factor);
            rocket_obj.rocket_position += tank_obj.tank_pos + rocket_obj.rocket_velocity;
            rocket_obj.b_rocket.Min = new Vector3(tank_obj.tank_pos.X + rocket_obj.rocket_velocity.X, tank_obj.tank_pos.Y + rocket_obj.rocket_velocity.Y, 0);
            rocket_obj.b_rocket.Max = new Vector3(tank_obj.tank_pos.X + rocket_obj.rocket_velocity.X + 5, tank_obj.tank_pos.Y + rocket_obj.rocket_velocity.Y + 5, 0);
            rocket_obj.is_visible = true;
            rocket_ob.rockets.Add(rocket_obj);
        }

        public  void GetRotation(Object tank_ob)
        {
            tank_obj = tank_ob as Tank;
            if (((tank_obj.r == Direction.Left) && (tank_obj.current_direction == Direction.Right)) || ((tank_obj.r == Direction.Right) && (tank_obj.current_direction == Direction.Left)) || ((tank_obj.r == Direction.Straight) && (tank_obj.current_direction == Direction.Back)) || ((tank_obj.r == Direction.Back) && (tank_obj.current_direction == Direction.Straight)))
            {
                tank_obj.rotation += (float)Math.PI;
            }
            else if (((tank_obj.r == Direction.Left) && (tank_obj.current_direction == Direction.Back)) || ((tank_obj.r == Direction.Straight) && (tank_obj.current_direction == Direction.Left)) || ((tank_obj.r == Direction.Back) && (tank_obj.current_direction == Direction.Right)) || ((tank_obj.r == Direction.Right) && (tank_obj.current_direction == Direction.Straight)))
            {
                tank_obj.rotation += ((float)Math.PI / 2) * 3;
            }
            else if (((tank_obj.r == Direction.Straight) && (tank_obj.current_direction == Direction.Right)) || ((tank_obj.r == Direction.Right) && (tank_obj.current_direction == Direction.Back)) || ((tank_obj.r == Direction.Back) && (tank_obj.current_direction == Direction.Left)) || ((tank_obj.r == Direction.Left) && (tank_obj.current_direction == Direction.Straight)))
            {
                tank_obj.rotation += (float)Math.PI / 2;
            }
        }

        public void GetImage(Texture2D tank, SpriteBatch spriteBatch,Object tank_ob)
        {
            tank_obj = tank_ob as Tank;
            spriteBatch.Draw(tank, tank_obj.tank_pos, null, Color.White, tank_obj.rotation, tank_obj.tank_origin, 1f, SpriteEffects.None, 0);
           
        }

        public void UpdateAppearanceAnimation(GameTime gameTime)
        {
            rectangle = new Rectangle(current_frame * FRAME_WIDTH, 0, FRAME_WIDTH, FRAME_HEIGHT);
            origin_position = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            current_frame++;
            if (current_frame > 15) current_frame = 0;

        }
        public void UpdateExplotionAnimation(GameTime gameTime)
        {
            rectangle_2 = new Rectangle(current_frame_2 * FRAME_WIDTH_2, 0, FRAME_WIDTH_2, FRAME_HEIGHT_2);
            origin_position_2 = new Vector2(rectangle_2.Width / 2, rectangle_2.Height / 2);
            current_frame_2++;
            if (current_frame_2 > 15) current_frame_2 = 0;
        }
        public void AppearanceAnimation(GameTime gameTime)
        {

            for (int i = 0; i < appeared_tank.Count; i++)
            {
                appeared_tank[i].UpdateAppearanceAnimation(gameTime);
                appeared_tank[i].timer_tank_appearance += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (appeared_tank[i].timer_tank_appearance > 2000)
                {
                    my_tank_list.Add(appeared_tank[i]);
                    appeared_tank.RemoveAt(i);
                }

            }

        }
        public void ExplotionAnimation(GameTime gameTime)
        {
            for (int j = 0; j < exploded_tank.Count; j++)
            {
                if (pull_update == 0) pull_update = 0;
                else pull_update--;
                if (pull_update == 0)
                {
                    Game1.counter_2++;
                    exploded_tank[j].UpdateExplotionAnimation(gameTime);
                    pull_update = 3;
                    if (Game1.counter_2 == 16)
                    {
                        exploded_tank.RemoveAt(j);
                        Game1.create_tank = true;
                        Game1.counter_2 = 0;
                    }
                }
            }
        }       
    }
}


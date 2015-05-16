using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Battte_city_v._0._1
{
    class Bot:ITank,IAnimation
    {
        public Vector2 bot_pos;
        public BoundingBox b_bot;
        public Vector2 bot_origin;
        public int speed_bot;
        public Direction bot_direction;
        public Direction r = Direction.Straight;
        public float bot_rotation = 0;
        public int count = 0;
        public int sub = 0;
        public bool condition = true;
        public static int lives = 20;
        public Vector2 bot_explotion_vect;
        public Vector2 bot_appearance_vect;
        public float timer_bot_appearance;
        public List<Bot> bots = new List<Bot>();
        public List<Bot> exploded_bots = new List<Bot>();
        public List<Bot> appeared_bots = new List<Bot>();
        Random rand = new Random();
        Bot bot_obj;
        BotProcess bot_proc_obj;
        RocketBot rocket_bot_ob;     
        public const int FRAME_WIDTH_2 = 63;
        public const int FRAME_HEIGHT_2 = 57;
        public const  int FRAME_WIDTH = 30;
        public const int FRAME_HEIGHT = 30;
        public int current_frame;
        public int current_frame_2;
        public Vector2 origin_position;
        public Vector2 origin_position_2;
        public Rectangle rectangle;
        public Rectangle rectangle_2;
        public int pull_update;
        public int counter;


        public Bot(Vector2 position, int speed, Direction direction)
        {
            bot_pos.X = position.X;
            bot_pos.Y = position.Y;
            speed_bot = speed;
            bot_direction = direction;
        }

        public void GetObject(Texture2D bot)
        {
            for (int k = 0; k < 3; k++)
            {
                bot_obj = new Bot(new Vector2(Game1.shift_x + 45 + (30 * rand.Next(0, 13)), Game1.shift_y + 75), rand.Next(1,3), (Direction)rand.Next(1, 5));
                bot_obj.bot_origin = new Vector2(bot.Width / 2, bot.Height / 2);
                bots.Add(bot_obj);
            }
        }
           public  void GetShoot(Texture2D rocket_bot, Object bot_ob,Object bot_proc_ob,Object new_rocket_bot)
           {
               bot_obj = bot_ob as Bot;
               bot_proc_obj = bot_proc_ob as BotProcess;
               rocket_bot_ob = new_rocket_bot as RocketBot;
               RocketBot rocket_bot_obj = new RocketBot();
               rocket_bot_obj.rocket_bot_origin = new Vector2(rocket_bot.Width / 2, rocket_bot.Height / 2);
               rocket_bot_obj.rocket_bot_velocity += new Vector2((float)Math.Sin(bot_obj.bot_rotation) * 5f, (float)Math.Cos(bot_obj.bot_rotation + Math.PI) * 5f);
               rocket_bot_obj.rocket_bot_position += bot_obj.bot_pos + rocket_bot_obj.rocket_bot_velocity;
               rocket_bot_obj.b_bot_rocket.Min = new Vector3(bot_obj.bot_pos.X + rocket_bot_obj.rocket_bot_velocity.X, bot_obj.bot_pos.Y + rocket_bot_obj.rocket_bot_velocity.Y, 0);
               rocket_bot_obj.b_bot_rocket.Max = new Vector3(bot_obj.bot_pos.X + rocket_bot_obj.rocket_bot_velocity.X + 5, bot_obj.bot_pos.Y + rocket_bot_obj.rocket_bot_velocity.Y + 5, 0);
               rocket_bot_obj.is_visible = true;
               rocket_bot_ob.rockets_bot.Add(rocket_bot_obj);
           }

           public void GetRotation(Object bot_ob)
           {
               bot_obj = bot_ob as Bot;
               if (((bot_obj.r == Direction.Left) && (bot_obj.bot_direction == Direction.Right)) || ((bot_obj.r == Direction.Right) && (bot_obj.bot_direction == Direction.Left)) || ((bot_obj.r == Direction.Straight) && (bot_obj.bot_direction == Direction.Back)) || ((bot_obj.r == Direction.Back) && (bot_obj.bot_direction == Direction.Straight)))
               {
                   bot_obj.bot_rotation += (float)Math.PI;
               }
               else if (((bot_obj.r == Direction.Left) && (bot_obj.bot_direction == Direction.Back)) || ((bot_obj.r == Direction.Straight) && (bot_obj.bot_direction == Direction.Left)) || ((bot_obj.r == Direction.Back) && (bot_obj.bot_direction == Direction.Right)) || ((bot_obj.r == Direction.Right) && (bot_obj.bot_direction == Direction.Straight)))
               {
                   bot_obj.bot_rotation += ((float)Math.PI / 2) * 3;
               }
               else if (((bot_obj.r == Direction.Straight) && (bot_obj.bot_direction == Direction.Right)) || ((bot_obj.r == Direction.Right) && (bot_obj.bot_direction == Direction.Back)) || ((bot_obj.r == Direction.Back) && (bot_obj.bot_direction == Direction.Left)) || ((bot_obj.r == Direction.Left) && (bot_obj.bot_direction == Direction.Straight)))
               {
                   bot_obj.bot_rotation += (float)Math.PI / 2;
               }
              
           }

           public void GetImage(Texture2D bot,SpriteBatch spriteBatch,Object bot_ob)
           {
               bot_obj = bot_ob as Bot;
               spriteBatch.Draw(bot, bot_obj.bot_pos, null, Color.White, bot_obj.bot_rotation, bot_obj.bot_origin, 1f, SpriteEffects.None, 0);                            
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
               for (int i = 0; i < appeared_bots.Count; i++)
               {            
                       appeared_bots[i].UpdateAppearanceAnimation(gameTime);
                       appeared_bots[i].timer_bot_appearance += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                       if (appeared_bots[i].timer_bot_appearance > 2000)
                       {
                           bots.Add(appeared_bots[i]);
                           appeared_bots.RemoveAt(i);
                       }
                  
               }

           }
           public void ExplotionAnimation(GameTime gameTime)
           {
               for (int j = 0; j < exploded_bots.Count; j++)
               {
                   if (exploded_bots[j].pull_update == 0) exploded_bots[j].pull_update = 0;
                   else exploded_bots[j].pull_update--;
                   if (exploded_bots[j].pull_update == 0)
                   {
                       exploded_bots[j].counter++;
                       exploded_bots[j].UpdateExplotionAnimation(gameTime);
                       exploded_bots[j].pull_update = 3;
                       if (exploded_bots[j].counter == 16)
                       {
                               exploded_bots.RemoveAt(j);
                               Game1.create_bot = true;
                       }
                   }
               }
           }
    }
}

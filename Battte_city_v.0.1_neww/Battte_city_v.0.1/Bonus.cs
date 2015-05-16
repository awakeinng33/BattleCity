using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Battte_city_v._0._1
{
  class Bonus
    {
      public List<Star> star_list = new List<Star>();
      public List<Defend> defend_list = new List<Defend>();
      public List<Live> live_list = new List<Live>();
      public static Random rand = new Random();
      public float timer_star = 0;
      float timer_star_catch = 0;
      public float timer_defend = 0;
      public float timer_live = 0;
      float timer2_defend = 0;
      public static bool bonus_defend_on = false;
      public int[] buffer = new int[6];
      public bool star_bonus_catch = false;
      public bool defend_bonus_catch = false;
      public bool live_bonus_catch = false;
      public int star_catch = 0;
    

     public class Star
        {     
            public bool star_bonus_visible = false;
            public BoundingBox b_star = new BoundingBox();
            public Vector2 star_bonus_pos;
            public Star()
            {
                star_bonus_pos = new Vector2(30 * rand.Next(1, 14) + Game1.shift_x, 30 * rand.Next(2, 19) + Game1.shift_y);

            }
      }
     
   public  class Defend
        {
         
         public bool defend_bonus_visible = false;
         public BoundingBox b_defend = new BoundingBox();
         public Vector2 defend_bonus_pos;

         public Defend()
         {
             defend_bonus_pos = new Vector2(30 * rand.Next(1, 14) + Game1.shift_x, 30 * rand.Next(2, 19) + Game1.shift_y);
         }
       
         }
     public  class Live
        {
            
            public bool live_bonus_visible = false;
            public BoundingBox b_live = new BoundingBox();
            public Vector2 live_bonus_pos;

            public Live()
            {
                live_bonus_pos = new Vector2(30 * rand.Next(1, 14) + Game1.shift_x, 30 * rand.Next(2, 19) + Game1.shift_y);
            }
        }

     public void BonusCheck(GameTime gameTime,Map map_obj)
     {
         timer_star += (float)gameTime.ElapsedGameTime.TotalSeconds;
         timer_defend += (float)gameTime.ElapsedGameTime.TotalSeconds;
         timer_live += (float)gameTime.ElapsedGameTime.TotalSeconds;
         if (bonus_defend_on)
         {
             timer2_defend += (float)gameTime.ElapsedGameTime.TotalSeconds;

         }
         if (timer2_defend > 10 && bonus_defend_on)
         {
             bonus_defend_on = false;

             map_obj.Main_Map[(int)(600 - Game1.shift_y) / map_obj.Delta, (int)(210 - Game1.shift_x) / map_obj.Delta] = (byte)buffer[0];
             map_obj.Main_Map[(int)(630 - Game1.shift_y) / map_obj.Delta, (int)(210 - Game1.shift_x) / map_obj.Delta] = (byte)buffer[1];
             map_obj.Main_Map[(int)(660 - Game1.shift_y) / map_obj.Delta, (int)(210 - Game1.shift_x) / map_obj.Delta] = (byte)buffer[2];
             map_obj.Main_Map[(int)(600 - Game1.shift_y) / map_obj.Delta, (int)(270 - Game1.shift_x) / map_obj.Delta] = (byte)buffer[3];
             map_obj.Main_Map[(int)(630 - Game1.shift_y) / map_obj.Delta, (int)(270 - Game1.shift_x) / map_obj.Delta] = (byte)buffer[4];
             map_obj.Main_Map[(int)(660 - Game1.shift_y) / map_obj.Delta, (int)(270 - Game1.shift_x) / map_obj.Delta] = (byte)buffer[5];


         }

         if (star_catch == 1) timer_star_catch += (float)gameTime.ElapsedGameTime.TotalSeconds;
         if (timer_star_catch > 10)
         {
             star_catch = 0;
             timer_star_catch = 0;
             timer_star = 0;
             Tank.factor = 1;
         }

         if ((timer_star > 3) && (!star_bonus_catch))
         {
             Bonus.Star star_obj = new Bonus.Star();
             star_obj.star_bonus_visible = true;
             star_bonus_catch = true;
             timer_star = 0;
             star_list.Add(star_obj);
         }

         if (((timer_defend - timer2_defend) > 3) && (!defend_bonus_catch))
         {
             Bonus.Defend defend_obj = new Bonus.Defend();
             defend_obj.defend_bonus_visible = true;
             defend_bonus_catch = true;
             timer_defend = 0;
             timer2_defend = 0;
             defend_list.Add(defend_obj);
         }

         if ((timer_live > 5) && (!live_bonus_catch))
         {
             Bonus.Live live_obj = new Bonus.Live();
             live_obj.live_bonus_visible = true;
             live_bonus_catch = true;
             timer_live = 0;
             live_list.Add(live_obj);
         }
     }
     public void Bonus_Intersects(Bonus bonus_obj,Map map_obj,Tank tank_obj)
     {
         for (int i = 0; i < bonus_obj.star_list.Count; i++)
         {
             bonus_obj.star_list[i].b_star.Min = new Vector3(bonus_obj.star_list[i].star_bonus_pos.X, bonus_obj.star_list[i].star_bonus_pos.Y, 0);
             bonus_obj.star_list[i].b_star.Max = new Vector3(bonus_obj.star_list[i].star_bonus_pos.X + map_obj.Delta, bonus_obj.star_list[i].star_bonus_pos.Y + map_obj.Delta, 0);
             if (tank_obj.bTank.Intersects(bonus_obj.star_list[i].b_star))
             {
                 star_catch = 1;
                 Tank.factor = 2;
                 bonus_obj.star_list.RemoveAt(i);
                 star_bonus_catch = false;
                 timer_star = 0;
             }
         }
         for (int j = 0; j < bonus_obj.defend_list.Count; j++)
         {
             bonus_obj.defend_list[j].b_defend.Min = new Vector3(bonus_obj.defend_list[j].defend_bonus_pos.X, bonus_obj.defend_list[j].defend_bonus_pos.Y, 0);
             bonus_obj.defend_list[j].b_defend.Max = new Vector3(bonus_obj.defend_list[j].defend_bonus_pos.X + map_obj.Delta, bonus_obj.defend_list[j].defend_bonus_pos.Y + map_obj.Delta, 0);
             if (tank_obj.tank_obj.bTank.Intersects(bonus_obj.defend_list[j].b_defend))
             {
                 bonus_obj.defend_list.RemoveAt(j);

                 bonus_defend_on = true;

                 buffer[0] = map_obj.Main_Map[(int)(600 - Game1.shift_y) / map_obj.Delta, (int)(210 - Game1.shift_x) / map_obj.Delta];
                 buffer[1] = map_obj.Main_Map[(int)(630 - Game1.shift_y) / map_obj.Delta, (int)(210 - Game1.shift_x) / map_obj.Delta];
                 buffer[2] = map_obj.Main_Map[(int)(660 - Game1.shift_y) / map_obj.Delta, (int)(210 - Game1.shift_x) / map_obj.Delta];
                 buffer[3] = map_obj.Main_Map[(int)(600 - Game1.shift_y) / map_obj.Delta, (int)(270 - Game1.shift_x) / map_obj.Delta];
                 buffer[4] = map_obj.Main_Map[(int)(630 - Game1.shift_y) / map_obj.Delta, (int)(270 - Game1.shift_x) / map_obj.Delta];
                 buffer[5] = map_obj.Main_Map[(int)(660 - Game1.shift_y) / map_obj.Delta, (int)(270 - Game1.shift_x) / map_obj.Delta];

                 map_obj.Main_Map[(int)(600 - Game1.shift_y) / map_obj.Delta, (int)(210 - Game1.shift_x) / map_obj.Delta] = 2;
                 map_obj.Main_Map[(int)(630 - Game1.shift_y) / map_obj.Delta, (int)(210 - Game1.shift_x) / map_obj.Delta] = 2;
                 map_obj.Main_Map[(int)(660 - Game1.shift_y) / map_obj.Delta, (int)(210 - Game1.shift_x) / map_obj.Delta] = 2;
                 map_obj.Main_Map[(int)(600 - Game1.shift_y) / map_obj.Delta, (int)(270 - Game1.shift_x) / map_obj.Delta] = 2;
                 map_obj.Main_Map[(int)(630 - Game1.shift_y) / map_obj.Delta, (int)(270 - Game1.shift_x) / map_obj.Delta] = 2;
                 map_obj.Main_Map[(int)(660 - Game1.shift_y) / map_obj.Delta, (int)(270 - Game1.shift_x) / map_obj.Delta] = 2;

                 defend_bonus_catch = false;
                 timer_defend = 0;
             }

         }
         for (int k = 0; k < bonus_obj.live_list.Count; k++)
         {
             bonus_obj.live_list[k].b_live.Min = new Vector3(bonus_obj.live_list[k].live_bonus_pos.X, bonus_obj.live_list[k].live_bonus_pos.Y, 0);
             bonus_obj.live_list[k].b_live.Max = new Vector3(bonus_obj.live_list[k].live_bonus_pos.X + map_obj.Delta, bonus_obj.live_list[k].live_bonus_pos.Y + map_obj.Delta, 0);
             if (tank_obj.bTank.Intersects(bonus_obj.live_list[k].b_live))
             {
                 Tank.lives++;
                 bonus_obj.live_list.RemoveAt(k);
                 live_bonus_catch = false;
                 timer_live = 0;
             }
         }
     }

     public void Draw(Bonus bonus_obj,Texture2D star_bonus,Texture2D defend_bonus,Texture2D live_bonus,SpriteBatch spriteBatch)
     {
         for (int i = 0; i < bonus_obj.star_list.Count; i++)
         {
             if (bonus_obj.star_list[i].star_bonus_visible)
             {
                 spriteBatch.Draw(star_bonus, bonus_obj.star_list[i].star_bonus_pos, Color.White);
             }
         }
         for (int j = 0; j < bonus_obj.defend_list.Count; j++)
         {
             if (bonus_obj.defend_list[j].defend_bonus_visible)
             {
                 spriteBatch.Draw(defend_bonus, bonus_obj.defend_list[j].defend_bonus_pos, Color.White);
             }
         }
         for (int k = 0; k < bonus_obj.live_list.Count; k++)
         {
             if (bonus_obj.live_list[k].live_bonus_visible)
             {
                 spriteBatch.Draw(live_bonus, bonus_obj.live_list[k].live_bonus_pos, Color.White);
             }
         }

     }
  
  }
}

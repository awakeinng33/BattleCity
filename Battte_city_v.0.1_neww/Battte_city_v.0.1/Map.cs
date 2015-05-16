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
 public  class Map
    {
        public int Delta { get; set; }
        public int Map_Width { get; set; }
        public int Map_Height { get; set; }
        public Vector2 vect = new Vector2(30, 30);
        public Vector2 vect_map2;
        public Vector2 vect_border;
        public Vector2 vect_life;
        public Vector2 vect_life_count;
        public int count = 0;
        byte[,] main_map = { { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 },{ 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 }, { 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4 }, { 4, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 4 }, { 4, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 4 }, { 4, 0, 1, 0, 1, 0, 1, 2, 1, 0, 1, 0, 1, 0, 4 }, { 4, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 4 }, { 4, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 4 }, { 4, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 4 }, { 4, 2, 0, 1, 1, 0, 0, 0, 0, 0, 1, 1, 0, 2, 4 }, { 4, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 4 }, { 4, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 4 }, { 4, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 4 }, { 4, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 4 }, { 4, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 4 }, { 4, 0, 1, 0, 1, 0, 1, 2, 1, 0, 1, 0, 1, 0, 4 }, { 4, 0, 0, 0, 1, 0, 1, 3, 1, 0, 1, 0, 0, 0, 4 }, { 4, 0, 0, 0, 0, 0, 1, 2, 1, 0, 0, 0, 0, 0, 4 }, { 4, 1, 0, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 1, 4 }, { 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4 } };
        public BoundingBox[] b_map = new BoundingBox[73];
        public int count_wall = 0;
        public BoundingBox[] b_map_2 = new BoundingBox[79];
        public int count_wall_2 = 0;
        public BoundingBox[] b_map_3 = new BoundingBox[20];
        public int count_wall_3 = 0;
        public static int count_border_1 = 10;
        public static int count_border_2 = 10;
        public BoundingBox b_eagle = new BoundingBox();

        public Map(int delta,int map_width,int map_height)
        {
            Delta = delta;
            Map_Width = map_width;
            Map_Height = map_height;

        }       
        public byte[,] Main_Map
        {
            get
            {
                return main_map;
            }

        }
        public void DrawMap(Texture2D Tmap, Texture2D Tstone, Texture2D Teagle, Texture2D Tborder, Texture2D Ttank_live, Texture2D Tlife_picture, Texture2D Tnumber0, Texture2D Tnumber1, Texture2D Tnumber2, Texture2D Tnumber3, Texture2D Tnumber4, Texture2D Tnumber5, Texture2D Tnumber6, Texture2D Tnumber7, Texture2D Tnumber8, Texture2D Tnumber9, SpriteBatch spriteBatch)
        {

                for (int i = 0; i < Map_Height; i++)
                {
                    for (int j = 0; j < Map_Width; j++)
                    {
                        //
                        if (Bonus.bonus_defend_on)
                        {
                            vect_map2.X = Game1.shift_x + Delta * j;
                            vect_map2.Y = Game1.shift_y + Delta * i;
                            if ((vect_map2.Y == (Game1.shift_y + 17 * Delta)) && (vect_map2.X == (Game1.shift_x + 6 * Delta)) || (vect_map2.Y == (Game1.shift_y + 16 * Delta)) && (vect_map2.X == (Game1.shift_x + 6 * Delta)) || (vect_map2.Y == (Game1.shift_y + 15 * Delta)) && (vect_map2.X == (Game1.shift_x + 6 * Delta)) || (vect_map2.Y == (Game1.shift_y + 17 * Delta)) && (vect_map2.X == (Game1.shift_x + 8 * Delta)) || (vect_map2.Y == (Game1.shift_y + 16 * Delta)) && (vect_map2.X == (Game1.shift_x + 8 * Delta)) || (vect_map2.Y == (Game1.shift_y + 15 * Delta)) && (vect_map2.X == (Game1.shift_x + 8 * Delta)))
                            {

                                b_map[count_wall].Min = new Vector3(0, 0, 0);
                                b_map[count_wall].Max = new Vector3(0, 0, 0);
                                count_wall++;
                            }
                        }
                        else
                        {
                            vect_map2.X = Game1.shift_x + Delta * j;
                            vect_map2.Y = Game1.shift_y + Delta * i;
                            if ((vect_map2.Y == (Game1.shift_y + 17 * Delta)) && (vect_map2.X == (Game1.shift_x + 6 * Delta)) || (vect_map2.Y == (Game1.shift_y + 16 * Delta)) && (vect_map2.X == (Game1.shift_x + 6 * Delta)) || (vect_map2.Y == (Game1.shift_y + 15 * Delta)) && (vect_map2.X == (Game1.shift_x + 6 * Delta)) || (vect_map2.Y == (Game1.shift_y + 17 * Delta)) && (vect_map2.X == (Game1.shift_x + 8 * Delta)) || (vect_map2.Y == (Game1.shift_y + 16 * Delta)) && (vect_map2.X == (Game1.shift_x + 8 * Delta)) || (vect_map2.Y == (Game1.shift_y + 15 * Delta)) && (vect_map2.X == (Game1.shift_x + 8 * Delta)))
                            {

                                b_map_3[count_wall_3].Min = new Vector3(0, 0, 0);
                                b_map_3[count_wall_3].Max = new Vector3(0, 0, 0);
                                count_wall_3++;
                            }
                        }
                        if (main_map[i, j] == 1)
                        {
                            vect_map2.X = Game1.shift_x + Delta * j;
                            vect_map2.Y = Game1.shift_y + Delta * i;
                            b_map[count_wall].Min = new Vector3(vect_map2.X, vect_map2.Y, 0);
                            b_map[count_wall].Max = new Vector3(vect_map2.X + Delta, vect_map2.Y + Delta, 0);
                            count_wall++;
                            spriteBatch.Draw(Tmap, vect_map2, Color.White);
                        }
                        if (main_map[i, j] == 4)
                        {
                            vect_map2.X = Game1.shift_x + Delta * j;
                            vect_map2.Y = Game1.shift_y + Delta * i;

                            b_map_2[count_wall_2].Min = new Vector3(vect_map2.X, vect_map2.Y, 0);
                            b_map_2[count_wall_2].Max = new Vector3(vect_map2.X + Delta, vect_map2.Y + Delta, 0);
                            spriteBatch.Draw(Tborder, vect_map2, Color.White);
                            count_wall_2++;

                        }
                        if (main_map[i, j] == 2)
                        {
                            vect_map2.X = Game1.shift_x + Delta * j;
                            vect_map2.Y = Game1.shift_y + Delta * i;

                            b_map_3[count_wall_3].Min = new Vector3(vect_map2.X, vect_map2.Y, 0);
                            b_map_3[count_wall_3].Max = new Vector3(vect_map2.X + Delta, vect_map2.Y + Delta, 0);
                            count_wall_3++;
                            spriteBatch.Draw(Tstone, vect_map2, Color.White);

                        }
                        if (main_map[i, j] == 3)
                        {
                            vect_map2.X = Game1.shift_x + Delta * j;
                            vect_map2.Y = Game1.shift_y + Delta * i;

                            b_eagle.Min = new Vector3(vect_map2.X, vect_map2.Y, 0);
                            b_eagle.Max = new Vector3(vect_map2.X + Delta, vect_map2.Y + Delta, 0);

                            spriteBatch.Draw(Teagle, vect_map2, Color.White);

                        }
                    }

                }
            count_wall = 0;
            count_wall_2 = 0;
            count_wall_3 = 0;

            for (int i = 0; i < count_border_1; i++)
            {
                if(i ==0)
                vect_border.X = Game1.shift_x + Delta + Tborder.Width * i;
                else
                    vect_border.X = Game1.shift_x + Delta + Tborder.Width * i-10*i;
                vect_border.Y = Game1.shift_y + Delta;
                spriteBatch.Draw(Ttank_live, vect_border, Color.White);
            }

            for (int i = 0; i < count_border_2; i++)
            {
                if (i == 0)
                    vect_border.X = Game1.shift_x + Delta + Tborder.Width * i;
                else
                    vect_border.X = Game1.shift_x + Delta + Tborder.Width * i - 10 * i;
                vect_border.Y = Game1.shift_y + 10;
                spriteBatch.Draw(Ttank_live, vect_border, Color.White);
            }
            vect_life.X = Game1.shift_x + 11 * Delta;
            vect_life.Y = Game1.shift_y + 5;
            spriteBatch.Draw(Tlife_picture, vect_life, Color.White);
            vect_life_count.X = Game1.shift_x + 11 * Delta + 30;
            vect_life_count.Y = Game1.shift_y + 30;
            switch (Tank.lives)
            {
                case 0:
                    spriteBatch.Draw(Tnumber0, vect_life_count, Color.White);
                    break;
                case 1:
                    spriteBatch.Draw(Tnumber1, vect_life_count, Color.White);
                    break;
                case 2:
                    spriteBatch.Draw(Tnumber2, vect_life_count, Color.White);
                    break;
                case 3:
                    spriteBatch.Draw(Tnumber3, vect_life_count, Color.White);
                    break;
                case 4:
                    spriteBatch.Draw(Tnumber4, vect_life_count, Color.White);
                    break;
                case 5:
                    spriteBatch.Draw(Tnumber5, vect_life_count, Color.White);
                    break;
                case 6:
                    spriteBatch.Draw(Tnumber6, vect_life_count, Color.White);
                    break;
                case 7:
                    spriteBatch.Draw(Tnumber7, vect_life_count, Color.White);
                    break;
                case 8:
                    spriteBatch.Draw(Tnumber8, vect_life_count, Color.White);
                    break;
                case 9:
                    spriteBatch.Draw(Tnumber9, vect_life_count, Color.White);
                    break;
                default:
                    break;
            }
        }
    }
}
           



   


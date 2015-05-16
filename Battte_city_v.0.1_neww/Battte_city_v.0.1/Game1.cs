using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Threading;

public enum Direction { Left = 1, Right, Straight, Back }

namespace Battte_city_v._0._1
{
    public interface ITank
    {
        void GetObject(Texture2D texture);
        void GetRotation(Object ob);
        void GetShoot(Texture2D texture, Object ob, Object proc_ob, Object rocket_ob);
        void GetImage(Texture2D texture, SpriteBatch spriteBatch,Object ob);

    }
    public interface IRocket
    {
        void RocketIntersects(Map map_obj, GameProcess game_proc);
        void GetImage(Texture2D texture, Object proc_ob, SpriteBatch spriteBatch);
    }
    public interface IAnimation
    {
        void UpdateAppearanceAnimation(GameTime gameTime);
        void UpdateExplotionAnimation(GameTime gameTime);
        void ExplotionAnimation(GameTime gameTime);
        void AppearanceAnimation(GameTime gameTime);

    }
    public class Game1 : Game
    {
        GraphicsDeviceManager Graphics;
        SpriteBatch spriteBatch;
        TankProcess tank_proc_obj;
        Map map_obj;
        Bonus bonus_obj;
        RocketBot rocket_bot_obj;
        Rocket rocket_obj;
        Tank tank_game1_obj;
        Bot bot_game1_obj;
        Intersects intersects;

        float[,] timer_botbot = new float[,] { { 1501, 1501, 1501 }, { 1501, 1501, 1501 }, { 1501, 1501, 1501 } };

        BotProcess bot_proc_obj;
        GameProcess game_process;
        Texture2D bot;
        Texture2D rocket_bot;
        Texture2D tank;
        Texture2D rocket;
        Texture2D map;
        Texture2D eagle;
        Texture2D stone;
        Texture2D border;
        Texture2D sprite_explotion_texture;
        Texture2D sprite_appearance_texture;
        Texture2D game_over_texture;
        Texture2D star_bonus;
        Texture2D defend_bonus;
        Texture2D live_bonus;
        Texture2D tank_live;
        Texture2D button_newgame;
        Texture2D life_picture;
        Texture2D number0;
        Texture2D number1;
        Texture2D number2;
        Texture2D number3;
        Texture2D number4;
        Texture2D number5;
        Texture2D number6;
        Texture2D number7;
        Texture2D number8;
        Texture2D number9;
        Texture2D number0w;
        Texture2D number1w;
        Texture2D number2w;
        Texture2D number3w;
        Texture2D number4w;
        Texture2D number5w;
        Texture2D number6w;
        Texture2D number7w;
        Texture2D number8w;
        Texture2D number9w;
        Texture2D scoreText;
        Texture2D battle_city;
        Vector2 Tbattle_city;
        Vector2 Tbutton_newgame;

        int current_width;
        int current_heigth;
        float timer_shoot_bot_1 = 0;
        float timer_shoot_bot_2 = 0;
        float timer_shoot_bot_3 = 0;
       
        static public bool create_bot = false;
        static public bool create_tank = false;
        public static int counter = 0;
        public static int counter_2 = 0;

        Random rand = new Random();

        int width_device;
        int height_device;
        static public float shift_x;
        static public float shift_y;
        float shift_button_ng_x;
        float shift_button_ng_y;
        float shift_gameover_x;
        float shift_gameover_y;
        int count_shift;


        public static bool end_animation = true;
        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Graphics.IsFullScreen = true;
            current_width = Graphics.PreferredBackBufferWidth;
            current_heigth = Graphics.PreferredBackBufferHeight;


            TouchPanel.EnabledGestures = GestureType.Flick | GestureType.Tap;
        }


        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);
            sprite_explotion_texture = Content.Load<Texture2D>("взрыв6357");
            sprite_appearance_texture=Content.Load<Texture2D>("вспышка16(30 на 480)");
            width_device = GraphicsDevice.Viewport.Width;
            height_device = GraphicsDevice.Viewport.Height;
            count_shift = height_device;
            tank = Content.Load<Texture2D>("Танчик30(2)");
            border = Content.Load<Texture2D>("gray30");
            tank_live = Content.Load<Texture2D>("tankblack15");
            life_picture = Content.Load<Texture2D>("50высота");
            battle_city = Content.Load<Texture2D>("battle");
            number0 = Content.Load<Texture2D>("0");
            number1 = Content.Load<Texture2D>("1");
            number2 = Content.Load<Texture2D>("2");
            number3 = Content.Load<Texture2D>("3");
            number4 = Content.Load<Texture2D>("4");
            number5 = Content.Load<Texture2D>("5");
            number6 = Content.Load<Texture2D>("6");
            number7 = Content.Load<Texture2D>("7");
            number8 = Content.Load<Texture2D>("8");
            number9 = Content.Load<Texture2D>("9");
            scoreText = Content.Load<Texture2D>("score");
            number0w = Content.Load<Texture2D>("0w");
            number1w = Content.Load<Texture2D>("1w");
            number2w = Content.Load<Texture2D>("2w");
            number3w = Content.Load<Texture2D>("3w");
            number4w = Content.Load<Texture2D>("4w");
            number5w = Content.Load<Texture2D>("5w");
            number6w = Content.Load<Texture2D>("6w");
            number7w = Content.Load<Texture2D>("7w");
            number8w = Content.Load<Texture2D>("8w");
            number9w = Content.Load<Texture2D>("9w");
            map_obj = new Map(30, 15, 20);
            shift_x = (width_device - map_obj.Map_Width * map_obj.Delta) / 2;
            shift_y = (height_device - map_obj.Map_Height * map_obj.Delta) * 2 / 3;

            //бонусы
            star_bonus = Content.Load<Texture2D>("star");
            defend_bonus = Content.Load<Texture2D>("defend");
            live_bonus = Content.Load<Texture2D>("live");
            // 
            button_newgame = Content.Load<Texture2D>("button1");
            map = Content.Load<Texture2D>("стена30");
            stone = Content.Load<Texture2D>("бетон30");
            bot = Content.Load<Texture2D>("танквраг30");
            rocket = Content.Load<Texture2D>("rocket");
            eagle = Content.Load<Texture2D>("орел30");
            rocket_bot = Content.Load<Texture2D>("rocket_bot");
            tank_proc_obj = new TankProcess();
            game_process = new GameProcess();
            intersects = new Intersects();
            bonus_obj = new Bonus();
            rocket_obj = new Rocket();
            rocket_bot_obj = new RocketBot();
            tank_game1_obj = new Tank(new Vector2(0, 0));
            bot_game1_obj = new Bot(new Vector2(0, 0), 0, 0);
            tank_game1_obj.GetObject(tank);
            bot_proc_obj = new BotProcess();
            bot_game1_obj.GetObject(bot);
            game_over_texture = Content.Load<Texture2D>("gameover2");
            shift_gameover_x = (width_device - game_over_texture.Width) / 2;
            shift_gameover_y = height_device * 1 / 3;
            shift_button_ng_x = (width_device - button_newgame.Width) / 2;
            shift_button_ng_y = height_device * 1 / 2 ;
            Tbattle_city = new Vector2();
            Tbattle_city.X = (width_device - battle_city.Width) / 2;
            Tbattle_city.Y = height_device/8;
            Tbutton_newgame = new Vector2(shift_button_ng_x, shift_button_ng_y);

        }

        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            if (game_process.is_game)
            {
                while (TouchPanel.IsGestureAvailable)
                {

                    GestureSample gesture = TouchPanel.ReadGesture();


                    if (gesture.GestureType == GestureType.Tap)
                    {
                        if ((gesture.Position.X >= shift_button_ng_x && gesture.Position.X <= (shift_button_ng_x + button_newgame.Width)) && (gesture.Position.Y >= shift_button_ng_y && gesture.Position.Y <= (shift_button_ng_y + button_newgame.Height)))
                        {
                            game_process.NewGame();
                        }

                    }

                }
            }
            else
            {
               
                ///////////////вызываем апдейт анимации при взрыве 
                bot_game1_obj.ExplotionAnimation(gameTime);
                tank_game1_obj.ExplotionAnimation(gameTime);
                ///////////////////
                ///////////////анимация появления 
                bot_game1_obj.AppearanceAnimation(gameTime);
                tank_game1_obj.AppearanceAnimation(gameTime);
                ///////////////////////////////////////          
                tank_proc_obj.TankUpdate(tank_game1_obj.tank_obj, tank_proc_obj, map_obj, rocket, rocket_obj);    
                rocket_obj.RocketIntersects(map_obj, game_process);

                foreach (var oneBot in bot_game1_obj.bots)
                {
                    bot_proc_obj.BotUpdate(oneBot, map_obj, gameTime);
                }

                ///////////////////частота выстрелов 
                timer_shoot_bot_1 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                timer_shoot_bot_2 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                timer_shoot_bot_3 += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if ((timer_shoot_bot_1 >= 500) && (bot_game1_obj.bots.Count > 0))
                {
                    bot_game1_obj.GetShoot(rocket_bot, bot_game1_obj.bots[0], bot_proc_obj, rocket_bot_obj);
                    timer_shoot_bot_1 = 0;
                }
                if ((timer_shoot_bot_2 >= 1000) && (bot_game1_obj.bots.Count > 1))
                {
                    bot_game1_obj.GetShoot(rocket_bot, bot_game1_obj.bots[1], bot_proc_obj, rocket_bot_obj);
                    timer_shoot_bot_2 = 0;
                }
                if ((timer_shoot_bot_3 >= 1500) && (bot_game1_obj.bots.Count > 2))
                {
                    bot_game1_obj.GetShoot(rocket_bot, bot_game1_obj.bots[2], bot_proc_obj, rocket_bot_obj);
                    timer_shoot_bot_3 = 0;
                }

                /////////////////////////////////////////        
                bonus_obj.BonusCheck(gameTime, map_obj);
                bonus_obj.Bonus_Intersects(bonus_obj, map_obj, tank_game1_obj.tank_obj);
                intersects.RocketVsBot(bot,bot_game1_obj,rocket_obj,game_process);
                intersects.RocketBotVsTank(tank,tank_game1_obj,rocket_bot_obj,game_process);
                intersects.RocketVsRocketBot(rocket_obj,rocket_bot_obj);
                rocket_bot_obj.RocketIntersects(map_obj, game_process);
            //    intersects.BotVsBotIntersects(bot_game1_obj);

            }
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            if (game_process.is_game)
            {
                spriteBatch.Draw(battle_city,Tbattle_city,Color.White);
                spriteBatch.Draw(button_newgame, Tbutton_newgame, Color.White);
            }
            else if (game_process.is_lose)
            {
                if (count_shift > shift_gameover_y)
                {
                    spriteBatch.Draw(game_over_texture, new Vector2(shift_gameover_x, count_shift), Color.White);
                    count_shift -= 15;
                }
                else
                {
                    spriteBatch.Draw(game_over_texture, new Vector2(shift_gameover_x, count_shift), Color.White);
                    spriteBatch.Draw(scoreText, new Vector2((width_device - scoreText.Width) / 2, count_shift + game_over_texture.Height + 50), Color.White);
                    int score = (20 - Bot.lives) * 100;
                    string score_text = score.ToString();
                    float x = (width_device - score_text.Length * number0w.Width - (score_text.Length-1)*20) / 2;
                    float y = count_shift + game_over_texture.Height + scoreText.Height + 90;
                    foreach (char c in score_text)
                    {
                        switch (c)
                        {
                            case '0':
                                spriteBatch.Draw(number0w, new Vector2(x, y), Color.White);
                                x += 50;
                                break;
                            case '1':
                                spriteBatch.Draw(number1w, new Vector2(x, y), Color.White);
                                x += 50;
                                break;
                            case '2':
                                spriteBatch.Draw(number2w, new Vector2(x, y), Color.White);
                                x += 50;
                                break;
                            case '3':
                                spriteBatch.Draw(number3w, new Vector2(x, y), Color.White);
                                x += 50;
                                break;
                            case '4':
                                spriteBatch.Draw(number4w, new Vector2(x, y), Color.White);
                                x += 50;
                                break;
                            case '5':
                                spriteBatch.Draw(number5w, new Vector2(x, y), Color.White);
                                x += 50;
                                break;
                            case '6':
                                spriteBatch.Draw(number6w, new Vector2(x, y), Color.White);
                                x += 50;
                                break;
                            case '7':
                                spriteBatch.Draw(number7w, new Vector2(x, y), Color.White);
                                x += 50;
                                break;
                            case '8':
                                spriteBatch.Draw(number8w, new Vector2(x, y), Color.White);
                                x += 50;
                                break;
                            case '9':
                                spriteBatch.Draw(number9w, new Vector2(x, y), Color.White);
                                x += 50;
                                break;


                        }

                    }
                }
            }
            else
            {
                map_obj.DrawMap(map, stone, eagle, border, tank_live, life_picture, number0, number1, number2, number3, number4, number5, number6, number7, number8, number9, spriteBatch);
                ////////// прорисовка танка  

                foreach (var oneTank in tank_game1_obj.my_tank_list)
                {
                    tank_game1_obj.GetImage(tank, spriteBatch, oneTank);
                }
                foreach (var oneTank in tank_game1_obj.exploded_tank)
                {
                    spriteBatch.Draw(sprite_explotion_texture, oneTank.tank_explotion_vect, oneTank.rectangle_2, Color.White, 0f, oneTank.origin_position_2, 1f, SpriteEffects.None, 0);
                }
                foreach (var oneTank in tank_game1_obj.appeared_tank)
                {
                    spriteBatch.Draw(sprite_appearance_texture, oneTank.tank_appearance_vect, oneTank.rectangle, Color.White, 0f, oneTank.origin_position, 1f, SpriteEffects.None, 0);
                }

                 //////////прорисовка бота 

                foreach (var oneBot in bot_game1_obj.bots)
                {
                    bot_game1_obj.GetImage(bot, spriteBatch, oneBot);
                }
                foreach (var oneBot in bot_game1_obj.exploded_bots)
                {
                    spriteBatch.Draw(sprite_explotion_texture, oneBot.bot_explotion_vect, oneBot.rectangle_2, Color.White, 0f, oneBot.origin_position_2, 1f, SpriteEffects.None, 0);
                }
                foreach (var oneBot in bot_game1_obj.appeared_bots)
                {              
                    spriteBatch.Draw(sprite_appearance_texture, oneBot.bot_appearance_vect, oneBot.rectangle, Color.White, 0f, oneBot.origin_position, 1f, SpriteEffects.None, 0);                  
                }
        
                rocket_obj.GetImage(rocket, tank_proc_obj, spriteBatch); /////ракеты танка 
                rocket_bot_obj.GetImage(rocket_bot, bot_proc_obj, spriteBatch); ///// ракеты бота 
                bonus_obj.Draw(bonus_obj, star_bonus, defend_bonus, live_bonus, spriteBatch);//////прорисовка бонусов 

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

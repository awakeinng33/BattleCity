using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battte_city_v._0._1
{
   public class GameProcess
    {
        public bool is_win = false;
        public bool is_lose = false;
        public bool is_game = true;
        public int score = 0;

        public void WinGame()
        {
            is_win = true;
            is_lose = false;
            is_game = false;
        }

        public void LoseGame()
        {
            is_win = false;
            is_lose = true;
            is_game = false;
        }
        public void NewGame()
        {
            is_win = false;
            is_lose = false;
            is_game = false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcaby
{
    class Player
    {
        public string name, time_string;
        public int time;
    }
    class Ranking
    {
        private string username;
        private int minutes, seconds;
        Player player = new Player();

        public Ranking()
        {
            this.username = "default";
            this.minutes = 0;
            this.seconds = 0;
        }
        public void GetUsername(string username)
        {
            this.username = username;
            player.name = username;
        }

        public void SaveResults(string username, string time)   // Save results into ...\bin\Debug\Data\ranking.txt
        {
            string filename = "ranking.txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", filename);
            string data = username + " " + time + Environment.NewLine;


            if (!File.Exists(path))   // If file doesn't exist, create it
            {
                File.WriteAllText(path, data);
            }

            File.AppendAllText(path, data);
        }

        public string ReadResults()
        {
            string filename = "ranking.txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", filename);
            string readrank = File.ReadAllText(path);

            return readrank;
        }

        public void SaveToList(string readrank)
        {
            Player newplayer = new Player();
            List<Player> playerlist = new List<Player>();
            string value = readrank;
            string[] substrings = value.Split(' ');
            for (int i = 0; i < substrings.Length; i++)     // Don't know if and how works
            {
                if(i == 0 || i % 2 == 0)
                {
                    // NAME
                    newplayer.name = substrings[i];
                }
                else
                {
                    // times
                    newplayer.time_string = substrings[i];
                    newplayer.time = ConvertStringToThousands(substrings[i]);
                }
                playerlist.Add(newplayer);
            }
            Console.WriteLine(playerlist.Count());
            playerlist.Sort();
            foreach (var substring in substrings)
                Console.WriteLine(substring);
        }

        public int ConvertStringToThousands(string time)
        {
            int value = 0;
            string tmp, tmp1;
            var chars = time.ToCharArray();
            tmp = Convert.ToString(chars[0]);
            tmp1 = Convert.ToString(chars[1]);
            tmp =tmp + tmp1;
            tmp1 = Convert.ToString(chars[3]);
            tmp = tmp + tmp1;
            tmp1 = Convert.ToString(chars[4]);
            tmp = tmp + tmp1;
            value = Convert.ToInt32(tmp);
            return value;
        }
    }
}

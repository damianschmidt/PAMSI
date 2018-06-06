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
        public string name;
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

        public int ConvertTime(string time)
        {
            string minutes, seconds;
            string[] newtime = time.Split(':'); // Separate by :
            if (newtime.Length != 2)
            {
                minutes = newtime[0];
                seconds = newtime[1];
                int x = Int32.Parse(minutes);   // Convert to int, didn't check if works correctly
                int y = Int32.Parse(seconds);
                this.minutes = x;               // Temporary names x => minutes
                this.seconds = y;               // Temporary names y => seconds
                return y;
            }
            else
            {
                return 0;
            }
        }

        public void SaveResults()   // Save results into ...\bin\Debug\Data\ranking.txt
        {
            string filename = "ranking.txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", filename);
            List<string> PlayerList = new List<string>();
            PlayerList.Add(username);


            if(File.Exists(path))   // Deleting file if exists, temporary
            {
                File.Delete(path);
            }

            using (FileStream fs = File.Create(path, 1024))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes("Default text in file");
                fs.Write(info, 0, info.Length);
            }
        }

    }
}

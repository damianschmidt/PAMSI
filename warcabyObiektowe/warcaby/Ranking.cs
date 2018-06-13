using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace warcaby
{
    class Ranking
    {
        public string username { get; set; }   // Username as string
        public string time_str { get; set; }   // Time as string
        public int time_int { get; set; }      // Time converted to int
        private MainWindow mainWindow;
        public Ranking(MainWindow mainWindow)
        {
            username = "default";
            time_int = 0;
            time_str = "0";
            this.mainWindow = mainWindow;
        }

        public Ranking(string name, string timestr, int timeint)
        {
            username = name;
            time_str = timestr;
            time_int = timeint;
        }
        public void GetUsername(string username)
        {
            this.username = username;
        }

        public void SaveResults(string username, string time)   // Save results into ...\bin\Debug\Data\ranking.txt
        {
            string filename = "ranking.txt";
            string path = Path.Combine(Environment.CurrentDirectory, filename);
            string data = username + ";" + time + ";";
            string data2 = "default;99:99;";


            if (!File.Exists(path))   // If file doesn't exist, create it
            {

                for (var i = 0; i < 15; i++)
                    File.AppendAllText(path, data2);
            }

            File.AppendAllText(path, data);
        }

        public string ReadResults()
        {
            string filename = "ranking.txt";
            string path = Path.Combine(Environment.CurrentDirectory, filename);
            string readrank = File.ReadAllText(path);
            return readrank;
        }

        public void Sort(string readrank, string current_username, string current_time)
        {
            Ranking newranking = new Ranking(mainWindow);
            string value = readrank;
            string[] substrings = value.Split(';');
            var i = substrings.Length;
            Ranking[] times = { new Ranking(username = substrings[i-21], time_str = substrings[i-20], time_int = ConvertStringToThousands(substrings[i-20])),
                                new Ranking(username = substrings[i-19], time_str = substrings[i-18], time_int = ConvertStringToThousands(substrings[i-18])),
                                new Ranking(username = substrings[i-17], time_str = substrings[i-16], time_int = ConvertStringToThousands(substrings[i-16])),
                                new Ranking(username = substrings[i-15], time_str = substrings[i-14], time_int = ConvertStringToThousands(substrings[i-14])),
                                new Ranking(username = substrings[i-13], time_str = substrings[i-12], time_int = ConvertStringToThousands(substrings[i-12])),
                                new Ranking(username = substrings[i-11], time_str = substrings[i-10], time_int = ConvertStringToThousands(substrings[i-10])),
                                new Ranking(username = substrings[i-9], time_str = substrings[i-8], time_int = ConvertStringToThousands(substrings[i-8])),
                                new Ranking(username = substrings[i-7], time_str = substrings[i-6], time_int = ConvertStringToThousands(substrings[i-6])),
                                new Ranking(username = substrings[i-5], time_str = substrings[i-4], time_int = ConvertStringToThousands(substrings[i-4])),
                                new Ranking(username = substrings[i-3], time_str = substrings[i-2], time_int = ConvertStringToThousands(substrings[i-2])) };
            IEnumerable<Ranking> query = times.OrderBy(time => time.time_int);
            foreach (Ranking rank in query)
                Console.WriteLine(rank.username + " " + rank.time_int);

            string filename = "sorted_ranking.txt";
            string path = Path.Combine(Environment.CurrentDirectory, filename);

            if (File.Exists(path))
            {
                File.Delete(path);
                File.WriteAllText(path, "RANKING;");
                foreach (Ranking rank in query)
                {
                    string data = rank.username + ";" + rank.time_str + ";";
                    File.AppendAllText(path, data);
                }
            }
            query = null;
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

        public string ReadRanking()
        {
            string filename = "sorted_ranking.txt";
            string path = Path.Combine(Environment.CurrentDirectory, filename);
            string data = "default;99:99;";
            if (!File.Exists(path))
            {
                File.WriteAllText(path, "RANKING;");
                for(var i = 0; i < 15; i++)
                    File.AppendAllText(path, data);
            }
            string readrank = File.ReadAllText(path);
            return readrank;
        }

        public void ShowRank(string readrank)
        {
            string value = readrank;
            string[] substring = value.Split(';');
            ((Label)this.mainWindow.FindName("username1")).Content = substring[1];
            ((Label)this.mainWindow.FindName("username1_time")).Content = substring[2];
            ((Label)this.mainWindow.FindName("username2")).Content = substring[3];
            ((Label)this.mainWindow.FindName("username2_time")).Content = substring[4];
            ((Label)this.mainWindow.FindName("username3")).Content = substring[5];
            ((Label)this.mainWindow.FindName("username3_time")).Content = substring[6];
            ((Label)this.mainWindow.FindName("username4")).Content = substring[7];
            ((Label)this.mainWindow.FindName("username4_time")).Content = substring[8];
            ((Label)this.mainWindow.FindName("username5")).Content = substring[9];
            ((Label)this.mainWindow.FindName("username5_time")).Content = substring[10];
            ((Label)this.mainWindow.FindName("username6")).Content = substring[11];
            ((Label)this.mainWindow.FindName("username6_time")).Content = substring[12];
            ((Label)this.mainWindow.FindName("username7")).Content = substring[13];
            ((Label)this.mainWindow.FindName("username7_time")).Content = substring[14];
            ((Label)this.mainWindow.FindName("username8")).Content = substring[15];
            ((Label)this.mainWindow.FindName("username8_time")).Content = substring[16];
            ((Label)this.mainWindow.FindName("username9")).Content = substring[17];
            ((Label)this.mainWindow.FindName("username9_time")).Content = substring[18];
            ((Label)this.mainWindow.FindName("username10")).Content = substring[19];
            ((Label)this.mainWindow.FindName("username10_time")).Content = substring[20];
        }
    }
}

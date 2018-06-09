using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warcaby
{
    class Ranking
    {
        public string username { get; set; }   // Username as string
        public string time_str { get; set; }   // Time as string
        public int time_int { get; set; }      // Time converted to int

        public Ranking()
        {
            this.username = "default";
            this.time_int = 0;
            this.time_str = "0";
        }

        public Ranking(string name, string timestr, int timeint)
        {
            this.username = name;
            this.time_str = timestr;
            this.time_int = timeint;
        }
        public void GetUsername(string username)
        {
            this.username = username;
        }

        public void SaveResults(string username, string time)   // Save results into ...\bin\Debug\Data\ranking.txt
        {
            string filename = "ranking.txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", filename);
            string data = username + ";" + time + ";" + Environment.NewLine;


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

        public void Sort(string readrank, string current_username, string current_time)
        {
            Ranking newranking = new Ranking();
            string value = readrank;
            string[] substrings = value.Split(';');
            Ranking[] times = { new Ranking { username = substrings[0], time_str = substrings[1], time_int = ConvertStringToThousands(substrings[1])},
                                new Ranking { username = substrings[2], time_str = substrings[3], time_int = ConvertStringToThousands(substrings[3])},
                                new Ranking { username = substrings[4], time_str = substrings[5], time_int = ConvertStringToThousands(substrings[5])},
                                new Ranking { username = substrings[6], time_str = substrings[7], time_int = ConvertStringToThousands(substrings[7])},
                                new Ranking { username = substrings[8], time_str = substrings[9], time_int = ConvertStringToThousands(substrings[9])},
                                new Ranking { username = substrings[10], time_str = substrings[11], time_int = ConvertStringToThousands(substrings[11])},
                                new Ranking { username = substrings[12], time_str = substrings[13], time_int = ConvertStringToThousands(substrings[13])},
                                new Ranking { username = substrings[14], time_str = substrings[15], time_int = ConvertStringToThousands(substrings[15])},
                                new Ranking { username = substrings[16], time_str = substrings[17], time_int = ConvertStringToThousands(substrings[17])},
                                new Ranking { username = substrings[18], time_str = substrings[19], time_int = ConvertStringToThousands(substrings[19])},
                                new Ranking { username = current_username, time_str = current_time, time_int = ConvertStringToThousands(current_time) } };
            IEnumerable<Ranking> query = times.OrderBy(time => time.time_int);
            foreach (Ranking rank in query)
                Console.WriteLine(rank.username + " " + rank.time_int);

            string filename = "sorted_ranking.txt";
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", filename);

            if (File.Exists(path))
            {
                File.Delete(path);
                File.WriteAllText(path, "RANKING;");
                foreach (Ranking rank in query)
                {
                    string data = rank.username + ";" + rank.time_str + ";";// + Environment.NewLine;
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
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", filename);
            string readrank = File.ReadAllText(path);
            return readrank;
        }

        public void ShowRank(string readrank)   // TU CZYTAJ KOMENTARZE DAMIANIE
        {
            string value = readrank;
            string[] substring = value.Split(';');
            Console.WriteLine(substring[0]);        // substring[0] to napis RANKING, tego nie używaj
            Console.WriteLine(substring[1]);        // substring[1] == username1
            Console.WriteLine(substring[2]);        // substring[2] == time_username1
            Console.WriteLine(substring[3]);        // substring[3] == username2
            Console.WriteLine(substring[4]);        // substring[4] == time_username2
            Console.WriteLine(substring[5]);        // ...
            Console.WriteLine(substring[6]);        // ...
            Console.WriteLine(substring[7]);        // substring[19] == username10
        }                                           // substring[20] == time_username10
    }
}

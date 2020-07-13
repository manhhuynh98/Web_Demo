using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GetData_NCT
{
    class Program
    {
        public static string GetHtml(string url)
        {
            HttpClient http = new HttpClient();
            string html = http.GetStringAsync(url).Result;
            return html;
        }
        public static void addData(List<Album1> albums)
        {
            string sql = "Server = localhost; Database = zingmp3; port = 3306; user id = root; password = ";
            MySqlConnection connection = new MySqlConnection(sql);
            connection.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "INSERT INTO `album1`( `img`, `name`) VALUES (@image,@name)";
            cmd.Connection = connection;

            cmd.Parameters.AddWithValue("@name", albums[0].name);
            cmd.Parameters.AddWithValue("@image", albums[0].img);

            cmd.ExecuteNonQuery();
            connection.Close();

        }
        static void Main(string[] args)
        {
            string html = "https://www.nhaccuatui.com/";
            html = GetHtml(html);

            string album1 = Regex.Match(html, @"<div class=""list_album ngheGiHomNay"">(.+?);"">Hit Rewind", RegexOptions.Singleline).Value.Replace("Hit Rewind", "Hit Rewind (Vol. 4)</a></li>");
            var data = Regex.Matches(album1, @"<li>(.+?)</li>", RegexOptions.Singleline);

            foreach (var item in data)
            {
                string image = Regex.Match(item.ToString(), @"src=""(.+?)""", RegexOptions.Singleline).Value.Replace("src=","").Replace("\"","");
                string name = Regex.Match(item.ToString(), @"e']\);"">(.+?)</a>", RegexOptions.Singleline).Value.Replace("e\']);\">", "").Replace("</a>","");
                List<Album1> albums = new List<Album1>();
                albums.Add(new Album1(image, name));
                addData(albums);
            }
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}

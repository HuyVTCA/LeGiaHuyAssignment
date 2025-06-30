using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Firebase.Database;
using Firebase.Database.Query;

namespace LAB11
{
    public class Player
    {
        public string? Name { get; set; }
        public int Gold { get; set; }
        public int Coins { get; set; }
        public string? Region { get; set; }
        public int VipLevel { get; set; }
        public DateTime LastLogin { get; set; }
    }

    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            try
            {
                var client = new HttpClient();
                var firebaseClient = new FirebaseClient("your_firebase_database_url_here");
                string json = await client.GetStringAsync("https://raw.githubusercontent.com/NTH-VTC/OnlineDemoC-/main/simple_players.json");
                List<Player> players = JsonConvert.DeserializeObject<List<Player>>(json);

                if (players == null || !players.Any())
                {
                    Console.WriteLine("No players found in JSON data.");
                    return;
                }

                // Bai 1: Phân tích Tài chính Người chơi
                var richPlayers = players
                    .Where(p => p.Gold > 1000 && p.Coins > 100)
                    .OrderByDescending(p => p.Gold)
                    .Select(p => new { p.Name, p.Gold, p.Coins })
                    .ToList();

                Console.WriteLine("***   Bài 1: PHÂN TÍCH TÀI CHÍNH NGƯỜI CHƠI  ***");
                foreach (var player in richPlayers)
                {
                    Console.WriteLine($"Tên người chơi: {player.Name}, Điểm Gold: {player.Gold}, Điểm Coins: {player.Coins}");
                }

                await firebaseClient
                    .Child("quiz_bai1_richPlayers")
                    .PutAsync(richPlayers);




                // BÀI 2: Thống kê và Tìm kiếm Người chơi VIP
                int vipCount;
                vipCount = players.Count(p => p.VipLevel > 0);
                Console.WriteLine("***   BÀI 2: THỐNG KÊ VÀ TÌM KIẾM NGƯỜI CHƠI VIP   ***");
                Console.WriteLine($"Tong so nguoi choi VIP: {vipCount}");

                var vipByRegion = players
                    .Where(p => p.VipLevel > 0)
                    .GroupBy(p => p.Region)
                    .Select(g => new { Region = g.Key, Count = g.Count() })
                    .ToList();
                Console.WriteLine("*** Số lượng người chơi VIP tại các khu vực:");
                foreach (var region in vipByRegion)
                {
                    Console.WriteLine($"Khu vuc: {region.Region}, So nguoi choi VIP: {region.Count} người chơi");
                }

                DateTime now = new DateTime(2025, 06, 30, 0, 0, 0);
                var recentVipPlayers = players
                    .Where(p => p.VipLevel > 0 && (now - p.LastLogin).TotalDays <= 2)
                    .OrderByDescending(p => p.LastLogin)
                    .Select(p => new { p.Name, p.VipLevel, p.LastLogin })
                    .ToList();
                Console.WriteLine("*** Những người chơi VIP mới đăng nhập gần đây:");
                foreach (var player in recentVipPlayers)
                {
                    Console.WriteLine($"Tên người chơi: {player.Name}, Cấp độ VIP: {player.VipLevel}, Ngày đăng nhập: {player.LastLogin}");
                }

                await firebaseClient
                    .Child("quiz_bai2_recentVipPlayers")
                    .PutAsync(recentVipPlayers);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error fetching JSON data: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error deserializing JSON: {ex.Message}");
            }
            catch (FirebaseException ex)
            {
                Console.WriteLine($"Error with Firebase: {ex.Message}. Ensure Firebase rules allow public write access.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
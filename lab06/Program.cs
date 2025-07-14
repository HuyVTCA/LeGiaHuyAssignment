using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using Firebase.Database;
using Firebase.Database.Query;

internal class Program
{
    public class LeaderboardEntry
    {
        public required string Name { get; set; }
        public int Exp { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }

    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Firebase Admin SDK initialization starting...");
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("serviceAccountKey.json")
        });
        Console.WriteLine("Firebase Admin SDK initialized successfully!");
        await AddTestData();
        await ReadTestData();
        await UpdateTestData();
        await GenerateTestPlayer(30);
        await LoadLeaderboard(10);
    }

    public static async Task AddTestData()
    {
        var firebase = new FirebaseClient("firebase.link");
        var testData = new
        {
            Message = "Hello, Firebase!",
            Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
        };
        await firebase.Child("testData").PutAsync(testData);
        Console.WriteLine("Dữ liệu đã được thêm vào Firebase Realtime Database!");
    }

    public static async Task ReadTestData()
    {
        var firebase = new FirebaseClient("firebase.link");
        var testData = await firebase.Child("testData").OnceSingleAsync<dynamic>();
        Console.WriteLine($"Message: {testData.Message}, Timestamp: {testData.Timestamp}");
    }

    public static async Task UpdateTestData()
    {
        var firebase = new FirebaseClient("firebase.link"");
        var updatedData = new
        {
            Message = "Updated message",
            Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
        };
        await firebase.Child("testData").PutAsync(updatedData);
        Console.WriteLine("Dữ liệu đã được cập nhật trong Firebase Realtime Database!");
    }

    public static async Task GenerateTestPlayer(int num)
    {
        var firebase = new FirebaseClient(firebase.link");
        var random = new Random();
        for (int i = 0; i < num; i++)
        {
            string playerName = $"Player{i}";
            int gold = random.Next(1000, 10000);
            int ruby = random.Next(100, 500);
            int vip = random.Next(0, 10);
            int exp = random.Next(5000, 20000);
            int wins = random.Next(0, 50);
            int losses = random.Next(0, 30);
            await firebase.Child("players").Child(playerName).PutAsync(new
            {
                Name = playerName,
                Gold = gold,
                Ruby = ruby,
                VIP = vip,
                Exp = exp,
                Wins = wins,
                Losses = losses
            });
        }
        Console.WriteLine("Danh sách test người chơi đã được tạo thành công!");
    }

    public static async Task LoadLeaderboard(int limit = 10)
    {
        var firebase = new FirebaseClient("firebase.link"");
        try
        {
            var players = await firebase
                .Child("players")
                .OrderBy("Exp")
                .LimitToLast(limit)
                .OnceAsync<LeaderboardEntry>();
            var leaderboard = new List<LeaderboardEntry>();
            foreach (var player in players)
            {
                leaderboard.Add(player.Object);
            }
            leaderboard.Reverse();
            Console.WriteLine($"Top {limit} người chơi:");
            int rank = 1;
            foreach (var entry in leaderboard)
            {
                Console.WriteLine($"Top{rank} - {entry.Name}: {entry.Exp} Exp, Thắng: {entry.Wins}, Thua: {entry.Losses}");
                rank++;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi khi tải bảng xếp hạng: {ex.Message}");
        }
    }
}

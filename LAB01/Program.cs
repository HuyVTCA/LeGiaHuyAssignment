using System;
using System.IO;

public class NotNegativeException : Exception
{
    public NotNegativeException(string message) : base(message) { }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Console.Write("Nhập số nguyên x: ");
            int x = int.Parse(Console.ReadLine());

            Console.Write("Nhập số nguyên y: ");
            int y = int.Parse(Console.ReadLine());

            int tuSo = 3 * x + 2 * y;
            int mauSo = 2 * x - y;

            if (mauSo == 0)
                throw new DivideByZeroException("Mẫu số bằng 0!");

            double trongCan = (double)tuSo / mauSo;

            if (trongCan < 0)
                throw new NotNegativeException("Biểu thức dưới dấu căn nhỏ hơn 0!");

            double H = Math.Sqrt(trongCan);

            Console.WriteLine($"H = {H}");
            File.WriteAllText("input.txt", $"x = {x}, y = {y}, H = {H}");
            Console.WriteLine("Kết quả đã được lưu vào file input.txt");
        }
        catch (FormatException)
        {
            Console.WriteLine("Lỗi: Sai định dạng (phải nhập số nguyên).");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Lỗi: " + ex.Message);
        }
        catch (NotNegativeException ex)
        {
            Console.WriteLine("Lỗi: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi không xác định: " + ex.Message);
        }
    }
}

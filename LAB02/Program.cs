using System;
using System.Collections.Generic;

class Program
{
    static void Exercise1(int n)
    {
        List<int> numbers = new List<int>();
        Random random = new Random();
        for (int i = 0; i < n; i++)
            numbers.Add(random.Next(100));
        
        Console.WriteLine("\nDãy số gốc: " + string.Join(" ", numbers));
        numbers.Sort();
        Console.WriteLine("Dãy số sắp xếp: " + string.Join(" ", numbers));
    }

    struct MatHang
    {
        public int MaMH;
        public string TenMH;
        public int SoLuong;
        public double DonGia;

        public MatHang(int maMH, string tenMH, int soLuong, double donGia)
        {
            MaMH = maMH;
            TenMH = tenMH;
            SoLuong = soLuong;
            DonGia = donGia;
        }

        public double ThanhTien()
        {
            return SoLuong * DonGia;
        }

        public override string ToString()
        {
            return $"{MaMH} | {TenMH} | {SoLuong} | {DonGia} | {ThanhTien()}";
        }
    }

    static void ThemMatHang(List<MatHang> danhSach, MatHang m)
    {
        danhSach.Add(m);
    }

    static bool TimMatHang(List<MatHang> danhSach, int maMH)
    {
        foreach (var mh in danhSach)
            if (mh.MaMH == maMH)
                return true;
        return false;
    }

    static void XoaMatHang(List<MatHang> danhSach, int maMH)
    {
        for (int i = 0; i < danhSach.Count; i++)
            if (danhSach[i].MaMH == maMH)
            {
                danhSach.RemoveAt(i);
                break;
            }
    }

    static void XuatDanhSach(List<MatHang> danhSach)
    {
        Console.WriteLine("\nDanh sách mặt hàng:");
        Console.WriteLine("Mã | Tên | SL | Đơn giá | Thành tiền");
        foreach (var mh in danhSach)
            Console.WriteLine(mh);
    }

    static void NhapDanhSach(List<MatHang> danhSach)
    {
        while (true)
        {
            Console.Write("\nMã MH: ");
            int maMH = int.Parse(Console.ReadLine());
            Console.Write("Tên MH: ");
            string tenMH = Console.ReadLine();
            Console.Write("Số lượng: ");
            int soLuong = int.Parse(Console.ReadLine());
            Console.Write("Đơn giá: ");
            double donGia = double.Parse(Console.ReadLine());
            ThemMatHang(danhSach, new MatHang(maMH, tenMH, soLuong, donGia));
            Console.Write("Tiếp tục? (y/n): ");
            if (Console.ReadLine().ToLower() != "y")
                break;
        }
    }

    static void Main()
    {
        Console.Write("Nhập n: ");
        int n = int.Parse(Console.ReadLine());
        if (n > 0)
            Exercise1(n);
        else
            Console.WriteLine("Nhập n > 0!");

        List<MatHang> danhSach = new List<MatHang>();
        NhapDanhSach(danhSach);
        XuatDanhSach(danhSach);
        Console.Write("\nNhập mã MH cần xóa: ");
        int maMH = int.Parse(Console.ReadLine());
        if (TimMatHang(danhSach, maMH))
        {
            XoaMatHang(danhSach, maMH);
            XuatDanhSach(danhSach);
        }
        else
            Console.WriteLine("Không tìm thấy!");
    }
}
using System;
using System.Collections.Generic;

internal class Program
{
    static void Exercise1()
    {
        Console.WriteLine("=== Bài tập 1: Xóa số chẵn khỏi List<int> ===");
        List<int> numbers = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            numbers.Add(i);
        }

        numbers.RemoveAll(n => n % 2 == 0);

        Console.WriteLine("Danh sách số lẻ:");
        foreach (var number in numbers)
        {
            Console.WriteLine(number);
        }
    }

    static void Exercise2()
    {
        Console.WriteLine("\n=== Bài tập 2: Quản lý sản phẩm và giá ===");
        Dictionary<string, double> products = new Dictionary<string, double>
        {
            { "Laptop", 1200.50 },
            { "Phone", 699.99 },
            { "Tablet", 450.00 },
            { "Headphones", 89.99 },
            { "Mouse", 29.99 }
        };

        if (products.ContainsKey("Phone"))
        {
            products["Phone"] = 749.99;
            Console.WriteLine("Đã cập nhật giá Phone thành 749.99");
        }

        if (products.ContainsKey("Mouse"))
        {
            products.Remove("Mouse");
            Console.WriteLine("Đã xóa sản phẩm Mouse");
        }

        Console.WriteLine("Danh sách sản phẩm và giá:");
        foreach (var product in products)
        {
            Console.WriteLine($"Sản phẩm: {product.Key}, Giá: {product.Value:C}");
        }
    }

    static void Exercise3()
    {
        Console.WriteLine("\n=== Bài tập 3: Hàng đợi khách hàng ===");
        Queue<string> customers = new Queue<string>();

        customers.Enqueue("Alice");
        customers.Enqueue("Bob");
        customers.Enqueue("Charlie");
        customers.Enqueue("David");
        customers.Enqueue("Eve");

        Console.WriteLine("Đã thêm 5 khách hàng vào hàng đợi");

        if (customers.Count >= 2)
        {
            Console.WriteLine($"Khách hàng rời khỏi hàng đợi: {customers.Dequeue()}");
            Console.WriteLine($"Khách hàng rời khỏi hàng đợi: {customers.Dequeue()}");
        }

        Console.WriteLine("Khách hàng còn lại trong hàng đợi:");
        foreach (var customer in customers)
        {
            Console.WriteLine(customer);
        }
    }

    static void Exercise4(string input)
    {
        Console.WriteLine("\n=== Bài tập 4: Kiểm tra Palindrome ===");
        Console.WriteLine($"Chuỗi đầu vào: {input}");

        input = input.ToLower();
        Stack<char> stack = new Stack<char>();
        Queue<char> queue = new Queue<char>();

        foreach (char c in input)
        {
            if (char.IsLetterOrDigit(c))
            {
                stack.Push(c);
                queue.Enqueue(c);
            }
        }

        bool isPalindrome = true;
        while (stack.Count > 0 && queue.Count > 0)
        {
            if (stack.Pop() != queue.Dequeue())
            {
                isPalindrome = false;
                break;
            }
        }

        Console.WriteLine($"Chuỗi {(isPalindrome ? "là" : "không phải là")} palindrome");
    }

    static void Main(string[] args)
    {
        Exercise1();
        Exercise2();
        Exercise3();
        Exercise4("A man, a plan, a canal: Panama"); 
        Exercise4("race a car");
        Console.ReadLine();
    }
}
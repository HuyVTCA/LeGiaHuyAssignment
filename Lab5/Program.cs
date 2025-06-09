using System;
using System.Collections.Generic;

class Program
{
    delegate int MathOperation(int x);

    static int Square(int x)
    {
        return x * x;
    }

    static int Cube(int x)
    {
        return x * x * x;
    }

    class NumberList
    {
        public delegate void NumberAddedHandler(int number);
        public event NumberAddedHandler NumberAdded;

        private List<int> numbers = new List<int>();

        public void AddNumber(int number)
        {
            numbers.Add(number);
            NumberAdded?.Invoke(number);
        }
    }

    delegate int Operation(int a, int b);

    public class TemperatureChangedEventArgs : EventArgs
    {
        public double NewTemperature { get; set; }
        public TemperatureChangedEventArgs(double temp)
        {
            NewTemperature = temp;
        }
    }

    public class Thermometer
    {
        public event EventHandler<TemperatureChangedEventArgs> TemperatureChanged;

        public void SetTemperature(double temp)
        {
            TemperatureChanged?.Invoke(this, new TemperatureChangedEventArgs(temp));
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Bài 1: Delegate MathOperation");
        MathOperation operation = Square;
        Console.WriteLine("Bình phương của 3: " + operation(3));
        operation = Cube;
        Console.WriteLine("Lập phương của 3: " + operation(3));

        Console.WriteLine("\nBài 2: Event NumberAdded");
        NumberList list = new NumberList();
        list.NumberAdded += (number) => Console.WriteLine($"Số vừa thêm: {number}");
        list.AddNumber(5);
        list.AddNumber(10);
        list.AddNumber(15);

        Console.WriteLine("\nBài 3: Anonymous Method");
        Operation sum = delegate (int a, int b)
        {
            return a + b;
        };
        Console.WriteLine("Tổng của 5 và 7: " + sum(5, 7));

        Console.WriteLine("\nBài 4: Event TemperatureChanged");
        Thermometer thermometer = new Thermometer();
        thermometer.TemperatureChanged += (sender, e) =>
        {
            Console.WriteLine($"Nhiệt độ mới: {e.NewTemperature}°C");
        };
        thermometer.SetTemperature(25.5);
        thermometer.SetTemperature(30.0);
    }
}
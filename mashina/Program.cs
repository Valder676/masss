using System;
using System.Collections.Generic;

class Car
{
    public string Make { get; private set; }
    public string Model { get; private set; }
    public string Number { get; private set; }
    public string Color { get; private set; }
    private bool isStarted = false;
    private int speed = 0;
    private int gear = 0;
    private readonly int[] maxSpeeds = new int[] { 0, 30, 50, 70, 90, 120, 20 };

    public Car(string make, string model, string number, string color)
    {
        Make = make;
        Model = model;
        Number = number;
        Color = color;
    }

    public string StartEngine()
    {
        if (gear != 0 && gear != 1)
        {
            return "Не удалось завести машину. Проверьте передачу!";
        }
        isStarted = true;
        return $"Машина {Make} {Model} завелась";
    }

    public string StopEngine()
    {
        isStarted = false;
        return $"Машина {Make} {Model} остановилась";
    }

    public string PressGas()
    {
        if (!isStarted)
        {
            return "Машина не заведена!";
        }
        if (gear == 0)
        {
            return "Передача в нейтральном положении. Переключитесь на передачу, чтобы разогнаться.";
        }
        if (speed >= maxSpeeds[gear])
        {
            return $"Максимальная скорость для {gear}-й передачи уже достигнута.";
        }
     
        speed = Math.Min(speed + 10, maxSpeeds[gear]);
        return $"Машина {Make} {Model} разогналась до {speed} км/ч на {gear}-й передаче.";
    }

    public string PressBrake()
    {
        speed = Math.Max(0, speed - 10); 
        return $"Машина {Make} {Model} замедлила до {speed} км/ч";
    }

    public string ChangeGear(int newGear)
    {
        
        if ((newGear == 1 && speed > 30) ||
            (newGear == 2 && (speed < 20 || speed > 50)) ||
            (newGear == 3 && (speed < 40 || speed > 70)) ||
            (newGear == 4 && (speed < 60 || speed > 90)) ||
            (newGear == 5 && (speed < 80 || speed > 120)) ||
            (newGear == -1 && speed != 0))
        {
            isStarted = false;
            return $"Не удалось переключить передачу. Машина {Make} {Model} заглохла";
        }
        gear = newGear;
        return $"Переключена на {gear} передачу";
    }
}

class Program
{
    static void Main()
    {
        List<Car> cars = new List<Car>
        {
            new Car("ВАЗ", "2101", "A111AA", "Красный"),
            new Car("Mercedes", "600", "B222BB", "Черный"),
            
        };

        Console.WriteLine("Выберите машину:");
        for (int i = 0; i < cars.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cars[i].Make} {cars[i].Model}");
        }
        int choice = Convert.ToInt32(Console.ReadLine()) - 1;
        Car selectedCar = cars[choice];

        while (true)
        {
            Console.WriteLine("Выберите действие:\n1. Завести машину\n2. Заглушить машину\n3. Газануть\n4. Притормозить\n5. Переключить передачу\n6. Выйти из программы");
            int action = Convert.ToInt32(Console.ReadLine());

            string result = "";
            switch (action)
            {
                case 1:
                    result = selectedCar.StartEngine();
                    break;
                case 2:
                    result = selectedCar.StopEngine();
                    break;
                case 3:
                    result = selectedCar.PressGas();
                    break;
                case 4:
                    result = selectedCar.PressBrake();
                    break;
                case 5:
                    Console.WriteLine("На какую передачу переключить?");
                    int newGear = Convert.ToInt32(Console.ReadLine());
                    result = selectedCar.ChangeGear(newGear);
                    break;
                case 6:
                    return; // Выход из цикла и завершение программы
                default:
                    result = "Неизвестное действие. Пожалуйста, попробуйте снова.";
                    break;
            }

            Console.WriteLine(result);
        }
    }
}
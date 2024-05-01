using System;

// Singleton LogManager для управління логами
public sealed class LogManager
{
    private static LogManager instance = null; // Єдиний екземпляр класу
    private static readonly object padlock = new object(); // Об'єкт для потокобезпечності

    private LogManager() { } // Приватний конструктор, щоб заборонити створення екземплярів ззовні

    public static LogManager Instance // Властивість для доступу до єдиного екземпляру
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new LogManager(); // Створення єдиного екземпляру при першому виклику
                }
                return instance;
            }
        }
    }

    public void Log(string message)
    {
        Console.WriteLine($"{DateTime.Now}: {message}"); // Логування повідомлення разом з поточним часом
    }
}

// Адаптер для використання LogManager у складній системі адаптерів
public class Adapter
{
    private readonly LogManager logger; // Поле для екземпляру LogManager

    public Adapter()
    {
        logger = LogManager.Instance; // Ініціалізація LogManager через його єдиний екземпляр
    }

    public void DoSomething()
    {
        logger.Log("Adapter: Doing something..."); // Логування події "Adapter: Doing something..."
    }
}

// Клас Program для демонстрації роботи Singleton та Adapter
public class Program
{
    public static void Main()
    {
        Adapter adapter = new Adapter(); // Створення екземпляру Adapter
        adapter.DoSomething(); // Виклик методу для демонстрації логування
    }
}
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Observer pattern: користувачі підписуються на зміни рівня активності

        var tracker = new FitnessTracker();

        var user1 = new FitnessUser("Олег");
        var user2 = new FitnessUser("Андрій");

        tracker.Subscribe(user1);
        tracker.Subscribe(user2);

        tracker.SetActivityLevel("Високий");
        tracker.SetActivityLevel("Середній");

        tracker.Unsubscribe(user1);

        tracker.SetActivityLevel("Низький");
    }
}

public interface IFitnessObserver
{
    void Update(string data);
}

public class FitnessUser : IFitnessObserver
{
    private string _name;

    public FitnessUser(string name) { _name = name; }

    public void Update(string data)
    {
        Console.WriteLine($"{_name} отримав сповіщення: {data}");
    }
}

// Трекер, який повідомляє всіх підписників про зміни
public class FitnessTracker
{
    private List<IFitnessObserver> _observers = new List<IFitnessObserver>();
    private string _activityLevel;

    public void Subscribe(IFitnessObserver observer) => _observers.Add(observer);
    public void Unsubscribe(IFitnessObserver observer) => _observers.Remove(observer);

    public void SetActivityLevel(string newLevel)
    {
        _activityLevel = newLevel;
        NotifyAll();
    }

    private void NotifyAll()
    {
        foreach (var observer in _observers)
        {
            observer.Update($"Новий рівень активності: {_activityLevel}");
        }
    }
}
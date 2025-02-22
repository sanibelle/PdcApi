namespace Pdc.Application.Entities;

public class Todo
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime Date { get; set; }

    public Todo(string text)
    {
        Text = text;
        Id = Guid.NewGuid();
        Date = DateTime.Now;
        IsCompleted = false;
    }

    public Todo() { }
}
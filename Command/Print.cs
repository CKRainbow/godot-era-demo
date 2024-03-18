using System;

namespace EraLike.Command;

[Flags]
public enum PrintType
{
    None = 0,
    Line = 1,
    Wait = 2,
    WaitLine = Line | Wait
}

public class Print : ICommand, IWait
{

    
    public void SetPrint(string text, PrintType type)
    {
        Text = type.HasFlag(PrintType.Line) ? text + "\n" : text;
        Wait = type.HasFlag(PrintType.Wait);
    }
    public string Text { get; set; } = "";
    public bool   Wait { get; set; } = false;
    public void Execute()
    {
        DisplayMenu.Instance.Logger.Print(Text);
    }
}
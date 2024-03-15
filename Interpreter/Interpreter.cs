using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Godot;

public partial class Interpreter : Node
{
    private ManualResetEvent _waitLock = new(false);

    private DisplayMenu _displayMenu;

    private Queue<string> _inputBuffer = new();
    private RunState state = RunState.Normal;

    public override void _Ready()
    {
        _displayMenu = GetNode<DisplayMenu>("%DisplayMenu");
    }

    public void Continue()
    {
        if (state == RunState.WaitInput && _inputBuffer.Count <= 0)
        {
            return;
        }
        _waitLock.Set();
    }

    public void Wait(bool waitInput = false)
    {
        GD.Print("Wait");
        if (_inputBuffer.Count > 0)
        {
            GD.Print("Wait but Continue");
            Continue();
        }
        else
        {
            state = waitInput ? RunState.WaitInput : RunState.Wait;
            _waitLock.WaitOne();
        }
    }

    public void ReceiveInput(string input)
    {
        var splitedInput = input.Split('\n');
        foreach (var line in splitedInput)
        {
            _inputBuffer.Enqueue(line);
        }

        Print(_inputBuffer.First());
    }

    public void Print(string text)
    {
        Print(text, false);
    }

    public void PrintW(string text)
    {
        Print(text + "\n", true);
    }

    public void PrintL(string text)
    {
        Print(text + "\n", false);
    }

    public void Print(string text, bool wait)
    {
        _displayMenu.AddText(text);
        if (wait)
        {
            Wait();
        }
    }

    public string Input()
    {
        Wait(true);
        return _inputBuffer.Dequeue();
    }
}

public enum RunState
{
    Normal,
    Wait,
    WaitInput,
}

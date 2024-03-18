using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using EraLike.Command;
using Godot;

public enum RunState
{
    Normal,
    Wait,
    WaitInput,
}

public partial class Interpreter : Node
{
    public List<ICommand> Commands { get; set; } = new List<ICommand>();
    public int            CommandIndex = 0;
    public void           AddCommand(ICommand command) => Commands.Add(command);
    public void           ClearCommands()              => Commands.Clear();
    public bool           Wait;
    public bool           WaitInput;
    public bool           CanExecute => !Wait && !WaitInput && CommandIndex < Commands.Count;
    public void Execute()
    {
        var command = Commands[CommandIndex];
        command.Execute();
        Wait = command is IWait { Wait: true };
        WaitInput = command is IWaitInput;
        CommandIndex++;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        GD.Print("Continue");
 
    }
    public override void _Input(InputEvent @event)
    {
     
        if ((@event is InputEventKey inputEventKey && inputEventKey.IsActionReleased("ui_accept")) || (@event is InputEventMouseButton inputEventMouseButton && inputEventMouseButton.IsPressed()))

        {
            GD.Print("Continue");

            Wait = false;
            @event.Dispose();
        }
    }
    // private DisplayMenu           _displayMenu;
    // private Queue<(string, bool)> _printBuffer = new Queue<(string, bool)>();
    //
    // private Queue<string> _inputBuffer = new Queue<string>();
    //
    // private RunState      _state       = RunState.Normal;
    // public override void _UnhandledInput(InputEvent @event)
    // {
    //     if (@event is InputEventKey inputEventKey && inputEventKey.IsActionReleased("ui_accept"))
    //     {
    //         Continue();
    //         inputEventKey.Dispose();
    //     }
    // }
    // public override void _Ready()
    // {
    //     _displayMenu = GetNode<DisplayMenu>("%DisplayMenu");
    // }
    //
    // public void Continue()
    // {
    //     if (_state is RunState.WaitInput or RunState.Wait)
    //     {
    //         _state = RunState.Normal;
    //         var (text, wait) = _printBuffer.Dequeue();
    //         Print(text, wait);
    //         return;
    //     }
    // }
    //
    // public void Wait(bool waitInput = false)
    // {
    //     GD.Print("Wait");
    //     _state = waitInput ? RunState.WaitInput : RunState.Wait;
    //     GD.Print($"state: {_state}");
    //     if (_inputBuffer.Count > 0)
    //     {
    //         GD.Print("Wait but Continue");
    //         Continue();
    //     }
    // }
    //
    // public void ReceiveInput(string input)
    // {
    //     var splitedInput = input.Split('\n');
    //     foreach (var line in splitedInput)
    //     {
    //         _inputBuffer.Enqueue(line);
    //     }
    //
    //     Print(_inputBuffer.First());
    // }
    //
    // public void Print(string text)
    // {
    //     Print(text, false);
    // }
    //
    // public void PrintW(string text)
    // {
    //     Print(text + "\n", true);
    // }
    //
    // public void PrintL(string text)
    // {
    //     Print(text + "\n", false);
    // }
    //
    // public void Print(string text, bool wait)
    // {
    //     if (_state is RunState.WaitInput or RunState.Wait)
    //     {
    //         GD.Print($"[Print] Text: {text} Wait: {wait}");
    //         _printBuffer.Enqueue((text, wait));
    //         return;
    //     }
    //     _displayMenu.AddText(text);
    //     if (wait)
    //     {
    //         Wait();
    //     }
    // }
    //
    // public string Input()
    // {
    //     Wait(true);
    //     return _inputBuffer.Count == 0 ? "" : _inputBuffer.Dequeue();
    // }
}
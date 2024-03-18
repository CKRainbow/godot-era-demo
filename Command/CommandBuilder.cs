using System;
using System.Collections.Generic;

namespace EraLike.Command;

public class CommandBuilder
{
    private List<ICommand> _commands = new List<ICommand>();
    public static CommandBuilder Create()
    {
        return new CommandBuilder();
    }
    public CommandBuilder Add<TCommand>(Action<TCommand> callback) where TCommand : ICommand, new()
    {
        var command = Activator.CreateInstance<TCommand>();
        callback.Invoke(command);
        _commands.Add(command);
        return this;
    }
    public CommandBuilder AddCommand(ICommand command)
    {
        _commands.Add(command);
        return this;
    }
    public CommandBuilder AddPrint(string text, PrintType type = PrintType.None)
    {
        Add<Print>(print =>
        {
            print.SetPrint(text, type);
        });
        return this;
    }
    public CommandBuilder AddPrintLine(string text)
    {
        return AddPrint(text, PrintType.Line);
    }
    public CommandBuilder AddPrintWait(string text)
    {
        return AddPrint(text, PrintType.Wait);
    }
    public CommandBuilder AddPrintWaitLine(string text)
    {
        return AddPrint(text, PrintType.WaitLine);
    }
    public List<ICommand> Build()
    {
        return _commands;
    }
}
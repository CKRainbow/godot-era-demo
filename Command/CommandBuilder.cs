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
    public CommandBuilder AddPrint(string text, bool wait = false, bool line = false)
    {
        Add<Print>(print =>
        {
            print.Text = text;
            if (line)
            {
                print.Text += "\n";
            }
            print.Wait = wait;
        });
        return this;
    }
    public CommandBuilder AddPrintLine(string text)
    {
        return AddPrint(text, line: true);
    }
    public CommandBuilder AddPrintWait(string text)
    {
        return AddPrint(text, true);
    }
    public List<ICommand> Build()
    {
        return _commands;
    }
}
using System;
using System.Threading.Tasks;
using EraLike.Command;
using Godot;

public partial class Main : Node
{
    private DisplayMenu _displayMenu;
    private MainMenu    _mainMenu;
    private Interpreter _interpreter;

    public override void _Ready()
    {
        _mainMenu = GetNode<MainMenu>("%MainMenu");
        _displayMenu = GetNode<DisplayMenu>("%DisplayMenu");
        _interpreter = GetNode<Interpreter>("%Interpreter");

        _mainMenu.GameStarted += Main_GameStarted;
    }

    private async void Main_GameStarted()
    {
        _mainMenu.Visible = false;
        _displayMenu.Visible = true;

        Test();
    }

    private void Test()
    {

        _interpreter.Commands = CommandBuilder.Create()
            .AddPrintLine("Hello, World!").AddPrintLine("This is a test.")
            .AddPrintWait("Press any key to continue.\n").AddPrintLine("Goodbye!")
            .Build();
    }

    public override void _Process(double delta)
    {
        if (!_interpreter.CanExecute)
        {
            return;
        }
        _interpreter.Execute();
    }


}
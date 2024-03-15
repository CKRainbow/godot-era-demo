using System;
using System.Threading.Tasks;
using Godot;

public partial class Main : Node
{
    private DisplayMenu _displayMenu;
    private MainMenu _mainMenu;
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

        await Test();
    }

    private async Task Test()
    {
        await Task.Run(() =>
        {
            _interpreter.Print("测试");
            _interpreter.PrintL("测试");
            _interpreter.Input();
            _interpreter.PrintW("测试");
            _interpreter.Print("测试");
            _interpreter.PrintW("停止");
        });
    }

    public override void _Process(double delta) { }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventKey inputEventKey && inputEventKey.IsActionReleased("ui_accept"))
        {
            _interpreter.Continue();
        }
    }
}

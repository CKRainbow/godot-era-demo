using Godot;
using System;

public partial class MainMenu : Control, IUi
{
	public event Action GameStarted;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Button startButton = GetNode<Button>("%StartButton");
		Button quitButton  = GetNode<Button>("%QuitButton");

		startButton.ButtonDown += StartButton_ButtonDown;
		quitButton.ButtonDown += QuitButton_ButtonDown;
	}

    private void QuitButton_ButtonDown()
    {
		GetTree().Quit();
    }


    private void StartButton_ButtonDown()
    {
		GameStarted?.Invoke();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}

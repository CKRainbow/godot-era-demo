using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Godot;

public partial class DisplayMenu : Control, IUi
{
    private List<string> contentBuffer = new();
    private RichTextLabel _canvas;
    private Interpreter _interpreter;
    private LineEdit _inputLine;
    private bool _dirty;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _canvas = GetNode<RichTextLabel>("%Canvas");
        _inputLine = GetNode<LineEdit>("%InputLine");
        _interpreter = GetTree().CurrentScene.GetNode<Interpreter>("%Interpreter");

        _canvas.Text = string.Join("", contentBuffer);
        _inputLine.TextSubmitted += _inputLineTextSubmitted;
    }

    private void _inputLineTextSubmitted(string newText)
    {
        // FIXME: Enter is processed twice
        _interpreter.ReceiveInput(newText);
        _inputLine.Clear();
        _interpreter.Continue();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    public override void _Process(double delta)
    {
        if (_dirty)
        {
            _canvas.Text = string.Join("", contentBuffer);
            _inputLine.Clear();
            _dirty = false;
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (
            @event is InputEventMouseButton inputEventMouse
            && Visible
            && inputEventMouse.IsPressed()
        )
        {
            GD.Print("Position", inputEventMouse.Position);
            // TODO: The Edit won't be cleared
            if (_inputLine.Text.Length > 0)
            {
                _interpreter.ReceiveInput(_inputLine.Text);
            }
            else
            {
                _interpreter.Continue();
            }
        }
    }

    public void AddText(string text)
    {
        contentBuffer.Add(text);
        _dirty = true;
    }
}

enum DisplayState
{
    WaitInput,
    Wait,
    Normal,
}

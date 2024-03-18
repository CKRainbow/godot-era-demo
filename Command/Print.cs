namespace EraLike.Command;

public class Print : ICommand, IWait
{

    public Print()
    {
    }
    public Print(string text)
    {
        Text = text;
    }
    public string Text { get; set; } = "";
    public bool   Wait { get; set; } = false;
    public void Execute()
    {
        DisplayMenu.Instance.Logger.Print(Text);
    }
}
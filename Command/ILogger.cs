namespace EraLike.Command;

public interface ILogger
{
    void   Print(string     text);
    void   PrintLine(string text);
    string GetLog();
    void   ClearLog();
}
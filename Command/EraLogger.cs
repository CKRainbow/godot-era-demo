using System.Text;

namespace EraLike.Command;

public class EraLogger : ILogger
{
    public static ILogger       Default { get; } = new EraLogger();
    private       StringBuilder _stringBuilder = new StringBuilder(256);
    public        void          Print(string     text) => _stringBuilder.Append(text);
    public        void          PrintLine(string text) => _stringBuilder.AppendLine(text);

    public string GetLog()   => _stringBuilder.ToString();
    public void   ClearLog() => _stringBuilder.Clear();
}
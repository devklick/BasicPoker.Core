using System.Diagnostics.CodeAnalysis;

namespace BasicPoker.Core.Logging;

// TODO: Get rid of this quick and dirty logger for something more robust
public class Logger
{
    [MemberNotNullWhen(true, nameof(_initialized))]
    public static Logger Instance { get; private set; }

    private readonly Action<string> _writer;
    private static bool _initialized;

    private Logger(Action<string> writer)
    {
        _writer = writer;
    }

    public static void Initialize(Action<string> writer)
    {

        Instance = new Logger(writer);
        _initialized = true;
    }

    public void Log(params string[] messages)
    {
        foreach (var message in messages) _writer(message);
    }
}
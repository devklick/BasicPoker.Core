using BasicPoker.Core.Players;
using BasicPoker.Core.Arena;

namespace BasicPoker.Core.ConsoleClient;
class Program
{
    static void Main(string[] args)
    {
        var table = new Table();

        var player1 = new AIPlayerLevel1(50);
        var player2 = new AIPlayerLevel1(50);
        var player3 = new UIPlayer(50);

        table.SeatPlayer(player1);
        table.SeatPlayer(player2);
        table.SeatPlayer(player3);

        table.DealNewGame();
    }
}

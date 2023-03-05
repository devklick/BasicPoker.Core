using BasicPoker.Core.Players;

namespace BasicPoker.Core.Arena;

public class Seat
{
    public int SeatNumber { get; }
    public Player? Player { get; private set; }
    public bool IsAvailable => Player == null;

    public Seat(int seatNumber, Player? player = null)
    {
        SeatNumber = seatNumber;
        Player = player;
    }

    internal static List<Seat> CreateSequence(int count)
    {
        var seats = new List<Seat>();
        for (int seatNumber = 1; seatNumber <= count; seatNumber++)
        {
            seats.Add(new Seat(seatNumber));
        }
        return seats;
    }

    public void SeatPlayer(Player player) => Player = player;
}
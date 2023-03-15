namespace BasicPoker.Core.Hands;

public class HandProbability
{
    public Hand Hand { get; }
    public double Probability { get; }
    public HandProbability(Hand hand, double probability)
    {
        Hand = hand;
        Probability = probability;
    }
}

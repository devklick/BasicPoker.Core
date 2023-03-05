namespace BasicPoker.Core.Hands;

public class HandProbability
{
    public Hand Hand { get; set; }
    public double Probability { get; set; }
    public HandProbability(Hand hand, double probability)
    {
        Hand = hand;
        Probability = probability;
    }
}

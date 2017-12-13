namespace BallGatesPredictionGame.Models
{
    public class Gate
    {
        public bool IsOpen { get; set; }
        public string Label { get; set; }
        public bool HasBall { get; set; }
        public Gate LeftGate { get; set; }
        public Gate RightGate { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace BallGatesPredictionGame.Models
{
    public class Game
    {
        private int FinalGateCounter = 0;
        private int MaxNumberOfGates { get { return 32; } }
        private int NumberOfBalls = 15;
        private Gate RootGate {get;set;}
        private IList<String> gatesThatHaveABall { get; set; }

        public Game()
        {
            RootGate = BuildGateTree(MaxNumberOfGates, true);
            gatesThatHaveABall = new List<string>();
        }

        Gate BuildGateTree(int maxNumberOfGates,bool isGateOpen)
        {
            if (maxNumberOfGates < 2) return null;

            int median = maxNumberOfGates / 2;
            string label = string.Empty;
            if (maxNumberOfGates == 2)
            {
                label = Enum.GetName(typeof(GateLabels), FinalGateCounter);
                FinalGateCounter++;
            }
            return new Gate
            {
                Label = label,
                IsOpen = isGateOpen,
                LeftGate = BuildGateTree(median, true),
                RightGate = BuildGateTree(median, false)
            };
        }

        void TraverseGateTree(Gate leftGate, Gate rightGate)
        {
            if(leftGate.IsOpen)
            {
                leftGate.IsOpen = false;
                rightGate.IsOpen = true;
                if (String.IsNullOrEmpty(leftGate.Label))
                {
                    TraverseGateTree(leftGate.LeftGate, leftGate.RightGate);
                }
                else
                {
                    leftGate.HasBall = true;
                    gatesThatHaveABall.Add(leftGate.Label);
                }
                return;
            }

            if (rightGate.IsOpen)
            {
                rightGate.IsOpen = false;
                leftGate.IsOpen = true;
                if (String.IsNullOrEmpty(rightGate.Label))
                {
                    TraverseGateTree(rightGate.LeftGate, rightGate.RightGate);
                }
                else
                {
                    rightGate.HasBall = true;
                    gatesThatHaveABall.Add(rightGate.Label);
                }
                return;
            }
        }

        public void Start()
        {
            for(int i =0; i < NumberOfBalls; i++)
            {
                TraverseGateTree(RootGate.LeftGate, RootGate.RightGate);
            } 
        }

        public string GetNameOfGateWithoutBall()
        {
            foreach (GateLabels gateLabel in Enum.GetValues(typeof(GateLabels)))
            {
                string gateLabelString = gateLabel.ToString();
                if (!gatesThatHaveABall.Contains(gateLabelString)) return gateLabelString;
            }
            return string.Empty;
        }

    }
}

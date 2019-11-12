namespace MyShuttle.Dashboard.Client.Models
{
    using System;

    public class ServicesSatisfactionData
    {
        public double AcceptedPercent { get; set; }
        public double AcceptedDifference { get; set; }
        public double AbsAcceptedDifference => Math.Abs(AcceptedDifference);
        public double RefusedPercent => 100 - AcceptedPercent;
        public double AcceptedPercentEndAngle => -90 + (AcceptedPercent * 180 / 100);
        public double PositivePercent { get; set; }
        public double PositiveDifference { get; set; }
        public double AbsPositiveDifference => Math.Abs(PositiveDifference);
        public double NegativePercent => 100 - PositivePercent;
        public double PositivePercentEndAngle => -90 + (PositivePercent * 180 / 100);
        public bool AcceptedDifferenceIsNegative => AcceptedDifference < 0;
        public bool PositiveDifferenceIsNegative => PositiveDifference < 0;
    }
}

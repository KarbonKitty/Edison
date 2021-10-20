namespace Edison
{
    public class ResearchPointsValue
    {
        public double Value { get; }

        public ResearchPointsValue(double value)
        {
            Value = value;
        }

        public override string ToString() => $"{Value:N2} RP";

        public static bool operator >(ResearchPointsValue a, ResearchPointsValue b) => a.Value > b.Value;
        public static bool operator >=(ResearchPointsValue a, ResearchPointsValue b) => a.Value >= b.Value;
        public static bool operator <(ResearchPointsValue a, ResearchPointsValue b) => a.Value < b.Value;
        public static bool operator <=(ResearchPointsValue a, ResearchPointsValue b) => a.Value <= b.Value;
        public static ResearchPointsValue operator +(ResearchPointsValue a, ResearchPointsValue b) => new(a.Value + b.Value);
        public static ResearchPointsValue operator -(ResearchPointsValue a, ResearchPointsValue b) => new(a.Value - b.Value);
        public static ResearchPointsValue operator *(ResearchPointsValue p, double scale) => new(p.Value * scale);
        public static ResearchPointsValue operator /(ResearchPointsValue p, double scale) => new(p.Value / scale);
    }
}

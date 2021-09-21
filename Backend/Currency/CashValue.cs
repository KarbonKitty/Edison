namespace Edison
{
    public class CashValue
    {
        public double Value { get; }

        public CashValue(double value)
        {
            Value = value;
        }

        public override string ToString() => $"{Value:N2}$";

        public static bool operator >(CashValue a, CashValue b) => a.Value > b.Value;
        public static bool operator >=(CashValue a, CashValue b) => a.Value >= b.Value;
        public static bool operator <(CashValue a, CashValue b) => a.Value < b.Value;
        public static bool operator <=(CashValue a, CashValue b) => a.Value <= b.Value;
        public static CashValue operator +(CashValue a, CashValue b) => new CashValue(a.Value + b.Value);
        public static CashValue operator -(CashValue a, CashValue b) => new CashValue(a.Value - b.Value);
        public static CashValue operator *(CashValue p, double scale) => new CashValue(p.Value * scale);
        public static CashValue operator /(CashValue p, double scale) => new CashValue(p.Value / scale);
    }
}

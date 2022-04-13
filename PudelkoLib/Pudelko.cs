using System.Collections;
using System.Globalization;

namespace PudelkoLib
{
    public sealed partial class Pudelko : IFormattable, IEquatable<Pudelko>, IEnumerable
    {
        public double A { get; init; }
        public double B { get; init; }
        public double C { get; init; }
        public double Volume => Math.Round(A * B * C, 9);
        public string VolumeWithUnit => $"{Math.Round(A * B * C, 9):F9} m\u00B3";
        public double Area => Math.Round(2 * A * B + 2 * B * C + 2 * C * A, 6);
        public string AreaWithUnit => $"{Math.Round(2 * A * B + 2 * B * C + 2 * C * A, 6):F6} m\u00B3";
        public UnitOfMeasure Measure { get; init; }
        public double SumOfTheSidesOfBox => A + B + C;

        public Pudelko(double? a = null, double? b = null, double? c = null, UnitOfMeasure unit = UnitOfMeasure.meter)
        {
            double aNotNull = Math.Round(0.1000, 3);
            double bNotNull = Math.Round(0.1000, 3);
            double cNotNull = Math.Round(0.1000, 3);

            #region METER

            if (unit == UnitOfMeasure.meter)
            {
                if (a != null)
                    aNotNull = Math.Round((double)a, 3, MidpointRounding.ToZero);

                if (b != null)
                    bNotNull = Math.Round((double)b, 3, MidpointRounding.ToZero);

                if (c != null)
                    cNotNull = Math.Round((double)c, 3, MidpointRounding.ToZero);
            }

            #endregion METER

            #region CENTIMETER

            if (unit == UnitOfMeasure.centimeter)
            {
                if (a != null)
                    aNotNull = Math.Round(((double)a / 100), 3, MidpointRounding.ToZero);

                if (b != null)
                    bNotNull = Math.Round(((double)b / 100), 3, MidpointRounding.ToZero);

                if (c != null)
                    cNotNull = Math.Round(((double)c / 100), 3, MidpointRounding.ToZero);
            }

            #endregion CENTIMETER

            #region MILIMETER

            if (unit == UnitOfMeasure.milimeter)
            {
                if (a != null)
                    aNotNull = Math.Round(((double)a / 1000), 3, MidpointRounding.ToZero);

                if (b != null)
                    bNotNull = Math.Round(((double)b / 1000), 3, MidpointRounding.ToZero);

                if (c != null)
                    cNotNull = Math.Round(((double)c / 1000), 3, MidpointRounding.ToZero);
            }

            #endregion MILIMETER

            #region EXCEPTIONS

            if (aNotNull <= 0 || bNotNull <= 0 || cNotNull <= 0)
                throw new ArgumentOutOfRangeException();

            if (aNotNull > 10.000 || bNotNull > 10.000 || cNotNull > 10.000)
                throw new ArgumentOutOfRangeException();

            #endregion EXCEPTIONS

            // immutable initialize
            A = aNotNull;
            B = bNotNull;
            C = cNotNull;
            Measure = unit;
        }

        #region ToString

        public override string ToString()
        {
            return this.ToString("m");
        }

        public string ToString(string? format)
        {
            return this.ToString(format, CultureInfo.CurrentCulture);
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            char multiplicationSign = '\u00D7';
            if (format is null || format == string.Empty || format == "m")
                return $"{A:F3} m {multiplicationSign} {B:F3} m {multiplicationSign} {C:F3} m";
            else if (format == "cm")
                return $"{A * 100:F1} cm {multiplicationSign} {B * 100:F1} cm {multiplicationSign} {C * 100:F1} cm";
            else if (format == "mm")
                return $"{A * 1000:F0} mm {multiplicationSign} {B * 1000:F0} mm {multiplicationSign} {C * 1000:F0} mm";
            else
                throw new FormatException();
        }

        #endregion ToString

        #region Equals

        public override bool Equals(object? obj)
        {
            return this.Equals(obj as Pudelko);
        }

        public bool Equals(Pudelko? other)
        {
            Pudelko p1, p2;
            p1 = SortObject(this);
            p2 = SortObject(other);
            if (p1.A != p2.A || p1.B != p2.B || p1.C != p2.C)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.A, this.B, this.C);
        }

        public static bool operator ==(Pudelko p1, Pudelko p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Pudelko p1, Pudelko p2)
        {
            return !(p1 == p2);
        }

        #endregion Equals

        #region Addition Operator

        public static Pudelko operator +(Pudelko p1, Pudelko p2)
        {
            var x = SortObject(p1);
            var y = SortObject(p2);
            var z = x.A;
            if (x.A > y.A)
                z = y.A;
            return new Pudelko(((x.A > y.A) ? x.A : y.A), ((x.B > y.B) ? x.B : y.B), ((x.C > y.C) ? x.C + z : y.C + z));
        }

        #endregion Addition Operator

        #region Convertion

        public static explicit operator double[](Pudelko p)
        {
            return new double[] { p.A, p.B, p.C };
        }

        public static implicit operator Pudelko(ValueTuple<int, int, int> values)
        {
            return new Pudelko(values.Item1, values.Item2, values.Item3, UnitOfMeasure.milimeter);
        }

        #endregion Convertion

        #region Indexer

        public double this[int index]
        {
            get
            {
                if (index == 0) return this.A;
                else if (index == 1) return this.B;
                else if (index == 2) return this.C;
                else
                    throw new IndexOutOfRangeException("Box has only three values");
            }
        }

        #endregion Indexer

        #region Foreach iterator

        IEnumerator IEnumerable.GetEnumerator()
        {
            double[] valuesOfBoxStorredInArray = new double[] { this.A, this.B, this.C };
            foreach (double value in valuesOfBoxStorredInArray)
            {
                yield return value;
            }
        }

        #endregion Foreach iterator

        #region Parse

        public static Pudelko Parse(string stringToSplit)
        {
            string[] splittedString = stringToSplit.Split(" ");
            double a = double.Parse(splittedString[0].Replace('.', ','));
            double b = double.Parse(splittedString[3].Replace('.', ','));
            double c = double.Parse(splittedString[6].Replace('.', ','));
            if (splittedString[1] == "m" && splittedString[4] == "m" && splittedString[7] == "m")
                return new Pudelko(a, b, c, UnitOfMeasure.meter);
            else if (splittedString[1] == "cm" && splittedString[4] == "cm" && splittedString[7] == "cm")
                return new Pudelko(a, b, c, UnitOfMeasure.centimeter);
            else if (splittedString[1] == "mm" && splittedString[4] == "mm" && splittedString[7] == "mm")
                return new Pudelko(a, b, c, UnitOfMeasure.milimeter);
            else
                throw new InvalidDataException("Wrong value for parse");
        }

        #endregion Parse

        private static Pudelko SortObject(Pudelko pudelko)
        {
            SortedSet<double> pudlo = new SortedSet<double>() { pudelko.A, pudelko.B, pudelko.C };
            return new Pudelko(pudlo.ElementAt(0), pudlo.ElementAt(1), pudlo.ElementAt(2));
        }

        public static int BoxComparison(Pudelko p1, Pudelko p2)
        {
            if (p1.Volume < p2.Volume)
                return -1;
            else if (p1.Volume == p2.Volume)
            {
                if (p1.Area < p2.Area)
                    return -1;
                else if (p1.Area == p2.Area)
                {
                    if (p1.SumOfTheSidesOfBox < p2.SumOfTheSidesOfBox)
                        return -1;
                }
                return 0;
            }

            return 1;
        }
    }
}
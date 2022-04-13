using Microsoft.VisualStudio.TestTools.UnitTesting;
using PudelkoLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using static PudelkoLib.Pudelko;

namespace PudelkoUnitTests
{
    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }

    // ========================================

    [TestClass]
    public class UnitTestsPudelkoConstructors
    {
        private static double defaultSize = 0.1; // w metrach
        private static double accuracy = 0.001; //dok³adnoœæ 3 miejsca po przecinku

        private void AssertPudelko(Pudelko p, double expectedA, double expectedB, double expectedC)
        {
            Assert.AreEqual(expectedA, p.A, delta: accuracy);
            Assert.AreEqual(expectedB, p.B, delta: accuracy);
            Assert.AreEqual(expectedC, p.C, delta: accuracy);
        }

        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Pudelko p = new Pudelko();

            Assert.AreEqual(defaultSize, p.A, delta: accuracy);
            Assert.AreEqual(defaultSize, p.B, delta: accuracy);
            Assert.AreEqual(defaultSize, p.C, delta: accuracy);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów licz¹ siê 3 miejsca po przecinku
        public void Constructor_3params_DefaultMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów licz¹ siê 3 miejsca po przecinku
        public void Constructor_3params_InMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100.0, 25.5, 3.1,
                 1.0, 0.255, 0.031)]
        [DataRow(100.0, 25.58, 3.13,
                 1.0, 0.255, 0.031)] // dla centymertów liczy siê tylko 1 miejsce po przecinku
        public void Constructor_3params_InCentimeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a: a, b: b, c: c, unit: UnitOfMeasure.centimeter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100, 255, 3,
                 0.1, 0.255, 0.003)]
        [DataRow(100.0, 25.58, 3.13,
                 0.1, 0.025, 0.003)] // dla milimetrów nie licz¹ siê miejsca po przecinku
        public void Constructor_3params_InMilimeters(double a, double b, double c,
                                                     double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b, c: c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        // ----

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_DefaultMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a, b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_InMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a: a, b: b, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 2.5, 0.11, 0.025)]
        [DataRow(100.1, 2.599, 1.001, 0.025)]
        [DataRow(2.0019, 0.25999, 0.02, 0.002)]
        public void Constructor_2params_InCentimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 2.0, 0.011, 0.002)]
        [DataRow(100.1, 2599, 0.1, 2.599)]
        [DataRow(200.19, 2.5999, 0.2, 0.002)]
        public void Constructor_2params_InMilimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        // -------

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_DefaultMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_InMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 0.11)]
        [DataRow(100.1, 1.001)]
        [DataRow(2.0019, 0.02)]
        public void Constructor_1param_InCentimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 0.011)]
        [DataRow(100.1, 0.1)]
        [DataRow(200.19, 0.2)]
        public void Constructor_1param_InMilimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        // ---

        public static IEnumerable<object[]> DataSet1Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5, 3.1},
            new object[] {1.0, -2.5, 3.1},
            new object[] {1.0, 2.5, -3.1},
            new object[] {-1.0, -2.5, 3.1},
            new object[] {-1.0, 2.5, -3.1},
            new object[] {1.0, -2.5, -3.1},
            new object[] {-1.0, -2.5, -3.1},
            new object[] {0, 2.5, 3.1},
            new object[] {1.0, 0, 3.1},
            new object[] {1.0, 2.5, 0},
            new object[] {1.0, 0, 0},
            new object[] {0, 2.5, 0},
            new object[] {0, 0, 3.1},
            new object[] {0, 0, 0},
            new object[] {10.1, 2.5, 3.1},
            new object[] {10, 10.1, 3.1},
            new object[] {10, 10, 10.1},
            new object[] {10.1, 10.1, 3.1},
            new object[] {10.1, 10, 10.1},
            new object[] {10, 10.1, 10.1},
            new object[] {10.1, 10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_DefaultMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.01, 0.1, 1)]
        [DataRow(0.1, 0.01, 1)]
        [DataRow(0.1, 0.1, 0.01)]
        [DataRow(1001, 1, 1)]
        [DataRow(1, 1001, 1)]
        [DataRow(1, 1, 1001)]
        [DataRow(1001, 1, 1001)]
        [DataRow(1, 1001, 1001)]
        [DataRow(1001, 1001, 1)]
        [DataRow(1001, 1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InCentimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.1, 1, 1)]
        [DataRow(1, 0.1, 1)]
        [DataRow(1, 1, 0.1)]
        [DataRow(10001, 1, 1)]
        [DataRow(1, 10001, 1)]
        [DataRow(1, 1, 10001)]
        [DataRow(10001, 10001, 1)]
        [DataRow(10001, 1, 10001)]
        [DataRow(1, 10001, 10001)]
        [DataRow(10001, 10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMiliimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.milimeter);
        }

        public static IEnumerable<object[]> DataSet2Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5},
            new object[] {1.0, -2.5},
            new object[] {-1.0, -2.5},
            new object[] {0, 2.5},
            new object[] {1.0, 0},
            new object[] {0, 0},
            new object[] {10.1, 10},
            new object[] {10, 10.1},
            new object[] {10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_DefaultMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.01, 1)]
        [DataRow(1, 0.01)]
        [DataRow(0.01, 0.01)]
        [DataRow(1001, 1)]
        [DataRow(1, 1001)]
        [DataRow(1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InCentimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.1, 1)]
        [DataRow(1, 0.1)]
        [DataRow(0.1, 0.1)]
        [DataRow(10001, 1)]
        [DataRow(1, 10001)]
        [DataRow(10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMilimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.milimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(0.01)]
        [DataRow(1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InCentimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMilimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.milimeter);
        }

        #endregion Constructor tests ================================

        #region ToString tests ===================================

        [TestMethod, TestCategory("String representation")]
        public void ToString_Default_Culture_EN()
        {
            var p = new Pudelko(2.5, 9.321);
            string expectedStringEN = "2.500 m × 9.321 m × 0.100 m";

            Assert.AreEqual(expectedStringEN, p.ToString());
        }

        [DataTestMethod, TestCategory("String representation")]
        [DataRow(null, 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("m", 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("cm", 2.5, 9.321, 0.1, "250.0 cm × 932.1 cm × 10.0 cm")]
        [DataRow("mm", 2.5, 9.321, 0.1, "2500 mm × 9321 mm × 100 mm")]
        public void ToString_Formattable_Culture_EN(string format, double a, double b, double c, string expectedStringRepresentation)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Assert.AreEqual(expectedStringRepresentation, p.ToString(format));
        }

        [TestMethod, TestCategory("String representation")]
        [ExpectedException(typeof(FormatException))]
        public void ToString_Formattable_WrongFormat_FormatException()
        {
            var p = new Pudelko(1);
            var stringformatedrepreentation = p.ToString("wrong code");
        }

        #endregion ToString tests ===================================

        #region Volume/Area ======================================

        [DataTestMethod, TestCategory("Volume and area")]
        [DataRow(1.0, 2.543, 3.1, 7.883300000)]
        [DataRow(1.0, 2, 3.154648, 6.308000000)]
        [DataRow(1.0001, 2.54387, 3.1005, 7.883300000)]
        [DataRow(0.0011546, 6.5237, 3.54, 0.023091420)]
        public void PropertyVolume_WhenItIsDefaultMeters_ShouldReturnCorrectValue(double a, double b, double c, double expectedVolume)
        {
            Pudelko p = new Pudelko(a, b, c);
            Assert.AreEqual(expectedVolume, p.Volume);
        }

        [DataTestMethod, TestCategory("Volume and area")]
        [DataRow(1.0, 2.543, 3.1, 7.883300000)]
        [DataRow(1.0, 2, 3.154648, 6.308000000)]
        [DataRow(1.0001, 2.54387, 3.1005, 7.883300000)]
        [DataRow(0.0011546, 6.5237, 3.54, 0.023091420)]
        public void PropertyVolume_WhenUnitItIsMeters_ShouldReturnCorrectValue(double a, double b, double c, double expectedVolume)
        {
            Pudelko p = new Pudelko(a, b, c, UnitOfMeasure.meter);
            Assert.AreEqual(expectedVolume, p.Volume);
        }

        [DataTestMethod, TestCategory("Volume and area")]
        [DataRow(10, 243, 301.2, 0.731916000)]
        [DataRow(100.4323, 57.424, 985.33242123, 5.678244488)]
        [DataRow(210.011546, 652.37, 354, 48.491982000)]
        public void PropertyVolume_WhenUnitItIsCentimeter_ShouldReturnCorrectValue(double a, double b, double c, double expectedVolume)
        {
            Pudelko p = new Pudelko(a, b, c, UnitOfMeasure.centimeter);
            Assert.AreEqual(expectedVolume, p.Volume);
        }

        [DataTestMethod, TestCategory("Volume and area")]
        [DataRow(240, 3012.54, 3.21, 0.002168640)]
        [DataRow(2504.3, 5742, 9815.311, 141.119755920)]
        [DataRow(1, 210.011546, 8652.37, 0.001816920)]
        public void PropertyVolume_WhenUnitItIsMilimeter_ShouldReturnCorrectValue(double a, double b, double c, double expectedVolume)
        {
            Pudelko p = new Pudelko(a, b, c, UnitOfMeasure.milimeter);
            Assert.AreEqual(expectedVolume, p.Volume);
        }

        [DataTestMethod, TestCategory("Volume and area")]
        [DataRow(1.0, 2.543, 3.1, 27.052600)]
        [DataRow(1.0, 2, 3.154648, 22.924000)]
        [DataRow(1.0001, 2.54387, 3.1005, 27.052600)]
        [DataRow(0.0011546, 6.5237, 3.54, 46.202966)]
        public void PropertyArea_WhenItIsDefaultMeters_ShouldReturnCorrectValue(double a, double b, double c, double expectedVolume)
        {
            Pudelko p = new Pudelko(a, b, c);
            Assert.AreEqual(expectedVolume, p.Area);
        }

        [DataTestMethod, TestCategory("Volume and area")]
        [DataRow(1.0, 2.543, 3.1, 27.052600)]
        [DataRow(1.0, 2, 3.154648, 22.924000)]
        [DataRow(1.0001, 2.54387, 3.1005, 27.052600)]
        [DataRow(0.0011546, 6.5237, 3.54, 46.202966)]
        public void PropertyArea_WhenUnitItIsMeters_ShouldReturnCorrectValue(double a, double b, double c, double expectedVolume)
        {
            Pudelko p = new Pudelko(a, b, c, UnitOfMeasure.meter);
            Assert.AreEqual(expectedVolume, p.Area);
        }

        [DataTestMethod, TestCategory("Volume and area")]
        [DataRow(10, 243, 301.2, 15.726720)]
        [DataRow(100.4323, 57.424, 985.33242123, 32.248660)]
        [DataRow(210.011546, 652.37, 354, 88.447440)]
        public void PropertyArea_WhenUnitItIsCentimeter_ShouldReturnCorrectValue(double a, double b, double c, double expectedVolume)
        {
            Pudelko p = new Pudelko(a, b, c, UnitOfMeasure.centimeter);
            Assert.AreEqual(expectedVolume, p.Area);
        }

        [DataTestMethod, TestCategory("Volume and area")]
        [DataRow(240, 3012.54, 3.21, 1.465272)]
        [DataRow(2504.3, 5742, 9815.311, 190.624916)]
        [DataRow(1, 210.011546, 8652.37, 3.651564)]
        public void PropertyArea_WhenUnitItIsMilimeter_ShouldReturnCorrectValue(double a, double b, double c, double expectedVolume)
        {
            Pudelko p = new Pudelko(a, b, c, UnitOfMeasure.milimeter);
            Assert.AreEqual(expectedVolume, p.Area);
        }

        #endregion Volume/Area ======================================

        #region Equals ===========================================

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1, 2.1, 3.05, 1, 3.05, 2.1)]
        [DataRow(1, 2.1, 3.05, 2.1, 1, 3.05)]
        public void EqualsUnitDefault_WhenBoxesHasTheSameValueAndDifferentOrders_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C);
            Assert.IsTrue(p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1, 2.1, 3.05, 1, 3.05, 2.1)]
        [DataRow(1, 2.1, 3.05, 2.1, 1, 3.05)]
        public void EqualsUnitMeters_WhenBoxesHasTheSameValueAndDifferentOrders_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.meter);
            Assert.IsTrue(p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(100.0, 210, 305.000, 305, 210, 100)]
        [DataRow(990.2, 2.1, 35, 2.1, 990.20, 35)]
        public void EqualsUnitCentimeters_WhenBoxesHasTheSameValueAndDifferentOrders_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.centimeter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.centimeter);
            Assert.IsTrue(p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(1000, 305, 10, 305, 10, 1000)]
        [DataRow(2650, 4012, 543, 4012, 543, 2650)]
        public void EqualUnitMilimeters_WhenBoxesHasTheSameValueAndDifferentOrders_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.milimeter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.milimeter);
            Assert.IsTrue(p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(2.1, 1, 3.05, 305.0, 210.0, 100.0)]
        [DataRow(0.021, 1, 0.305, 100.0, 2.100, 30.5)]
        public void EqualsMetersAndCentimeters_WhenBoxesHasTheSameValueDifferentOrdersAndUnit_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.centimeter);
            Assert.IsTrue(p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(2.1, 1, 3.05, 1000, 2100, 3050)]
        [DataRow(1, 0.0021, 0.305, 2, 1000, 305)]
        public void EqualsMetersAndMilimeters_WhenBoxesHasTheSameValueDifferentOrdersAndUnit_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.milimeter);
            Assert.IsTrue(p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(2.1, 100, 30.5, 305, 21, 1000)]
        [DataRow(0.21, 1, 301.2, 2, 10, 3012)]
        public void EqualsCentimetersAndMilimeters_WhenBoxesHasTheSameValueDifferentOrdersAndUnit_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.centimeter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.milimeter);
            Assert.IsTrue(p1.Equals(p2));
        }

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(2.1, 1, 3.05, 210.0, 100.0, 305.0)]
        [DataRow(0.021, 1, 0.305, 2.100, 100.0, 30.5)]
        public void EqualsMetersAndMilimeters_WhenBoxesHasTheDifferentOrdersValueAndUnit_ShouldBeFalseAndReturnFalse(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.milimeter);
            Assert.IsFalse(p1.Equals(p2));
        }

        #endregion Equals ===========================================

        #region Operators overloading ===========================

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(2.1, 1, 3.05, 305.0, 210.0, 100.0)]
        [DataRow(0.021, 1, 0.305, 100.0, 2.100, 30.5)]
        public void OperatorEqualsMetersAndCentimeters_WhenBoxesHasTheSameValueDifferentOrdersAndUnit_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.centimeter);
            Assert.IsTrue(p1 == p2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(2.1, 1, 3.05, 1000, 2100, 3050)]
        [DataRow(1, 0.0021, 0.305, 2, 1000, 305)]
        public void OperatorEqualsMetersAndMilimeters_WhenBoxesHasTheSameValueDifferentOrdersAndUnit_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.milimeter);
            Assert.IsTrue(p1 == p2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(2.1, 100, 30.5, 305, 21, 1000)]
        [DataRow(0.21, 1, 301.2, 2, 10, 3012)]
        public void OperatorEqualsCentimetersAndMilimeters_WhenBoxesHasTheSameValueDifferentOrdersAndUnit_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.centimeter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.milimeter);
            Assert.IsTrue(p1 == p2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(2.1, 1.4, 3.05, 305.0, 210.0, 100.0)]
        [DataRow(0.021, 1, 3.605, 100.0, 2.100, 30.5)]
        public void OperatorNotEqualsMetersAndCentimeters_WhenBoxesHasTheSameValueDifferentOrdersAndUnit_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.centimeter);
            Assert.IsTrue(p1 != p2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(2.1, 1, 3.05, 1000, 2100, 3050)]
        [DataRow(1, 0.0021, 0.305, 2, 1000, 305)]
        public void OperatorNotEqualsMetersAndMilimeters_WhenBoxesHasTheSameValueDifferentOrdersAndUnit_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.milimeter);
            Assert.IsFalse(p1 != p2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(2.1, 100, 30.5, 305, 21, 1000)]
        [DataRow(0.21, 1, 301.2, 2, 10, 3012)]
        public void OperatorNotEqualsCentimetersAndMilimeters_WhenBoxesHasTheSameValueDifferentOrdersAndUnit_ShouldReturnTrue(double p1A, double p1B, double p1C, double p2A, double p2B, double p2C)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.centimeter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.milimeter);
            Assert.IsFalse(p1 != p2);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(2.5, 5.3, 9, 3.1, 4.5, 1, 132.500000)]
        [DataRow(2.6, 1.2, 0.0125, 0.2, 0.15, 0.10, 0.313440)]
        [DataRow(3.2, 2.2, 0.43, 0.012, 1.5, 0.130, 3.038552)]
        public void OperatorPlus_WhenAddedTwoBoxesInMeters_ShouldReturnSmallestBoundingBoxVolume(
            double p1A, double p1B, double p1C,
            double p2A, double p2B, double p2C,
            double expectedResult)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.meter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.meter);
            Pudelko p3 = p1 + p2;
            Assert.AreEqual(expectedResult, p3.Volume);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(250, 530, 900, 310, 450, 100, 132.5)]
        [DataRow(260, 120, 1.25, 20, 15, 10, 0.313440)]
        [DataRow(320, 220, 43, 1.2, 150, 13.0, 3.038552)]
        public void OperatorPlus_WhenAddedTwoBoxesInCentimeters_ShouldReturnSmallestBoundingBoxVolume(
            double p1A, double p1B, double p1C,
            double p2A, double p2B, double p2C,
            double expectedResult)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.centimeter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.centimeter);
            Pudelko p3 = p1 + p2;
            Assert.AreEqual(expectedResult, p3.Volume);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(2500, 5300, 9000, 3100, 4500, 1000, 132.5)]
        [DataRow(2600, 1200, 12.5, 200, 150, 100, 0.313440)]
        [DataRow(3200, 2200, 430, 12, 1500, 130, 3.038552)]
        public void OperatorPlus_WhenAddedTwoBoxesInMilimeters_ShouldReturnSmallestBoundingBoxVolume(
            double p1A, double p1B, double p1C,
            double p2A, double p2B, double p2C,
            double expectedResult)
        {
            Pudelko p1 = new Pudelko(p1A, p1B, p1C, UnitOfMeasure.milimeter);
            Pudelko p2 = new Pudelko(p2A, p2B, p2C, UnitOfMeasure.milimeter);
            Pudelko p3 = p1 + p2;
            Assert.AreEqual(expectedResult, p3.Volume);
        }

        #endregion Operators overloading ===========================

        #region Conversions =====================================

        [TestMethod]
        public void ExplicitConversion_ToDoubleArray_AsMeters()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            double[] tab = (double[])p;
            Assert.AreEqual(3, tab.Length);
            Assert.AreEqual(p.A, tab[0]);
            Assert.AreEqual(p.B, tab[1]);
            Assert.AreEqual(p.C, tab[2]);
        }

        [TestMethod]
        public void ImplicitConversion_FromAalueTuple_As_Pudelko_InMilimeters()
        {
            var (a, b, c) = (2500, 9321, 100); // in milimeters, ValueTuple
            Pudelko p = (a, b, c);
            Assert.AreEqual((int)(p.A * 1000), a);
            Assert.AreEqual((int)(p.B * 1000), b);
            Assert.AreEqual((int)(p.C * 1000), c);
        }

        #endregion Conversions =====================================

        #region Indexer, enumeration ============================

        [TestMethod]
        public void Indexer_ReadFrom()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            Assert.AreEqual(p.A, p[0]);
            Assert.AreEqual(p.B, p[1]);
            Assert.AreEqual(p.C, p[2]);
        }

        [TestMethod]
        public void ForEach_Test()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            var tab = new[] { p.A, p.B, p.C };
            int i = 0;
            foreach (double x in p)
            {
                Assert.AreEqual(x, tab[i]);
                i++;
            }
        }

        #endregion Indexer, enumeration ============================

        #region Parsing =========================================

        #endregion Parsing =========================================
    }
}
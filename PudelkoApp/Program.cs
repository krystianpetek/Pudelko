using PudelkoLib;

namespace PudelkoApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // 3
            Pudelko pudlo = new Pudelko(15.1321, 123.432, 1234.132141, Pudelko.UnitOfMeasure.milimeter);
            Console.WriteLine("Punkt 3");
            Console.WriteLine($"A: {pudlo.A}\nB: {pudlo.B}\nC: {pudlo.C}\n");

            // 4
            Pudelko pudlo4_1 = new Pudelko(15.1321, 123.432, 1234.132141, Pudelko.UnitOfMeasure.milimeter);
            Console.WriteLine("Punkt 4");
            Console.WriteLine($"{pudlo.ToString("m")}");
            Console.WriteLine($"{pudlo.ToString("cm")}");
            Console.WriteLine($"{pudlo.ToString("mm")}\n");

            // 5
            Console.WriteLine("Punkt 5");
            Console.WriteLine($"{pudlo.VolumeWithUnit}\n");

            // 6
            Console.WriteLine("Punkt 6");
            Console.WriteLine($"{pudlo.AreaWithUnit}\n");

            // 7
            Console.WriteLine("Punkt 7");
            Pudelko pudlo7_1 = new Pudelko(1, 2.1, 3.05);
            Pudelko pudlo7_2 = new Pudelko(1, 3.05, 2.1);
            Pudelko pudlo7_3 = new Pudelko(2.1, 1, 3.05);
            Pudelko pudlo7_4 = new Pudelko(2100, 1000, 3050, Pudelko.UnitOfMeasure.milimeter);
            Console.WriteLine($"1, 2.1, 3.05 == 1, 3.05, 2.1 {pudlo7_1.Equals(pudlo7_2)}");
            Console.WriteLine($"1, 3.05, 2.1 == 2.1, 1, 3.05 {pudlo7_2.Equals(pudlo7_3)}");
            Console.WriteLine($"2.1, 1, 3.05 == 2100, 1000, 3050 {pudlo7_3.Equals(pudlo7_4)}");
            Console.WriteLine($"2100, 1000, 3050 == 1, 3.05, 2.1 {pudlo7_4.Equals(pudlo7_2)}\n");

            // 8
            Console.WriteLine("Punkt 8");
            Pudelko pudlo8_1 = new Pudelko(2.5, 5.3, 9);
            Pudelko pudlo8_2 = new Pudelko(3.1, 4.5, 1);
            Pudelko pudlo8_3 = pudlo8_1 + pudlo8_2;
            Console.WriteLine($"p1:        {pudlo8_1}");
            Console.WriteLine($"p2:        {pudlo8_2}");
            Console.WriteLine($"p1 + p2:   {pudlo8_3}\n");

            // 9
            Console.WriteLine("Punkt 9");
            double[] tablicaWartosciPudelka = (double[])pudlo;
            ValueTuple<int, int, int> tuple = new(2311, 12, 648);
            Pudelko pudlo9 = tuple;
            Console.WriteLine(tuple);
            Console.WriteLine($"{pudlo9}\n");

            // 10
            Console.WriteLine("Punkt 10");
            Pudelko pudlo10 = new Pudelko(1, 2.1, 3.231);
            Console.WriteLine($"pudlo10[1] == {pudlo10[1]}\n");

            // 12
            Console.WriteLine("Punkt 11");
            Pudelko pudlo11 = new Pudelko(1, 2.1, 3.231);
            foreach (double p in pudlo11)
                Console.WriteLine(p);
            Console.WriteLine();

            // 12
            Console.WriteLine("Punkt 12");
            Pudelko pudlo12 = Pudelko.Parse("2.500 m × 9.321 m × 0.100 m");
            Console.WriteLine("string przed parsowaniem: 2.500 m × 9.321 m × 0.100 m");
            Console.WriteLine($"po parsowaniu (instancja): {pudlo12}\n");

            // 15
            Console.WriteLine("Punkt 15");
            Pudelko pudlo15_1 = new Pudelko(2.6, 1.2, 0.5);
            Pudelko pudlo15_2 = pudlo15_1.Kompresuj();
            Console.WriteLine($"Pudelko wejsciowe:              {pudlo15_1} { pudlo15_1.VolumeWithUnit}");
            Console.WriteLine($"Zbliżony dlugościami sześcian:  {pudlo15_2} { pudlo15_2.VolumeWithUnit}");
            Console.WriteLine("Różnica objętości polega na przechowywaniu w instancji długości boków z dokładnością do 3 miejsc\n");

            // 16
            Console.WriteLine("Punkt 16");
            Pudelko p1 = new Pudelko(2.5, 9.321, 0.1);
            Pudelko p2 = new Pudelko(1, 2.1, 3.05);
            Pudelko p3 = new Pudelko(2.1, 1, 3.05);
            Pudelko p4 = new Pudelko(2100, 1000, 3050, Pudelko.UnitOfMeasure.milimeter);
            Pudelko p5 = new Pudelko(20, 15, 10, Pudelko.UnitOfMeasure.centimeter);
            Pudelko p6 = new Pudelko(8.6, 5.5, 2.5, Pudelko.UnitOfMeasure.centimeter);
            Pudelko p7 = new Pudelko(2.6, 1.2, 0.0125);
            Pudelko p8 = new Pudelko(0.2, 0.15, 0.1);
            Pudelko p9 = new Pudelko(8.6, 3.5, 2.5);
            Pudelko p10 = new Pudelko(10, 10, 10);
            Pudelko p11 = Pudelko.Parse("2500 mm × 912 mm × 100 mm");
            Pudelko p12 = new Pudelko(32, 22, 43, Pudelko.UnitOfMeasure.milimeter);
            Pudelko p13 = new Pudelko(2, 5.4, 7.6, Pudelko.UnitOfMeasure.centimeter);
            Pudelko p14 = p12 + p13;
            Pudelko p15 = p14.Kompresuj();
            List<Pudelko> listaPudelek = new List<Pudelko>() { p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15 };

            int licznik = 0;
            foreach (Pudelko pudelko in listaPudelek)
            {
                Console.WriteLine($"{++licznik,2}.{"",1} {pudelko,-35} Volume: {pudelko.VolumeWithUnit}");
            }
            listaPudelek.Sort(Pudelko.BoxComparison);
            Console.WriteLine("\nPo sortowaniu");
            licznik = 0;
            foreach (Pudelko pudelko in listaPudelek)
            {
                Console.WriteLine($"{++licznik,2}.{"",1} {pudelko,-35} Volume: {pudelko.VolumeWithUnit}");
            }
        }
    }
}
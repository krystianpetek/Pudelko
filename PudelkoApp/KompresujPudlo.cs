using PudelkoLib;

namespace PudelkoApp
{
    public static class KompresujPudlo
    {
        public static Pudelko Kompresuj(this Pudelko x)
        {
            double rozklad = Math.Pow(x.Volume, (1.0 / 3.0));
            //var volumeOfNewBox = Math.Round(rozklad * rozklad * rozklad,6);
            //bool TheBoxItIsEquals = (x.Volume == volumeOfNewBox);
            return new Pudelko(rozklad, rozklad, rozklad, x.Measure);
        }
    }
}
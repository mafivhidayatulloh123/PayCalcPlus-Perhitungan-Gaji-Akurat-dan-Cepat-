namespace PayCalcPlus.Models
{
    public class GajiModel
    {
        public string Jabatan { get; set; }
        public decimal GajiPokok { get; set; }
        public decimal Tunjangan { get; set; }
        public decimal GajiBersih => GajiPokok + Tunjangan;
    }
}

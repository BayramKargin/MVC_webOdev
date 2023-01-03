using MessagePack;

namespace WebProgramlamaOdev2.Models
{
    public class Urunler
    {
        
        public int Id { get; set; }
        public string? UrunAdi { get; set; }
        public string? UrunAcıklama { get; set; }
        public int UrunFiyat { get; set; }

    }
}

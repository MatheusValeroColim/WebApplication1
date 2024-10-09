using MongoDB.Bson;

namespace WebApplication1.Domain
{
    public class Suco
    {
        public ObjectId Id { get; set; }
        public string Sabor { get; set; }
        public string Descricao { get; set; }
    }
}

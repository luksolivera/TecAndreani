namespace Domain.Entitiies
{
    public class Geolocalizacion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Cuidad { get; set; }
        public string Codigo_Postal { get; set; }
        public string Provincia { get; set; }
        public string Pais { get; set; }
        public string Latitud { get; set; }
        public string Longuitud { get; set; }
        public int EstadoId { get; set; }

        public virtual Estado Estado { get; set; }

    }
}

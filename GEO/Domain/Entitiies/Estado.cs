using System.Collections.Generic;

namespace Domain.Entitiies
{
    public class Estado
    {
        public int EstadoId { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Geolocalizacion> Geolocalizacion { get; set; }
    }
}

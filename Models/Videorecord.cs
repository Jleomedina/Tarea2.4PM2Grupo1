using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea2._4PM2Grupo1.Models
{
    public class Videorecord
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        public string nombres { get; set; }

        public string descripcion { get; set; }

        public byte[] imagen { get; set; }
    }
}

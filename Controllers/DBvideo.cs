using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tarea2._4PM2Grupo1.Models;
using SQLite;

namespace Tarea2._4PM2Grupo1.Controllers
{
    public class DBvideo
    {
        readonly SQLiteAsyncConnection dbase;

        public DBvideo(String dbpath)
        {
            dbase = new SQLiteAsyncConnection(dbpath);

            //Crearemos las tablas de la base de datos 
            dbase.CreateTableAsync<Videorecord>(); //Creado la tabla de Empleado
        }
        #region OperacionesDatos
        // CRUD - Create - Read - Update - Delete
        // Create
        public Task<int> VideoSave(Videorecord video)
        {
            if (video.codigo != 0)
            {
                return dbase.UpdateAsync(video); // Update
            }
            else
            {
                return dbase.InsertAsync(video); ;// Insert
            }
        }

        // Read
        public Task<List<Videorecord>> obtenerListaVideo()
        {
            return dbase.Table<Videorecord>().ToListAsync();
        }

        // Read V2
        public Task<Videorecord> obtenerVideo(int pid)
        {
            return dbase.Table<Videorecord>()
                .Where(i => i.codigo == pid)
                .FirstOrDefaultAsync();
        }

        // Delete
        public Task<int> VideoDelete(Videorecord emple)
        {
            return dbase.DeleteAsync(emple);
        }

        #endregion OperacionesDatos
    }
}

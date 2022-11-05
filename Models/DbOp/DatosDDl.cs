using Newtonsoft.Json;
using ReportesUdec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace ReportesUdec.DbOp
{
    public class DatosDDl
    {
        public string ListaEvento()
        {
            using (var db = new UdCReportEntities())
            {
                var Data = db.Evento.ToList();
                var Json = JsonConvert.SerializeObject(Data);
                return Json;
            }
        }

        public string ListaTipo(int EventoId)
        {
            using (var db = new UdCReportEntities())
            {
                var Data = db.Tipo.Where(o => o.idEvento == EventoId).ToList();
                var Json = JsonConvert.SerializeObject(Data);
                return Json;
            }
        }




        public string ListaZona()
        {
            using (var db = new UdCReportEntities())
            {
                var Data = db.Zona.ToList();
                var Json = JsonConvert.SerializeObject(Data);
                return Json;
            }
        }

        public string ListaLugar(int ZonaId)
        {
            using (var db = new UdCReportEntities())
            {
                var Data = db.Lugar.Where(o => o.idZona == ZonaId).ToList();
                var Json = JsonConvert.SerializeObject(Data);
                return Json;
            }
        }

        public string ListaAmbiente(int LugarId)
        {
            using (var db = new UdCReportEntities())
            {
                var Data = db.Ambiente.Where(o => o.idLugar == LugarId).ToList();
                var Json = JsonConvert.SerializeObject(Data);
                return Json;
            }
        }
    }
}

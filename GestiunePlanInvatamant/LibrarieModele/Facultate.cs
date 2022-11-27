using System;
using System.Data;

namespace LibrarieModele
{
    public class Facultate
    {
        public int IdFacultate { get; set; }
        public string Nume { get; set; }
        public DateTime AnInfiintare { get; set; }
        public string Domeniu { get; set; }
        public string EsteSters { get; set; }

        public Facultate()
        {
        }

        public Facultate(string name, DateTime founded, string domeniu)
        {
            Nume = name;
            AnInfiintare = founded;
            Domeniu = domeniu;
            EsteSters = "Nu";
        }

        public Facultate(DataRow linieBD)
        {
            IdFacultate = Convert.ToInt32(linieBD["idFacultate"].ToString());
            Nume = linieBD["nume"].ToString();
            AnInfiintare = Convert.ToDateTime(linieBD["anInfiintare"].ToString());
            Domeniu = linieBD["domeniu"].ToString();
            EsteSters = linieBD["esteSters"].ToString();
        }
    }
}

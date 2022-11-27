using System;
using System.Data;

namespace LibrarieModele
{
    public class ProgramDeStudiu
    {
        public int IdProgramDeStudiu { get; set; }
        public int IdFacultate { get; set; }

        public string Nume { get; set; }
        public int DurataStudiilor { get; set; }
        public string FormaInvatamant { get; set; }
        public string EsteSters { get; set; }

        public virtual Facultate Facultate { get; set; }

        public ProgramDeStudiu()
        {
        }

        public ProgramDeStudiu(int idFacultate, string nume, int durataStudiilor, string formaDeInvatamant)
        {
            IdFacultate = idFacultate;
            Nume = nume;
            DurataStudiilor = durataStudiilor;
            FormaInvatamant = formaDeInvatamant;
            EsteSters = "Nu";
        }

        public ProgramDeStudiu(DataRow linieBD)
        {
            IdProgramDeStudiu = Convert.ToInt32(linieBD["idProgramDeStudiu"].ToString());
            IdFacultate = Convert.ToInt32(linieBD["idFacultate"].ToString());
            Nume = linieBD["nume"].ToString();
            DurataStudiilor = Convert.ToInt32(linieBD["durataStudiilor"].ToString());
            FormaInvatamant = linieBD["formaInvatamant"].ToString();
            EsteSters = linieBD["esteSters"].ToString();
        }
    }
}

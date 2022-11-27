using System;
using System.Data;

namespace LibrarieModele
{
    public class PlanInvatamant
    {
        public int IdPlanInvatamant { get; set; }
        public int IdProgramDeStudiu { get; set; }

        public string Nume { get; set; }
        public DateTime Valabilitate { get; set; }
        public string TitluAbsolvire { get; set; }
        public string NivelDeStudiu { get; set; }
        public string CompetenteProfesionale { get; set; }
        public DateTime An { get; set; }
        public string EsteSters { get; set; }

        public virtual ProgramDeStudiu ProgramDeStudiu { get; set; }

        public PlanInvatamant()
        {
        }

        public PlanInvatamant(int idProgramDeStudiu, string nume, DateTime valabilitate, string titluAbsolvire, string nivelDeStudiu, string competenteProfesionale, DateTime an)
        {
            IdProgramDeStudiu = idProgramDeStudiu;
            Nume = nume;
            Valabilitate = valabilitate;
            TitluAbsolvire = titluAbsolvire;
            NivelDeStudiu = nivelDeStudiu;
            CompetenteProfesionale = competenteProfesionale;
            An = an;
            EsteSters = "Nu";
        }

        public PlanInvatamant(DataRow linieBD)
        {
            IdPlanInvatamant = Convert.ToInt32(linieBD["idPlanInvatamant"].ToString());
            IdProgramDeStudiu = Convert.ToInt32(linieBD["idProgramDeStudiu"].ToString());
            Nume = linieBD["nume"].ToString();
            Valabilitate = Convert.ToDateTime(linieBD["valabilitate"].ToString());
            TitluAbsolvire = linieBD["titluAbsolvire"].ToString();
            NivelDeStudiu = linieBD["niveldeStudiu"].ToString();
            CompetenteProfesionale = linieBD["competenteProfesionale"].ToString();
            An = Convert.ToDateTime(linieBD["an"].ToString());
            EsteSters = linieBD["esteSters"].ToString();
        }
    }
}

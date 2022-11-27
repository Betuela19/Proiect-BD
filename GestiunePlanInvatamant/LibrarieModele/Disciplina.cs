using System;
using System.Data;

namespace LibrarieModele
{
    public class Disciplina
    {
        public int IdDisciplina { get; set; }
        public int IdPlanInvatamant { get; set; }

        public string Nume { get; set; }
        public string TipDisciplina { get; set; }
        public int NumarCredite { get; set; }
        public string An { get; set; }
        public int Semestru { get; set; }
        public string CodDisciplina { get; set; }
        public int NumarOreSeminar { get; set; }
        public int NumarOreLaborator { get; set; }
        public int NumarOreCurs { get; set; }
        public string FormaDeVerificare { get; set; }
        public int TotalOreStudiuIndividual { get; set; }
        public string EsteSters { get; set; }

        public virtual PlanInvatamant PlanInvatamant { get; set; }

        public Disciplina(
            int idPlanInvatamant, 
            string nume, 
            string tipDisciplina, 
            int numarCredite, 
            string an, 
            int semestru, 
            string codDisciplina, 
            int numarOreSeminar, 
            int numarOreLaborator, 
            int numarOreCurs, 
            string formaDeVerificare, 
            int totalOreStudiuIndividual)
        {
            IdPlanInvatamant = idPlanInvatamant;
            Nume = nume;
            TipDisciplina = tipDisciplina;
            NumarCredite = numarCredite;
            An = an;
            Semestru = semestru;
            CodDisciplina = codDisciplina;
            NumarOreSeminar = numarOreSeminar;
            NumarOreLaborator = numarOreLaborator;
            NumarOreCurs = numarOreCurs;
            FormaDeVerificare = formaDeVerificare;
            TotalOreStudiuIndividual = totalOreStudiuIndividual;
            EsteSters = "Nu";
        }

        public Disciplina()
        {
        }

        public Disciplina(DataRow linieBD)
        {
            IdDisciplina = Convert.ToInt32(linieBD["idDisciplina"].ToString());
            IdPlanInvatamant = Convert.ToInt32(linieBD["idPlanInvatamant"].ToString());
            Nume = linieBD["nume"].ToString();
            TipDisciplina = linieBD["tipDisciplina"].ToString();
            NumarCredite = Convert.ToInt32(linieBD["numarCredite"].ToString());
            An = linieBD["an"].ToString();
            Semestru = Convert.ToInt32(linieBD["semestru"].ToString());
            CodDisciplina = linieBD["codDisciplina"].ToString();
            NumarOreSeminar = Convert.ToInt32(linieBD["numarOreSeminar"].ToString());
            NumarOreLaborator = Convert.ToInt32(linieBD["numarOreLaborator"].ToString());
            NumarOreCurs = Convert.ToInt32(linieBD["numarOreCurs"].ToString());
            FormaDeVerificare = linieBD["formaDeVerificare"].ToString();
            TotalOreStudiuIndividual = Convert.ToInt32(linieBD["totalOreStudiuIndividual"].ToString());
            EsteSters = linieBD["esteSters"].ToString();
        }
    }
}

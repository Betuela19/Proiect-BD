using LibrarieModele;
using NivelAccesDate;
using System;
using System.Configuration;

namespace InterfataUtilizator
{
    /// <summary>
    /// Factory Design Pattern
    /// </summary>
    public class StocareFactory
    {
        public IStocareFactory GetTipStocare(Type tipEntitate)
        {
            var formatSalvare = ConfigurationManager.AppSettings["FormatSalvare"];
            if (formatSalvare != null)
            {
                switch (formatSalvare)
                {
                    default:
                    case "BazaDateOracle":
                        if (tipEntitate == typeof(Disciplina))
                        {
                            return new AdministrareDiscipline();
                        }
                        if (tipEntitate == typeof(Facultate))
                        {
                            return new AdministrareFacultati();
                        }
                        if (tipEntitate == typeof(ProgramDeStudiu))
                        {
                            return new AdministrareProgrameDeStudiu();
                        }
                        if (tipEntitate == typeof(PlanInvatamant))
                        {
                            return new AdministrarePlanDeInvatamant();
                        }
                        break;

                    case "BIN":
                        //instantiere clase care realizeaza salvarea in fisier binar
                        break;
                }
            }
            return null;
        }
    }
}

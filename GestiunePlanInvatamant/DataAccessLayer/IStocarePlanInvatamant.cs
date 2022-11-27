using LibrarieModele;
using System;
using System.Collections.Generic;

namespace NivelAccesDate
{
    public interface IStocarePlanInvatamant : IStocareFactory
    {
        List<PlanInvatamant> GetPlanuriInvatamant();
        PlanInvatamant GetPlanInvatamant(int idPlanInvatamant);
        bool AddPlanInvatamant(PlanInvatamant planDeInvatamant);
        bool UpdatePlanInvatamant(PlanInvatamant plantDeInvatamant);
        bool DeletePlanInvatamant(int idPlanInvatamant);
        bool SoftDeletePlanInvatamant(int idPlanInvatamant);
    }
}

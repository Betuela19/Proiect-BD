using LibrarieModele;
using System.Collections.Generic;

namespace NivelAccesDate
{
    public interface IStocareDiscipline : IStocareFactory
    {
        List<Disciplina> GetDiscipline();
        List<Disciplina> GetDiscipline(int idPlanInvatamant);
        Disciplina GetDisciplina(int idDisciplina);
        bool AddDisciplina(Disciplina disciplina);
        bool UpdateDisciplina(Disciplina disciplina);
        bool DeleteDisciplina(int idDisciplina);
        bool SoftDeleteDisciplina(int idDisciplina);
    }
}

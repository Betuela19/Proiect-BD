using LibrarieModele;
using System.Collections.Generic;

namespace NivelAccesDate
{
    public interface IStocareFacultati : IStocareFactory
    {
        List<Facultate> GetFacultati();
        Facultate GetFacultate(int idFacultate);
        bool AddFacultate(Facultate facultate);
        bool UpdateFacultate(Facultate facultate);
        bool DeleteFacultate(int idFacultate);
        bool SoftDeletefacultate(int idFacultate);
    }
}

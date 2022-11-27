using LibrarieModele;
using System.Collections.Generic;

namespace NivelAccesDate
{
    public interface IStocareProgrameDeStudiu : IStocareFactory
    {
        List<ProgramDeStudiu> GetProgrameDeStudiu();
        ProgramDeStudiu GetProgramStudiu(int idProgramDeStudiu);
        bool AddProgramStudiu(ProgramDeStudiu programDeStudiu);
        bool UpdateProgramDeStudiu(ProgramDeStudiu programDeStudiu);
        bool DeleteProgramDeStudiu(int idProgramDeStudiu);
        bool SoftDeleteProgramDeStudiu(int idProgramDeStudiu);
    }
}

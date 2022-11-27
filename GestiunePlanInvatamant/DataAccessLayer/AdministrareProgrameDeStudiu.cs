using LibrarieModele;
using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace NivelAccesDate
{
    public class AdministrareProgrameDeStudiu : IStocareProgrameDeStudiu
    {
        private const int PRIMUL_TABEL = 0;
        private const int PRIMA_LINIE = 0;

        public bool AddProgramStudiu(ProgramDeStudiu activitate)
        {
            var EsteSters = "Nu";

            return SqlDBHelper.ExecuteNonQuery(
                "INSERT INTO ProgramDeStudiu_CB VALUES (seq_ProgramDeStudiu_CB.nextval, :IdFacultate, :Nume, :DurataStudiilor, :FormaInvatamant, :EsteSters)", 
                CommandType.Text,
                new OracleParameter(":IdFacultate", OracleDbType.Int32, activitate.IdFacultate, ParameterDirection.Input),
                new OracleParameter(":Nume", OracleDbType.NVarchar2, activitate.Nume, ParameterDirection.Input),
                new OracleParameter(":DurataStudiilor", OracleDbType.Int32, activitate.DurataStudiilor, ParameterDirection.Input),
                new OracleParameter(":FormaInvatamant", OracleDbType.NVarchar2, activitate.FormaInvatamant, ParameterDirection.Input),
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));;
        }

        public ProgramDeStudiu GetProgramStudiu(int idProgramDeStudiu)
        {
            ProgramDeStudiu result = null;

            var EsteSters = "Nu";

            var dsProgrameDeStudiu = SqlDBHelper.ExecuteDataSet("SELECT * FROM ProgramDeStudiu_CB WHERE IdProgramDeStudiu = :IdProgramDeStudiu AND EsteSters = :EsteSters", CommandType.Text,
                new OracleParameter(":IdProgramDeStudiu", OracleDbType.Int32, idProgramDeStudiu, ParameterDirection.Input),
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));

            if (dsProgrameDeStudiu.Tables[PRIMUL_TABEL].Rows.Count > 0)
            {
                DataRow linieBD = dsProgrameDeStudiu.Tables[PRIMUL_TABEL].Rows[PRIMA_LINIE];
                result = new ProgramDeStudiu(linieBD);

                result.Facultate = new AdministrareFacultati().GetFacultate(result.IdFacultate);
            }

            return result;
        }

        public List<ProgramDeStudiu> GetProgrameDeStudiu()
        {
            var result = new List<ProgramDeStudiu>();

            var EsteSters = "Nu";

            var dsActivitateuri = SqlDBHelper.ExecuteDataSet("SELECT * FROM ProgramDeStudiu_CB WHERE EsteSters = :EsteSters", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));

            foreach (DataRow linieBD in dsActivitateuri.Tables[PRIMUL_TABEL].Rows)
            {
                var programDeStudiu = new ProgramDeStudiu(linieBD);
                programDeStudiu.Facultate = new AdministrareFacultati().GetFacultate(programDeStudiu.IdFacultate);

                result.Add(programDeStudiu);
            }

            return result;
        }

        public bool UpdateProgramDeStudiu(ProgramDeStudiu programDeStudiu)
        {
            return SqlDBHelper.ExecuteNonQuery(
                "UPDATE ProgramDeStudiu_CB SET IdFacultate = :IdFacultate, Nume = :Nume, " +
                "DurataStudiilor = :DurataStudiilor, FormaInvatamant = :FormaInvatamant WHERE IdProgramDeStudiu = :IdProgramDeStudiu", CommandType.Text,
                new OracleParameter(":IdFacultate", OracleDbType.Int32, programDeStudiu.IdFacultate, ParameterDirection.Input),
                new OracleParameter(":Nume", OracleDbType.NVarchar2, programDeStudiu.Nume, ParameterDirection.Input),
                new OracleParameter(":DurataStudiilor", OracleDbType.Int32, programDeStudiu.DurataStudiilor, ParameterDirection.Input),
                new OracleParameter(":FormaInvatamant", OracleDbType.NVarchar2, programDeStudiu.FormaInvatamant, ParameterDirection.Input),
                new OracleParameter(":IdProgramDeStudiu", OracleDbType.Int32, programDeStudiu.IdProgramDeStudiu, ParameterDirection.Input));
        }

        public bool DeleteProgramDeStudiu(int idProgramDeStudiu)
        {
            SqlDBHelper.ExecuteNonQuery("DELETE FROM ProgramDeStudiu_CB WHERE IdProgramDeStudiu = :IdProgramDeStudiu", CommandType.Text,
                new OracleParameter(":IdProgramDeStudiu", OracleDbType.Int32, idProgramDeStudiu, ParameterDirection.Input));

            return SqlDBHelper.ExecuteNonQuery("DELETE FROM ProgramDeStudiu_CB WHERE IdProgramDeStudiu = :IdProgramDeStudiu", CommandType.Text,
                new OracleParameter(":IdProgramDeStudiu", OracleDbType.Int32, idProgramDeStudiu, ParameterDirection.Input));
        }

        public bool SoftDeleteProgramDeStudiu(int idProgramDeStudiu)
        {
            var EsteSters = "Da";

            SqlDBHelper.ExecuteNonQuery(
                "UPDATE PlanInvatamant_CB SET EsteSters = :EsteSters WHERE IdProgramDeStudiu = :IdProgramDeStudiu", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input),
                new OracleParameter(":IdProgramDeStudiu", OracleDbType.Int32, idProgramDeStudiu, ParameterDirection.Input));

            return SqlDBHelper.ExecuteNonQuery(
                "UPDATE ProgramDeStudiu_CB SET EsteSters = :EsteSters WHERE IdProgramDeStudiu = :IdProgramDeStudiu", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input),
                new OracleParameter(":IdProgramDeStudiu", OracleDbType.Int32, idProgramDeStudiu, ParameterDirection.Input));
        }
    }
}

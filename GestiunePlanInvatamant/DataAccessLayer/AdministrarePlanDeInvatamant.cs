using LibrarieModele;
using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace NivelAccesDate
{
    public class AdministrarePlanDeInvatamant : IStocarePlanInvatamant
    {
        private const int PRIMUL_TABEL = 0;
        private const int PRIMA_LINIE = 0;

        public bool AddPlanInvatamant(PlanInvatamant planDeInvatamant)
        {
            var EsteSters = "Nu";

            return SqlDBHelper.ExecuteNonQuery(
                "INSERT INTO PlanInvatamant_CB VALUES (seq_PlanInvatamant_CB.nextval, :IdProgramDeStudiu, :Nume, :Valabilitate, :TitluAbsolvire, :NivelDeStudiu, :CompetenteProfesionale, :An, :EsteSters)", CommandType.Text,
                new OracleParameter(":IdProgramDeStudiu", OracleDbType.Int32, planDeInvatamant.IdProgramDeStudiu, ParameterDirection.Input),
                new OracleParameter(":Nume", OracleDbType.NVarchar2, planDeInvatamant.Nume, ParameterDirection.Input),
                new OracleParameter(":Valabilitate", OracleDbType.Date, planDeInvatamant.Valabilitate, ParameterDirection.Input),
                new OracleParameter(":TitluAbsolvire", OracleDbType.NVarchar2, planDeInvatamant.TitluAbsolvire, ParameterDirection.Input),
                new OracleParameter(":NivelDeStudiu", OracleDbType.NVarchar2, planDeInvatamant.TitluAbsolvire, ParameterDirection.Input),
                new OracleParameter(":CompetenteProfesionale", OracleDbType.NVarchar2, planDeInvatamant.CompetenteProfesionale, ParameterDirection.Input),
                new OracleParameter(":An", OracleDbType.Date, planDeInvatamant.An, ParameterDirection.Input),
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));
        }

        public PlanInvatamant GetPlanInvatamant(int idPlanInvatamant)
        {
            PlanInvatamant result = null;

            var EsteSters = "Nu";

            var dsPlanInvatamante = SqlDBHelper.ExecuteDataSet("SELECT * FROM PlanInvatamant_CB where IdPlanInvatamant = :IdPlanInvatamant AND EsteSters = :EsteSters", CommandType.Text,
                new OracleParameter(":IdPlanInvatamant", OracleDbType.Int32, idPlanInvatamant, ParameterDirection.Input),
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));

            if (dsPlanInvatamante.Tables[PRIMUL_TABEL].Rows.Count > 0)
            {
                DataRow linieBD = dsPlanInvatamante.Tables[PRIMUL_TABEL].Rows[PRIMA_LINIE];
                result = new PlanInvatamant(linieBD);

                result.ProgramDeStudiu = new AdministrareProgrameDeStudiu().GetProgramStudiu(result.IdProgramDeStudiu);
            }

            return result;
        }

        public List<PlanInvatamant> GetPlanuriInvatamant()
        {
            var result = new List<PlanInvatamant>();

            var EsteSters = "Nu";

            var dsPlanInvatamante = SqlDBHelper.ExecuteDataSet("SELECT * FROM PlanInvatamant_CB WHERE EsteSters = :EsteSters", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));

            foreach (DataRow linieBD in dsPlanInvatamante.Tables[PRIMUL_TABEL].Rows)
            {
                var planDeInvatamant = new PlanInvatamant(linieBD);
                planDeInvatamant.ProgramDeStudiu = new AdministrareProgrameDeStudiu().GetProgramStudiu(planDeInvatamant.IdProgramDeStudiu);

                result.Add(planDeInvatamant);
            }

            return result;
        }

        public bool UpdatePlanInvatamant(PlanInvatamant planDeInvatamant)
        { 
            return SqlDBHelper.ExecuteNonQuery(
                "UPDATE PlanInvatamant_CB SET IdProgramDeStudiu = :IdProgramDeStudiu, Nume = :Nume, Valabilitate = :Valabilitate, TitluAbsolvire = :TitluAbsolvire, " +
                "NivelDeStudiu = :NivelDeStudiu, CompetenteProfesionale = :CompetenteProfesionale, An = :An WHERE IdPlanInvatamant = :IdPlanInvatamant", CommandType.Text,
                new OracleParameter(":IdProgramDeStudiu", OracleDbType.Int32, planDeInvatamant.IdProgramDeStudiu, ParameterDirection.Input),
                new OracleParameter(":Nume", OracleDbType.NVarchar2, planDeInvatamant.Nume, ParameterDirection.Input),
                new OracleParameter(":Valabilitate", OracleDbType.Date, planDeInvatamant.Valabilitate, ParameterDirection.Input),
                new OracleParameter(":TitluAbsolvire", OracleDbType.NVarchar2, planDeInvatamant.TitluAbsolvire, ParameterDirection.Input),
                new OracleParameter(":NivelDeStudiu", OracleDbType.NVarchar2, planDeInvatamant.NivelDeStudiu, ParameterDirection.Input),
                new OracleParameter(":CompetenteProfesionale", OracleDbType.NVarchar2, planDeInvatamant.CompetenteProfesionale, ParameterDirection.Input),
                new OracleParameter(":An", OracleDbType.Date, planDeInvatamant.An, ParameterDirection.Input),
                new OracleParameter(":IdPlanInvatamant", OracleDbType.Int32, planDeInvatamant.IdPlanInvatamant, ParameterDirection.Input));
        }

        public bool SoftDeletePlanInvatamant(int idPlanInvatamant)
        {
            var EsteSters = "Da";

            SqlDBHelper.ExecuteNonQuery(
                "UPDATE Disciplina_CB SET EsteSters = :EsteSters WHERE IdPlanInvatamant = :IdPlanInvatamant", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input),
                new OracleParameter(":IdPlanInvatamant", OracleDbType.Int32, idPlanInvatamant, ParameterDirection.Input));

            return SqlDBHelper.ExecuteNonQuery(
                "UPDATE PlanInvatamant_CB SET EsteSters = :EsteSters WHERE IdPlanInvatamant = :IdPlanInvatamant", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input),
                new OracleParameter(":IdPlanInvatamant", OracleDbType.Int32, idPlanInvatamant, ParameterDirection.Input));
        }

        public bool DeletePlanInvatamant(int idPlanInvatamant)
        {
            SqlDBHelper.ExecuteNonQuery("DELETE FROM Disciplina_CB WHERE IdPlanInvatamant = :IdPlanInvatamant", CommandType.Text,
                new OracleParameter(":IdPlanInvatamant", OracleDbType.Int32, idPlanInvatamant, ParameterDirection.Input));

            return SqlDBHelper.ExecuteNonQuery("DELETE FROM PlanInvatamant_CB WHERE IdPlanInvatamant = :IdPlanInvatamant", CommandType.Text,
                new OracleParameter(":IdPlanInvatamant", OracleDbType.Int32, idPlanInvatamant, ParameterDirection.Input));
        }
    }
}

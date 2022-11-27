using LibrarieModele;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace NivelAccesDate
{
    public class AdministrareDiscipline : IStocareDiscipline
    {
        private const int PRIMUL_TABEL = 0;
        private const int PRIMA_LINIE = 0;

        public bool AddDisciplina(Disciplina disciplina)
        {
            var EsteSters = "Nu";

            return SqlDBHelper.ExecuteNonQuery(
                "INSERT INTO Disciplina_CB VALUES (seq_Disciplina_CB.nextval, :IdPlanInvatamant, :Nume, :TipDisciplina, :NumarCredite, :An, :Semestru, :CodDisciplina, :NumarOreSeminar, " +
                ":NumarOreLaborator, :NumarOreCurs, :FormaDeVerificare, :TotalOreStudiuIndividual, :EsteSters)", CommandType.Text,
                new OracleParameter(":IdPlanInvatamant", OracleDbType.Int32, disciplina.IdPlanInvatamant, ParameterDirection.Input),
                new OracleParameter(":Nume", OracleDbType.NVarchar2, disciplina.Nume, ParameterDirection.Input),
                new OracleParameter(":TipDisciplina", OracleDbType.NVarchar2, disciplina.TipDisciplina, ParameterDirection.Input),
                new OracleParameter(":NumarCredite", OracleDbType.Int32, disciplina.NumarCredite, ParameterDirection.Input),
                new OracleParameter(":An", OracleDbType.NVarchar2, disciplina.An, ParameterDirection.Input),
                new OracleParameter(":Semestru", OracleDbType.Int32, disciplina.Semestru, ParameterDirection.Input),
                new OracleParameter(":CodDisciplina", OracleDbType.NVarchar2, disciplina.CodDisciplina, ParameterDirection.Input),
                new OracleParameter(":NumarOreSeminar", OracleDbType.Int32, disciplina.NumarOreSeminar, ParameterDirection.Input),
                new OracleParameter(":NumarOreLaborator", OracleDbType.Int32, disciplina.NumarOreLaborator, ParameterDirection.Input),
                new OracleParameter(":NumarOreCurs", OracleDbType.Int32, disciplina.NumarOreCurs, ParameterDirection.Input),
                new OracleParameter(":FormaDeVerificare", OracleDbType.NVarchar2, disciplina.FormaDeVerificare, ParameterDirection.Input),
                new OracleParameter(":TotalOreStudiuIndividual", OracleDbType.Int32, disciplina.TotalOreStudiuIndividual, ParameterDirection.Input),
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));
        }

        public Disciplina GetDisciplina(int idDisciplina)
        {
            Disciplina result = null;

            var EsteSters = "Nu";

            var dsDiscipline = SqlDBHelper.ExecuteDataSet("SELECT * FROM Disciplina_CB WHERE IdDisciplina = :IdDisciplina AND EsteSters = :EsteSters", CommandType.Text,
                new OracleParameter(":IdDisciplina", OracleDbType.Int32, idDisciplina, ParameterDirection.Input),
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));

            if (dsDiscipline.Tables[PRIMUL_TABEL].Rows.Count > 0)
            {
                DataRow linieDB = dsDiscipline.Tables[PRIMUL_TABEL].Rows[PRIMA_LINIE];
                result = new Disciplina(linieDB);

                result.PlanInvatamant = new AdministrarePlanDeInvatamant().GetPlanInvatamant(result.IdPlanInvatamant);
            }

            return result;
        }

        public List<Disciplina> GetDiscipline()
        {
            var result = new List<Disciplina>();

            var EsteSters = "Nu";

            var dsDiscipline = SqlDBHelper.ExecuteDataSet("SELECT * FROM Disciplina_CB WHERE EsteSters = :EsteSters", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));

            foreach (DataRow linieDB in dsDiscipline.Tables[PRIMUL_TABEL].Rows)
            {
                var disciplina = new Disciplina(linieDB);
                disciplina.PlanInvatamant = new AdministrarePlanDeInvatamant().GetPlanInvatamant(disciplina.IdPlanInvatamant);

                result.Add(disciplina);
            }

            return result;
        }

        public List<Disciplina> GetDiscipline(int idPlanInvatamant)
        {
            var result = new List<Disciplina>();

            var EsteSters = "Nu";

            var dsDiscipline = SqlDBHelper.ExecuteDataSet("SELECT * FROM Disciplina_CB WHERE IdPlanInvatamant = :IdPlanInvatamant AND EsteSters = :EsteSters", CommandType.Text,
                new OracleParameter(":IdPlanInvatamant", OracleDbType.Int32, idPlanInvatamant, ParameterDirection.Input),
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));

            foreach (DataRow linieDB in dsDiscipline.Tables[PRIMUL_TABEL].Rows)
            {
                var disciplina = new Disciplina(linieDB);
                disciplina.PlanInvatamant = new AdministrarePlanDeInvatamant().GetPlanInvatamant(disciplina.IdPlanInvatamant);

                result.Add(disciplina);
            }

            return result;
        }

        public bool UpdateDisciplina(Disciplina disciplina)
        {
            return SqlDBHelper.ExecuteNonQuery(
                "UPDATE Disciplina_CB SET IdPlanInvatamant = :IdPlanInvatamant, Nume = :Nume, TipDisciplina = :TipDisciplina, NumarCredite = :NumarCredite, " +
                "An = :An, Semestru = :Semestru, CodDisciplina = :CodDisciplina, NumarOreSeminar = :NumarOreSeminar, NumarOreLaborator = :NumarOreLaborator, " +
                "NumarOreCurs = :NumarOreCurs, FormaDeVerificare = :FormaDeVerificare, TotalOreStudiuIndividual = :TotalOreStudiuIndividual WHERE IdDisciplina = :IdDisciplina", CommandType.Text,
                new OracleParameter(":IdPlanInvatamant", OracleDbType.Int32, disciplina.IdPlanInvatamant, ParameterDirection.Input),
                new OracleParameter(":Nume", OracleDbType.NVarchar2, disciplina.Nume, ParameterDirection.Input),
                new OracleParameter(":TipDisciplina", OracleDbType.NVarchar2, disciplina.TipDisciplina, ParameterDirection.Input),
                new OracleParameter(":NumarCredite", OracleDbType.Int32, disciplina.NumarCredite, ParameterDirection.Input),
                new OracleParameter(":An", OracleDbType.NVarchar2, disciplina.An, ParameterDirection.Input),
                new OracleParameter(":Semestru", OracleDbType.Int32, disciplina.Semestru, ParameterDirection.Input),
                new OracleParameter(":CodDisciplina", OracleDbType.NVarchar2, disciplina.CodDisciplina, ParameterDirection.Input),
                new OracleParameter(":NumarOreSeminar", OracleDbType.Int32, disciplina.NumarOreSeminar, ParameterDirection.Input),
                new OracleParameter(":NumarOreLaborator", OracleDbType.Int32, disciplina.NumarOreLaborator, ParameterDirection.Input),
                new OracleParameter(":NumarOreCurs", OracleDbType.Int32, disciplina.NumarOreCurs, ParameterDirection.Input),
                new OracleParameter(":FormaDeVerificare", OracleDbType.NVarchar2, disciplina.FormaDeVerificare, ParameterDirection.Input),
                new OracleParameter(":TotalOreStudiuIndividual", OracleDbType.Int32, disciplina.TotalOreStudiuIndividual, ParameterDirection.Input),
                new OracleParameter(":IdDisciplina", OracleDbType.Int32, disciplina.IdDisciplina, ParameterDirection.Input));
        }

        public bool DeleteDisciplina(int idDisciplina)
        {
            return SqlDBHelper.ExecuteNonQuery("DELETE FROM Disciplina_CB WHERE IdDisciplina = :IdDisciplina", CommandType.Text,
                new OracleParameter(":IdDisciplina", OracleDbType.Int32, idDisciplina, ParameterDirection.Input));
        }

        public bool SoftDeleteDisciplina(int idDisciplina)
        {
            var EsteSters = "Da";

            return SqlDBHelper.ExecuteNonQuery(
                "UPDATE Disciplina_CB SET EsteSters = :EsteSters WHERE IdDisciplina = :IdDisciplina", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input),
                new OracleParameter(":IdDisciplina", OracleDbType.Int32, idDisciplina, ParameterDirection.Input));
        }
    }
}

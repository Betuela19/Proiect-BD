using LibrarieModele;
using Oracle.DataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace NivelAccesDate
{
    public class AdministrareFacultati : IStocareFacultati
    {
        private const int PRIMUL_TABEL = 0;
        private const int PRIMA_LINIE = 0;

        public bool AddFacultate(Facultate facultate)
        {
            var EsteSters = "Nu";

            return SqlDBHelper.ExecuteNonQuery(
                "INSERT INTO Facultate_CB VALUES (seq_Facultate_CB.nextval, :Nume, :AnInfiintare, :Domeniu, :EsteSters)", CommandType.Text,
                new OracleParameter(":Nume", OracleDbType.NVarchar2, facultate.Nume, ParameterDirection.Input),
                new OracleParameter(":AnInfiintare", OracleDbType.Date, facultate.AnInfiintare, ParameterDirection.Input),
                new OracleParameter(":Domeniu", OracleDbType.NVarchar2, facultate.Domeniu, ParameterDirection.Input),
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));
        }

        public Facultate GetFacultate(int idFacultate)
        {
            Facultate result = null;

            var EsteSters = "Nu";

            var dsFacultati = SqlDBHelper.ExecuteDataSet("SELECT * FROM Facultate_CB WHERE IdFacultate = :IdFacultate AND EsteSters = :EsteSters", CommandType.Text,
                new OracleParameter(":IdFacultate", OracleDbType.Int32, idFacultate, ParameterDirection.Input),
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));

            if (dsFacultati.Tables[PRIMUL_TABEL].Rows.Count > 0)
            {
                DataRow linieDB = dsFacultati.Tables[PRIMUL_TABEL].Rows[PRIMA_LINIE];

                result = new Facultate(linieDB);
            }

            return result;
        }

        public List<Facultate> GetFacultati()
        {
            var result = new List<Facultate>();

            var EsteSters = "Nu";

            var dsFacultati = SqlDBHelper.ExecuteDataSet("SELECT * FROM Facultate_CB WHERE EsteSters = :EsteSters", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input));

            foreach (DataRow linieBD in dsFacultati.Tables[PRIMUL_TABEL].Rows)
            {
                var facultate = new Facultate(linieBD);

                result.Add(facultate);
            }

            return result;
        }

        public bool UpdateFacultate(Facultate facultate)
        {
            return SqlDBHelper.ExecuteNonQuery(
                "UPDATE Facultate_CB SET Nume = :Nume, AnInfiintare = :AnInfiintare, Domeniu = :Domeniu WHERE IdFacultate = :IdFacultate", CommandType.Text,
                new OracleParameter(":Nume", OracleDbType.NVarchar2, facultate.Nume, ParameterDirection.Input),
                new OracleParameter(":AnInfiintare", OracleDbType.Date, facultate.AnInfiintare, ParameterDirection.Input),
                new OracleParameter(":Domeniu", OracleDbType.NVarchar2, facultate.Domeniu, ParameterDirection.Input),
                new OracleParameter(":IdFacultate", OracleDbType.Int32, facultate.IdFacultate, ParameterDirection.Input));
        }

        public bool DeleteFacultate(int idFacultate)
        {
            SqlDBHelper.ExecuteNonQuery("DELETE FROM ProgramDeStudiu_CB WHERE IdFacultate = :IdFacultate", CommandType.Text,
                new OracleParameter(":IdFacultate", OracleDbType.Int32, idFacultate, ParameterDirection.Input));

            return SqlDBHelper.ExecuteNonQuery("DELETE FROM Facultate_CB WHERE IdFacultate = :IdFacultate", CommandType.Text,
                new OracleParameter(":IdFacultate", OracleDbType.Int32, idFacultate, ParameterDirection.Input));
        }

        public bool SoftDeletefacultate(int idFacultate)
        {
            var EsteSters = "Da";

            SqlDBHelper.ExecuteNonQuery(
                "UPDATE ProgramDeStudiu_CB SET EsteSters = :EsteSters WHERE IdFacultate = :IdFacultate", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input),
                new OracleParameter(":Idfacultate", OracleDbType.Int32, idFacultate, ParameterDirection.Input));

            return SqlDBHelper.ExecuteNonQuery(
                "UPDATE Facultate_CB SET EsteSters = :EsteSters WHERE IdFacultate = :IdFacultate", CommandType.Text,
                new OracleParameter(":EsteSters", OracleDbType.NVarchar2, EsteSters, ParameterDirection.Input),
                new OracleParameter(":Idfacultate", OracleDbType.Int32, idFacultate, ParameterDirection.Input));
        }
    }
}

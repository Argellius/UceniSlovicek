using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UceniSlovicek
{
    public class Database_Tools
    {
        private string connectionString;
        private SqlConnection sqlConnection;


        public Database_Tools(string conn)
        {
            this.connectionString = conn;
            sqlConnection = new SqlConnection(connectionString);
        }

        public void Add_Record(Vocabulary cze_v, Vocabulary eng_v)
        {
            int id_cze = -1;
            int id_eng = -1;
            if (cze_v.Kind_Voc == KindOfVocabulary.Czech)
            {
                id_cze = Add_Czech_Voc(cze_v);

            }
            else
            {
                throw new NotImplementedException("Incorrect of implementation czech word");
            }

            if (eng_v.Kind_Voc == KindOfVocabulary.English)
            {
                id_eng = Add_English_Voc(eng_v);
            }
            else
            {
                throw new NotImplementedException("Incorrect of implementation english word");
            }

            if (id_cze != -1 && id_eng != -1)
            {
                Add_References_Into_Vocabulary(id_cze, id_eng);
            }


        }

        private int Add_Czech_Voc(Vocabulary voc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Czech_Vocabulary(Noun, Adjective, Verb) output INSERTED.ID VALUES(@N, @A, @V)";
            if (string.IsNullOrEmpty(voc.Noun))
                cmd.Parameters.AddWithValue("@N", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@N", voc.Noun);

            if (string.IsNullOrEmpty(voc.Adjective))
                cmd.Parameters.AddWithValue("@A", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@A", voc.Adjective);

            if (string.IsNullOrEmpty(voc.Verb))
                cmd.Parameters.AddWithValue("@V", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@V", voc.Verb);
            sqlConnection.Open();

            int id_inserted = (int)cmd.ExecuteScalar();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
            return id_inserted;

        }

        private int Add_English_Voc(Vocabulary voc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO English_Vocabulary(Noun, Adjective, Verb) output INSERTED.ID VALUES(@N, @A, @V)";
            if (string.IsNullOrEmpty(voc.Noun))
                cmd.Parameters.AddWithValue("@N", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@N", voc.Noun);

            if (string.IsNullOrEmpty(voc.Adjective))
                cmd.Parameters.AddWithValue("@A", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@A", voc.Adjective);

            if (string.IsNullOrEmpty(voc.Verb))
                cmd.Parameters.AddWithValue("@V", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@V", voc.Verb);

            sqlConnection.Open();

            int id_inserted = (int)cmd.ExecuteScalar();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
            return id_inserted;

        }

        //Insert id_cze and id_eng into entity vocabulary
        private void Add_References_Into_Vocabulary(int id_cze, int id_eng)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "INSERT INTO Vocabulary(id_cze, id_eng) output INSERTED.ID VALUES(@id_cze, @id_eng)";
            cmd.Parameters.AddWithValue("@id_cze", id_cze);
            cmd.Parameters.AddWithValue("@id_eng", id_eng);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
        }

        //Return DataTable with content from entity Vocabulary -> Id, Cze_Id and Eng_Id words
        public DataTable Get_All_IDs_Voc()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Vocabulary";
            var dt = new DataTable();
            sqlConnection.Open();
            dt.Load(cmd.ExecuteReader());
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
            return dt;
        }

        public Vocabulary Get_English_Voc_By_Id(int Id)
        {
            Vocabulary voc;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM English_Vocabulary WHERE Id='" + Id + "'";
            sqlConnection.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            if (sqlDataReader.Read())
            {
                voc = new Vocabulary(KindOfVocabulary.English, sqlDataReader[1].ToString(), sqlDataReader[2].ToString(), sqlDataReader[3].ToString());

            }
            else
                voc = null;
            sqlDataReader.Close();
            sqlConnection.Close();

            return voc;
        }


        /*
         * ARRAY VOCABULARY
         * [0] - CZE WORD
         * [1] - ENG WORD
         */
        public Vocabulary[] GetCzechandEnglishWordsById(int id)
        {
            Vocabulary[] voc = new Vocabulary[2];
            int[] voc_id = GetVocabularyById(id);
            voc[0] = this.Get_Czech_Voc_By_Id(voc_id[0]);
            voc[1] = this.Get_English_Voc_By_Id(voc_id[1]);

            return voc;
        }

        /*
        * ARRAY INT
        * [0] - ID_CZE WORD
        * [1] - ID_ENG WORD
        */
        public int[] GetVocabularyById(int Id)
        {
            int[] voc = new int[2];
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Vocabulary WHERE Id='" + Id + "'";
            sqlConnection.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            if (sqlDataReader.Read())
            {
                voc[0] = (int)sqlDataReader[1];
                voc[1] = (int)sqlDataReader[2];


            }
            sqlDataReader.Close();
            sqlConnection.Close();
            return voc;

        }


        public Vocabulary Get_Czech_Voc_By_Id(int Id)
        {
            Vocabulary voc;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM Czech_Vocabulary WHERE Id='" + Id + "'";
            sqlConnection.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            if (sqlDataReader.Read())
            {
                voc = new Vocabulary(KindOfVocabulary.Czech, sqlDataReader[1].ToString(), sqlDataReader[2].ToString(), sqlDataReader[3].ToString());

            }
            else
                voc = null;
            sqlDataReader.Close();
            sqlConnection.Close();

            return voc;
        }

        public void UpdateCzechVocById(int id, Vocabulary voc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE Czech_Vocabulary SET Noun = @N, Adjective = @A, Verb = @V WHERE Id='" + id + "'";
            if (string.IsNullOrEmpty(voc.Noun))
                cmd.Parameters.AddWithValue("@N", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@N", voc.Noun);

            if (string.IsNullOrEmpty(voc.Adjective))
                cmd.Parameters.AddWithValue("@A", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@A", voc.Adjective);

            if (string.IsNullOrEmpty(voc.Verb))
                cmd.Parameters.AddWithValue("@V", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@V", voc.Verb);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
        }

        public void UpdateEnglishVocById(int id, Vocabulary voc)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sqlConnection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE English_VocabularySET Noun = @N, Adjective = @A, Verb = @V WHERE Id='" + id + "'";
            if (string.IsNullOrEmpty(voc.Noun))
                cmd.Parameters.AddWithValue("@N", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@N", voc.Noun);

            if (string.IsNullOrEmpty(voc.Adjective))
                cmd.Parameters.AddWithValue("@A", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@A", voc.Adjective);

            if (string.IsNullOrEmpty(voc.Verb))
                cmd.Parameters.AddWithValue("@V", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@V", voc.Verb);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
                sqlConnection.Close();
        }
    }
}

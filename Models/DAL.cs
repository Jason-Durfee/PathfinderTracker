using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PathfinderTracker.Models
{
    public static class DAL
    {
        private static string _ConnectionString;

        private static string ConnectionString {
            get {
                if(_ConnectionString == null) {
                    string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    string path = (System.IO.Path.GetDirectoryName(executable));
                    AppDomain.CurrentDomain.SetData("DataDirectory", path);
                    _ConnectionString = "Data Source=.;Initial Catalog=PathfinderTracker;Integrated Security=True;";
                }
                return _ConnectionString;
            }
        }

        /// <summary>
        /// connects to the database with the appropriate Edit/Read connection string string
        /// </summary>ChangedDepartment
        internal static void ConnectToDatabase(SqlCommand comm) {
            try {
                comm.Connection = new SqlConnection(ConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
            }
            catch(Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Gets the data reader for a database command
        /// </summary>
        public static SqlDataReader GetDataReader(SqlCommand comm) {
            try {
                ConnectToDatabase(comm);
                comm.Connection.Open();
                return comm.ExecuteReader();
            }
            catch(Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }

        #region Objects
        /// <summary>
        /// adds an object to the database
        /// </summary>
        /// <returns>the objects new ID or -1 if it fails</returns>
        internal static int AddObject(SqlCommand comm, string parameterName) {
            int retInt = 0;
            try {
                comm.Connection = new SqlConnection(ConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                SqlParameter retParameter;
                retParameter = comm.Parameters.Add(parameterName, SqlDbType.Int);
                retParameter.Direction = System.Data.ParameterDirection.Output;
                comm.ExecuteNonQuery();
                retInt = (int)retParameter.Value;
                comm.Connection.Close();
            }
            catch(Exception ex) {
                if(comm.Connection != null)
                    comm.Connection.Close();

                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }


        /// <summary>
        /// Sets Connection and Executes given comm on the database
        /// </summary>
        /// <param name="comm">MySqlCommand to execute</param>
        /// <returns>number of rows affected; -1 on failure, positive value on success.</returns>
        /// <remarks>Must make sure to populate the command with MySqltext and any parameters before passing to this function.
        ///       Edit Connection is set here.</remarks>
        internal static int UpdateObject(SqlCommand comm) {
            int retInt = 0;
            try {
                comm.Connection = new SqlConnection(ConnectionString);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Connection.Open();
                retInt = comm.ExecuteNonQuery();
                comm.Connection.Close();
            }
            catch(Exception ex) {
                if(comm.Connection != null)
                    comm.Connection.Close();

                retInt = -1;
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
            return retInt;
        }
        #endregion

        #region Alignments
        /// <summary>
        /// Gets all Alignment objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Alignment> GetAlignments() {
            List<Alignment> alignments = new List<Alignment>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocAlignmentsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Alignment alignment = new Alignment(dr);
                    alignments.Add(alignment);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return alignments;
        }

        /// <summary>
        /// gets a specific Alignment from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Alignment GetAlignment(int id) {
            SqlCommand comm = new SqlCommand("sprocAlignmentGet");
            Alignment retObj = null;
            try {
                comm.Parameters.AddWithValue("@AlignmentID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Alignment(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Alignment object in the database
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static int CreateAlignment(Alignment alignment) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_AlignmentAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", alignment.Name);
                comm.Parameters.AddWithValue("@Description", alignment.Description);
                comm.Parameters.AddWithValue("@Abbreviation", alignment.Abbreviation);

                comm.Parameters.Add("@AlignmentID", SqlDbType.Int);
                comm.Parameters["@AlignmentID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@AlignmentID"].Value;
                alignment.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Alignment object in the database
        /// </summary>
        /// <param name="alignment"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateAlignment(Alignment alignment, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_AlignmentUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@AlignmentID", id);
                comm.Parameters.AddWithValue("@Name", alignment.Name);
                comm.Parameters.AddWithValue("@Description", alignment.Description);
                comm.Parameters.AddWithValue("@Abbreviation", alignment.Abbreviation);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Alignment object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteAlignment(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_AlignmentDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@AlignmentID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Armors
        /// <summary>
        /// Gets all Armor objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Armor> GetArmors() {
            List<Armor> armors = new List<Armor>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocArmorsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Armor armor = new Armor(dr);
                    armors.Add(armor);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return armors;
        }

        /// <summary>
        /// gets a specific Armor from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Armor GetArmor(int id) {
            SqlCommand comm = new SqlCommand("sprocArmorGet");
            Armor retObj = null;
            try {
                comm.Parameters.AddWithValue("@ArmorID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Armor(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Armor object in the database
        /// </summary>
        /// <param name="armor"></param>
        /// <returns></returns>
        public static int CreateArmor(Armor armor) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MaterialID", armor.MaterialID);
                comm.Parameters.AddWithValue("@ArmorTypeID", armor.ArmorTypeID);
                comm.Parameters.AddWithValue("@ArmorAddonID", armor.ArmorAddonID);
                comm.Parameters.AddWithValue("@SpecialAttributes", armor.SpecialAttributes);

                comm.Parameters.Add("@ArmorID", SqlDbType.Int);
                comm.Parameters["@ArmorID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ArmorID"].Value;
                armor.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Armor object in the database
        /// </summary>
        /// <param name="Armor"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateArmor(Armor Armor, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorID", id);
                comm.Parameters.AddWithValue("@MaterialID", Armor.MaterialID);
                comm.Parameters.AddWithValue("@ArmorTypeID", Armor.ArmorTypeID);
                comm.Parameters.AddWithValue("@ArmorAddonID", Armor.ArmorAddonID);
                comm.Parameters.AddWithValue("@SpecialAttributes", Armor.SpecialAttributes);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Armor object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteArmor(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region ArmorAddons
        /// <summary>
        /// Gets all ArmorAddon objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<ArmorAddon> GetArmorAddons() {
            List<ArmorAddon> armorAddons = new List<ArmorAddon>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocArmorAddonsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    ArmorAddon armorAddon = new ArmorAddon(dr);
                    armorAddons.Add(armorAddon);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return armorAddons;
        }

        /// <summary>
        /// gets a specific ArmorAddon from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArmorAddon GetArmorAddon(int id) {
            SqlCommand comm = new SqlCommand("sprocArmorAddonGet");
            ArmorAddon retObj = null;
            try {
                comm.Parameters.AddWithValue("@ArmorAddonID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new ArmorAddon(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an ArmorAddon object in the database
        /// </summary>
        /// <param name="armorAddon"></param>
        /// <returns></returns>
        public static int CreateArmorAddon(ArmorAddon armorAddon) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorAddonAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", armorAddon.Name);
                comm.Parameters.AddWithValue("@MaterialID", armorAddon.MaterialID);

                comm.Parameters.Add("@ArmorAddonID", SqlDbType.Int);
                comm.Parameters["@ArmorAddonID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ArmorAddonID"].Value;
                armorAddon.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific ArmorAddon object in the database
        /// </summary>
        /// <param name="armorAddon"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateArmorAddon(ArmorAddon armorAddon, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorAddonUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorAddonID", id);
                comm.Parameters.AddWithValue("@Name", armorAddon.Name);
                comm.Parameters.AddWithValue("@MaterialID", armorAddon.MaterialID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific ArmorAddon object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteArmorAddon(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorAddonDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorAddonID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region ArmorAddonTypes
        /// <summary>
        /// Gets all ArmorAddonType objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<ArmorAddonType> GetArmorAddonTypes() {
            List<ArmorAddonType> armorAddonTypes = new List<ArmorAddonType>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocArmorAddonTypesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    ArmorAddonType armorAddonType = new ArmorAddonType(dr);
                    armorAddonTypes.Add(armorAddonType);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return armorAddonTypes;
        }

        /// <summary>
        /// gets a specific ArmorAddonType from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArmorAddonType GetArmorAddonType(int id) {
            SqlCommand comm = new SqlCommand("sprocArmorAddonTypeGet");
            ArmorAddonType retObj = null;
            try {
                comm.Parameters.AddWithValue("@ArmorAddonTypeID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new ArmorAddonType(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an ArmorAddonType object in the database
        /// </summary>
        /// <param name="armorAddonType"></param>
        /// <returns></returns>
        public static int CreateArmorAddonType(ArmorAddonType armorAddonType) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorAddonTypeAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", armorAddonType.Name);
                comm.Parameters.AddWithValue("@BaseGPValue", armorAddonType.BaseGPValue);
                comm.Parameters.AddWithValue("@Weight", armorAddonType.Weight);
                comm.Parameters.AddWithValue("@ArmorCheckPenalty", armorAddonType.ArmorCheckPenalty);

                comm.Parameters.Add("@ArmorAddonTypeID", SqlDbType.Int);
                comm.Parameters["@ArmorAddonTypeID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ArmorAddonTypeID"].Value;
                armorAddonType.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific ArmorAddonType object in the database
        /// </summary>
        /// <param name="armorAddonType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateArmorAddonType(ArmorAddonType armorAddonType, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorAddonTypeUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorAddonTypeID", id);
                comm.Parameters.AddWithValue("@Name", armorAddonType.Name);
                comm.Parameters.AddWithValue("@BaseGPValue", armorAddonType.BaseGPValue);
                comm.Parameters.AddWithValue("@Weight", armorAddonType.Weight);
                comm.Parameters.AddWithValue("@ArmorCheckPenalty", armorAddonType.ArmorCheckPenalty);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific ArmorAddonType object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteArmorAddonType(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorAddonTypeDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorAddonTypeID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region ArmorCoreTypes
        /// <summary>
        /// Gets all ArmorCoreType objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<ArmorCoreType> GetArmorCoreTypes() {
            List<ArmorCoreType> armorCoreTypes = new List<ArmorCoreType>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocArmorCoreTypesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    ArmorCoreType armorCoreType = new ArmorCoreType(dr);
                    armorCoreTypes.Add(armorCoreType);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return armorCoreTypes;
        }

        /// <summary>
        /// gets a specific ArmorCoreType from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArmorCoreType GetArmorCoreType(int id) {
            SqlCommand comm = new SqlCommand("sprocArmorCoreTypeGet");
            ArmorCoreType retObj = null;
            try {
                comm.Parameters.AddWithValue("@ArmorCoreTypeID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new ArmorCoreType(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an ArmorCoreType object in the database
        /// </summary>
        /// <param name="armorCoreType"></param>
        /// <returns></returns>
        public static int CreateArmorCoreType(ArmorCoreType armorCoreType) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorCoreTypeAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", armorCoreType.Name);

                comm.Parameters.Add("@ArmorCoreTypeID", SqlDbType.Int);
                comm.Parameters["@ArmorCoreTypeID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ArmorCoreTypeID"].Value;
                armorCoreType.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific ArmorCoreType object in the database
        /// </summary>
        /// <param name="armorCoreType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateArmorCoreType(ArmorCoreType armorCoreType, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorCoreTypeUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorCoreTypeID", id);
                comm.Parameters.AddWithValue("@Name", armorCoreType.Name);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific ArmorCoreType object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteArmorCoreType(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorCoreTypeDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorCoreTypeID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region ArmorTypes
        /// <summary>
        /// Gets all ArmorType objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<ArmorType> GetArmorTypes() {
            List<ArmorType> armorTypes = new List<ArmorType>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocArmorTypesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    ArmorType armorType = new ArmorType(dr);
                    armorTypes.Add(armorType);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return armorTypes;
        }

        /// <summary>
        /// gets a specific ArmorType from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ArmorType GetArmorType(int id) {
            SqlCommand comm = new SqlCommand("sprocArmorTypeGet");
            ArmorType retObj = null;
            try {
                comm.Parameters.AddWithValue("@ArmorTypeID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new ArmorType(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an ArmorType object in the database
        /// </summary>
        /// <param name="armorType"></param>
        /// <returns></returns>
        public static int CreateArmorType(ArmorType armorType) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorTypeAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", armorType.Name);
                comm.Parameters.AddWithValue("@BaseGPValue", armorType.BaseGPValue);
                comm.Parameters.AddWithValue("@ACBonus", armorType.ACBonus);
                comm.Parameters.AddWithValue("@ArmorCheckPenalty", armorType.ArmorCheckPenalty);
                comm.Parameters.AddWithValue("@Weight", armorType.Weight);

                comm.Parameters.Add("@ArmorTypeID", SqlDbType.Int);
                comm.Parameters["@ArmorTypeID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ArmorTypeID"].Value;
                armorType.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific ArmorType object in the database
        /// </summary>
        /// <param name="armorType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateArmorType(ArmorType armorType, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorTypeUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorTypeID", id);
                comm.Parameters.AddWithValue("@Name", armorType.Name);
                comm.Parameters.AddWithValue("@BaseGPValue", armorType.BaseGPValue);
                comm.Parameters.AddWithValue("@ACBonus", armorType.ACBonus);
                comm.Parameters.AddWithValue("@ArmorCheckPenalty", armorType.ArmorCheckPenalty);
                comm.Parameters.AddWithValue("@Weight", armorType.Weight);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific ArmorType object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteArmorType(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorTypeDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorTypeID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Bloodlines
        /// <summary>
        /// Gets all Bloodline objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Bloodline> GetBloodlines() {
            List<Bloodline> bloodlines = new List<Bloodline>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocBloodlinesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Bloodline bloodline = new Bloodline(dr);
                    bloodlines.Add(bloodline);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return bloodlines;
        }

        /// <summary>
        /// gets a specific Bloodline from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Bloodline GetBloodline(int id) {
            SqlCommand comm = new SqlCommand("sprocBloodlineGet");
            Bloodline retObj = null;
            try {
                comm.Parameters.AddWithValue("@BloodlineID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Bloodline(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Bloodline object in the database
        /// </summary>
        /// <param name="bloodline"></param>
        /// <returns></returns>
        public static int CreateBloodline(Bloodline bloodline) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_BloodlineAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", bloodline.Name);
                comm.Parameters.AddWithValue("@Description", bloodline.Description);

                comm.Parameters.Add("@BloodlineID", SqlDbType.Int);
                comm.Parameters["@BloodlineID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@BloodlineID"].Value;
                bloodline.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Bloodline object in the database
        /// </summary>
        /// <param name="bloodline"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateBloodline(Bloodline bloodline, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_BloodlineUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@BloodlineID", id);
                comm.Parameters.AddWithValue("@Name", bloodline.Name);
                comm.Parameters.AddWithValue("@Description", bloodline.Description);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Bloodline object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteBloodline(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_BloodlineDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@BloodlineID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Campaigns
        /// <summary>
        /// Gets all Campaign objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Campaign> GetCampaigns() {
            List<Campaign> campaigns = new List<Campaign>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocCampaignsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Campaign campaign = new Campaign(dr);
                    campaigns.Add(campaign);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return campaigns;
        }

        /// <summary>
        /// gets a specific Campaign from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Campaign GetCampaign(int id) {
            SqlCommand comm = new SqlCommand("sprocCampaignGet");
            Campaign retObj = null;
            try {
                comm.Parameters.AddWithValue("@CampaignID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Campaign(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Campaign object in the database
        /// </summary>
        /// <param name="campaign"></param>
        /// <returns></returns>
        public static int CreateCampaign(Campaign campaign) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CampaignAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", campaign.Name);
                comm.Parameters.AddWithValue("@CurrentTime", campaign.CurrentTime);

                comm.Parameters.Add("@CampaignID", SqlDbType.Int);
                comm.Parameters["@CampaignID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@CampaignID"].Value;
                campaign.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Campaign object in the database
        /// </summary>
        /// <param name="campaign"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateCampaign(Campaign campaign, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CampaignUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CampaignID", id);
                comm.Parameters.AddWithValue("@Name", campaign.Name);
                comm.Parameters.AddWithValue("@CurrentTime", campaign.CurrentTime);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Campaign object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteCampaign(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CampaignDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CampaignID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Characters
        /// <summary>
        /// Gets all Character objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Character> GetCharacters() {
            List<Character> characters = new List<Character>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocCharactersGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Character character = new Character(dr);
                    characters.Add(character);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return characters;
        }

        /// <summary>
        /// gets a specific Character from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Character GetCharacter(int id) {
            SqlCommand comm = new SqlCommand("sprocCharacterGet");
            Character retObj = null;
            try {
                comm.Parameters.AddWithValue("@CharacterID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Character(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Character object in the database
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int CreateCharacter(Character character) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CharacterAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", character.Name);
                comm.Parameters.AddWithValue("@AlignmentID", character.AlignmentID);
                comm.Parameters.AddWithValue("@Bonuses", character.Bonuses);
                comm.Parameters.AddWithValue("@CampaignID", character.CampaignID);
                comm.Parameters.AddWithValue("@DeityID", character.DeityID);
                comm.Parameters.AddWithValue("@IsNPC", character.IsNPC);
                comm.Parameters.AddWithValue("@Level", character.Level);
                comm.Parameters.AddWithValue("@RaceID", character.RaceID);
                comm.Parameters.AddWithValue("@CharacterID", character.PlayerID);
                comm.Parameters.AddWithValue("@HPCurrent", character.HPCurrent);
                comm.Parameters.AddWithValue("@HPMax", character.HPMax);

                comm.Parameters.Add("@CharacterID", SqlDbType.Int);
                comm.Parameters["@CharacterID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@CharacterID"].Value;
                character.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Character object in the database
        /// </summary>
        /// <param name="character"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateCharacter(Character character, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CharacterUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CharacterID", id);
                comm.Parameters.AddWithValue("@Name", character.Name);
                comm.Parameters.AddWithValue("@AlignmentID", character.AlignmentID);
                comm.Parameters.AddWithValue("@Bonuses", character.Bonuses);
                comm.Parameters.AddWithValue("@CampaignID", character.CampaignID);
                comm.Parameters.AddWithValue("@DeityID", character.DeityID);
                comm.Parameters.AddWithValue("@IsNPC", character.IsNPC);
                comm.Parameters.AddWithValue("@Level", character.Level);
                comm.Parameters.AddWithValue("@RaceID", character.RaceID);
                comm.Parameters.AddWithValue("@CharacterID", character.PlayerID);
                comm.Parameters.AddWithValue("@HPCurrent", character.HPCurrent);
                comm.Parameters.AddWithValue("@HPMax", character.HPMax);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Character object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteCharacter(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CharacterDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CharacterID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region CharacterClasses
        /// <summary>
        /// Gets all CharacterClass objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<CharacterClass> GetCharacterClasses() {
            List<CharacterClass> characterClasses = new List<CharacterClass>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocCharacterClassesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    CharacterClass characterClass = new CharacterClass(dr);
                    characterClasses.Add(characterClass);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return characterClasses;
        }

        /// <summary>
        /// gets a specific CharacterClass from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CharacterClass GetCharacterClass(int id) {
            SqlCommand comm = new SqlCommand("sprocCharacterClassGet");
            CharacterClass retObj = null;
            try {
                comm.Parameters.AddWithValue("@CharacterClassID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new CharacterClass(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an CharacterClass object in the database
        /// </summary>
        /// <param name="characterCharacterClass"></param>
        /// <returns></returns>
        public static int CreateCharacterClass(CharacterClass characterClass) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CharacterClassAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", characterClass.Name);
                comm.Parameters.AddWithValue("@HasBloodline", characterClass.HasBloodline);
                comm.Parameters.AddWithValue("@HasDomain", characterClass.HasDomain);
                comm.Parameters.AddWithValue("@HasMagicSchool", characterClass.HasMagicSchool);

                comm.Parameters.Add("@CharacterClassID", SqlDbType.Int);
                comm.Parameters["@CharacterClassID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@CharacterClassID"].Value;
                characterClass.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific CharacterClass object in the database
        /// </summary>
        /// <param name="characterCharacterClass"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateCharacterClass(CharacterClass characterClass, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CharacterClassUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CharacterClassID", id);
                comm.Parameters.AddWithValue("@Name", characterClass.Name);
                comm.Parameters.AddWithValue("@HasBloodline", characterClass.HasBloodline);
                comm.Parameters.AddWithValue("@HasDomain", characterClass.HasDomain);
                comm.Parameters.AddWithValue("@HasMagicSchool", characterClass.HasMagicSchool);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific CharacterClass object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteCharacterClass(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CharacterClassDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CharacterClassID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region ClassesToCharacters
        /// <summary>
        /// Gets all ClassesToCharacter objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<ClassesToCharacter> GetClassesToCharacters() {
            List<ClassesToCharacter> classesToCharacters = new List<ClassesToCharacter>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocClassesToCharactersGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    ClassesToCharacter classesToCharacter = new ClassesToCharacter(dr);
                    classesToCharacters.Add(classesToCharacter);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return classesToCharacters;
        }

        /// <summary>
        /// gets a specific ClassesToCharacter from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ClassesToCharacter GetClassesToCharacter(int id) {
            SqlCommand comm = new SqlCommand("sprocClassesToCharacterGet");
            ClassesToCharacter retObj = null;
            try {
                comm.Parameters.AddWithValue("@ClassesToCharacterID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new ClassesToCharacter(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an ClassesToCharacter object in the database
        /// </summary>
        /// <param name="classesToCharacter"></param>
        /// <returns></returns>
        public static int CreateClassesToCharacter(ClassesToCharacter classesToCharacter) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ClassesToCharacterAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ClassLevel", classesToCharacter.ClassLevel);
                comm.Parameters.AddWithValue("@CharacterID", classesToCharacter.CharacterID);
                comm.Parameters.AddWithValue("@CharacterClassID", classesToCharacter.CharacterClassID);
                comm.Parameters.AddWithValue("@BloodlineID", classesToCharacter.BloodlineID);
                comm.Parameters.AddWithValue("@DomainID", classesToCharacter.DomainID);
                comm.Parameters.AddWithValue("@MagicSchoolID", classesToCharacter.MagicSchoolID);

                comm.Parameters.Add("@ClassesToCharacterID", SqlDbType.Int);
                comm.Parameters["@ClassesToCharacterID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ClassesToCharacterID"].Value;
                classesToCharacter.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific ClassesToCharacter object in the database
        /// </summary>
        /// <param name="classesToCharacter"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateClassesToCharacter(ClassesToCharacter classesToCharacter, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ClassesToCharacterUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ClassesToCharacterID", id);
                comm.Parameters.AddWithValue("@ClassLevel", classesToCharacter.ClassLevel);
                comm.Parameters.AddWithValue("@CharacterID", classesToCharacter.CharacterID);
                comm.Parameters.AddWithValue("@CharacterClassID", classesToCharacter.CharacterClassID);
                comm.Parameters.AddWithValue("@BloodlineID", classesToCharacter.BloodlineID);
                comm.Parameters.AddWithValue("@DomainID", classesToCharacter.DomainID);
                comm.Parameters.AddWithValue("@MagicSchoolID", classesToCharacter.MagicSchoolID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific ClassesToCharacter object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteClassesToCharacter(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ClassesToCharacterDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ClassesToCharacterID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Conditions
        /// <summary>
        /// Gets all Condition objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Condition> GetConditions() {
            List<Condition> conditions = new List<Condition>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocConditionsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Condition condition = new Condition(dr);
                    conditions.Add(condition);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return conditions;
        }

        /// <summary>
        /// gets a specific Condition from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Condition GetCondition(int id) {
            SqlCommand comm = new SqlCommand("sprocConditionGet");
            Condition retObj = null;
            try {
                comm.Parameters.AddWithValue("@ConditionID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Condition(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Condition object in the database
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static int CreateCondition(Condition condition) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ConditionAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", condition.Name);
                comm.Parameters.AddWithValue("@Effect", condition.Effect);

                comm.Parameters.Add("@ConditionID", SqlDbType.Int);
                comm.Parameters["@ConditionID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ConditionID"].Value;
                condition.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Condition object in the database
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateCondition(Condition condition, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ConditionUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ConditionID", id);
                comm.Parameters.AddWithValue("@Name", condition.Name);
                comm.Parameters.AddWithValue("@Effect", condition.Effect);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Condition object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteCondition(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ConditionDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ConditionID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Deitys
        /// <summary>
        /// Gets all Deity objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Deity> GetDeities() {
            List<Deity> deitys = new List<Deity>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocDeitiesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Deity deity = new Deity(dr);
                    deitys.Add(deity);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return deitys;
        }

        /// <summary>
        /// gets a specific Deity from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Deity GetDeity(int id) {
            SqlCommand comm = new SqlCommand("sprocDeityGet");
            Deity retObj = null;
            try {
                comm.Parameters.AddWithValue("@DeityID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Deity(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Deity object in the database
        /// </summary>
        /// <param name="deity"></param>
        /// <returns></returns>
        public static int CreateDeity(Deity deity) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DeityAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", deity.Name);
                comm.Parameters.AddWithValue("@AlignmentID", deity.AlignmentID);
                comm.Parameters.AddWithValue("@Description", deity.Description);

                comm.Parameters.Add("@DeityID", SqlDbType.Int);
                comm.Parameters["@DeityID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@DeityID"].Value;
                deity.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Deity object in the database
        /// </summary>
        /// <param name="deity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateDeity(Deity deity, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DeityUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@DeityID", id);
                comm.Parameters.AddWithValue("@Name", deity.Name);
                comm.Parameters.AddWithValue("@AlignmentID", deity.AlignmentID);
                comm.Parameters.AddWithValue("@Description", deity.Description);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Deity object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteDeity(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DeityDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@DeityID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Domains
        /// <summary>
        /// Gets all Domain objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Domain> GetDomains() {
            List<Domain> domains = new List<Domain>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocDomainsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Domain Domain = new Domain(dr);
                    domains.Add(Domain);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return domains;
        }

        /// <summary>
        /// gets a specific Domain from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Domain GetDomain(int id) {
            SqlCommand comm = new SqlCommand("sprocDomainGet");
            Domain retObj = null;
            try {
                comm.Parameters.AddWithValue("@DomainID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Domain(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Domain object in the database
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static int CreateDomain(Domain domain) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DomainAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", domain.Name);
                comm.Parameters.AddWithValue("@Description", domain.Description);

                comm.Parameters.Add("@DomainID", SqlDbType.Int);
                comm.Parameters["@DomainID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@DomainID"].Value;
                domain.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Domain object in the database
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateDomain(Domain domain, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DomainUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@DomainID", id);
                comm.Parameters.AddWithValue("@Name", domain.Name);
                comm.Parameters.AddWithValue("@Description", domain.Description);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Domain object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteDomain(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DomainDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@DomainID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Feats
        /// <summary>
        /// Gets all Feat objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Feat> GetFeats() {
            List<Feat> feats = new List<Feat>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocFeatsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Feat feat = new Feat(dr);
                    feats.Add(feat);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return feats;
        }

        /// <summary>
        /// gets a specific Feat from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Feat GetFeat(int id) {
            SqlCommand comm = new SqlCommand("sprocFeatGet");
            Feat retObj = null;
            try {
                comm.Parameters.AddWithValue("@FeatID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Feat(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Feat object in the database
        /// </summary>
        /// <param name="feat"></param>
        /// <returns></returns>
        public static int CreateFeat(Feat feat) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_FeatAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", feat.Name);
                comm.Parameters.AddWithValue("@Description", feat.Description);
                comm.Parameters.AddWithValue("@Prerequisites", feat.Prerequisites);
                comm.Parameters.AddWithValue("@FeatTypeID", feat.FeatTypeID);

                comm.Parameters.Add("@FeatID", SqlDbType.Int);
                comm.Parameters["@FeatID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@FeatID"].Value;
                feat.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Feat object in the database
        /// </summary>
        /// <param name="feat"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateFeat(Feat feat, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_FeatUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@FeatID", id);
                comm.Parameters.AddWithValue("@Name", feat.Name);
                comm.Parameters.AddWithValue("@Description", feat.Description);
                comm.Parameters.AddWithValue("@Prerequisites", feat.Prerequisites);
                comm.Parameters.AddWithValue("@FeatTypeID", feat.FeatTypeID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Feat object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteFeat(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_FeatDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@FeatID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region FeatTypes
        /// <summary>
        /// Gets all FeatType objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<FeatType> GetFeatTypes() {
            List<FeatType> featTypees = new List<FeatType>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocFeatTypesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    FeatType featType = new FeatType(dr);
                    featTypees.Add(featType);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return featTypees;
        }

        /// <summary>
        /// gets a specific FeatType from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FeatType GetFeatType(int id) {
            SqlCommand comm = new SqlCommand("sprocFeatTypeGet");
            FeatType retObj = null;
            try {
                comm.Parameters.AddWithValue("@FeatTypeID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new FeatType(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an FeatType object in the database
        /// </summary>
        /// <param name="featType"></param>
        /// <returns></returns>
        public static int CreateFeatType(FeatType featType) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_FeatTypeAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", featType.Name);

                comm.Parameters.Add("@FeatTypeID", SqlDbType.Int);
                comm.Parameters["@FeatTypeID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@FeatTypeID"].Value;
                featType.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific FeatType object in the database
        /// </summary>
        /// <param name="featType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateFeatType(FeatType featType, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_FeatTypeUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@FeatTypeID", id);
                comm.Parameters.AddWithValue("@Name", featType.Name);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific FeatType object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteFeatType(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_FeatTypeDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@FeatTypeID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Items
        /// <summary>
        /// Gets all Item objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Item> GetItems() {
            List<Item> items = new List<Item>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocItemsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Item item = new Item(dr);
                    items.Add(item);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return items;
        }

        /// <summary>
        /// gets a specific Item from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Item GetItem(int id) {
            SqlCommand comm = new SqlCommand("sprocItemGet");
            Item retObj = null;
            try {
                comm.Parameters.AddWithValue("@ItemID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Item(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Item object in the database
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static int CreateItem(Item item) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ItemAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", item.Name);
                comm.Parameters.AddWithValue("@Description", item.Description);
                comm.Parameters.AddWithValue("@ConstructionRequirements", item.ConstructionRequirements);
                comm.Parameters.AddWithValue("@GPValue", item.GPValue);
                comm.Parameters.AddWithValue("@SlotID", item.SlotID);

                comm.Parameters.Add("@ItemID", SqlDbType.Int);
                comm.Parameters["@ItemID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ItemID"].Value;
                item.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Item object in the database
        /// </summary>
        /// <param name="item"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateItem(Item item, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ItemUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ItemID", id);
                comm.Parameters.AddWithValue("@Name", item.Name);
                comm.Parameters.AddWithValue("@Description", item.Description);
                comm.Parameters.AddWithValue("@ConstructionRequirements", item.ConstructionRequirements);
                comm.Parameters.AddWithValue("@GPValue", item.GPValue);
                comm.Parameters.AddWithValue("@SlotID", item.SlotID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Item object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteItem(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ItemDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ItemID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region MagicSchools
        /// <summary>
        /// Gets all MagicSchool objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<MagicSchool> GetMagicSchools() {
            List<MagicSchool> magicSchools = new List<MagicSchool>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocMagicSchoolsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    MagicSchool magicSchool = new MagicSchool(dr);
                    magicSchools.Add(magicSchool);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return magicSchools;
        }

        /// <summary>
        /// gets a specific MagicSchool from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MagicSchool GetMagicSchool(int id) {
            SqlCommand comm = new SqlCommand("sprocMagicSchoolGet");
            MagicSchool retObj = null;
            try {
                comm.Parameters.AddWithValue("@MagicSchoolID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new MagicSchool(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an MagicSchool object in the database
        /// </summary>
        /// <param name="magicSchool"></param>
        /// <returns></returns>
        public static int CreateMagicSchool(MagicSchool magicSchool) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_MagicSchoolAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", magicSchool.Name);
                comm.Parameters.AddWithValue("@Description", magicSchool.Description);

                comm.Parameters.Add("@MagicSchoolID", SqlDbType.Int);
                comm.Parameters["@MagicSchoolID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@MagicSchoolID"].Value;
                magicSchool.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific MagicSchool object in the database
        /// </summary>
        /// <param name="magicSchool"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateMagicSchool(MagicSchool magicSchool, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_MagicSchoolUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MagicSchoolID", id);
                comm.Parameters.AddWithValue("@Name", magicSchool.Name);
                comm.Parameters.AddWithValue("@Description", magicSchool.Description);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific MagicSchool object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteMagicSchool(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_MagicSchoolDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MagicSchoolID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Materials
        /// <summary>
        /// Gets all Material objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Material> GetMaterials() {
            List<Material> materials = new List<Material>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocMaterialsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Material material = new Material(dr);
                    materials.Add(material);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return materials;
        }

        /// <summary>
        /// gets a specific Material from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Material GetMaterial(int id) {
            SqlCommand comm = new SqlCommand("sprocMaterialGet");
            Material retObj = null;
            try {
                comm.Parameters.AddWithValue("@MaterialID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Material(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Material object in the database
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public static int CreateMaterial(Material material) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_MaterialAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", material.Name);
                comm.Parameters.AddWithValue("@AmmoAddedGold", material.AmmoAddedGold);
                comm.Parameters.AddWithValue("@LightAddedGold", material.LightAddedGold);
                comm.Parameters.AddWithValue("@MediumAddedGold", material.MediumAddedGold);
                comm.Parameters.AddWithValue("@HeavyAddedGold", material.HeavyAddedGold);
                comm.Parameters.AddWithValue("@ShieldAddedGold", material.ShieldAddedGold);
                comm.Parameters.AddWithValue("@WeaponAddedGold", material.WeaponAddedGold);
                comm.Parameters.AddWithValue("@WeightGoldMultiplier", material.WeightGoldMultiplier);
                comm.Parameters.AddWithValue("@BaseGoldMultiplier", material.BaseGoldMultiplier);

                comm.Parameters.Add("@MaterialID", SqlDbType.Int);
                comm.Parameters["@MaterialID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@MaterialID"].Value;
                material.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Material object in the database
        /// </summary>
        /// <param name="material"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateMaterial(Material material, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_MaterialUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MaterialID", id);
                comm.Parameters.AddWithValue("@Name", material.Name);
                comm.Parameters.AddWithValue("@AmmoAddedGold", material.AmmoAddedGold);
                comm.Parameters.AddWithValue("@LightAddedGold", material.LightAddedGold);
                comm.Parameters.AddWithValue("@MediumAddedGold", material.MediumAddedGold);
                comm.Parameters.AddWithValue("@HeavyAddedGold", material.HeavyAddedGold);
                comm.Parameters.AddWithValue("@ShieldAddedGold", material.ShieldAddedGold);
                comm.Parameters.AddWithValue("@WeaponAddedGold", material.WeaponAddedGold);
                comm.Parameters.AddWithValue("@WeightGoldMultiplier", material.WeightGoldMultiplier);
                comm.Parameters.AddWithValue("@BaseGoldMultiplier", material.BaseGoldMultiplier);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Material object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteMaterial(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_MaterialDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MaterialID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Players
        /// <summary>
        /// Gets all Player objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Player> GetPlayers() {
            List<Player> players = new List<Player>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocPlayersGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Player player = new Player(dr);
                    players.Add(player);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return players;
        }

        /// <summary>
        /// gets a specific Player from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Player GetPlayer(int id) {
            SqlCommand comm = new SqlCommand("sprocPlayerGet");
            Player retObj = null;
            try {
                comm.Parameters.AddWithValue("@PlayerID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Player(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Player object in the database
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static int CreatePlayer(Player player) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_PlayerAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", player.Name);

                comm.Parameters.Add("@PlayerID", SqlDbType.Int);
                comm.Parameters["@PlayerID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@PlayerID"].Value;
                player.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Player object in the database
        /// </summary>
        /// <param name="player"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdatePlayer(Player player, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_PlayerUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@PlayerID", id);
                comm.Parameters.AddWithValue("@Name", player.Name);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Player object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeletePlayer(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_PlayerDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@PlayerID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Races
        /// <summary>
        /// Gets all Race objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Race> GetRaces() {
            List<Race> races = new List<Race>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocRacesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Race race = new Race(dr);
                    races.Add(race);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return races;
        }

        /// <summary>
        /// gets a specific Race from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Race GetRace(int id) {
            SqlCommand comm = new SqlCommand("sprocRaceGet");
            Race retObj = null;
            try {
                comm.Parameters.AddWithValue("@RaceID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Race(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Race object in the database
        /// </summary>
        /// <param name="race"></param>
        /// <returns></returns>
        public static int CreateRace(Race race) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_RaceAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", race.Name);

                comm.Parameters.Add("@RaceID", SqlDbType.Int);
                comm.Parameters["@RaceID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@RaceID"].Value;
                race.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Race object in the database
        /// </summary>
        /// <param name="race"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateRace(Race race, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_RaceUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@RaceID", id);
                comm.Parameters.AddWithValue("@Name", race.Name);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Race object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteRace(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_RaceDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@RaceID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Spells
        /// <summary>
        /// Gets all Spell objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Spell> GetSpells() {
            List<Spell> spells = new List<Spell>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocSpellsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Spell spell = new Spell(dr);
                    spells.Add(spell);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return spells;
        }

        /// <summary>
        /// gets a specific Spell from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Spell GetSpell(int id) {
            SqlCommand comm = new SqlCommand("sprocSpellGet");
            Spell retObj = null;
            try {
                comm.Parameters.AddWithValue("@SpellID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Spell(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Spell object in the database
        /// </summary>
        /// <param name="spell"></param>
        /// <returns></returns>
        public static int CreateSpell(Spell spell) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SpellAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", spell.Name);
                comm.Parameters.AddWithValue("@CastingTime", spell.CastingTime);
                comm.Parameters.AddWithValue("@Description", spell.Description);
                comm.Parameters.AddWithValue("@Duration", spell.Duration);
                comm.Parameters.AddWithValue("@MagicSchoolID", spell.MagicSchoolID);
                comm.Parameters.AddWithValue("@RangeDistance", spell.RangeDistance);
                comm.Parameters.AddWithValue("@SavingThrow", spell.SavingThrow);
                comm.Parameters.AddWithValue("@SpellResistance", spell.SpellResistance);
                comm.Parameters.AddWithValue("@Target", spell.Target);

                comm.Parameters.Add("@SpellID", SqlDbType.Int);
                comm.Parameters["@SpellID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@SpellID"].Value;
                spell.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Spell object in the database
        /// </summary>
        /// <param name="spell"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateSpell(Spell spell, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SpellUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SpellID", id);
                comm.Parameters.AddWithValue("@Name", spell.Name);
                comm.Parameters.AddWithValue("@CastingTime", spell.CastingTime);
                comm.Parameters.AddWithValue("@Description", spell.Description);
                comm.Parameters.AddWithValue("@Duration", spell.Duration);
                comm.Parameters.AddWithValue("@MagicSchoolID", spell.MagicSchoolID);
                comm.Parameters.AddWithValue("@RangeDistance", spell.RangeDistance);
                comm.Parameters.AddWithValue("@SavingThrow", spell.SavingThrow);
                comm.Parameters.AddWithValue("@SpellResistance", spell.SpellResistance);
                comm.Parameters.AddWithValue("@Target", spell.Target);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Spell object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteSpell(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SpellDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SpellID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Slots
        /// <summary>
        /// Gets all Slot objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Slot> GetSlots() {
            List<Slot> slots = new List<Slot>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocSlotsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Slot slot = new Slot(dr);
                    slots.Add(slot);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return slots;
        }

        /// <summary>
        /// gets a specific Slot from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Slot GetSlot(int id) {
            SqlCommand comm = new SqlCommand("sprocSlotGet");
            Slot retObj = null;
            try {
                comm.Parameters.AddWithValue("@SlotID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Slot(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Slot object in the database
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public static int CreateSlot(Slot slot) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SlotAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", slot.Name);

                comm.Parameters.Add("@SlotID", SqlDbType.Int);
                comm.Parameters["@SlotID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@SlotID"].Value;
                slot.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Slot object in the database
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateSlot(Slot slot, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SlotUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SlotID", id);
                comm.Parameters.AddWithValue("@Name", slot.Name);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Slot object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteSlot(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SlotDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SlotID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region Weapons
        /// <summary>
        /// Gets all Weapon objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Weapon> GetWeapons() {
            List<Weapon> weapons = new List<Weapon>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocWeaponsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Weapon weapon = new Weapon(dr);
                    weapons.Add(weapon);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return weapons;
        }

        /// <summary>
        /// gets a specific Weapon from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Weapon GetWeapon(int id) {
            SqlCommand comm = new SqlCommand("sprocWeaponGet");
            Weapon retObj = null;
            try {
                comm.Parameters.AddWithValue("@WeaponID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Weapon(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an Weapon object in the database
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        public static int CreateWeapon(Weapon weapon) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MaterialID", weapon.MaterialID);
                comm.Parameters.AddWithValue("@SpecialAttributes", weapon.SpecialAttributes);
                comm.Parameters.AddWithValue("@WeaponTypeID", weapon.WeaponTypeID);

                comm.Parameters.Add("@WeaponID", SqlDbType.Int);
                comm.Parameters["@WeaponID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@WeaponID"].Value;
                weapon.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific Weapon object in the database
        /// </summary>
        /// <param name="weapon"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateWeapon(Weapon weapon, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponID", id);
                comm.Parameters.AddWithValue("@MaterialID", weapon.MaterialID);
                comm.Parameters.AddWithValue("@SpecialAttributes", weapon.SpecialAttributes);
                comm.Parameters.AddWithValue("@WeaponTypeID", weapon.WeaponTypeID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific Weapon object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteWeapon(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region WeaponCategories
        /// <summary>
        /// Gets all WeaponCategory objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<WeaponCategory> GetWeaponCategories() {
            List<WeaponCategory> weaponCategories = new List<WeaponCategory>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocWeaponCategoriesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    WeaponCategory weaponCategory = new WeaponCategory(dr);
                    weaponCategories.Add(weaponCategory);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return weaponCategories;
        }

        /// <summary>
        /// gets a specific WeaponCategory from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WeaponCategory GetWeaponCategory(int id) {
            SqlCommand comm = new SqlCommand("sprocWeaponCategoryGet");
            WeaponCategory retObj = null;
            try {
                comm.Parameters.AddWithValue("@WeaponCategoryID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new WeaponCategory(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an WeaponCategory object in the database
        /// </summary>
        /// <param name="weaponCategory"></param>
        /// <returns></returns>
        public static int CreateWeaponCategory(WeaponCategory weaponCategory) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponCategoryAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", weaponCategory.Name);

                comm.Parameters.Add("@WeaponCategoryID", SqlDbType.Int);
                comm.Parameters["@WeaponCategoryID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@WeaponCategoryID"].Value;
                weaponCategory.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific WeaponCategory object in the database
        /// </summary>
        /// <param name="weaponCategory"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateWeaponCategory(WeaponCategory weaponCategory, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponCategoryUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponCategoryID", id);
                comm.Parameters.AddWithValue("@Name", weaponCategory.Name);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific WeaponCategory object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteWeaponCategory(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponCategoryDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponCategoryID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region WeaponCoreTypes
        /// <summary>
        /// Gets all WeaponCoreType objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<WeaponCoreType> GetWeaponCoreTypes() {
            List<WeaponCoreType> weaponCoreTypes = new List<WeaponCoreType>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocWeaponCoreTypesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    WeaponCoreType weaponCoreType = new WeaponCoreType(dr);
                    weaponCoreTypes.Add(weaponCoreType);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return weaponCoreTypes;
        }

        /// <summary>
        /// gets a specific WeaponCoreType from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WeaponCoreType GetWeaponCoreType(int id) {
            SqlCommand comm = new SqlCommand("sprocWeaponCoreTypeGet");
            WeaponCoreType retObj = null;
            try {
                comm.Parameters.AddWithValue("@WeaponCoreTypeID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new WeaponCoreType(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an WeaponCoreType object in the database
        /// </summary>
        /// <param name="weaponCoreType"></param>
        /// <returns></returns>
        public static int CreateWeaponCoreType(WeaponCoreType weaponCoreType) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponCoreTypeAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", weaponCoreType.Name);

                comm.Parameters.Add("@WeaponCoreTypeID", SqlDbType.Int);
                comm.Parameters["@WeaponCoreTypeID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@WeaponCoreTypeID"].Value;
                weaponCoreType.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific WeaponCoreType object in the database
        /// </summary>
        /// <param name="weaponCoreType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateWeaponCoreType(WeaponCoreType weaponCoreType, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponCoreTypeUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponCoreTypeID", id);
                comm.Parameters.AddWithValue("@Name", weaponCoreType.Name);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific WeaponCoreType object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteWeaponCoreType(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponCoreTypeDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponCoreTypeID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion

        #region WeaponTypes
        /// <summary>
        /// Gets all WeaponType objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<WeaponType> GetWeaponTypes() {
            List<WeaponType> weaponTypes = new List<WeaponType>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocWeaponTypesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    WeaponType weaponType = new WeaponType(dr);
                    weaponTypes.Add(weaponType);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return weaponTypes;
        }

        /// <summary>
        /// gets a specific WeaponType from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WeaponType GetWeaponType(int id) {
            SqlCommand comm = new SqlCommand("sprocWeaponTypeGet");
            WeaponType retObj = null;
            try {
                comm.Parameters.AddWithValue("@WeaponTypeID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new WeaponType(dr);
                }
                comm.Connection.Close();
            }
            catch(Exception ex) {
                comm.Connection.Close();
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return retObj;

        }

        /// <summary>
        /// inserts an WeaponType object in the database
        /// </summary>
        /// <param name="weaponType"></param>
        /// <returns></returns>
        public static int CreateWeaponType(WeaponType weaponType) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponTypeAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", weaponType.Name);
                comm.Parameters.AddWithValue("@AttackDiceMedium", weaponType.AttackDiceMedium);
                comm.Parameters.AddWithValue("@AttackDiceSmall", weaponType.AttackDiceSmall);
                comm.Parameters.AddWithValue("@AttackRange", weaponType.AttackRange);
                comm.Parameters.AddWithValue("@Critical", weaponType.Critical);
                comm.Parameters.AddWithValue("@GPValue", weaponType.GPValue);
                comm.Parameters.AddWithValue("@Weight", weaponType.Weight);
                comm.Parameters.AddWithValue("@HasReach", weaponType.HasReach);
                comm.Parameters.AddWithValue("@DamageType", weaponType.DamageType);
                comm.Parameters.AddWithValue("@WeaponCategoryID", weaponType.WeaponCategoryID);
                comm.Parameters.AddWithValue("@WeaponCoreTypeID", weaponType.WeaponCoreTypeID);

                comm.Parameters.Add("@WeaponTypeID", SqlDbType.Int);
                comm.Parameters["@WeaponTypeID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@WeaponTypeID"].Value;
                weaponType.ID = ID;
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// updates a specific WeaponType object in the database
        /// </summary>
        /// <param name="weaponType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateWeaponType(WeaponType weaponType, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponTypeUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponTypeID", id);
                comm.Parameters.AddWithValue("@Name", weaponType.Name);
                comm.Parameters.AddWithValue("@AttackDiceMedium", weaponType.AttackDiceMedium);
                comm.Parameters.AddWithValue("@AttackDiceSmall", weaponType.AttackDiceSmall);
                comm.Parameters.AddWithValue("@AttackRange", weaponType.AttackRange);
                comm.Parameters.AddWithValue("@Critical", weaponType.Critical);
                comm.Parameters.AddWithValue("@GPValue", weaponType.GPValue);
                comm.Parameters.AddWithValue("@HasReach", weaponType.HasReach);
                comm.Parameters.AddWithValue("@DamageType", weaponType.DamageType);
                comm.Parameters.AddWithValue("@Weight", weaponType.Weight);
                comm.Parameters.AddWithValue("@WeaponCategoryID", weaponType.WeaponCategoryID);
                comm.Parameters.AddWithValue("@WeaponCoreTypeID", weaponType.WeaponCoreTypeID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }

        /// <summary>
        /// deletes a specific WeaponType object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteWeaponType(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponTypeDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponTypeID", ID);
                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return retVal;
        }
        #endregion
    }
}

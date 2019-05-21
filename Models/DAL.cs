using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<Alignment> Alignments = new List<Alignment>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocAlignmentsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Alignment Alignment = new Alignment(dr);
                    Alignments.Add(Alignment);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Alignments;
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
        /// <param name="Alignment"></param>
        /// <returns></returns>
        public static int CreateAlignment(Alignment Alignment) {
            int retVal = -1;
            Alignment AlignmentToSave = new Alignment();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_AlignmentAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Alignment.Name);
                comm.Parameters.AddWithValue("@Description", Alignment.Description);
                comm.Parameters.AddWithValue("@Abbreviation", Alignment.Abbreviation);

                comm.Parameters.Add("@AlignmentID", SqlDbType.Int);
                comm.Parameters["@AlignmentID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@AlignmentID"].Value;
                Alignment.ID = ID;
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
        /// <param name="Alignment"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateAlignment(Alignment Alignment, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Alignment AlignmentToSave = new Alignment();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_AlignmentUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@AlignmentID", id);
                comm.Parameters.AddWithValue("@Name", Alignment.Name);
                comm.Parameters.AddWithValue("@Description", Alignment.Description);
                comm.Parameters.AddWithValue("@Abbreviation", Alignment.Abbreviation);
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
            List<Armor> Armors = new List<Armor>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocArmorsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Armor Armor = new Armor(dr);
                    Armors.Add(Armor);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Armors;
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
        /// <param name="Armor"></param>
        /// <returns></returns>
        public static int CreateArmor(Armor Armor) {
            int retVal = -1;
            Armor ArmorToSave = new Armor();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Armor.Name);
                comm.Parameters.AddWithValue("@GPValue", Armor.GPValue);
                comm.Parameters.AddWithValue("@ACBonus", Armor.ACBonus);
                comm.Parameters.AddWithValue("@ArmorCheckPenalty", Armor.ArmorCheckPenalty);
                comm.Parameters.AddWithValue("@Weight", Armor.Weight);
                comm.Parameters.AddWithValue("@MaterialID", Armor.MaterialID);
                comm.Parameters.AddWithValue("@ArmorTypeID", Armor.ArmorTypeID);
                comm.Parameters.AddWithValue("@ArmorAddonID", Armor.ArmorAddonID);
                comm.Parameters.AddWithValue("@SpecialAttributes", Armor.SpecialAttributes);

                comm.Parameters.Add("@ArmorID", SqlDbType.Int);
                comm.Parameters["@ArmorID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ArmorID"].Value;
                Armor.ID = ID;
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
            Armor ArmorToSave = new Armor();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorID", id);
                comm.Parameters.AddWithValue("@Name", Armor.Name);
                comm.Parameters.AddWithValue("@GPValue", Armor.GPValue);
                comm.Parameters.AddWithValue("@ACBonus", Armor.ACBonus);
                comm.Parameters.AddWithValue("@ArmorCheckPenalty", Armor.ArmorCheckPenalty);
                comm.Parameters.AddWithValue("@Weight", Armor.Weight);
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
            List<ArmorAddon> ArmorAddons = new List<ArmorAddon>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocArmorAddonsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    ArmorAddon ArmorAddon = new ArmorAddon(dr);
                    ArmorAddons.Add(ArmorAddon);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return ArmorAddons;
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
        /// <param name="ArmorAddon"></param>
        /// <returns></returns>
        public static int CreateArmorAddon(ArmorAddon ArmorAddon) {
            int retVal = -1;
            ArmorAddon ArmorAddonToSave = new ArmorAddon();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorAddonAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", ArmorAddon.Name);
                comm.Parameters.AddWithValue("@GPValue", ArmorAddon.GPValue);
                comm.Parameters.AddWithValue("@Weight", ArmorAddon.Weight);
                comm.Parameters.AddWithValue("@MaterialID", ArmorAddon.MaterialID);
                comm.Parameters.AddWithValue("@ArmorCheckPenalty", ArmorAddon.ArmorCheckPenalty);

                comm.Parameters.Add("@ArmorAddonID", SqlDbType.Int);
                comm.Parameters["@ArmorAddonID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ArmorAddonID"].Value;
                ArmorAddon.ID = ID;
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
        /// <param name="ArmorAddon"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateArmorAddon(ArmorAddon ArmorAddon, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            ArmorAddon ArmorAddonToSave = new ArmorAddon();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorAddonUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorAddonID", id);
                comm.Parameters.AddWithValue("@Name", ArmorAddon.Name);
                comm.Parameters.AddWithValue("@GPValue", ArmorAddon.GPValue);
                comm.Parameters.AddWithValue("@Weight", ArmorAddon.Weight);
                comm.Parameters.AddWithValue("@MaterialID", ArmorAddon.MaterialID);
                comm.Parameters.AddWithValue("@ArmorCheckPenalty", ArmorAddon.ArmorCheckPenalty);
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

        #region ArmorTypes
        /// <summary>
        /// Gets all ArmorType objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<ArmorType> GetArmorTypes() {
            List<ArmorType> ArmorTypes = new List<ArmorType>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocArmorTypesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    ArmorType ArmorType = new ArmorType(dr);
                    ArmorTypes.Add(ArmorType);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return ArmorTypes;
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
        /// <param name="ArmorType"></param>
        /// <returns></returns>
        public static int CreateArmorType(ArmorType ArmorType) {
            int retVal = -1;
            ArmorType ArmorTypeToSave = new ArmorType();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorTypeAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", ArmorType.Name);

                comm.Parameters.Add("@ArmorTypeID", SqlDbType.Int);
                comm.Parameters["@ArmorTypeID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ArmorTypeID"].Value;
                ArmorType.ID = ID;
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
        /// <param name="ArmorType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateArmorType(ArmorType ArmorType, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            ArmorType ArmorTypeToSave = new ArmorType();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ArmorTypeUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ArmorTypeID", id);
                comm.Parameters.AddWithValue("@Name", ArmorType.Name);
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

        #region Campaigns
        /// <summary>
        /// Gets all Campaign objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Campaign> GetCampaigns() {
            List<Campaign> Campaigns = new List<Campaign>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocCampaignsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Campaign Campaign = new Campaign(dr);
                    Campaigns.Add(Campaign);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Campaigns;
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
        /// <param name="Campaign"></param>
        /// <returns></returns>
        public static int CreateCampaign(Campaign Campaign) {
            int retVal = -1;
            Campaign CampaignToSave = new Campaign();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CampaignAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Campaign.Name);

                comm.Parameters.Add("@CampaignID", SqlDbType.Int);
                comm.Parameters["@CampaignID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@CampaignID"].Value;
                Campaign.ID = ID;
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
        /// <param name="Campaign"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateCampaign(Campaign Campaign, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Campaign CampaignToSave = new Campaign();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CampaignUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CampaignID", id);
                comm.Parameters.AddWithValue("@Name", Campaign.Name);
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
            List<Character> Characters = new List<Character>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocCharactersGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Character Character = new Character(dr);
                    Characters.Add(Character);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Characters;
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
        /// <param name="Character"></param>
        /// <returns></returns>
        public static int CreateCharacter(Character Character) {
            int retVal = -1;
            Character CharacterToSave = new Character();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CharacterAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Character.Name);


                comm.Parameters.Add("@CharacterID", SqlDbType.Int);
                comm.Parameters["@CharacterID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@CharacterID"].Value;
                Character.ID = ID;
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
        /// <param name="Character"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateCharacter(Character Character, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Character CharacterToSave = new Character();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_CharacterUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@CharacterID", id);
                comm.Parameters.AddWithValue("@Name", Character.Name);
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

        #region Classs
        /// <summary>
        /// Gets all Class objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Class> GetClasss() {
            List<Class> Classs = new List<Class>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocClasssGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Class Class = new Class(dr);
                    Classs.Add(Class);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Classs;
        }

        /// <summary>
        /// gets a specific Class from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Class GetClass(int id) {
            SqlCommand comm = new SqlCommand("sprocClassGet");
            Class retObj = null;
            try {
                comm.Parameters.AddWithValue("@ClassID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new Class(dr);
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
        /// inserts an Class object in the database
        /// </summary>
        /// <param name="Class"></param>
        /// <returns></returns>
        public static int CreateClass(Class Class) {
            int retVal = -1;
            Class ClassToSave = new Class();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ClassAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Class.Name);
                comm.Parameters.AddWithValue("@SubClassID", Class.SubClassID);


                comm.Parameters.Add("@ClassID", SqlDbType.Int);
                comm.Parameters["@ClassID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ClassID"].Value;
                Class.ID = ID;
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
        /// updates a specific Class object in the database
        /// </summary>
        /// <param name="Class"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateClass(Class Class, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Class ClassToSave = new Class();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ClassUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ClassID", id);
                comm.Parameters.AddWithValue("@Name", Class.Name);
                comm.Parameters.AddWithValue("@SubClassID", Class.SubClassID);
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
        /// deletes a specific Class object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteClass(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ClassDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ClassID", ID);
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
            List<ClassesToCharacter> ClassesToCharacters = new List<ClassesToCharacter>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocClassesToCharactersGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    ClassesToCharacter ClassesToCharacter = new ClassesToCharacter(dr);
                    ClassesToCharacters.Add(ClassesToCharacter);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return ClassesToCharacters;
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
        /// <param name="ClassesToCharacter"></param>
        /// <returns></returns>
        public static int CreateClassesToCharacter(ClassesToCharacter ClassesToCharacter) {
            int retVal = -1;
            ClassesToCharacter ClassesToCharacterToSave = new ClassesToCharacter();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ClassesToCharacterAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ClassLevel", ClassesToCharacter.ClassLevel);
                comm.Parameters.AddWithValue("@CharacterID", ClassesToCharacter.CharacterID);
                comm.Parameters.AddWithValue("@ClassID", ClassesToCharacter.ClassID);

                comm.Parameters.Add("@ClassesToCharacterID", SqlDbType.Int);
                comm.Parameters["@ClassesToCharacterID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@ClassesToCharacterID"].Value;
                ClassesToCharacter.ID = ID;
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
        /// <param name="ClassesToCharacter"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateClassesToCharacter(ClassesToCharacter ClassesToCharacter, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            ClassesToCharacter ClassesToCharacterToSave = new ClassesToCharacter();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_ClassesToCharacterUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@ClassesToCharacterID", id);
                comm.Parameters.AddWithValue("@ClassLevel", ClassesToCharacter.ClassLevel);
                comm.Parameters.AddWithValue("@CharacterID", ClassesToCharacter.CharacterID);
                comm.Parameters.AddWithValue("@ClassID", ClassesToCharacter.ClassID);
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

        #region DamageTypes
        /// <summary>
        /// Gets all DamageType objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<DamageType> GetDamageTypes() {
            List<DamageType> DamageTypes = new List<DamageType>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocDamageTypesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    DamageType DamageType = new DamageType(dr);
                    DamageTypes.Add(DamageType);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return DamageTypes;
        }

        /// <summary>
        /// gets a specific DamageType from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DamageType GetDamageType(int id) {
            SqlCommand comm = new SqlCommand("sprocDamageTypeGet");
            DamageType retObj = null;
            try {
                comm.Parameters.AddWithValue("@DamageTypeID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new DamageType(dr);
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
        /// inserts an DamageType object in the database
        /// </summary>
        /// <param name="DamageType"></param>
        /// <returns></returns>
        public static int CreateDamageType(DamageType DamageType) {
            int retVal = -1;
            DamageType DamageTypeToSave = new DamageType();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DamageTypeAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", DamageType.Name);

                comm.Parameters.Add("@DamageTypeID", SqlDbType.Int);
                comm.Parameters["@DamageTypeID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@DamageTypeID"].Value;
                DamageType.ID = ID;
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
        /// updates a specific DamageType object in the database
        /// </summary>
        /// <param name="DamageType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateDamageType(DamageType DamageType, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            DamageType DamageTypeToSave = new DamageType();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DamageTypeUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@DamageTypeID", id);
                comm.Parameters.AddWithValue("@Name", DamageType.Name);
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
        /// deletes a specific DamageType object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteDamageType(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DamageTypeDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@DamageTypeID", ID);
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
            List<Deity> Deitys = new List<Deity>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocDeitiesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Deity Deity = new Deity(dr);
                    Deitys.Add(Deity);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Deitys;
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
        /// <param name="Deity"></param>
        /// <returns></returns>
        public static int CreateDeity(Deity Deity) {
            int retVal = -1;
            Deity DeityToSave = new Deity();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DeityAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Deity.Name);
                comm.Parameters.AddWithValue("@AlignmentID", Deity.AlignmentID);
                comm.Parameters.AddWithValue("@Description", Deity.Description);

                comm.Parameters.Add("@DeityID", SqlDbType.Int);
                comm.Parameters["@DeityID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@DeityID"].Value;
                Deity.ID = ID;
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
        /// <param name="Deity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateDeity(Deity Deity, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Deity DeityToSave = new Deity();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_DeityUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@DeityID", id);
                comm.Parameters.AddWithValue("@Name", Deity.Name);
                comm.Parameters.AddWithValue("@AlignmentID", Deity.AlignmentID);
                comm.Parameters.AddWithValue("@Description", Deity.Description);
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

        #region Feats
        /// <summary>
        /// Gets all Feat objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<Feat> GetFeats() {
            List<Feat> Feats = new List<Feat>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocFeatsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Feat Feat = new Feat(dr);
                    Feats.Add(Feat);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Feats;
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
        /// <param name="Feat"></param>
        /// <returns></returns>
        public static int CreateFeat(Feat Feat) {
            int retVal = -1;
            Feat FeatToSave = new Feat();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_FeatAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Feat.Name);
                comm.Parameters.AddWithValue("@Description", Feat.Description);

                comm.Parameters.Add("@FeatID", SqlDbType.Int);
                comm.Parameters["@FeatID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@FeatID"].Value;
                Feat.ID = ID;
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
        /// <param name="Feat"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateFeat(Feat Feat, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Feat FeatToSave = new Feat();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_FeatUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@FeatID", id);
                comm.Parameters.AddWithValue("@Name", Feat.Name);
                comm.Parameters.AddWithValue("@Description", Feat.Description);
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

        #region MagicSchools
        /// <summary>
        /// Gets all MagicSchool objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<MagicSchool> GetMagicSchools() {
            List<MagicSchool> MagicSchools = new List<MagicSchool>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocMagicSchoolsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    MagicSchool MagicSchool = new MagicSchool(dr);
                    MagicSchools.Add(MagicSchool);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return MagicSchools;
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
        /// <param name="MagicSchool"></param>
        /// <returns></returns>
        public static int CreateMagicSchool(MagicSchool MagicSchool) {
            int retVal = -1;
            MagicSchool MagicSchoolToSave = new MagicSchool();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_MagicSchoolAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", MagicSchool.Name);
                comm.Parameters.AddWithValue("@Description", MagicSchool.Description);

                comm.Parameters.Add("@MagicSchoolID", SqlDbType.Int);
                comm.Parameters["@MagicSchoolID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@MagicSchoolID"].Value;
                MagicSchool.ID = ID;
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
        /// <param name="MagicSchool"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateMagicSchool(MagicSchool MagicSchool, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            MagicSchool MagicSchoolToSave = new MagicSchool();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_MagicSchoolUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MagicSchoolID", id);
                comm.Parameters.AddWithValue("@Name", MagicSchool.Name);
                comm.Parameters.AddWithValue("@Description", MagicSchool.Description);
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
            List<Material> Materials = new List<Material>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocMaterialsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Material Material = new Material(dr);
                    Materials.Add(Material);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Materials;
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
        /// <param name="Material"></param>
        /// <returns></returns>
        public static int CreateMaterial(Material Material) {
            int retVal = -1;
            Material MaterialToSave = new Material();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_MaterialAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Material.Name);

                comm.Parameters.Add("@MaterialID", SqlDbType.Int);
                comm.Parameters["@MaterialID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@MaterialID"].Value;
                Material.ID = ID;
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
        /// <param name="Material"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateMaterial(Material Material, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Material MaterialToSave = new Material();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_MaterialUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@MaterialID", id);
                comm.Parameters.AddWithValue("@Name", Material.Name);
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
            List<Player> Players = new List<Player>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocPlayersGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Player Player = new Player(dr);
                    Players.Add(Player);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Players;
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
        /// <param name="Player"></param>
        /// <returns></returns>
        public static int CreatePlayer(Player Player) {
            int retVal = -1;
            Player PlayerToSave = new Player();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_PlayerAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Player.Name);
                comm.Parameters.AddWithValue("@Bonuses", Player.Bonuses);
                comm.Parameters.AddWithValue("@CharacterID", Player.CharacterID);
                comm.Parameters.AddWithValue("@HPCurrent", Player.HPCurrent);
                comm.Parameters.AddWithValue("@HPMax", Player.HPMax);

                comm.Parameters.Add("@PlayerID", SqlDbType.Int);
                comm.Parameters["@PlayerID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@PlayerID"].Value;
                Player.ID = ID;
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
        /// <param name="Player"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdatePlayer(Player Player, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Player PlayerToSave = new Player();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_PlayerUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@PlayerID", id);
                comm.Parameters.AddWithValue("@Name", Player.Name);
                comm.Parameters.AddWithValue("@Bonuses", Player.Bonuses);
                comm.Parameters.AddWithValue("@CharacterID", Player.CharacterID);
                comm.Parameters.AddWithValue("@HPCurrent", Player.HPCurrent);
                comm.Parameters.AddWithValue("@HPMax", Player.HPMax);
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
            List<Race> Races = new List<Race>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocRacesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Race Race = new Race(dr);
                    Races.Add(Race);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Races;
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
        /// <param name="Race"></param>
        /// <returns></returns>
        public static int CreateRace(Race Race) {
            int retVal = -1;
            Race RaceToSave = new Race();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_RaceAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Race.Name);

                comm.Parameters.Add("@RaceID", SqlDbType.Int);
                comm.Parameters["@RaceID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@RaceID"].Value;
                Race.ID = ID;
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
        /// <param name="Race"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateRace(Race Race, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Race RaceToSave = new Race();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_RaceUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@RaceID", id);
                comm.Parameters.AddWithValue("@Name", Race.Name);
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
            List<Spell> Spells = new List<Spell>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocSpellsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Spell Spell = new Spell(dr);
                    Spells.Add(Spell);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Spells;
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
        /// <param name="Spell"></param>
        /// <returns></returns>
        public static int CreateSpell(Spell Spell) {
            int retVal = -1;
            Spell SpellToSave = new Spell();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SpellAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Spell.Name);
                comm.Parameters.AddWithValue("@CastingTime", Spell.CastingTime);
                comm.Parameters.AddWithValue("@Description", Spell.Description);
                comm.Parameters.AddWithValue("@Duration", Spell.Duration);
                comm.Parameters.AddWithValue("@MagicSchoolID", Spell.MagicSchoolID);
                comm.Parameters.AddWithValue("@RangeDistance", Spell.RangeDistance);
                comm.Parameters.AddWithValue("@SavingThrow", Spell.SavingThrow);
                comm.Parameters.AddWithValue("@SpellResistance", Spell.SpellResistance);
                comm.Parameters.AddWithValue("@Target", Spell.Target);

                comm.Parameters.Add("@SpellID", SqlDbType.Int);
                comm.Parameters["@SpellID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@SpellID"].Value;
                Spell.ID = ID;
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
        /// <param name="Spell"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateSpell(Spell Spell, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Spell SpellToSave = new Spell();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SpellUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SpellID", id);
                comm.Parameters.AddWithValue("@Name", Spell.Name);
                comm.Parameters.AddWithValue("@CastingTime", Spell.CastingTime);
                comm.Parameters.AddWithValue("@Description", Spell.Description);
                comm.Parameters.AddWithValue("@Duration", Spell.Duration);
                comm.Parameters.AddWithValue("@MagicSchoolID", Spell.MagicSchoolID);
                comm.Parameters.AddWithValue("@RangeDistance", Spell.RangeDistance);
                comm.Parameters.AddWithValue("@SavingThrow", Spell.SavingThrow);
                comm.Parameters.AddWithValue("@SpellResistance", Spell.SpellResistance);
                comm.Parameters.AddWithValue("@Target", Spell.Target);
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

        #region SubClasss
        /// <summary>
        /// Gets all SubClass objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<SubClass> GetSubClasss() {
            List<SubClass> SubClasss = new List<SubClass>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocSubClasssGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    SubClass SubClass = new SubClass(dr);
                    SubClasss.Add(SubClass);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return SubClasss;
        }

        /// <summary>
        /// gets a specific SubClass from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SubClass GetSubClass(int id) {
            SqlCommand comm = new SqlCommand("sprocSubClassGet");
            SubClass retObj = null;
            try {
                comm.Parameters.AddWithValue("@SubClassID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new SubClass(dr);
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
        /// inserts an SubClass object in the database
        /// </summary>
        /// <param name="SubClass"></param>
        /// <returns></returns>
        public static int CreateSubClass(SubClass SubClass) {
            int retVal = -1;
            SubClass SubClassToSave = new SubClass();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SubClassAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", SubClass.Name);
                comm.Parameters.AddWithValue("@Description", SubClass.Description);

                comm.Parameters.Add("@SubClassID", SqlDbType.Int);
                comm.Parameters["@SubClassID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@SubClassID"].Value;
                SubClass.ID = ID;
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
        /// updates a specific SubClass object in the database
        /// </summary>
        /// <param name="SubClass"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateSubClass(SubClass SubClass, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            SubClass SubClassToSave = new SubClass();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SubClassUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SubClassID", id);
                comm.Parameters.AddWithValue("@Name", SubClass.Name);
                comm.Parameters.AddWithValue("@Description", SubClass.Description);
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
        /// deletes a specific SubClass object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteSubClass(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_SubClassDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@SubClassID", ID);
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
            List<Weapon> Weapons = new List<Weapon>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocWeaponsGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    Weapon Weapon = new Weapon(dr);
                    Weapons.Add(Weapon);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return Weapons;
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
        /// <param name="Weapon"></param>
        /// <returns></returns>
        public static int CreateWeapon(Weapon Weapon) {
            int retVal = -1;
            Weapon WeaponToSave = new Weapon();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", Weapon.Name);
                comm.Parameters.AddWithValue("@AttackDiceMedium", Weapon.AttackDiceMedium);
                comm.Parameters.AddWithValue("@AttackDiceSmall", Weapon.AttackDiceSmall);
                comm.Parameters.AddWithValue("@AttackRange", Weapon.AttackRange);
                comm.Parameters.AddWithValue("@Critical", Weapon.Critical);
                comm.Parameters.AddWithValue("@DamageTypeID", Weapon.DamageTypeID);
                comm.Parameters.AddWithValue("@GPValue", Weapon.GPValue);
                comm.Parameters.AddWithValue("@MaterialID", Weapon.MaterialID);
                comm.Parameters.AddWithValue("@SpecialAttributes", Weapon.SpecialAttributes);
                comm.Parameters.AddWithValue("@WeaponTypeID", Weapon.WeaponTypeID);
                comm.Parameters.AddWithValue("@Weight", Weapon.Weight);

                comm.Parameters.Add("@WeaponID", SqlDbType.Int);
                comm.Parameters["@WeaponID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@WeaponID"].Value;
                Weapon.ID = ID;
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
        /// <param name="Weapon"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateWeapon(Weapon Weapon, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            Weapon WeaponToSave = new Weapon();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponID", id);
                comm.Parameters.AddWithValue("@Name", Weapon.Name);
                comm.Parameters.AddWithValue("@AttackDiceMedium", Weapon.AttackDiceMedium);
                comm.Parameters.AddWithValue("@AttackDiceSmall", Weapon.AttackDiceSmall);
                comm.Parameters.AddWithValue("@AttackRange", Weapon.AttackRange);
                comm.Parameters.AddWithValue("@Critical", Weapon.Critical);
                comm.Parameters.AddWithValue("@DamageTypeID", Weapon.DamageTypeID);
                comm.Parameters.AddWithValue("@GPValue", Weapon.GPValue);
                comm.Parameters.AddWithValue("@MaterialID", Weapon.MaterialID);
                comm.Parameters.AddWithValue("@SpecialAttributes", Weapon.SpecialAttributes);
                comm.Parameters.AddWithValue("@WeaponTypeID", Weapon.WeaponTypeID);
                comm.Parameters.AddWithValue("@Weight", Weapon.Weight);
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

        #region WeaponSubTypes
        /// <summary>
        /// Gets all WeaponSubType objects from the database
        /// </summary>
        /// <returns></returns>
        public static List<WeaponSubType> GetWeaponSubTypes() {
            List<WeaponSubType> WeaponSubTypes = new List<WeaponSubType>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocWeaponSubTypesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    WeaponSubType WeaponSubType = new WeaponSubType(dr);
                    WeaponSubTypes.Add(WeaponSubType);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return WeaponSubTypes;
        }

        /// <summary>
        /// gets a specific WeaponSubType from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static WeaponSubType GetWeaponSubType(int id) {
            SqlCommand comm = new SqlCommand("sprocWeaponSubTypeGet");
            WeaponSubType retObj = null;
            try {
                comm.Parameters.AddWithValue("@WeaponSubTypeID", id);
                SqlDataReader dr = GetDataReader(comm);
                while(dr.Read()) {
                    retObj = new WeaponSubType(dr);
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
        /// inserts an WeaponSubType object in the database
        /// </summary>
        /// <param name="WeaponSubType"></param>
        /// <returns></returns>
        public static int CreateWeaponSubType(WeaponSubType WeaponSubType) {
            int retVal = -1;
            WeaponSubType WeaponSubTypeToSave = new WeaponSubType();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponSubTypeAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", WeaponSubType.Name);

                comm.Parameters.Add("@WeaponSubTypeID", SqlDbType.Int);
                comm.Parameters["@WeaponSubTypeID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@WeaponSubTypeID"].Value;
                WeaponSubType.ID = ID;
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
        /// updates a specific WeaponSubType object in the database
        /// </summary>
        /// <param name="WeaponSubType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateWeaponSubType(WeaponSubType WeaponSubType, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            WeaponSubType WeaponSubTypeToSave = new WeaponSubType();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponSubTypeUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponSubTypeID", id);
                comm.Parameters.AddWithValue("@Name", WeaponSubType.Name);
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
        /// deletes a specific WeaponSubType object from the database
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteWeaponSubType(int ID) {
            int retVal = -1;
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponSubTypeDelete");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponSubTypeID", ID);
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
            List<WeaponType> WeaponTypes = new List<WeaponType>();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sprocWeaponTypesGetAll");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Connection = conn;
                SqlDataReader dr = comm.ExecuteReader();
                while(dr.Read()) {
                    WeaponType WeaponType = new WeaponType(dr);
                    WeaponTypes.Add(WeaponType);
                }
            }
            catch(Exception error) {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }
            finally {
                if(conn != null) conn.Close();
            }
            return WeaponTypes;
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
        /// <param name="WeaponType"></param>
        /// <returns></returns>
        public static int CreateWeaponType(WeaponType WeaponType) {
            int retVal = -1;
            WeaponType WeaponTypeToSave = new WeaponType();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponTypeAdd");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@Name", WeaponType.Name);

                comm.Parameters.Add("@WeaponTypeID", SqlDbType.Int);
                comm.Parameters["@WeaponTypeID"].Direction = ParameterDirection.Output;

                comm.Connection = conn;
                retVal = comm.ExecuteNonQuery();
                int ID = (int)comm.Parameters["@WeaponTypeID"].Value;
                WeaponType.ID = ID;
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
        /// <param name="WeaponType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int UpdateWeaponType(WeaponType WeaponType, int id) {
            int retVal = -1;
            if(id < 0) {
                return retVal;
            }
            WeaponType WeaponTypeToSave = new WeaponType();
            SqlConnection conn = null;
            try {
                conn = new SqlConnection(ConnectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("sproc_WeaponTypeUpdate");
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@WeaponTypeID", id);
                comm.Parameters.AddWithValue("@Name", WeaponType.Name);
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

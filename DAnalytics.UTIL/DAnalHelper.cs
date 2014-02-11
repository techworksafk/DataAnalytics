using System;
using System.Data;
using System.IO;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.InteropServices;

namespace DAnalytics.UTIL
{
    
    /// <summary>
    /// Utility Layer class for performing common and basic operations
    /// </summary>
    public static class DAnalHelper
    {

        #region Conversions

        public static double ToUnixTimestamp(this DateTime dateTime)
        {
            TimeSpan span = (dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return span.TotalMilliseconds;
        }
        /// <summary>
        /// Convert to Int32
        /// </summary>
        /// <param name="data">Data to be converted</param>
        /// <returns>Int32 of the passed data</returns>
        public static Int32 ConvertToInt32(this object data)
        {
            Int32 result = 0;
            if (data == null)
                return 0;
            if (data != null && data != DBNull.Value)
            {
                if (data.IsInteger())
                {
                    result = Convert.ToInt32(data.ToString());
                }
            }
            return result;
        }

        /// <summary>
        /// Convert to Int64
        /// </summary>
        /// <param name="data">Data to be converted</param>
        /// <returns>Int32 of the passed data</returns>
        public static Int64 ConvertToInt64(this object data)
        {
            Int64 result = 0;
            if (data == null)
                return 0;
            if (data != null && data != DBNull.Value)
            {
                if (data.IsInteger())
                {
                    result = Convert.ToInt64(data.ToString());
                }
            }
            return result;
        }

        /// <summary>
        /// Convert to Double
        /// </summary>
        /// <param name="data">Data to be converted</param>
        /// <returns>Double representation of the passed data</returns>
        public static double ConvertToDouble(this object data)
        {
            double result = 0;
            if (data == null)
                return 0;
            if (data != null && data != DBNull.Value)
            {
                Double.TryParse(data.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// Convert to Double
        /// </summary>
        /// <param name="data">Data to be converted</param>
        /// <returns>Double representation of the passed data</returns>
        public static decimal ConvertToDecimal(this object data)
        {
            decimal result = 0;
            if (data == null)
                return 0;
            if (data != null && data != DBNull.Value)
            {
                Decimal.TryParse(data.ToString(), out result);
            }
            return result;
        }

        /// <summary>
        /// Convert to String
        /// </summary>
        /// <param name="data">Data to be converted</param>
        /// <returns>String data</returns>
        public static string ConvertToString(this object data)
        {
            string result = string.Empty;
            if (data == null)
                return "";
            if (data != null && data != DBNull.Value)
            {
                result = data.ToString();
            }
            return result;
        }

        public static DateTime ConvertToSysDate(this string ddmmyyyy)
        {
            DateTime result = DateTime.Now;
            if (!string.IsNullOrEmpty(ddmmyyyy) && ddmmyyyy.Trim().Length == 10)
            {
                string[] date = ddmmyyyy.Split('/');
                result = new DateTime(ConvertToInt32(date[2]), ConvertToInt32(date[1]), ConvertToInt32(date[0]));
            }
            return result;
        }

        public static DateTime ConvertToDate(this string dt)
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("en-GB");
            return Convert.ToDateTime(dt, culture);
        }

        public static byte[] ConvertStreamToByteArray(Stream input,long contentLength)
        {

            byte[] buffer = new byte[contentLength];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        #endregion

        #region Validations

        /// <summary>
        /// Check for DBNull
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsDBNull(this object value)
        {
            bool isDBNull = false;
            isDBNull = (value == DBNull.Value) ? true : false;
            return isDBNull;
        }

        /// <summary>
        /// check for Integer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsInteger(this object data)
        {
            bool result = false;
            if (data == null)
                return false;
            Regex r = new Regex(@"^\d+$");
            if (r.IsMatch(data.ToString()))
                result = true;
            return result;
        }

        /// <summary>
        /// Check for valid date
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsDate(this object data)
        {
            bool result = false;
            try
            {
                DateTime valiDate = Convert.ToDateTime(data);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }

        #endregion

        /// <summary>
        /// Get the value from the config file for given key
        /// </summary>
        /// <param name="key">The key of the configuration setting</param>
        /// <returns>Value for the given key from config file</returns>
        public static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Get the connection string from the config file
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings[DAnalyticsConfig.CONNECTION_STRING_KEY].ConnectionString;
            }
        }

        /// <summary>
        /// get the string value given for a particular key from resource file
        /// </summary>
        /// <param name="key">Key to get the string.</param>
        /// <returns>Correspoding string for the given key.</returns>
        public static string GetStringResource(string key)
        {
            string resString = string.Empty;

            //Initialize resource manager with resource and assembly.
            ResourceManager resManager = new ResourceManager(DAnalyticsConfig.RESOURCE_NAME, System.Reflection.Assembly.GetExecutingAssembly());

            //Use the getstring method to get the value for the key.
            resString = resManager.GetString(key, System.Globalization.CultureInfo.CurrentUICulture);

            return resString;
        }

        /// <summary>
        /// Returns a control if one by that name exists in the hierarchy of the controls collection of the start control
        /// </summary>
        /// <param name="start"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static System.Web.UI.Control FindControl(System.Web.UI.Control start, string id)
        {
            System.Web.UI.Control foundControl;
            if (start != null)
            {
                foundControl = start.FindControl(id);
                if (foundControl != null)
                    return foundControl;

                foreach (System.Web.UI.Control c in start.Controls)
                {
                    foundControl = FindControl(c, id);
                    if (foundControl != null)
                        return foundControl;
                }
            }
            return null;
        }

        /// <summary>
        /// Function to clear the cache from memory        
        /// </summary>
        public static void NoCacheHeaders()
        {
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
            System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
            System.Web.HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            System.Web.HttpContext.Current.Response.Expires = -1;
        }

        /// <summary>
        /// To create and register the alert javascript for display alert messages on startup.
        /// </summary>
        /// <param name="type">Type of the page.</param>
        /// <param name="page">Page where the script should register.</param>
        /// <param name="key">Key for the script.</param>
        /// <param name="message">Message to be displayed as alert.</param>
        public static void GenerateJavaScriptAlert(Type type, System.Web.UI.Page page, string key, string message)
        {
            if (page == null || key == null || message == null)
                throw new ArgumentException("Argument can not be null.");

            StringBuilder alertJavascript = new StringBuilder();
            ClientScriptManager clientScriptManager = page.ClientScript;

            //Add the javascript code to the string builder.
            alertJavascript.Append("<script language='javascript' type='text/javascript'>");
            alertJavascript.Append("alert('" + message + "');");
            alertJavascript.Append("</script>");

            try
            {
                //Register the script to the given page.
                clientScriptManager.RegisterStartupScript(type, key, alertJavascript.ToString());
            }
            finally
            {
                //Dispose the objects.
                alertJavascript = null;
                clientScriptManager = null;
            }
        }

        /// <summary>
        /// To bind Listcontrol
        /// </summary>
        /// <typeparam name="T">Type of object to bind.This object must be a list</typeparam>
        /// <param name="listCtrl">Listcontrol</param>
        /// <param name="dataSource">Datasource</param>
        /// <param name="strDataTextField">Datatextfield</param>
        /// <param name="strDataValueField">Datavaluefield</param>
        /// <param name="firstItem">First item to be insert into lititem</param>
        /// <param name="itemValue">First item value to be insert into lititem</param>
        public static void BindData<T>(System.Web.UI.WebControls.ListControl listCtrl, T dataSource, string dataTextField, string dataValueField, string firstItem, string itemValue)
        {
            listCtrl.DataSource = dataSource;
            listCtrl.DataTextField = dataTextField;
            listCtrl.DataValueField = dataValueField;
            listCtrl.DataBind();
            listCtrl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(firstItem, itemValue));
        }

        /// <summary>
        /// To bind gridview
        /// </summary>
        /// <typeparam name="T">Type of object to bind.This object must be a list</typeparam>
        /// <param name="dataBoundCtrl">CompositeDataBoundControl</param>
        /// <param name="dataSource">Datasource</param>
        public static void BindData<T>(System.Web.UI.WebControls.CompositeDataBoundControl dataBoundCtrl, T dataSource)
        {
            dataBoundCtrl.DataSource = dataSource;
            dataBoundCtrl.DataBind();
        }

        /// <summary>
        /// To bind gridview
        /// </summary>
        /// <typeparam name="T">Type of object to bind.This object must be a list</typeparam>
        /// <param name="rptrCtrl">Repeater control</param>
        /// <param name="dataSource">Datasource</param>
        public static void BindData<T>(System.Web.UI.WebControls.Repeater rptrCtrl, T dataSource)
        {
            rptrCtrl.DataSource = dataSource;
            rptrCtrl.DataBind();
        }

        /// <summary>
        /// To bind checkbox list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listCtrl"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataTextField"></param>
        /// <param name="dataValueField"></param>
        public static void BindData<T>(System.Web.UI.WebControls.ListControl listCtrl, T dataSource, string dataTextField, string dataValueField)
        {
            listCtrl.DataSource = dataSource;
            listCtrl.DataTextField = dataTextField;
            listCtrl.DataValueField = dataValueField;
            listCtrl.DataBind();
        }

        /// <summary>
        /// To reset all form controls
        /// </summary>
        /// <param name="root">Page</param>
        /// <param name="ctrlId">Content control</param>
        public static void ResetForm(Control root, string ctrlId)
        {
            Control start = FindControl(root, ctrlId);
            foreach (Control c in start.Controls)
            {
                if (c.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    ((System.Web.UI.WebControls.TextBox)c).Text = "";
                }
                else if (c.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    ((System.Web.UI.WebControls.DropDownList)c).SelectedIndex = 0;
                }
                else if (c.GetType().ToString() == "System.Web.UI.WebControls.ListBox")
                {
                    ((System.Web.UI.WebControls.ListBox)c).SelectedIndex = 0;
                }
                else if (c.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    ((System.Web.UI.WebControls.CheckBox)c).Checked = false;
                }
            }
        }

        public static void LogError(string exceptionMessage, string errorPage)
        {
            System.Data.SqlClient.SqlParameter[] sqlParams = new System.Data.SqlClient.SqlParameter[2];

            sqlParams[0] = UTIL.SqlHelper.CreateParameter("@ErrorMsg", exceptionMessage, SqlDbType.Text, ParameterDirection.Input);

            sqlParams[1] = UTIL.SqlHelper.CreateParameter("@ErrorPage", errorPage, SqlDbType.Text, ParameterDirection.Input);

            UTIL.SqlHelper.ExecuteScalar(UTIL.DAnalHelper.ConnectionString, CommandType.StoredProcedure, "usp_Error_Log", sqlParams);
        }

        #region "Serialize/Deserialize"

        /// <summary>
        /// To generate XML fron the given "serializable" class and its type
        /// </summary>
        /// <param name="dataToSerialize">Serializable class to generate XML.</param>
        /// <returns>XML as string generated from the given serializable class.</returns>
        public static string Serialize(object dataToSerialize)
        {
            string returnXMLString = string.Empty;
            StringBuilder returnXML = null;
            XmlSerializer xmlSerializer = null;
            TextWriter txtWriter = null;
            StringReader reader = null;
            XmlDocument xmlDoc = null;

            try
            {

                //Validate the argument and raise an argument exception if invalid.
                if (dataToSerialize == null)
                    throw new ArgumentException("Argument can not be null.");

                //Initialize the string builder.
                returnXML = new StringBuilder();
                //Initialize the XMLSerializer with the type of the object used for serializing.
                xmlSerializer = new XmlSerializer(dataToSerialize.GetType());
                //Initialize the string writer for writing the serialized XML to the stringbuilder.
                txtWriter = new StringWriter(returnXML);

                //Serialize the data and write to the string builder.
                xmlSerializer.Serialize(txtWriter, dataToSerialize);

                //Here we can have the xml from the "returnXML" object. But that contains the 
                //version informations and meta datas. To remove them (for DB purpose) load the 
                //xml to the dataset then create a xmldocument from the dataset. Then take the 
                //outerxml of the xml document.

                //Read the Xml from the StringReader object
                reader = new StringReader(txtWriter.ToString());

                //Initialize the dataset object.
                DataSet dsXML = new DataSet();
                //Read the xml from the reader.
                dsXML.ReadXml(reader);

                //Initialize new xml document.
                xmlDoc = new XmlDocument();
                //Load the xml from the dataset.
                xmlDoc.LoadXml(dsXML.GetXml());
                //Assign the outxml to the string variable.
                returnXMLString = xmlDoc.OuterXml;
            }
            finally
            {
                //Dispose the objects.
                if (xmlSerializer != null)
                    xmlSerializer = null;

                if (txtWriter != null)
                {
                    txtWriter.Dispose();
                }

                if (returnXML != null)
                    returnXML = null;

                if (reader != null)
                {
                    reader.Dispose();
                }

                if (xmlDoc != null)
                    xmlDoc = null;
            }

            return returnXMLString;
        }

        /// <summary>
        /// To deserialize the given xml and assign data to the given serializable class
        /// </summary>
        /// <param name="dataToDeserialize">Serializable class to populate values.</param>
        /// <param name="xmlToDeserialize">Corresponding XML string to deserialize the given class.</param>
        /// <returns>Populated given class as object.</returns>
        public static object Deserialize(object dataToDeserialize, string xmlToDeserialize)
        {

            XmlSerializer xmlSerializer = null;
            TextReader txtReader = null;

            //Validate the arguments and raise an argument exception if invalid.
            if (dataToDeserialize == null || xmlToDeserialize == null)
                throw new ArgumentException("Argument can not be null.");

            try
            {
                //Initialize textreader and Read the given xml string.
                txtReader = new StringReader(xmlToDeserialize);

                //Initialize the XMLSerializer with the type of the object used for deserializing.
                xmlSerializer = new XmlSerializer(dataToDeserialize.GetType());

                //Deserialize the xml and assign to the given object.
                dataToDeserialize = xmlSerializer.Deserialize(txtReader);

            }
            finally
            {
                //Dispose the objects.
                if (xmlSerializer != null)
                    xmlSerializer = null;

                if (txtReader != null)
                {
                    txtReader.Dispose();
                }
            }

            return dataToDeserialize;
        }

        /// <summary>
        /// To modify the given xml and deserialize to assign data to the given serializable entity class
        /// </summary>
        /// <param name="dataToDeserialize">Serializable class to populate values.</param>
        /// <param name="xmlToDeserialize">XML to modify as string.</param>
        /// <param name="datasetName">DataSetName to remove from the xml.</param>
        /// <param name="tableName">TableName to replace with entityname in the xml.</param>
        /// <param name="entityName">Entity name to replace tablename in the xml.</param>
        /// <returns>Populated given class as object.</returns>
        public static object Deserialize(object dataToDeserialize, string xmlToDeserialize, string datasetName)
        {
            string xmlForSingleEntity = string.Empty;

            //Validate the argument and raise an argument exception if invalid.
            if (dataToDeserialize == null || xmlToDeserialize == null || datasetName == null)
                throw new ArgumentException("Argument can not be null.");

            //As the given xml is generated from dataset.getxml() method it contains the  dataset/table 
            //default name or given name. For deserializing the xml to a single entity we need to remove 
            //the dataset name and modify the table name to match entity name.
            //XML.substring(
            //PARAM 1 : datasetName.Length + 2 = given datasetname + 2(for '<' & '>') as startindex (So it will cut 
            //the dataset name start tag).
            //PARAM 2 :(xmlToDeSerialize.Length - (datasetName.Length + 2)) - (datasetName.Length + 3)
            //As we have removed the start tag current string length = total length - removed start tag length.
            //After calculating the current string length remove the endtag (Dataset name) by 
            //subtracting dataset name + 3 (For '<' & '/' & '>').
            xmlForSingleEntity = xmlToDeserialize.Substring(datasetName.Length + 2, (xmlToDeserialize.Length - (datasetName.Length + 2)) - (datasetName.Length + 3));

            //Call the deserialize function to deserialize the modified xml to the given class object.
            return Deserialize(dataToDeserialize, xmlForSingleEntity);
        }

        /// <summary>
        /// To modify the given xml and deserialize to assign data to the given serializable entity class
        /// </summary>
        /// <param name="dataToDeserialize">Serializable class to populate values.</param>
        /// <param name="xmlToDeserialize">XML to modify as string.</param>
        /// <param name="datasetName">DataSetName to remove from the xml.</param>
        /// <param name="tableName">TableName to replace with entityname in the xml.</param>
        /// <param name="entityName">Entity name to replace tablename in the xml.</param>
        /// <returns>Populated given class as object.</returns>
        public static object Deserialize(object dataToDeserialize, string xmlToDeserialize, string datasetName, string tableName, string entityName)
        {
            string xmlForSingleEntity = string.Empty;

            //Validate the argument and raise an argument exception if invalid.
            if (dataToDeserialize == null || xmlToDeserialize == null || datasetName == null || tableName == null || entityName == null)
                throw new ArgumentException("Argument can not be null.");

            //As the given xml is generated from dataset.getxml() method it contains the  dataset/table 
            //default name or given name. For deserializing the xml to a single entity we need to remove 
            //the dataset name and modify the table name to match entity name.
            //XML.substring(
            //PARAM 1 : datasetName.Length + 2 = given datasetname + 2(for '<' & '>') as startindex (So it will cut 
            //the dataset name start tag).
            //PARAM 2 :(xmlToDeSerialize.Length - (datasetName.Length + 2)) - (datasetName.Length + 3)
            //As we have removed the start tag current string length = total length - removed start tag length.
            //After calculating the current string length remove the endtag (Dataset name) by 
            //subtracting dataset name + 3 (For '<' & '/' & '>').
            xmlForSingleEntity = xmlToDeserialize.Substring(datasetName.Length + 2, (xmlToDeserialize.Length - (datasetName.Length + 2)) - (datasetName.Length + 3));

            //Replace the tablename with entity name.
            xmlForSingleEntity = xmlForSingleEntity.Replace(tableName, entityName);

            //Call the deserialize function to deserialize the modified xml to the given class object.
            return Deserialize(dataToDeserialize, xmlForSingleEntity);
        }

        /// <summary>
        /// To modify the given xml and deserialize to assign data to the given serializable collection class
        /// </summary>
        /// <param name="dataToDeserialize">Serializable class to populate values.</param>
        /// <param name="xmlToDeserialize">XML to modify as string.</param>
        /// <param name="datasetName">DataSetName to replace with collectionname in the xml.</param>
        /// <param name="collectionName">Collection name to replace the dataset name.</param>
        /// <param name="tableName">TableName to replace with entityname in the xml.</param>
        /// <param name="entityName">Entity name to replace tablename in the xml.</param>
        /// <returns>Populated given class as object.</returns>
        public static object Deserialize(object dataToDeserialize, string xmlToDeserialize, string datasetName, string collectionName, string tableName, string entityName)
        {
            string xmlForCollection = string.Empty;

            //Validate the argument and raise an argument exception if invalid.
            if (dataToDeserialize == null || xmlToDeserialize == null || datasetName == null || collectionName == null || tableName == null || entityName == null)
                throw new ArgumentException("Argument can not be null.");

            //Replace the datasetname with collection name.
            xmlForCollection = xmlToDeserialize.Replace(datasetName, collectionName);
            //Replace the tablename with entity name.
            xmlForCollection = xmlForCollection.Replace(tableName, entityName);

            //Call the deserialize function to deserialize the modified xml to the given class object.
            return Deserialize(dataToDeserialize, xmlForCollection);
        }

        #endregion

        #region Directory

        /// <summary>
        /// To create a new directory at the given location.
        /// </summary>
        /// <param name="directoryName">Full path including directory name.</param>
        public static void CreateDirectory(string directoryName)
        {

            //Check whether the given directory exists and if exists then delete the directory.
            DeleteDirectory(directoryName);

            //Create the directory.
            Directory.CreateDirectory(directoryName);
        }

        /// <summary>
        /// To delete a given directory and containing files from the given location.
        /// </summary>
        /// <param name="directoryName">Full path including directory name.</param>
        public static void DeleteDirectory(string directoryName)
        {
            //Check whether the given directory exists and if exists then delete the directory.
            if (Directory.Exists(directoryName))
            {
                //Delete the directory. Second parameter is given to remove all sub directories
                //and files while deleting. 
                Directory.Delete(directoryName, true);
            }
        }

        #endregion       

        public static DateTime FirstDayOfMonth(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static DateTime LastDayOfMonth(DateTime dateTime)
        {
            DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

    }

    public class DAnalyticsCredentials : Microsoft.Reporting.WebForms.IReportServerCredentials
    {
        string _userName, _password, _domain;

        public DAnalyticsCredentials(string userName, string password, string domain)
        {
            _userName = userName;
            _password = password;
            _domain = domain;
        }

        public System.Security.Principal.WindowsIdentity ImpersonationUser
        {
            get { return null; }
        }

        public System.Net.ICredentials NetworkCredentials
        {
            get { return new System.Net.NetworkCredential(_userName, _password, _domain); }
        }

        public bool GetFormsCredentials(out System.Net.Cookie authCoki, out string userName, out string password, out string authority)
        {
            userName = null;// _userName;
            password = null;// _password;
            authority = null;// _domain;
            authCoki = null;// new System.Net.Cookie(".ASPXAUTH", ".ASPXAUTH", "/", "Domain");
            return false;
        }
    }

    public static class NativeMethods
    {
        /// <summary>
        /// The type of logon operation to perform.
        /// </summary>
        public enum LogonType : int
        {
            /// <summary>
            /// This logon type is intended for users who will be interactively
            /// using the computer, such as a user being logged on by a
            /// terminal server, remote shell, or similar process.
            /// This logon type has the additional expense of caching logon
            /// information for disconnected operations; therefore, it is
            /// inappropriate for some client/server applications, such as a
            /// mail server.
            /// </summary>
            Interactive = 2,

            /// <summary>
            /// This logon type is intended for high performance servers to
            /// authenticate plaintext passwords.
            /// The LogonUser function does not cache credentials for this
            /// logon type.
            /// </summary>
            Network = 3,

            /// <summary>
            /// This logon type is intended for batch servers, where processes
            /// may be executing on behalf of a user without their direct
            /// intervention.  This type is also for higher performance servers
            /// that process many plaintext authentication attempts at a time,
            /// such as mail or Web servers.
            /// The LogonUser function does not cache credentials for this
            /// logon type.
            /// </summary>
            Batch = 4,

            /// <summary>
            /// Indicates a service-type logon.  The account provided must have
            /// the service privilege enabled.
            /// </summary>
            Service = 5,

            /// <summary>
            /// This logon type is for GINA DLLs that log on users who will be
            /// interactively using the computer.
            /// This logon type can generate a unique audit record that shows
            /// when the workstation was unlocked.
            /// </summary>
            Unlock = 7,

            /// <summary>
            /// This logon type preserves the name and password in the
            /// authentication package, which allows the server to make
            /// connections to other network servers while impersonating the
            /// client.  A server can accept plaintext credentials from a
            /// client, call LogonUser, verify that the user can access the
            /// system across the network, and still communicate with other
            /// servers.
            /// NOTE: Windows NT:  This value is not supported.
            /// </summary>
            NetworkCleartext = 8,

            /// <summary>
            /// This logon type allows the caller to clone its current token
            /// and specify new credentials for outbound connections.  The new
            /// logon session has the same local identifier but uses different
            /// credentials for other network connections.
            /// NOTE: This logon type is supported only by the
            /// LOGON32_PROVIDER_WINNT50 logon provider.
            /// NOTE: Windows NT:  This value is not supported.
            /// </summary>
            NewCredentials = 9
        }

        /// <summary>
        /// Specifies the logon provider.
        /// </summary>
        public enum LogonProvider : int
        {
            /// <summary>
            /// Use the standard logon provider for the system.
            /// The default security provider is negotiate, unless you pass
            /// NULL for the domain name and the user name is not in UPN format.
            /// In this case, the default provider is NTLM.
            /// NOTE: Windows 2000/NT:   The default security provider is NTLM.
            /// </summary>
            Default = 0,

            /// <summary>
            /// Use this provider if you'll be authenticating against a Windows
            /// NT 3.51 domain controller (uses the NT 3.51 logon provider).
            /// </summary>
            WinNT35 = 1,

            /// <summary>
            /// Use the NTLM logon provider.
            /// </summary>
            WinNT40 = 2,

            /// <summary>
            /// Use the negotiate logon provider.
            /// </summary>
            WinNT50 = 3
        }

        /// <summary>
        /// The type of logon operation to perform.
        /// </summary>
        public enum SecurityImpersonationLevel : int
        {
            /// <summary>
            /// The server process cannot obtain identification information
            /// about the client, and it cannot impersonate the client.  It is
            /// defined with no value given, and thus, by ANSI C rules,
            /// defaults to a value of zero.
            /// </summary>
            Anonymous = 0,

            /// <summary>
            /// The server process can obtain information about the client,
            /// such as security identifiers and privileges, but it cannot
            /// impersonate the client.  This is useful for servers that export
            /// their own objects, for example, database products that export
            /// tables and views.  Using the retrieved client-security
            /// information, the server can make access-validation decisions
            /// without being able to use other services that are using the
            /// client's security context.
            /// </summary>
            Identification = 1,

            /// <summary>
            /// The server process can impersonate the client's security
            /// context on its local system.  The server cannot impersonate the
            /// client on remote systems.
            /// </summary>
            Impersonation = 2,

            /// <summary>
            /// The server process can impersonate the client's security
            /// context on remote systems.
            /// NOTE: Windows NT:  This impersonation level is not supported.
            /// </summary>
            Delegation = 3
        }

        /// <summary>
        /// Logs on the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="domain">The domain.</param>
        /// <param name="password">The password.</param>
        /// <param name="logonType">Type of the logon.</param>
        /// <param name="logonProvider">The logon provider.</param>
        /// <param name="token">The token.</param>
        /// <returns>True if the function succeeds, false if the function fails.
        /// To get extended error information, call GetLastError.</returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LogonUser(
            string userName,
            string domain,
            string password,
            LogonType logonType,
            LogonProvider logonProvider,
            out IntPtr token);

        /// <summary>
        /// Duplicates the token.
        /// </summary>
        /// <param name="existingTokenHandle">The existing token
        /// handle.</param>
        /// <param name="securityImpersonationLevel">The security impersonation
        /// level.</param>
        /// <param name="duplicateTokenHandle">The duplicate token
        /// handle.</param>
        /// <returns>True if the function succeeds, false if the function fails.
        /// To get extended error information, call GetLastError.</returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DuplicateToken(
            IntPtr existingTokenHandle,
            SecurityImpersonationLevel securityImpersonationLevel,
            out IntPtr duplicateTokenHandle);

        /// <summary>
        /// Closes the handle.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns>True if the function succeeds, false if the function fails.
        /// To get extended error information, call GetLastError.</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr handle);
    }
}

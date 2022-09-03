using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{

    public const string SessionOrganization = "organizationId";

    public const string SessionAllDataForPrint = "SessionAllDataForPrint";

    public const string ViewStateEncounterData = "EMR_EncounterData";
    public const string ViewStatePageData = "EMR_PageData";
    public const string ViewStatePatientHistoryInner = "EMR_PatientHistoryInner";
    public const string sessionPharmacist = "sessionPharmacist";
    public const string sessionPatientHistoryLite = "sessionPatientHistoryLite";
    public const string ViewStatePatientHistoryEye = "EMR_PatientHistoryEye";
    public const string ViewStatePatientHistoryMove = "EMR_PatientHistoryMove";
    public const string ViewStatePatientHistoryVerbal = "EMR_PatientHistoryVerbal";
    public const string ViewStatePatientHistoryDoseUOM = "EMR_PatientHistoryDoseUOM";
    public const string ViewStateListData = "EMR_ListData";
    public const string SessionMedicalHistoryFiltered = "EMR_medicalHistoryFiltered";
    public const string SessionMedicalHistory = "EMR_medicalHistory";
    public const string ViewStatePatientHistoryPatientType = "EMR_PatientHistoryPatientType";
    public const string ViewStatePatientHistoryHOPEemr = "EMR_PatientHistoryHOPEemr";
    public const string ViewStateOtherUnitMR = "EMR_OtherUnitMR";
    public const string ViewStatePatientHistoryCompound = "EMR_PatientHistoryCompound";
    public const string ViewStateScannedData = "EMR_ScannedData";
    public const string SessionPreviousRowIndex = "EMR_PreviousRowIndex";//PreviousRowIndex
    //add by carel 2019-10-02
    public const string CollectionDischargeProcess = "BO_CollectionDischargeProcess";
    public const string CommonColorGrid = "BO_CommonColorGrid";
    public const string wardcollection = "BO_wardcollection";
    public const string SessionWard = "BO_SessionWard";
    public const string CollectionDischargePlan = "BO_CollectionDischargePlan";
    public const string CollectionDischargeDone = "BO_CollectionDischargeDone";
    //close add by carel

    public const string SessionListTeleConsult = "PO_SessionListTeleConsult";
    public const string SessionRevHeader = "EMR_RevisionHeader";
    public const string SessionRevSoap = "EMR_RevisionSoap";
    public const string SessionRevCpoe = "EMR_RevisionCpoe";
    public const string SessionRevPres = "EMR_RevisionPrescription";

    public const string SessionRacikanDetail = "PharOff_SessionRacikanDetail";
    public const string SessionRacikanDetailAdd = "PharOff_SessionRacikanDetailAdd";

    public const string SessionPatientList = "PharOff_SessionPatientListOPDCheckpoint";

    public const string SessionDocumentData = "Viewwer_DocumentData";


    public Helper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static long organizationId
    {
        get
        {
            if (HttpContext.Current.Session[SessionOrganization] != null)
            {
                return long.Parse(HttpContext.Current.Session[SessionOrganization].ToString());
            }
            else
                return 0;
        }
    }

    public static DataTable ToDataTable<T>(List<T> items)
    {
        DataTable dataTable = new DataTable(typeof(T).Name);

        //Get all the properties
        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (PropertyInfo prop in Props)
        {
            //Defining type of data column gives proper data table 
            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
            //Setting column names as Property names
            dataTable.Columns.Add(prop.Name, type);
        }
        foreach (T item in items)
        {
            var values = new object[Props.Length];
            for (int i = 0; i < Props.Length; i++)
            {
                //inserting property values to datatable rows
                values[i] = Props[i].GetValue(item, null);
            }
            dataTable.Rows.Add(values);
        }
        //put a breakpoint here and check datatable
        return dataTable;
    }

    public static string formatDecimal(string ColumnOfTable)
    {
        string[] temp = ColumnOfTable.ToString().Split('.');

        if (temp.Count() > 1)
        {
            if (temp[1].Length == 3)
            {
                if (temp[1] == "000")
                {
                    ColumnOfTable = decimal.Parse(temp[0]).ToString();
                }
                else if (temp[1].Substring(temp[1].Length - 2) == "00")
                {
                    ColumnOfTable = temp[0] + "." + temp[1].Substring(0, 1);
                }
                else if (temp[1].Substring(temp[1].Length - 1) == "0")
                {
                    ColumnOfTable = temp[0] + "." + temp[1].Substring(0, 2);
                }
            }
        }

        return ColumnOfTable;
    }

    public static string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }
}
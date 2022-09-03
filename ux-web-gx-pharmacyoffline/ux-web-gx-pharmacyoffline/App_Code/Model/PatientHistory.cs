using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientHistory
/// </summary>
public class PatientHistory
{
    public PatientHistory()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}

public class physicalExm
{
    public int idph { get; set; }
    public string name { get; set; }
}

public class PatientHeader //EMR Extension
{
    public Guid EncounterId { get; set; }
    public string PatientName { get; set; }
    public int Gender { get; set; }
    public string MrNo { get; set; }
    public string DoctorName { get; set; }
    public string AdmissionNo { get; set; }
    public DateTime BirthDate { get; set; }
    public string Religion { get; set; }
    public string PayerName { get; set; }
    public long PayerId { get; set; }
    public Int16 AdmissionTypeId { get; set; }
    public string Formularium { get; set; }
}

public class ResultPatientHeader
{
    //private List<Doctor> lists = new List<Doctor>();
    [JsonProperty("data")]
    public PatientHeader header { get; set; }
}

public class ScannedMR
{
    public Int64? OrganizationId { get; set; }
    public Int64? PatientId { get; set; }
    public Int64? AdmissionId { get; set; }
    public string AdmissionNo { get; set; }
    public string AdmissionDate { get; set; }
    public Int64? MrNo { get; set; }
    public string AdmissionType { get; set; }
    public string FormTypeName { get; set; }
    public string Path { get; set; }
    public string DoctorName { get; set; }

}
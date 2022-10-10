using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdmissionMRDetail
/// </summary>
public class AdmissionMRDetail
{
    public long PatientId { get; set; }
    public long AdmissionId { get; set; }
    public string AdmissionNo { get; set; }
    public string AdmissionDate { get; set; }
    public string DoctorName { get; set; }
    public short AdmissionTypeID { get; set; }
}
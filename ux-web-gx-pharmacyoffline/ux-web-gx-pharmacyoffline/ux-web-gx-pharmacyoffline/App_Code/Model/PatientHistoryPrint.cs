using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientHistoryPrint
/// </summary>
public class PatientHistoryPrint
{
    public PatientHistoryHeaderPrint header { get; set; }
    public List<PatientHistoryPrescriptionPrint> prescription { get; set; }
}

public class PatientHistoryHeaderPrint
{
    public string Admission { get; set; }
    public string DoctorName { get; set; }
    public string LocalMrNo { get; set; }
    public string PatientName { get; set; }
    public string BirthDate { get; set; }
    public string Gender { get; set; }
    public string PatientComplaint { get; set; }
    public string Anamnesis { get; set; }
    public string Objective { get; set; }
    public string Diagnosis { get; set; }
    public string PlanningProcedure { get; set; }
}

public class PatientHistoryPrescriptionPrint
{
    public string ItemName { get; set; }
    public string Quantity { get; set; }
    public string Uom { get; set; }
    public string Frequency { get; set; }
    public string Dose { get; set; }
    public string DoseUom { get; set; }
    public string Instruction { get; set; }
    public string Route { get; set; }
    public int Iter { get; set; }
    public string Routine { get; set; }
}


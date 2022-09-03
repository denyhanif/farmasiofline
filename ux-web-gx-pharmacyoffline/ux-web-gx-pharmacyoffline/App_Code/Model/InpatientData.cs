using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InpatientData
/// </summary>
public class InpatientData
{
    public Nullable<int> status_id { get; set; }
    public string user_id { get; set; }
    public Nullable<int> operation_schedule_additional_id { get; set; }
    public Nullable<Guid> operation_schedule_id { get; set; }
    public Nullable<Guid> encounter_id { get; set; }
    public string patient_id { get; set; }
    public bool? use_tools { get; set; }
    public string tools_detail { get; set; }
    public Nullable<int> category { get; set; }
    public string category_detail { get; set; }
    public string recovery_room { get; set; }
    public bool? fasting_procedure { get; set; }
    public Nullable<int> fasting_procedure_time { get; set; }
    public string other_rad { get; set; }
    public string other_lab { get; set; }
    public string instruction { get; set; }
    public string remarks { get; set; }
    public string doctor_id { get; set; }
    public string doctor_name { get; set; }
    public string diagnosis { get; set; }
    public string admission_date { get; set; }
    public string ward_id { get; set; }
    public string ward_name { get; set; }
    public string estimation_day { get; set; }
    public string patientName { get; set; }
    public string birthDate { get; set; } // date
    public string umur { get; set; }
    public string sexId { get; set; }
    public string seks { get; set; }
    public string localMrNo { get; set; }
    public string create_encounter { get; set; }
    public string created_date { get; set; }
    public string created_by { get; set; }
    public string modified_date { get; set; }
    public string modified_by { get; set; }
    public bool? is_pregnancy { get; set; }
    public bool? is_edited { get; set; }

    public string spesialis_dokter { get; set; }

    public OperationScheduleHeader operation_schedule_header { get; set; }
    public List<LabRadAdditional> lab_Rad_Additionals { get; set; }
    public List<OperationProcedure> operation_procedures { get; set; }
}

public class OperationScheduleHeader
{
    public Nullable<Guid> operation_schedule_id { get; set; }
    public Nullable<long> organization_id { get; set; }
    public string operation_schedule_date { get; set; }
    public string incision_time { get; set; }
    public bool? is_infectious { get; set; }
    public bool? is_cito { get; set; }
    public Nullable<int> positioning_time { get; set; }
    public string anesthetia_type_name { get; set; }
    public Nullable<int> anesthetia_user_id { get; set; }
    public Nullable<int> anesthetia_duration { get; set; }
    public string recovery_room { get; set; }
    public string admitted_to_ward_date { get; set; }
    public string doctor_notes { get; set; }
    public bool? is_confirmed { get; set; }
    public bool? is_active { get; set; }
    public Nullable<Guid> room_id { get; set; }
    public string status_booking_id { get; set; }

    public string status_booking_name { get; set; }
    public string patient_id { get; set; }
    public string created_date { get; set; }
    public string created_by { get; set; }
    public string modified_date { get; set; }
    public string modified_by { get; set; }
    public bool? by_preop { get; set; }
    public bool? is_from_opd { get; set; }
    public Nullable<int> recovery_time { get; set; }
    public Nullable<int> recovery_room_id { get; set; }
    public string admission_no { get; set; }
    public string ipd_admission_no { get; set; }
    public string nurse_notes { get; set; }
    public Nullable<int> note_update { get; set; }
    public string note_update_string { get; set; }
    public string additional_note_update { get; set; }
    public string note_delete { get; set; }
    public Nullable<int> note_pembatalan { get; set; }
    public string note_pembatalan_string { get; set; }
    public string additional_note_pembatalan { get; set; }
    public string cancel_by { get; set; }
    public string cancel_date { get; set; }
    public bool? by_doctor { get; set; }
    public string status_date { get; set; }
    public bool? is_rujukan { get; set; }
    public bool? report_rujukan { get; set; }
    public string temp_patientname { get; set; }
    public string temp_dob { get; set; }
    public string temp_contactno { get; set; }
}
public class OperationProcedure
{
    public string operation_procedure_id { get; set; }
    public Nullable<Guid> operation_schedule_id { get; set; }
    public string procedure_name { get; set; }
    public Nullable<int> procedure_user_id { get; set; }
    public int procedure_estimate_time { get; set; }
    public Nullable<int> procedure_name_id { get; set; }
    public Nullable<int> asisten_operator_id { get; set; }
    public Nullable<int> konsultan_operator_id { get; set; }
    public string start_time { get; set; }
    public bool? is_active { get; set; }
    public string created_by { get; set; }
    public string created_date { get; set; }
    public string modified_by { get; set; }
    public string modified_date { get; set; }
}
public class LabRadAdditional
{
    public Nullable<int> lab_rad_additional_id { get; set; }
    public Nullable<int> operation_schedule_additional_id { get; set; }
    public Nullable<int> item_id { get; set; }
    public string item_name { get; set; }
    public string item_type { get; set; }
    public string item_type_name { get; set; }
    public bool? is_rad { get; set; }
    public string created_date { get; set; }
    public string created_by { get; set; }
    public string modified_date { get; set; }
    public string modified_by { get; set; }
    public bool? is_active { get; set; }
}

public class ResultInpatient
{
    private InpatientData lists = new InpatientData();
    [JsonProperty("data")]
    public InpatientData list { get { return lists; } set { lists = value; } }
    public string status { get; set; }
    public int code { get; set; }
    public string message { get; set; }
}

public class RecoveryRoom
{
    public long recovery_room_id { get; set; }
    public string recovery_room_name { get; set; }
}

public class ResultRecoveryRoom
{
    public List<RecoveryRoom> data { set; get; }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class CpoeTrans
{
    public Int64 id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public int isnew { get; set; }
    public int iscito { get; set; }
    public int issubmit { get; set; }
    public int isdelete { get; set; }
    public int ischeck { get; set; }
    public string remarks { get; set; }
    public int IsSendHope { get; set; } = 0;
    public bool IsFutureOrder { get; set; }
    public DateTime? FutureOrderDate { get; set; }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Functional
{
    public class PharmacyPickingList
    {
        public PharmacyPickingListHeader pickingListHeader { get; set; }
        public List<PharmacyPickingListPres> pickingListPres { get; set; }
        public List<PharmacyPickingListAllergy> pickingListAllergy { get; set; }
        public List<PharmacyPickingListObjective> pickingListObjective { get; set; }
        public List<PharmacyPickingListCompoundHeader> pickingListCompoundHeader { get; set; }
        public List<PharmacyPickingListCompoundDetail> pickingListCompoundDetail { get; set; }
    }
    public class PharmacyPickingListHeader
    {
        public Guid EncounterId { get; set; }
        public long OrganizationId { get; set; }
        public long PatientId { get; set; }
        public long AdmissionId { get; set; }
        public string AdmissionNo { get; set; }
        public string LocalMrNo { get; set; }
        public string PatientName { get; set; }
        public string BirthDate { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string DoctorName { get; set; }
        public string SpecialtyName { get; set; }
        public string PrescriptionNo { get; set; }
        public string PrescriptionDate { get; set; }
        public string PayerName { get; set; }
        public string QueueNo { get; set; }
        public bool IsCOVID { get; set; }
        public string SipNo { get; set; }
        public string PrintNumber { get; set; }
        public string StoreName { get; set; }
        public string IssueDate { get; set; }
        public string IssueBy { get; set; }
        public string IssueCode { get; set; }
        public string IssueAdmissionNo { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryFee { get; set; }
        public string DeliveryCourier { get; set; }
    }

    public class PharmacyPickingListPres
    {
        public long prescription_id { get; set; }
        public long item_id { get; set; }
        public string item_name { get; set; }
        public string quantity { get; set; }
        public long uom_id { get; set; }
        public string uom_code { get; set; }
        public long frequency_id { get; set; }
        public string frequency_code { get; set; }
        public string dosage_id { get; set; }
        public long dose_uom_id { get; set; }
        public string dose_uom { get; set; }
        public string dose_text { get; set; }
        public long administration_route_id { get; set; }
        public string administration_route_code { get; set; }
        public int iteration { get; set; }
        public string remarks { get; set; }
        public bool is_routine { get; set; }
        public bool is_consumables { get; set; }
        public Guid compound_id { get; set; }
        public string compound_name { get; set; }
        public long origin_prescription_id { get; set; }
        public string RackName { get; set; }
        public string PrescriptionDate { get; set; }
        public bool IsDoseText { get; set; }
        public string IssuedQty { get; set; }
        public int item_sequence { get; set; }
        public Guid editedId { get; set; }
        public string LocationDrug { get; set; }
    }

    public class PharmacyPickingListAllergy
    {
        public string allergy { get; set; }
        public string reaction { get; set; }
    }

    public class PharmacyPickingListObjective
    {
        public Guid soap_mapping_id { get; set; }
        public string soap_mapping_name { get; set; }
        public string value { get; set; }
    }

    public class PharmacyPickingListCompoundHeader
    {
        public Guid prescription_compound_header_id { get; set; }
        public string compound_name { get; set; }
        public string quantity { get; set; }
        public long uom_id { get; set; }
        public string uom_code { get; set; }
        public string dose { get; set; }
        public long dose_uom_id { get; set; }
        public string dose_uom { get; set; }
        public long administration_frequency_id { get; set; }
        public string frequency_code { get; set; }
        public long administration_route_id { get; set; }
        public string administration_route_code { get; set; }
        public string administration_instruction { get; set; }
        public int iter { get; set; }
        public short item_sequence { get; set; }
        public string PrescriptionDate { get; set; }
        public string dose_text { get; set; }
        public bool IsDoseText { get; set; }
        public string compound_note { get; set; }
        public string IssuedQty { get; set; }
    }

    public class PharmacyPickingListCompoundDetail
    {
        public Guid prescription_compound_detail_id { get; set; }
        public Guid prescription_compound_header_id { get; set; }
        public long item_id { get; set; }
        public string item_name { get; set; }
        public string quantity { get; set; }
        public long uom_id { get; set; }
        public string uom_code { get; set; }
        public short item_sequence { get; set; }
        public string RackName { get; set; }
        public string dose { get; set; }
        public long dose_uom_id { get; set; }
        public string dose_uom_code { get; set; }
        public string dose_text { get; set; }
        public bool IsDoseText { get; set; }
        public string IssuedQty { get; set; }
        public Guid editedId { get; set; }
        public string LocationDrug { get; set; }
    }


    public class CompleteIssue
    {
        public long origin_prescription_id { get; set; }
        public bool IsCompound { get; set; }
        public bool IsTake { get; set; }
        public bool IsReturn { get; set; }
        public string ActionRemark { get; set; }
    }

    public class RequestCompleteIssue
    {
        public List<CompleteIssue> paramcompleteissue { get; set; }
    }
}

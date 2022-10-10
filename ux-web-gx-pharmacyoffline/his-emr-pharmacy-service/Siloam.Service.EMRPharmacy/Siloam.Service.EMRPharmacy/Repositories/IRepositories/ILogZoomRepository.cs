using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.Functional;

namespace Siloam.Service.EMRPharmacy.Repositories.IRepositories
{
    public interface ILogZoomRepository
    {
        string DoneCheckInTele(Guid EncounterId, DateTime time, long OrganizationId, long PatientId, long AdmissionId);
        string InsertLogZoom(Guid EncounterId, DateTime time);
    }
}

using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ConsultationRepository : IConsultationRepository
    {
        public void DeleteConsultation(Consultation consultation)
            => ConsultationDAO.Instance.DeleteConsultation(consultation);

        public List<Consultation> GetAllConsultations()
            => ConsultationDAO.Instance.GetAllConsultations();

        public Consultation GetConsultationById(int consultationId)
            => ConsultationDAO.Instance.GetConsultationById(consultationId);

        public void SaveConsultation(Consultation consultation)
            => ConsultationDAO.Instance.SaveConsultation(consultation);

        public void UpdateConsultation(Consultation consultation)
            => ConsultationDAO.Instance.UpdateConsultation(consultation);
    }
}

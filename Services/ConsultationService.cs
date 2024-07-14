using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ConsultationService : IConsultationService
    {
        private readonly IConsultationRepository consultationRepository;

        public ConsultationService(IConsultationRepository consultationRepository)
        {
            this.consultationRepository = consultationRepository;
        }

        public void DeleteConsultation(Consultation consultation)
            => consultationRepository.DeleteConsultation(consultation);

        public List<Consultation> GetAllConsultations()
            => consultationRepository.GetAllConsultations();

        public Consultation GetConsultationById(int consultationId)
            => consultationRepository.GetConsultationById(consultationId);

        public void SaveConsultation(Consultation consultation)
            => consultationRepository.SaveConsultation(consultation);

        public void UpdateConsultation(Consultation consultation)
            => consultationRepository.UpdateConsultation(consultation);
    }
}

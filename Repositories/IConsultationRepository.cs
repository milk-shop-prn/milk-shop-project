using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IConsultationRepository
    {
        Consultation GetConsultationById(int consultationId);

        List<Consultation> GetAllConsultations();

        void SaveConsultation(Consultation consultation);

        void UpdateConsultation(Consultation consultation);

        void DeleteConsultation(Consultation consultation);
    }
}

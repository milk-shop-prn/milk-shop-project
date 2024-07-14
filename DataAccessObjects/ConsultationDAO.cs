using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class ConsultationDAO
    {
        private static ConsultationDAO instance = null!;
        private static readonly object lockObject = new object();

        private ConsultationDAO() { }

        public static ConsultationDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ConsultationDAO();
                    }
                    return instance;
                }
            }
        }

        public Consultation GetConsultationById(int consultationId)
        {
            using var db = new MilkShopContext();
            return db.Consultations.SingleOrDefault(c => c.ConsultationId == consultationId);
        }

        public List<Consultation> GetAllConsultations()
        {
            using var db = new MilkShopContext();
            return db.Consultations.ToList();
        }

        public void SaveConsultation(Consultation consultation)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Consultations.Add(consultation);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateConsultation(Consultation consultation)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry(consultation).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteConsultation(Consultation consultation)
        {
            try
            {
                using var db = new MilkShopContext();
                var existingConsultation = db.Consultations.Find(consultation.ConsultationId);
                if (existingConsultation != null)
                {
                    db.Consultations.Remove(existingConsultation);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class VoucherDAO
    {
        private static VoucherDAO instance = null!;
        private static readonly object lockObject = new object();

        private VoucherDAO() { }

        public static VoucherDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new VoucherDAO();
                    }
                    return instance;
                }
            }
        }

        public Voucher GetVoucherById(int voucherId)
        {
            using var db = new MilkShopContext();
            return db.Vouchers.SingleOrDefault(v => v.VoucherId == voucherId);
        }

        public List<Voucher> GetAllVouchers()
        {
            using var db = new MilkShopContext();
            return db.Vouchers.ToList();
        }

        public void SaveVoucher(Voucher voucher)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Vouchers.Add(voucher);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateVoucher(Voucher voucher)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry(voucher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteVoucher(Voucher voucher)
        {
            try
            {
                using var db = new MilkShopContext();
                var existingVoucher = db.Vouchers.Find(voucher.VoucherId);
                if (existingVoucher != null)
                {
                    db.Vouchers.Remove(existingVoucher);
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

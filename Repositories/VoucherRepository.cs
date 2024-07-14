using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        public void DeleteVoucher(Voucher voucher)
            => VoucherDAO.Instance.DeleteVoucher(voucher);

        public List<Voucher> GetAllVouchers()
            => VoucherDAO.Instance.GetAllVouchers();

        public Voucher GetVoucherById(int voucherId)
            => VoucherDAO.Instance.GetVoucherById(voucherId);

        public void SaveVoucher(Voucher voucher)
            => VoucherDAO.Instance.SaveVoucher(voucher);

        public void UpdateVoucher(Voucher voucher)
            => VoucherDAO.Instance.UpdateVoucher(voucher);
    }
}

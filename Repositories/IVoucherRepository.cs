using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IVoucherRepository
    {
        Voucher GetVoucherById(int voucherId);

        List<Voucher> GetAllVouchers();

        void SaveVoucher(Voucher voucher);

        void UpdateVoucher(Voucher voucher);

        void DeleteVoucher(Voucher voucher);
    }
}

using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository voucherRepository;

        public VoucherService( )
        {
            this.voucherRepository = new VoucherRepository();
        }

        public void DeleteVoucher(Voucher voucher)
            => voucherRepository.DeleteVoucher(voucher);

        public List<Voucher> GetAllVouchers()
            => voucherRepository.GetAllVouchers();

        public Voucher GetVoucherById(int voucherId)
            => voucherRepository.GetVoucherById(voucherId);

        public void SaveVoucher(Voucher voucher)
            => voucherRepository.SaveVoucher(voucher);

        public void UpdateVoucher(Voucher voucher)
            => voucherRepository.UpdateVoucher(voucher);
    }
}

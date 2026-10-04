using BillApi.Application.Services.Interfaces;
using BillApi.Domain.Entities;
using BillApi.Dto_s;
using BillApi.Infrastructure.Data.DbConnectionContext;
using BillApi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Constant;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Persistance.Interfaces;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace BillApi.Application.Services.Implemantations
{
    public class BillManager : IBillService
    {
        private readonly IBillRepository _billRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BillManager(
            IBillRepository billRepository,
            IUnitOfWork unitOfWork)
        {
            _billRepository = billRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<BillResponseDto>> CreateABill(CreateBillDTO dto)
        {
            try
            {
                Random random = new Random();

                int billID = random.Next(0, 100);
                int invoiceNum = random.Next(0, 100000);

                var newBill = new Bill
                {
                    BillID = billID,
                    BuyerTaxID = dto.BuyerTaxID,
                    SuplierTaxID = dto.SuplierTaxID,
                    InovoiceStatus = Status.New,
                    InvoiceCost = dto.InvoiceCost,
                    InvoiceNumber = invoiceNum.ToString(),
                    TermDate = string.IsNullOrEmpty(dto.TermDate) ? DateTime.Now.ToShortDateString() : dto.TermDate
                };


                await _billRepository.AddAsync(newBill);

                await _unitOfWork.CommitAsync();

                return new SuccessDataResult<BillResponseDto>(BillResponseDto.GetViewModel(newBill));

            }
            catch (Exception)
            {
                return new ErrorDataResult<BillResponseDto>();
                throw;
            }

        }

        public async Task<IDataResult<List<BillListingDTO>>> GetBillDtowithBuyerID(string buyerTaxId)
        {
            try
            {
                var result = await _billRepository.ListAsync(b => b.BuyerTaxID == buyerTaxId);
                if (result != null)
                {
                    var dtoList = result.ToList().Select(BillListingDTO.GetViewModel).ToList();
                    return new SuccessDataResult<List<BillListingDTO>>(dtoList);

                }

                return new ErrorDataResult<List<BillListingDTO>>();
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<BillListingDTO>>();
                throw;
            }

        }

        public async Task<IDataResult<List<BillListingDTO>>> GetBillDtowithSupplierID(string supplierTaxId)
        {
            try
            {
                var result = await _billRepository.ListAsync(b => b.SuplierTaxID == supplierTaxId);
                if (result != null)
                {
                    var dtoList = result.Select(BillListingDTO.GetViewModel).ToList();
                    return new SuccessDataResult<List<BillListingDTO>>(dtoList);

                }

                return new ErrorDataResult<List<BillListingDTO>>(Messages.FailedProccess);
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<BillListingDTO>>();
                throw;
            }

        }

        public async Task<IDataResult<PaymentRequestDto>> CreatePaymentRequest(string invoiceNumber)
        {
            try
            {

                var result = await _billRepository.GetAsync(b => b.InvoiceNumber == invoiceNumber);

                if (result == null)
                {
                    return new ErrorDataResult<PaymentRequestDto>(Messages.FailedProccess);
                }

                result.InovoiceStatus = Status.Usage;

                _billRepository.Update(result);

                await _unitOfWork.CommitAsync();

                return new SuccessDataResult<PaymentRequestDto>(PaymentRequestDto.GetViewModel(result), Messages.SuccessProccess);
            }
            catch (Exception)
            {
                return new ErrorDataResult<PaymentRequestDto>();

                throw;
            }

        }

        public async Task<IResult> GetPaymentResponse(string invoiceNumber)
        {
            try
            {

                var entity = await _billRepository.GetAsync(b => b.InvoiceNumber == invoiceNumber);


                if (entity != null)
                {
                    entity.InovoiceStatus = Status.Paid;

                    _billRepository.Update(entity);

                    await _unitOfWork.CommitAsync();

                    return new SuccesResult();

                }
                return new ErrorResult();
            }
            catch (Exception)
            {
                return new ErrorResult();
                throw;
            }

        }
    }
}

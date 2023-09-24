using Application.Dtos;
using Application.Services.Contracts;
using Application.Utils;
using Application.Validations;
using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class PayableService : IPayableService
    {
        private readonly IPayableRepository _payableRepository;
        private readonly IMapper _mapper;

        public PayableService(IPayableRepository payableRepository, IMapper mapper)
        {
            _payableRepository = payableRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<PayableDto>> CreateAsync(PayableDto payableDto)
        {
            if (payableDto == null)
            {
                return ResultService.Fail<PayableDto>("Objeto deve ser informado");
            }

            var result = new PayableValidation().Validate(payableDto);

            if (!result.IsValid) return ResultService.RequestError<PayableDto>("Problemas de validação ", result);
           
            PayableEntity payableEntity = _mapper.Map<PayableEntity>(payableDto);

            var data = await _payableRepository.Create(payableEntity);

            return ResultService.Ok(_mapper.Map<PayableDto>(data));
        }

        public async Task<ResultService<ICollection<PayableDto>>> GetAll()
        {
            var payables = await _payableRepository.GetAll();
            return ResultService.Ok(_mapper.Map<ICollection<PayableDto>>(payables));
        }

        public async Task<ResultService<PayablesDto>> GetAllPayables()
        {
            var availables = await _payableRepository.GetAllPayable(PayableStatusEnum.AVAILABLE);
            var waiting = await _payableRepository.GetAllPayable(PayableStatusEnum.WAITING_FUNDS);
            var retorno = new PayablesDto(RoundNumber(Reduce(availables)), RoundNumber(Reduce(waiting)));
         
            return ResultService.Ok(retorno);
        }


        public double Reduce(ICollection<PayableEntity> entity)
        {
            return (double)entity.Aggregate(0, (acc, x) => (int)(acc + x.Amount));
        }
        public double RoundNumber(double number)
        {
            return Math.Round(number,2) ;
        }
    }
}

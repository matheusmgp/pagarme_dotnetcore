﻿

using Application.Dtos;
using Domain.Entities;

namespace Application.Services.Contracts
{
    public interface IPayableService : IBaseService<PayableDto>
    {
        Task<ResultService<PayablesDto>> GetAllPayables();
      
    }
}

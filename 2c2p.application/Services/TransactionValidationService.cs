using _2c2p.application.Contracts;
using _2c2p.application.Models;
using _2c2p.domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _2c2p.application.Services
{
    public class TransactionValidationService : IValidationService
    {
        public Task<ValidationResult> Validate(List<Transaction> data)
        {
            throw new NotImplementedException();
        }
    }
}

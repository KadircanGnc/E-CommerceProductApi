using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IPaymentService
    {        
        Task<bool> IsPayCompleted(CreditCardDTO cardInfo);
    }
}

﻿using EtaaApi.Core.Models;
using System.Linq.Expressions;

namespace EtaaAPI.Core.Repos
{
    // By inheriting from the IBaseRepo we're getting all of it's methods and here
    // we're defining some that are specific to the Projects model
    public interface IFinancialStatementsRepo : IBaseRepo<FinancialStatement>
    {
        FinancialStatement CheckAlreadyPresent(int ProjectId);
        PaymentVoucher CheckAlreadyPresentInPaymentVoucher(int ProjectId);
    }
}
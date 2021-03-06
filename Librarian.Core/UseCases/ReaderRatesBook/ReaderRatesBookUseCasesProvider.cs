﻿using Librarian.Core.UseCases.ReaderRatesBook.AddRate;
using Librarian.Core.UseCases.ReaderRatesBook.GetRates;

namespace Librarian.Core.UseCases.ReaderRatesBook
{
    public class ReaderRatesBookUseCasesProvider : IReaderRatesBookUseCasesProvider
    {
        public ReaderRatesBookUseCasesProvider(
            IGetRatesUseCase getRates,
            IAddRateUseCase addRate
        )
        {
            this.GetRates = getRates;
            this.AddRate = addRate;
        }

        public IGetRatesUseCase GetRates { get; set; }
        public IAddRateUseCase AddRate { get; set; }
    }
}

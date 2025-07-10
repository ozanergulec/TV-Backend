using System;
using System.Collections.Generic;

namespace TV_Backend.Models.HotelProduct.Lookup
{
    public class GetCurrenciesResponse
    {
        public CurrBody Body { get; set; }
    }

    public class CurrBody
    {
        public List<Currency> Currencies { get; set; }
    }

    public class Currency
    {
        public string Code { get; set; }
        public string InternationalCode { get; set; }
        public string IconText { get; set; }
        public string Name { get; set; }
    }
}
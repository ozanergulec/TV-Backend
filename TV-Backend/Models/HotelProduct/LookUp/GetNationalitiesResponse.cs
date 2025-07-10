using System;
using System.Collections.Generic;

namespace TV_Backend.Models.HotelProduct.Lookup
{
    public class GetNationalitiesResponse
    {
        public NatBody Body { get; set; }
    }

    public class NatBody
    {
        public List<Nationality> Nationalities { get; set; }
    }

    public class Nationality
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IsdCode { get; set; }
        public string ThreeLetterCode { get; set; }
    }
}

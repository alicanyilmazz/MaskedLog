using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MaskedLog
{
    public class Atm
    {
        [MaskSensitiveData(6, 4)]
        public string CardNumber { get; set; }

        [MaskSensitiveData(6, 0)]
        public string TerminalId { get; set; }

        [MaskSensitiveData(0, 4)]
        public string PinBlock { get; set; }

        public string Track2 { get; set; }
    }

    public class Card
    {
        [MaskSensitiveData(2, 2)]
        public string Name { get; set; }

        [MaskSensitiveData(3, 0)]
        public string Surname { get; set; }

        [MaskSensitiveData(0, 4)]
        public string Age { get; set; }

        public string CityName { get; set; }
    }
}

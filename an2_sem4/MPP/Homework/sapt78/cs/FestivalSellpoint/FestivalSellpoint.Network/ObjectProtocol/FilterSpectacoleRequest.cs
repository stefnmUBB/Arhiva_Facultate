using FestivalSellpoint.Network.Utils;
using System;

namespace FestivalSellpoint.Network.ObjectProtocol
{
    [Serializable]
    public class FilterSpectacoleRequest : IRequest
    {
        private string _Day;
        public FilterSpectacoleRequest(DateTime day) => _Day = day.ToString(Constants.DTODateFormatter);
        public DateTime Day => DateTime.ParseExact(_Day, Constants.DTODateFormatter, null);
    }
}

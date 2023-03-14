using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Helper
{
    public class BaseRequest<TRequest>
    {
        public BaseRequest()
        {

        }
        public BaseRequest(TRequest _requestData)
        {
            RequestData = _requestData;
        }
        public TRequest RequestData { get; set; }
    }
}

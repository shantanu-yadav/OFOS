using System;
using System.Collections.Generic;
using System.Text;

namespace OFOS.CustomException
{
    public class InvalidOrderIdException:ApplicationException
    {
        public override string Message { get { return "Invalid Order ID!!"; } }
    }
}

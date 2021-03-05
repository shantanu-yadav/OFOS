using System;

namespace OFOS.CustomException
{
    public class InvalidCardNumberExceptions:ApplicationException
    {
        public override string Message { get { return "Invalid Card Number"; } }
    }
}

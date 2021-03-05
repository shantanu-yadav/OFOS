using System;
using System.Collections.Generic;
using System.Text;

namespace CustomExceptions
{
    public class InvalidCardNumberExceptions : ApplicationException
    {
        public override string Message { get { return "Invalid Card Number"; } }

    }
}

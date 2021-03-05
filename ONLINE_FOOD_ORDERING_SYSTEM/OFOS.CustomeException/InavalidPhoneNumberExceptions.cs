using System;
using System.Collections.Generic;
using System.Text;

namespace OFOS.CustomException
{
    public class InavalidPhoneNumberExceptions:ApplicationException
    {
        public override string Message { get { return "Inavlid Phone Number"; } }
    }
}

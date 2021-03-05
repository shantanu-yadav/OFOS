using System;

namespace CustomExceptions
{
    public class InvalidPhoneNumberExceptions : ApplicationException
    {
        public override string Message { get { return "Invalid Phone Number"; } }
    }
}

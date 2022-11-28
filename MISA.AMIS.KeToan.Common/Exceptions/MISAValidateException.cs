using MISA.AMIS.KeToan.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Exceptions
{
    public class MISAValidateException:Exception
    {
        string? MsgErrorValidate = null;
        ErrorCode errCode;
        public MISAValidateException(string msg,ErrorCode codeErr)
        {
            this.MsgErrorValidate = msg;
            this.errCode = codeErr;
        }

        public override string Message { get { return this.MsgErrorValidate; } }

        public ErrorCode ErrorCodeValidate { get { return this.errCode; } }

    }
}

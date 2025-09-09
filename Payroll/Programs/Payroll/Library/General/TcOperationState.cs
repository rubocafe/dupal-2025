using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.Library.General
{
    public class TcOperationState
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
        public bool IsWarning { get; set; }

        public TcOperationState()
        {
        }

        public TcOperationState(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public string FullErrorMessage()
        {
            StringBuilder builder = new StringBuilder();

            if (!string.IsNullOrEmpty(Message))
            {
                builder.Append(Message);
            }

            if (Error != null)
            {
                if (Message != null)
                {
                    builder.Append(Environment.NewLine);
                }

                builder.Append(Error.Message);
                builder.Append(Environment.NewLine);
                builder.Append(Error.StackTrace);

                if (Error.InnerException != null)
                {
                    builder.Append(Environment.NewLine);
                    builder.Append(Error.InnerException.Message);
                    builder.Append(Environment.NewLine);
                    builder.Append(Error.InnerException.StackTrace);
                }
            }

            return builder.ToString();
        }
    }
}
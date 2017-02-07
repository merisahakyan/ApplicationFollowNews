using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Following
{
    class UncorrectMailAddress : Exception
    {
        public UncorrectMailAddress()
        {

        }
        public UncorrectMailAddress(string message) : base(message)
        {

        }
        public UncorrectMailAddress(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}

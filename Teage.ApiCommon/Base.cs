using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.ApiCommon
{
    public class Base<T>
    {
        public Base()
        {
            Meta = new Meta();
        }
        public Meta Meta { get; set; }
        public T Body { get; set; }

        public static implicit operator Base<T>(Base<object> v)
        {
            throw new NotImplementedException();
        }
    }

    public class Meta
    {
        public string ErrorCode { get; set; }
        public string ErrorMsg { get; set; }
    }
}

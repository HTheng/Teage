using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Entity
{
    //优递字段
    public class ExpressInfo
    {
        public string ID { get; set; }
        public string UserID { get; set; }
        public string AddressID { get; set; }
        public string vState { get; set; }
        public Nullable<System.DateTime> ExpectTime { get; set; }
        public string Destination { get; set; }
        public string HamalUserID { get; set; }
        public Nullable<System.DateTime> ReleaseTime { get; set; }
        public int TypeID { get; set; }
        public string ExpressName { get; set; }
        public string ExpressPwd { get; set; }
        public string Phone { get; set; }
        public string Zy { get; set; }
        public string Rel { get; set; }
        public string Token { get; set; }
        public string Flag { get; set; }
        public EnumType JobType { get; set; }
    }
}

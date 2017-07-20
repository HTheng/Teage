using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Teage.ApiCommon.BD_API
{
    public class LocationIpResult
    {
        public int status { get; set; }

        public string address { get; set; }

        public LocationIpResult_Content content { get; set; }
    }

    public class LocationIpResult_Content
    {
        public string address { get; set; }

        public LocationIpResult_Content_Address_Detail address_detail { get; set; }
    }

    public class LocationIpResult_Content_Address_Detail
    {
        public string city { get; set; }

        public int city_code { get; set; }

        public string district { get; set; }

        public string province { get; set; }

        public string street { get; set; }

        public string street_number { get; set; }
    }
}

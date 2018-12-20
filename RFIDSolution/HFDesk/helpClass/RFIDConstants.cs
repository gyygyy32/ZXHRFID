using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HFDesk.helpClass
{
    public class RFIDConstants
    {
        public string mfg_country { get; set; }

        public string mfg_name { get; set; }

        public string iec_date { get; set; }

        public string iec_verfy { get; set; }

        public string iso_desc { get; set; }

        public string product_type { get; set; }

        public RFIDConstants(string s_mfg_country, string s_mfg_name, string s_iec_date, string s_iec_verfy, string s_iso_desc, string s_product_type)
        {
            mfg_country = s_mfg_country;
            mfg_name = s_mfg_name;
            iec_date = s_iec_date;
            iec_verfy = s_iec_verfy;
            iso_desc = s_iso_desc;
            product_type = s_product_type;
        }
    }
}

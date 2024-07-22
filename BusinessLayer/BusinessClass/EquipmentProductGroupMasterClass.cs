using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessClass
{
    public class EquipmentProductGroupMasterClass
    {
       public int PrpId { get; set; }
        public string prpProductGroup { get; set; }
        public string prpProductDesc { get; set; }
        public bool prpStatus { get; set; }


        public string SaveEquipmentMaster()
        {
            return string.Empty;
        }

        public DataSet FetchEquipmentProductGroupMasterOnId()
        {
            return new DataSet();
        }

        public DataTable FetchEquipmentProductGroupMaster(string condition)
        {
            return new DataTable();
        }

    }
}

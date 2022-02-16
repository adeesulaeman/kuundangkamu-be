using Service.Base.Contracts;
using Service.Base.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Data.Models
{
    public class Ucapan : BaseEntity, IEntity
    {
        public string Nama { get; set; }
        public string UcapanDoa { get; set; }
        public string Kehadiran { get; set; }
        public string Status { get; set; }
        public string Code { get; set; }
    }
}

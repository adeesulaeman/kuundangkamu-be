using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Data.ViewModels
{
    public class UcapanVM
    {
        public string Nama { get; set; }
        public string Ucapan { get; set; }
        public string Kehadiran { get; set; }
        public string Code { get; set; }
    }

    public class UcapanResponseVM
    {
        public long Id { get; set; }
        public string Nama { get; set; }
        public string Ucapan { get; set; }
        public string Kehadiran { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
    }
}

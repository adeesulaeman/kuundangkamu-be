using Service.Base.Repository;
using Service.Data.Contracts;
using Service.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Data.Repositories
{
    public class UcapanRepository : BaseRepository<Ucapan>, IUcapanRepository
    {
        public UcapanRepository(DataContext context) : base(context) { }

        public override Ucapan OnCreating(Ucapan entity) => entity;

        public override Ucapan OnUpdating(Ucapan local, Ucapan db) => db;
    }
}

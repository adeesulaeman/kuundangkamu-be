using MediatR;
using Service.Base;
using Service.Base.ViewModels.Common;
using Service.Data.Contracts;
using Service.Data.Models;
using Service.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Service.Data.CQRS.Queries
{
    public class GetUcapanQuery : IRequest<PagedResultVM<UcapanResponseVM>>
    {
        public PagedQueryVM PageQuery { get; set; }
        public SortableVM SortAble { get; set; }
        public string Code { get; set; }
    }

    public class GetUcapanQueryHandler : IRequestHandler<GetUcapanQuery, PagedResultVM<UcapanResponseVM>>
    {
        private readonly IUcapanRepository _ucapanRepository;
        public GetUcapanQueryHandler(IUcapanRepository ucapanRepository)
        {
            _ucapanRepository = ucapanRepository;
        }
        public async Task<PagedResultVM<UcapanResponseVM>> Handle(GetUcapanQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Ucapan, bool>> predicate = p => p.IsActive && p.Code.Equals(request.Code);

            if (!string.IsNullOrEmpty(request.PageQuery.Search))
                predicate = predicate.AndAlso(p => p.Nama.ToLower().Contains(request.PageQuery.Search));

            var rawData = await _ucapanRepository.GetWithRelationsAsync(predicate);

            var totalRecord = rawData.Count();

            #region sort
            if (request.SortAble != null && request.SortAble.Field == "nama")
            {
                if (request.SortAble.Order.Equals(-1))
                    rawData = rawData.OrderByDescending(x => x.Nama);
                else
                    rawData = rawData.OrderBy(x => x.Nama);
            }
            else
            {
                rawData = rawData.OrderByDescending(x => x.CreatedDate);
            }
            #endregion

            var resData = rawData
                .Skip((request.PageQuery.Page - 1) * request.PageQuery.ItemsPerPage)
                .Take(request.PageQuery.ItemsPerPage)
                .Select(x => new UcapanResponseVM()
                {
                    Id = x.Id,
                    Nama = x.Nama,
                    Ucapan = x.UcapanDoa,
                    Kehadiran = x.Kehadiran,
                    Code = x.Code,
                    Status = x.Status
                });

            var result = new PagedResultVM<UcapanResponseVM>()
            {
                CurrentPage = request.PageQuery.Page,
                ResultPerPage = request.PageQuery.ItemsPerPage,
                TotalRecords = totalRecord,
                Data = resData
            };

            return result;
        }

    }
}

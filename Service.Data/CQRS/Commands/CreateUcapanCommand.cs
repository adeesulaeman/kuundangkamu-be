using MediatR;
using Service.Base.ViewModels.Common;
using Service.Base.ViewModels.Identity;
using Service.Data.Contracts;
using Service.Data.Models;
using Service.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Data.CQRS.Commands
{
    public class CreateUcapanCommand : IRequest<SuccessResponseVM>
    {
        public UcapanVM Payload { get; set; }
        public ProfileVM Actor { get; set; }
    }
    public class CreateUcapanCommandHandler : IRequestHandler<CreateUcapanCommand, SuccessResponseVM>
    {
        private readonly IUcapanRepository _ucapanRepository;
        public CreateUcapanCommandHandler(IUcapanRepository ucapanRepository)
        {
            _ucapanRepository = ucapanRepository;
        }

        public async Task<SuccessResponseVM> Handle(CreateUcapanCommand command, CancellationToken cancellationToken)
        {
            var result = new SuccessResponseVM();

            var request = command.Payload;
            var actor = command.Actor.Name;

            using (var role = _ucapanRepository.CreateTransaction((int)IsolationLevel.Serializable))
            {
                try
                {
                    Ucapan data = new Ucapan
                    {
                        Nama = request.Nama,
                        UcapanDoa = request.Ucapan,
                        Kehadiran = request.Kehadiran,
                        Code = request.Code,
                        Status = "SHOW"
                    };
                    _ucapanRepository.SetActor(actor);

                    var roleCreated = await _ucapanRepository.CreateAsync(data);

                    result.IsSuccess = true;

                    await _ucapanRepository.CommitTransaction(role);
                }
                catch (Exception ex)
                {
                    await _ucapanRepository.RollbackTransaction(role);
                    throw ex;
                }
            }

            return result;
        }
    }
}

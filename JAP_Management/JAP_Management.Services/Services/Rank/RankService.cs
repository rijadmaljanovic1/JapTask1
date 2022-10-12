using AutoMapper;
using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using JAP_Management.Infrastructure.Database;
using JAP_Management.Repositories.Repositories.Ranks;
using JAP_Management.Repositories.Repositories.Students;
using JAP_Management.Services.Services.EmailSender;
using JAP_Management.Services.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Services.Services.Ranks
{
    public class RankService : IRankService
    {
        private readonly IRankRepository _rankRepository;
        private readonly IMapper _mapper;

        public RankService(IRankRepository rankRepository, IMapper mapper)
        {
            _rankRepository = rankRepository;
            _mapper = mapper;
        }

        public List<RankModel> GetRanks()
        {
            return _mapper.Map<List<RankModel>>(_rankRepository.GetRanks());
        }
    }
}

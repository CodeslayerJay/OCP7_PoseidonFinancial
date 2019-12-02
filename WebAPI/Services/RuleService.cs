using AutoMapper;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class RuleService : IRuleService
    {
        private readonly IRuleRepository _ruleRepo;
        private readonly IMapper _mapper;

        public RuleService(IRuleRepository ruleRepository, IMapper mapper)
        {
            _ruleRepo = ruleRepository;
            _mapper = mapper;
        }

        public RuleNameResource Add(EditRuleNameResource resource)
        {

            if (resource == null)
                throw new ArgumentNullException();

            var rulename = _mapper.Map<RuleName>(resource);
            _ruleRepo.Add(rulename);
            _ruleRepo.SaveChanges();

            return _mapper.Map<RuleNameResource>(rulename);

        }

        public void Update(int id, EditRuleNameResource resource)
        {
            var rulename = _ruleRepo.FindById(id);

            if (resource != null && rulename != null)
            {
                _ruleRepo.Update(id, _mapper.Map(resource, rulename));
                _ruleRepo.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            _ruleRepo.Delete(id);
        }

        public RuleNameResource[] GetAll()
        {
            return _ruleRepo.GetAll().Where(x => x.Id > 0).Select(x => _mapper.Map<RuleNameResource>(x)).ToArray();
        }

        public RuleNameResource FindById(int id)
        {
            var rulename = _ruleRepo.FindById(id);
            return _mapper.Map<RuleNameResource>(rulename);
        }
    }
}

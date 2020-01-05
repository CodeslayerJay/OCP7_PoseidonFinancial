using AutoMapper;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;
using WebApi.ModelValidators;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepo;
        private readonly IMapper _mapper;

        public TradeService(ITradeRepository tradeRepository, IMapper mapper)
        {
            _tradeRepo = tradeRepository;
            _mapper = mapper;
        }

        public ValidationResult ValidateResource(EditTradeResource resource)
        {
            var result = new ValidationResult();

            if (resource != null)
            {
                var validator = new TradeValidator();
                var vr = validator.Validate(resource);

                if (vr.IsValid)
                {
                    result.IsValid = true;
                    return result;
                }


                if (vr.Errors.Any())
                {
                    foreach (var error in vr.Errors)
                    {
                        result.ErrorMessages.Add(error.PropertyName, error.ErrorMessage);
                    }
                }
            }

            return result;
        }

        public TradeResource Add(EditTradeResource resource)
        {

            if (resource == null)
                throw new ArgumentNullException();

            var trade = _mapper.Map<Trade>(resource);
            _tradeRepo.Add(trade);
            _tradeRepo.SaveChanges();

            return _mapper.Map<TradeResource>(trade);

        }

        public void Update(int id, EditTradeResource resource)
        {
            var trade = _tradeRepo.FindById(id);

            if (resource != null && trade != null)
            {
                _tradeRepo.Update(id, _mapper.Map(resource, trade));
                _tradeRepo.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            _tradeRepo.Delete(id);
            _tradeRepo.SaveChanges();
        }

        public TradeResource[] GetAll()
        {
            return _tradeRepo.GetAll().Where(x => x.TradeId > 0).Select(x => _mapper.Map<TradeResource>(x)).ToArray();
        }

        public TradeResource FindById(int id)
        {
            var trade = _tradeRepo.FindById(id);
            return _mapper.Map<TradeResource>(trade);
        }
    }
}

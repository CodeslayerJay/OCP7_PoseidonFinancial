using AutoMapper;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;
using WebApi.ModelValidators;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class BidService : IBidService
    {
        private readonly IBidListRepository _bidRepo;
        private readonly IMapper _mapper;

        public BidService(IBidListRepository bidRepository, IMapper mapper)
        {
            _bidRepo = bidRepository;
            _mapper = mapper;
        }

        public BidResource Add(EditBidResource resource)
        {

            if (resource == null)
                throw new ArgumentNullException();

            var bid = _mapper.Map<BidList>(resource);
            _bidRepo.Add(bid);
            _bidRepo.SaveChanges();

            return _mapper.Map<BidResource>(bid);

        }

        public ValidationResult ValidateResource(EditBidResource resource)
        {
            var result = new ValidationResult();

            if(resource != null)
            {
                var validator = new BidValidator();
                var vr = validator.Validate(resource);

                if (vr.IsValid)
                {
                    result.IsValid = true;
                    return result;
                }


                if (vr.Errors.Any())
                {
                    foreach(var error in vr.Errors)
                    {
                        result.ErrorMessages.Add(error.PropertyName, error.ErrorMessage);
                    }
                }
            }

            return result;
        }

        public void Update(int id, EditBidResource resource)
        {
            var bidToUpdate = _bidRepo.FindById(id);

            if(bidToUpdate != null && resource != null)
            {
                _bidRepo.Update(id, _mapper.Map(resource, bidToUpdate));
                _bidRepo.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            _bidRepo.Delete(id);
            _bidRepo.SaveChanges();
        }

        public BidResource[] GetAll()
        {
            return _bidRepo.GetAll().Where(x => x.BidListId > 0).Select(x => _mapper.Map<BidResource>(x)).ToArray();
        }

        public BidResource FindById(int id)
        {
            var bid = _bidRepo.FindById(id);
            return _mapper.Map<BidResource>(bid);
        }
    }
}

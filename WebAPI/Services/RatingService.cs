using AutoMapper;
using Dot.Net.WebApi.Data;
using Dot.Net.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ApiResources;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepo;
        private readonly IMapper _mapper;

        public RatingService(IRatingRepository ratingRepository, IMapper mapper)
        {
            _ratingRepo = ratingRepository;
            _mapper = mapper;
        }

        public RatingResource Add(CreateRatingResource resource)
        {

            if (resource == null)
                throw new ArgumentNullException();

            var rating = _mapper.Map<Rating>(resource);
            _ratingRepo.Add(rating);
            _ratingRepo.SaveChanges();

            return _mapper.Map<RatingResource>(rating);

        }

        public void Update(int id, EditRatingResource resource)
        {
            var rating = _ratingRepo.FindById(id);

            if (resource != null && rating != null)
            {
                _ratingRepo.Update(id, _mapper.Map(resource, rating));
                _ratingRepo.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            _ratingRepo.Delete(id);
        }

        public RatingResource[] GetAll()
        {
            return _ratingRepo.GetAll().Where(x => x.Id > 0).Select(x => _mapper.Map<RatingResource>(x)).ToArray();
        }

        public RatingResource FindById(int id)
        {
            var rating = _ratingRepo.FindById(id);
            return _mapper.Map<RatingResource>(rating);
        }
    }
}

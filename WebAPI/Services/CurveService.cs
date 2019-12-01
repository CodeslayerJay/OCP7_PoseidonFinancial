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
    public class CurveService : ICurveService
    {
        private readonly ICurvePointRepository _curveRepo;
        private readonly IMapper _mapper;

        public CurveService(ICurvePointRepository curvePointRepository, IMapper mapper)
        {
            _curveRepo = curvePointRepository;
            _mapper = mapper;
        }

        public CurveResource Add(CurveResource resource)
        {

            if (resource == null)
                throw new ArgumentNullException();

            var curve = _mapper.Map<CurvePoint>(resource);
            _curveRepo.Add(curve);
            _curveRepo.SaveChanges();

            return _mapper.Map<CurveResource>(curve);

        }

        public void Update(int id, CurveResource resource)
        {
            var curve = _curveRepo.FindById(id);

            if (resource != null && curve != null)
            {
                _curveRepo.Update(id, _mapper.Map(resource, curve));
                _curveRepo.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            _curveRepo.Delete(id);
        }

        public CurveResource[] GetAll()
        {
            return _curveRepo.GetAll().Where(x => x.Id > 0).Select(x => _mapper.Map<CurveResource>(x)).ToArray();
        }

        public CurveResource FindById(int id)
        {
            var curve = _curveRepo.FindById(id);
            return _mapper.Map<CurveResource>(curve);
        }
    }
}

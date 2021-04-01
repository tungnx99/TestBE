using AutoMapper;
using Common.Paganation;
using Domain.DTOs;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Domain.Entities.Supplier> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierService(IRepository<Domain.Entities.Supplier> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Delete(Guid id)
        {
            var entity = _repository.Find(id);
            _repository.Delete(entity);
            _unitOfWork.SaveChanges();
        }

        public void Insert(Domain.Entities.Supplier entity)
        {
            _repository.Insert(entity);
            _unitOfWork.SaveChanges();
        }

        public void InsertRange(List<Domain.Entities.Supplier> entities)
        {
            _unitOfWork.BeginTransaction();
            _repository.InsertRange(entities);
            _unitOfWork.Commit();
        }

        public Paganation<SupplierDTO> SearchPagination(SerachPaganationDTO<Domain.Entities.Supplier> entity)
        {
            if (entity == null)
            {
                return new Paganation<SupplierDTO>();
            }

            var result = _mapper.Map<SerachPaganationDTO<Domain.Entities.Supplier>, Paganation<SupplierDTO>>(entity);

            var datas = _repository.Queryable().Where(it => entity.Search == null ||
                (
                    it.Id.ToString().Contains(entity.Search.Id.ToString()) &&
                    it.Name.Contains(entity.Search.Name) &&
                    it.Description.Contains(entity.Search.Description)
                )
            ).OrderBy(t => t.Name);

            var datats = datas.Take(entity.Take).Skip(entity.Skip);

            result.InputData(totalItems: datas.Count(), data: _mapper.Map<List<Domain.Entities.Supplier>, List<SupplierDTO>>(datats.ToList()));

            return result;
        }

        public void Update(Domain.Entities.Supplier entity)
        {
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
        }
    }
}

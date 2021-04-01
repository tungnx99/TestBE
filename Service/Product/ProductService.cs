using AutoMapper;
using Common.Paganation;
using Domain.DTOs;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Product
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Domain.Entities.Product> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Domain.Entities.Product> repository, IUnitOfWork unitOfWork, IMapper mapper)
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

        public void Insert(Domain.Entities.Product entity)
        {
            _repository.Insert(entity);
            _unitOfWork.SaveChanges();
        }

        public void InsertRange(List<Domain.Entities.Product> entities)
        {
            _unitOfWork.BeginTransaction();
            _repository.InsertRange(entities);
            _unitOfWork.Commit();
        }

        public Paganation<ProductDTO> SearchPagination(SerachPaganationDTO<ProductDTO> entity)
        {
            if (entity == null)
            {
                return new Paganation<ProductDTO>();
            }

            var result = _mapper.Map<SerachPaganationDTO<ProductDTO>, Paganation<ProductDTO>>(entity);
            var query = _repository.Queryable();
           
            var queryData = query.Where(it => entity.Search == null ||
                (
                    it.Name.Contains(entity.Search.Name)
                )
            ).OrderBy(t => t.Name).ThenBy(t => t.SupplierId).ThenBy(t => t.CategoryId);

            var datats = queryData.Take(entity.Take).Skip(entity.Skip).ToList();
            var count = query.Count();
            result.InputData(totalItems: count, data: _mapper.Map<List<Domain.Entities.Product>, List<ProductDTO>>(datats));

            return result;
        }

        public void Update(Domain.Entities.Product entity)
        {
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
        }
    }
}

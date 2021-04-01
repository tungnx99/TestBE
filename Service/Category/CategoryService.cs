using AutoMapper;
using Common.Paganation;
using Domain.DTOs;
using Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Domain.Entities.Category> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Domain.Entities.Category> repository, IUnitOfWork unitOfWork, IMapper mapper)
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

        public void Insert(Domain.Entities.Category entity)
        {
            _repository.Insert(entity);
            _unitOfWork.SaveChanges();
        }

        public void InsertRange(List<Domain.Entities.Category> entities)
        {
            _unitOfWork.BeginTransaction();
            _repository.InsertRange(entities);
            _unitOfWork.Commit();
        }

        public Paganation<CategoryDTO> SearchPagination(SerachPaganationDTO<CategoryDTO> entity)
        {
            if (entity == null)
            {
                return new Paganation<CategoryDTO>();
            }

            var result = _mapper.Map<SerachPaganationDTO<CategoryDTO>, Paganation<CategoryDTO>>(entity);

            var datas = _repository.Queryable().Where(it => entity.Search == null ||
                (
                    ( entity.Search.Id == Guid.Empty ? true : it.Id == entity.Search.Id) &&
                    it.Name.Contains(entity.Search.Name) &&
                    it.Description.Contains(entity.Search.Description)
                )
            ).OrderBy(t => t.Name);

            var datats = _mapper.Map<List<Domain.Entities.Category>, List<CategoryDTO>>(datas.Take(entity.Take).Skip(entity.Skip).ToList());

            result.InputData(totalItems: datas.Count(), data: datats);

            return result;
        }

        public void Update(Domain.Entities.Category entity)
        {
            _repository.Update(entity);
            _unitOfWork.SaveChanges();
        }
    }
}

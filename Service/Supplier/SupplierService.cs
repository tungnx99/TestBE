using AutoMapper;
using Common;
using Common.Http;
using Common.Paganation;
using Domain.DTOs;
using Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly IRepository<Domain.Entities.Supplier> _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SupplierService(IRepository<Domain.Entities.Supplier> repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public PaginatedList<SupplierDTO> SearchPagination(SerachPaganationDTO<SupplierDTO> entity)
        {
            if (entity == null)
            {
                return new PaginatedList<SupplierDTO>(null, 0, 0, 0);
            }

            var query = _repository.Queryable().Where(it => it.IsDeleted == false &&
                (
                    entity.Search == null ||
                        (
                            (entity.Search.Id == Guid.Empty ? false : it.Id == entity.Search.Id) ||
                            it.Name.Contains(entity.Search.Name) ||
                            it.Description.Contains(entity.Search.Description)
                        )
                )
            ).OrderBy(t => t.Name);

            //clean
            var data = _mapper.Map<List<Domain.Entities.Supplier>, List<SupplierDTO>>(query.ToList());
            var result = new PaginatedList<SupplierDTO>(data, data.Count, entity.PageIndex, entity.PageSize);
            result.GetPageData();

            return result;
        }

        public Task<IActionResult> Create(SupplierDTOInsert entity)
        {
            IActionResult result;
            try
            {
                var item = _mapper.Map<SupplierDTOInsert, Domain.Entities.Supplier>(entity);
                _repository.Insert(item);
                _unitOfWork.SaveChanges();
                result = new JsonResult(new CommonResponse<string>(0, Common.Constants.Data.InsertSuccess));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = new JsonResult(new CommonResponse<string>(1, Common.Constants.Server.ErrorServer));
            }

            return Task.FromResult(result);
        }

        public Task<IActionResult> Delete(Guid id)
        {
            IActionResult result;
            try
            {
                var item = _repository.Find(id);
                item.IsDeleted = true;
                _repository.Update(item);
                _unitOfWork.SaveChanges();
                result = new JsonResult(new CommonResponse<string>(0, Common.Constants.Data.DeleteSuccess));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = new JsonResult(new CommonResponse<string>(1, Common.Constants.Server.ErrorServer));
            }

            return Task.FromResult(result);
        }

        public Task<IActionResult> Update(SupplierDTO entity)
        {
            IActionResult result;
            try
            {
                var item = _mapper.Map<SupplierDTO, Domain.Entities.Supplier>(entity);
                _repository.Update(item);
                _unitOfWork.SaveChanges();
                result = new JsonResult(new CommonResponse<string>(0, Common.Constants.Data.UpdateSuccess));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: ", ex.ToString());
                result = new JsonResult(new CommonResponse<string>(1, Common.Constants.Server.ErrorServer));
            }

            return Task.FromResult(result);
        }

        public Task<IActionResult> GetList()
        {
            IActionResult result;
            try
            {
                var products = _repository.Queryable().Where(it => it.IsDeleted == false).ToList();
                result = new JsonResult(new CommonResponse<List<Domain.Entities.Supplier>>(0, products));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.ToString());
                result = new JsonResult(new CommonResponse<String>(1, Constants.Server.ErrorServer));
            }

            return Task.FromResult(result);
        }
    }
}

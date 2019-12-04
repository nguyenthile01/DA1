using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Y.Authorization;
using Y.Core;
using Y.Dto;

namespace Y.Services
{
    public class EmployerAppService : YAppServiceBase
    {
        private readonly IRepository<Employer> employerRepository;
        public EmployerAppService(IRepository<Employer> employerRepository
        )
        {
            this.employerRepository = employerRepository;
        }
        public async Task<EmployerDto> GetForLogin(string userName, string password)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("User name null");
            }

            if (password == null)
            {
                throw new ArgumentNullException("Password null");
            }

            var entity = await employerRepository.GetAll()
                .Where(p => p.Password == password)
                .Where(p => p.IsActive == true)
                .FirstOrDefaultAsync(p => p.UserName == userName);

            if (entity == null)
            {
                throw new ArgumentNullException("Not Found");
            }
            else
            {
                var model = new EmployerDto();
                entity.MapTo(model);

                return model;
            }

        }
        //[AbpAuthorize(PermissionNames.AdminPage_Employer)]
        public virtual async Task<PagedResultDto<EmployerDto>> GetAll(EmployerFilterDto input)
        {

            var query = employerRepository.GetAll()
                 .WhereIf(input.Id != null, p => p.Id == input.Id)
                .WhereIf(input.NameCompany != null, p => p.NameCompany == input.NameCompany)
                .WhereIf(input.Email != null, p => p.EmailAddress == input.Email)
            .WhereIf(input.PhoneNumber != null, p => p.PhoneNumber == input.PhoneNumber)
            .WhereIf(input.PhoneNumber != null, p => p.PhoneNumber == input.PhoneNumber);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<EmployerDto>(
                totalCount,
                entities.Select(p => p.MapTo<EmployerDto>())
                    .ToList()
            );
        }
        public async Task<CreateOrEditEmployerDto> GetForEdit(int? id)
        {
            var model = new CreateOrEditEmployerDto();
            if (id == null)
            {
                return model;
            }

            var entity = await employerRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == id);

            entity.MapTo(model);
            //model = model.Translations
            //    .Concat(translations)
            //    .DistinctBy(p => p.Language)
            //    .ToList();
            return model;
        }
        //public bool CheckPassword(string password)
        //{
        //    //string MatchEmailPattern = "(?=.{6,})[a-zA-Z0-9]+[^a-zA-Z]+|[^a-zA-Z]+[a-zA-Z]+";
        //    string MatchEmailPattern = "^[a-zA-Z0-9]+$";

        //    if (password != null ) return Regex.IsMatch(password, MatchEmailPattern);
        //    else return false;


        //}
        //[AbpAuthorize(PermissionNames.AdminPage_Employer)]
        public async Task CreateOrUpdate(CreateOrEditEmployerDto input)
        {
            var email = employerRepository.GetAll()
                .FirstOrDefault(p => p.EmailAddress == input.EmailAddress);
            if (input.Id != 0)
            {
                if(input.EmailAddress == null || input.UserName == null || input.Password == null)
                {
                    throw new ArgumentNullException("Chưa nhập đủ thông tin");
                }
                await UpdateAsync(input);
            }
            else
            {
                if (input.EmailAddress == null || email != null || input.UserName == null || input.Password == null)
                {
                    throw new ArgumentNullException("Chưa nhập đủ thông tin");
                }
                await CreateUserAsync(input);
            }
        }
        //[AbpAuthorize(PermissionNames.AdminPage_Employer)]
        public async Task Delete(EntityDto<int> input)
        {
            await employerRepository.DeleteAsync(p => p.Id == input.Id);

        }
        protected virtual async Task UpdateAsync(CreateOrEditEmployerDto input)
        {
            var entity = await employerRepository.GetAll()
                .FirstOrDefaultAsync(p => p.Id == input.Id);

            ObjectMapper.Map(input, entity);
            await employerRepository.UpdateAsync(entity);
        }

        protected virtual async Task CreateUserAsync(CreateOrEditEmployerDto input)
        {

            var entity = ObjectMapper.Map<Employer>(input);

            await employerRepository.InsertAndGetIdAsync(entity);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
        protected virtual IQueryable<Employer> ApplySorting(IQueryable<Employer> query, EmployerFilterDto input)
        {
            var sortInput = input as ISortedResultRequest;
            if (sortInput != null)
            {
                if (sortInput.Sorting.IsNotNullOrEmpty())
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }

            if (input is ILimitedResultRequest)
            {
                return query.OrderByDescending(e => e.Id);
            }

            return query;
        }

        protected virtual IQueryable<Employer> ApplyPaging(IQueryable<Employer> query, EmployerFilterDto input)
        {
            var pagedInput = input as IPagedResultRequest;
            if (pagedInput != null)
            {
                return query.PageBy(pagedInput);
            }

            var limitedInput = input as ILimitedResultRequest;
            if (limitedInput != null)
            {
                return query.Take(limitedInput.MaxResultCount);
            }

            return query;
        }
    }
}

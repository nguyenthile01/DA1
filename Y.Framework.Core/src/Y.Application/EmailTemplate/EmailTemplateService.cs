using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Y.Core;
using Y.Dto;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc;
using System;
//using Y.Event.ViewModel;
using Abp.Authorization;
using Abp.Timing;

namespace Y.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class EmailTemplateService : AsyncCrudAppService<Y.Core.EmailTemplate, EmailTemplateDto, int,
        EmailTemplateFilter, EmailTemplateCreateDto, EmailTemplateUpdateDto>, IEmailTemplateService

    {
        private readonly IRepository<Y.Core.EmailTemplate> Repository;
        public EmailTemplateService(IRepository<Y.Core.EmailTemplate> Repository) : base(Repository)
        {
            this.Repository = Repository;
        }


        //public List<EventDto> GetAllEvents()
        //{
        //    var eventList = Repository.GetAll()
        //                .Include(p => p.Organizers)
        //                .Include(p => p.Tickets)
        //                .OrderBy(p=>p.StartTime)
        //                .Select(p => new EventDto
        //                {
        //                    Id = p.Id,
        //                    Name = p.Name,
        //                  //  FullDescription = p.FullDescription,
        //                   // ShortDescription = p.ShortDescription,
        //                    BannerUrl = p.BannerUrl,
        //                    LogoUrl = p.LogoUrl,

        //                    Location = p.Location,
        //                    MinimumPrice = p.Tickets.OrderBy(pp => pp.Price)
        //                                    .Select(s => s.Price)
        //                                    .FirstOrDefault(),
        //                    StartTime = p.StartTime,
        //                    EndTime = p.EndTime,
        //                    Requirement = p.Requirement,
        //                    OpenHours = p.OpenHours,
        //                    IsComingSoon=p.IsComingSoon
        //                })
                        
        //                .ToList();


        //    return eventList;
        //}
    }
}


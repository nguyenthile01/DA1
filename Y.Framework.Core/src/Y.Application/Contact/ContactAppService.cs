using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Net.Mail;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Y.Authorization;
using Y.Core;
using Y.Dto;

namespace Y.Services
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class ContactAppService : YAppServiceBase, IContactAppService
    {
        private readonly IRepository<Contact> contactRepository;
        private readonly IEmailSender emailSender;


        public ContactAppService(IRepository<Contact> contactRepository, IEmailSender emailSender)
        {
            this.emailSender = emailSender;
            this.contactRepository = contactRepository;
        }

        [AbpAuthorize(PermissionNames.AdminPage_Contact)]
        public List<ContactExcelDto> GetExcelData(ContactFilterDto input)
        {
            return contactRepository.GetAll()
                        .Where(p => p.ContactType == ContactType.GetInTouch)
                        .WhereIf(input.FullName.IsNotNullOrEmpty(), p => p.FullName.Contains(input.FullName))
                        .WhereIf(input.Email.IsNotNullOrEmpty(), p => p.Email.Contains(input.Email))
                        .WhereIf(input.Phone.IsNotNullOrEmpty(), p => p.Phone == input.Phone)
                        .Where(p => p.IsDeleted == false)
                        .ToList()
                        .Select(p => p.MapTo<ContactExcelDto>())
                        .ToList();
        }

       [AbpAuthorize(PermissionNames.AdminPage_Contact)]
        public virtual async Task<PagedResultDto<ContactDto>> GetAll(ContactFilterDto input)
        {

            var query = contactRepository.GetAll()
                .Where(p => p.ContactType == ContactType.GetInTouch)
                .WhereIf(input.FullName.IsNotNullOrEmpty(), p => p.FullName.Contains(input.FullName))
                .WhereIf(input.Email.IsNotNullOrEmpty(), p => p.Email.Contains(input.Email))
                .WhereIf(input.Phone.IsNotNullOrEmpty(), p => p.Phone == input.Phone);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<ContactDto>(
                totalCount,
                entities.Select(p => p.MapTo<ContactDto>())
                    .ToList()
            );
        }
        protected virtual IQueryable<Contact> ApplySorting(IQueryable<Contact> query, ContactFilterDto input)
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

        protected virtual IQueryable<Contact> ApplyPaging(IQueryable<Contact> query, ContactFilterDto input)
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

        [AbpAuthorize(PermissionNames.AdminPage_Contact)]
        public async Task Delete(EntityDto<int> input)
        {
            await contactRepository.DeleteAsync(p => p.Id == input.Id);
        }

        public async Task Create(CreateOrEditContactDto input)
        {
            input.ContactType = ContactType.GetInTouch;

            var entity = ObjectMapper.Map<Contact>(input);
            await contactRepository.InsertAsync(entity);

            await CurrentUnitOfWork.SaveChangesAsync();

            var email = await SettingManager.GetSettingValueAsync(EmailSettingNames.DefaultFromAddress);
            var content = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> " +
                          $"<html xmlns=\"http://www.w3.org/1999/xhtml\"> <head> <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />" +
                          $" <title></title> <style> body {{ line - height: 1.4; text-align: left; font-family: \"Roboto\", sans-serif; color: #333333; }}" +
                          $" #bodyTable {{ margin - top: 36px; margin-bottom: 30px; padding-bottom: 80px; }} #emailContainer {{ background: #fff; " +
                          $"border-bottom: 3px solid gainsboro; border: 1px solid #e2e2e2; }} h1 {{ margin - bottom: 0; }} .btn {{ background - color: #63bce5; " +
                          $"color: #fff; display: inline-block; padding: 6px 12px; margin-bottom: 0; font-size: 14px; font-weight: 400; line-height: 1.42857143; " +
                          $"text-align: center; white-space: nowrap; vertical-align: middle; border: 1px solid #fff; border-radius: 4px; text-decoration: none; }}" +
                          $" #emailContainer td {{ padding: 24px 10%; }} a {{ color: #0c82a5; text-decoration: none; }} </style> </head> <body style=\"background: #F5F5F5;\">" +
                          $" <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"100%\" width=\"100%\" id=\"bodyTable\"> <tr> <td align=\"center\" valign=\"top\">" +
                          $" <table border=\"0\" cellpadding=\"20\" cellspacing=\"0\" width=\"600\" id=\"emailContainer\"> <tr> <td valign=\"top\" " +
                          $"style=\"font-weight: 300; color: #333333; font-size: 16px; line-height: 1.4; text-align: left; font-family: 'Roboto', sans-serif;\"> <p>" +
                          $" Đã có khách hàng đã gửi liên hệ đến hệ thống theo thông tin như dưới đây: </p> <p><b>Thông tin người liên hệ</b></p> " +
                          $"<p> Khách hàng: <span style=\"text-transform:capitalize;\">{input.FullName}</span> <br /> Điện thoại : {input.Phone}" +
                          $" <br /> Email : {input.Email} <br /> Nội dung : {input.Content} </p> <p>Thân mến,<br />Ban quản trị TicketPage. </p> </td> </tr> </table> </td> " +
                          $"</tr> </table> </body> </html>";
            emailSender.Send(
          to: email,
          subject: "[Sample] Có liên hệ mới",
          body: content,
          isBodyHtml: true);

        }
    }
}


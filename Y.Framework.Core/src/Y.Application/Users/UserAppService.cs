using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Net.Mail;
using Abp.Runtime.Session;
using Abp.Runtime.Validation;
using Abp.UI;
using Y.Authorization;
using Y.Authorization.Accounts;
using Y.Authorization.Roles;
using Y.Authorization.Users;
using Y.Roles.Dto;
using Y.Users.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Y.Core;
using Y.Services;
using Abp.AutoMapper;
using Abp.Authorization.Users;
using Abp.Json;

namespace Y.Users
{
    //[AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>, IUserAppService
    {
        private readonly IPictureAppService pictureAppService;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly LogInManager _logInManager;
        private readonly IEmailSender emailSender;
        private readonly IRepository<User, long> repository;
        private readonly IRepository<UserRole, long> _userRoleRepository;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IAbpSession abpSession,
            LogInManager logInManager,
            IPictureAppService pictureAppService,
            IEmailSender emailSender,
            IRepository<UserRole, long> _userRoleRepository
            )
            : base(repository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _abpSession = abpSession;
            _logInManager = logInManager;
            this.pictureAppService = pictureAppService;
            this.emailSender = emailSender;
            this.repository = repository;
            this._userRoleRepository = _userRoleRepository;
        }

        [AbpAuthorize(PermissionNames.AdminPage_ManagementUser)]
        public virtual async Task<PagedResultDto<UserDto>> GetAllGuest(PagedUserResultRequestDto input)
        {
            var query = repository
                        .GetAll()
                        .Include(p => p.Roles)
                        .Where(p => !p.Roles.Any())
                        .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive)
                        .WhereIf(input.UserName.IsNotNullOrEmpty(), p => p.Name.Contains(input.UserName))
                        .WhereIf(input.Email.IsNotNullOrEmpty(), p => p.EmailAddress.Contains(input.Email));

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<UserDto>(
                totalCount,
                entities.Select(p => p.MapTo<UserDto>())
                    .ToList()
            );
        }

        [AbpAuthorize(PermissionNames.AdminPage_ManagementUser)]
        public virtual async Task<PagedResultDto<UserDto>> GetManagementUsers(PagedUserResultRequestDto input)
        {

            var query = repository
                        .GetAll()
                        .Include(p => p.Roles)
                        .Where(p => p.Roles
                                        .Any(u => u.RoleId == (long)EnumRoles.AdminId
                                                || u.RoleId == (long)EnumRoles.ManagerId
                                                || u.RoleId == (long)EnumRoles.CheckIn
                                                || u.RoleId == (long)EnumRoles.OrderOffline
                                                )
                                            )
                        .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword)
                                || x.Name.Contains(input.Keyword)
                                || x.EmailAddress.Contains(input.Keyword))
                        .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive)
                        .WhereIf(input.UserName.IsNotNullOrEmpty(), p => p.UserName == input.UserName)
                        .WhereIf(input.Email.IsNotNullOrEmpty(), p => p.EmailAddress == input.Email);

            var totalCount = await query.CountAsync();

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await query.ToListAsync();

            return new PagedResultDto<UserDto>(
                totalCount,
                entities.Select(p => p.MapTo<UserDto>())
                    .ToList()
            );
        }

        [AbpAuthorize(PermissionNames.AdminPage_User,PermissionNames.AdminPage_ManagementUser)]
        public async Task<CreateOrEditUserDto> GetForEdit(long? id)
        {
            var model = new CreateOrEditUserDto();
            if (id == null)
            {
                model.IsActive = true;
                model.RoleNames = new string[]{ };
                return model;
            }

            var entity = await repository
                              .GetAll()
                              .Include(p=>p.Roles)
                              .FirstOrDefaultAsync(p => p.Id == id);
            var user = entity.MapTo<CreateOrEditUserDto>();
            user.RoleNames =  _userManager.GetRolesAsync(entity).Result.ToArray();
            return user;
        }

        public async Task<UserDto> Register(CreateUserDto input)
        {
            var entity = Repository.GetAll().Any(p => p.EmailAddress == input.EmailAddress);
            if (entity)
            {
                throw new UserFriendlyException("Email đã được đăng ký!");
            }
            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.IsActive = true;
            
            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);
            
            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            //dòng await _userManager.SetRoles(user, input.RoleNames) sẽ nguy hiểm nếu user tự tuyền role vào
            //mà comment code thì gây ra lỗi source cant not null --> set lại roles names là string[0]
            input.RoleNames = new string[0];
            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        [AbpAuthorize(PermissionNames.AdminPage_User, PermissionNames.AdminPage_ManagementUser)]
        public override async Task<UserDto> Create(CreateUserDto input)
        {
            //CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            //user.UserName = user.EmailAddress;
            user.Password = _passwordHasher.HashPassword(user, input.Password);
            user.IsEmailConfirmed = true;

            await _userManager.InitializeOptionsAsync(AbpSession.TenantId);

            CheckErrors(await _userManager.CreateAsync(user, input.Password));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            CurrentUnitOfWork.SaveChanges();

            return MapToEntityDto(user);
        }

        [AbpAuthorize(PermissionNames.AdminPage_User, PermissionNames.AdminPage_ManagementUser)]
        public override async Task<UserDto> Update(UserDto input)
        {
            //CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }

        [AbpAuthorize(PermissionNames.AdminPage_User, PermissionNames.AdminPage_ManagementUser)]
        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task ChangeLanguage(ChangeUserLanguageDto input)
        {
            await SettingManager.ChangeSettingForUserAsync(
                AbpSession.ToUserIdentifier(),
                LocalizationSettingNames.DefaultLanguage,
                input.LanguageName
            );
        }

        

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            user.SetNormalizedNames();
            return user;
        }

        protected override void MapToEntity(UserDto input, User user)
        {
            ObjectMapper.Map(input, user);
            user.SetNormalizedNames();
        }

        protected override UserDto MapToEntityDto(User user)
        {
            var roles = _roleManager.Roles.Where(r => user.Roles.Any(ur => ur.RoleId == r.Id)).Select(r => r.NormalizedName);
            var userDto = base.MapToEntityDto(user);
            userDto.RoleNames = roles.ToArray();
            return userDto;
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedUserResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles)
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.Keyword) || x.Name.Contains(input.Keyword) || x.EmailAddress.Contains(input.Keyword))
                .WhereIf(input.IsActive.HasValue, x => x.IsActive == input.IsActive)
                .WhereIf(input.UserName.IsNotNullOrEmpty(), p => p.UserName == input.UserName)
                .WhereIf(input.Email.IsNotNullOrEmpty(), p => p.EmailAddress == input.Email);
        }

        //protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedUserResultRequestDto input)
        //{
        //    return query.OrderBy(r => r.UserName);
        //}

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<bool> ChangePassword(ChangePasswordDto input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("Please log in before attemping to change password.");
            }
            long userId = _abpSession.UserId.Value;
            var user = await _userManager.GetUserByIdAsync(userId);
            var loginAsync = await _logInManager.LoginAsync(user.UserName, input.CurrentPassword, shouldLockout: false);
            if (loginAsync.Result != AbpLoginResultType.Success)
            {
                throw new UserFriendlyException("Your 'Existing Password' did not match the one on record.  Please try again or contact an administrator for assistance in resetting your password.");
            }
            if (!new Regex(AccountAppService.PasswordRegex).IsMatch(input.NewPassword))
            {
                throw new UserFriendlyException("Passwords must be at least 8 characters, contain a lowercase, uppercase, and number.");
            }
            user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
            CurrentUnitOfWork.SaveChanges();
            return true;
        }

        //public async Task<bool> ResetPassword(ResetPasswordDto input)
        //{
        //    if (_abpSession.UserId == null)
        //    {
        //        throw new UserFriendlyException("Please log in before attemping to reset password.");
        //    }
        //    long currentUserId = _abpSession.UserId.Value;
        //    var currentUser = await _userManager.GetUserByIdAsync(currentUserId);
        //    var loginAsync = await _logInManager.LoginAsync(currentUser.UserName, input.AdminPassword, shouldLockout: false);
        //    if (loginAsync.Result != AbpLoginResultType.Success)
        //    {
        //        throw new UserFriendlyException("Your 'Admin Password' did not match the one on record.  Please try again.");
        //    }
        //    if (currentUser.IsDeleted || !currentUser.IsActive)
        //    {
        //        return false;
        //    }
        //    var roles = await _userManager.GetRolesAsync(currentUser);
        //    if (!roles.Contains(StaticRoleNames.Tenants.Admin))
        //    {
        //        throw new UserFriendlyException("Only administrators may reset passwords.");
        //    }

        //    var user = await _userManager.GetUserByIdAsync(input.UserId);
        //    if (user != null)
        //    {
        //        user.Password = _passwordHasher.HashPassword(user, input.NewPassword);
        //        CurrentUnitOfWork.SaveChanges();
        //    }

        //    return true;
        //}

        public async Task<IdentityResult> ResetPassword(ResetPasswordDto input)
        {
            var user = await Repository.GetAll()
                .WhereIf(input.Email.IsNotNullOrEmpty(), p => p.EmailAddress == input.Email)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new UserFriendlyException("Tài khoản không tồn tại!");
            }

            var token = input.Token.FromBase64();
            var result = await _userManager.ResetPasswordAsync(user, input.Token.FromBase64(), input.Password);
            return result;
        }

        [AbpAuthorize()]
        public async Task<UserMobileDto> GetProfile()
        {
            CheckGetPermission();
            var id = AbpSession.UserId;
            if (id == null)
                throw new UserFriendlyException("Bạn chưa đăng nhập");
            var entity = await GetEntityByIdAsync(id.Value);
            var permissionModelList = await _userManager.GetGrantedPermissionsAsync(entity);
            var roleList = await _userManager.GetRolesAsync(entity);
            return new UserMobileDto()
            {
                Name = entity.Name,
                UserName = entity.UserName,
                UserId = entity.Id,
                Surname = entity.Surname,
                EmailAddress = entity.EmailAddress,
                AvatarId = entity.AvatarId,
                AvatarUrl = pictureAppService.GetPictureUrl(entity.AvatarId, 200),
                PhoneNumber = entity.PhoneNumber,
                Gender = entity.Gender,
                Birthday = entity.Birthday,
                Permissions = permissionModelList.Select(p => p.Name).ToList(),
                RoleNames = roleList.ToList()
            };
        }
        public virtual async Task<object> UpdateProfile(UserMobileDto input)
        {
            CheckUpdatePermission();
            var userId = AbpSession.GetUserId();
            var entity = await GetEntityByIdAsync(userId);

            entity.Name = input.Name;
            entity.Surname = input.Surname;
            entity.AvatarId = input.AvatarId;
            entity.PhoneNumber = input.PhoneNumber;
            entity.Gender = input.Gender;
            entity.Birthday = input.Birthday;
            await CurrentUnitOfWork.SaveChangesAsync();
            var permissionModelList = await _userManager.GetGrantedPermissionsAsync(entity);
            return new UserMobileDto()
            {
                Name = entity.Name,
                UserName = entity.UserName,
                Surname = entity.Surname,
                EmailAddress = entity.EmailAddress,
                AvatarId = entity.AvatarId,
                AvatarUrl = pictureAppService.GetPictureUrl(entity.AvatarId, 200),
                PhoneNumber = entity.PhoneNumber,
                Gender = entity.Gender,
                Birthday = entity.Birthday,
                Permissions = permissionModelList.Select(p => p.Name).ToList()
            };
        }

        public async Task GetForgotPassword(string emailAddress)
        {
            var user = await Repository.GetAll()
                .WhereIf(emailAddress.IsNotNullOrEmpty(), p => p.EmailAddress == emailAddress)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new UserFriendlyException("Tài khoản không tồn tại!");
            }
            var token = await _userManager.
                GeneratePasswordResetTokenAsync(user);
            token = token.ToBase64();
            var url = $"{SettingManager.GetSettingValue(SettingKey.App_FrontEndBaseUrl)}resetPassword?token={token}";

            emailSender.Send(
                to: emailAddress,
                subject: "Password Recovery",
                body: $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" " +
                      $"\"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> " +
                      $"<html xmlns=\"http://www.w3.org/1999/xhtml\"><head><link rel=\"stylesheet\" " +
                      $"href=\"path/to/font-awesome/css/font-awesome.min.css\" /> <link rel=\"stylesheet\" " +
                      $"href=\"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css\" /> " +
                      $"<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" /><title></title> " +
                      $"<style>body {{line - height: 1.4;text-align: left;font-family: \"Roboto\", sans-serif;color: #333333;}}" +
                      $" #bodyTable {{margin - top: 36px; margin-bottom: 30px;padding-bottom: 80px;}} #emailContainer " +
                      $"{{background: #fff; border-bottom: 3px solid gainsboro;border: 1px solid #e2e2e2;}}h1 " +
                      $"{{margin - bottom: 0;}} .btn {{background - color: #63bce5;color: #fff; display: inline-block;padding: " +
                      $"6px 12px; margin-bottom: 0;font-size: 14px; font-weight: 400;line-height: 1.42857143; " +
                          $"text-align: center; white-space: nowrap;vertical-align: middle;border: 1px solid #fff;" +
                          $"border-radius: 4px; text-decoration: none;}} #emailContainer td {{ padding: 24px 10%;}} " +
                          $"</style></head><body style=\"background: #F5F5F5;\"> <table border=\"0\" cellpadding=\"0\" " +
                          $"cellspacing=\"0\" height=\"100%\" width=\"100%\" id=\"bodyTable\"> <div " +
                          $"style=\"text-align:center;margin-top:36px;\"> <img src=\"\" " +
                          $"style=\"vertical-align:middle;max-width: 100%;text-align: center; margin-bottom: 20px;\" /> " +
                          $"</div> <tr><td align=\"center\" valign=\"top\"> <table border=\"0\" cellpadding=\"20\" " +
                          $"cellspacing=\"0\" width=\"600\" id=\"emailContainer\"> <tr> <td valign=\"top\" " +
                          $"style=\"font-weight: 300; color: #333333; font-size: 16px; line-height: 1.4; " +
                          $"text-align: left; font-family: 'Roboto', sans-serif;\"> <p>Chào <b>{user.UserName}</b>," +
                          $"</p> <p>Anh/chị vừa yêu cầu thay đổi mật khẩu của tài khoản {user.UserName} trên Sample.</p>" +
                          $" <p> Để thay đổi mật khẩu, Quý khách vui lòng click vào nút bên dưới </p> <p> " +
                          $"<a class=\"btn\" style=\"background:#59c6d7;color:#fff;padding:0.5rem 0.75rem\" " +
                          $"href=\"{url}\">Lấy lại mật khẩu</a> </p> <p> Quý khách có 24h để đặt " +
                          $"lại mật khẩu, sau đó Quý khách phải yêu cầu mật khẩu mới. </p> <p> Nếu Quý khách không yêu " +
                          $"cầu thay đổi mật khẩu, vui lòng bỏ qua email này. </p> <p>Chúc anh/chị có những trải nghiệm " +
                          $"tuyệt vời trên website Sample.</p> <p>Thân mến,<br />Ban quản trị Sample.</p> </td>" +
                          $"</tr></table> </td></tr></table></body></html>",
                isBodyHtml: true
            );
        }
    }
}


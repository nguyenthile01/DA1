//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Abp.Application.Services;
//using Abp.Application.Services.Dto;
//using Abp.Domain.Repositories;
//using Microsoft.EntityFrameworkCore;
//using Y.Core;
//using Y.Dto;

//namespace Y.Services
//{
//    //[AbpAuthorize(PermissionNames.Pages_Users)]
//    public class ProductAppService : AsyncCrudAppService<Product, ProductDto, int,
//        ProductListDto, ProductCreateDto, ProductUpdateDto>, IProductAppService
//    {
//        private readonly IRepository<Product> productRepository;

//        public ProductAppService(IRepository<Product> productRepository) : base(productRepository)
//        {
//            this.productRepository = productRepository;
//        }


//        public async Task<ListResultDto<ProductListDto>> GetList()
//        {
//            var data = await productRepository
//                .GetAllIncluding(p => p.Translations)
//                .ToListAsync();
//            return new ListResultDto<ProductListDto>(ObjectMapper.Map<List<ProductListDto>>(data));
//        }

//        public override async Task<ProductDto> Update(ProductUpdateDto input)
//        {
//            CheckUpdatePermission();

//            var entity = await GetEntityByIdAsync(input.Id);
//            entity.Translations.Clear();
//            MapToEntity(input, entity);
//            await CurrentUnitOfWork.SaveChangesAsync();

//            return MapToEntityDto(entity);
//        }
//    }
//}




using System.Linq;
using Abp.AutoMapper;
using Abp.Localization;
using Abp.Timing;
using AutoMapper;
using AutoMapper.Configuration;
using Y.Core;
using Y.Dto;
using Y.Services;

namespace Y
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration, MultiLingualMapContext context)
        {
            //configuration.YCreateMultiLingualMap<Product, ProductTranslation, ProductListDto>(context);



            #region Category
            configuration.CreateMap<PostCategoryTranslationDto, CreateOrEditPostCategoryDto>().IgnoreAllNonExisting(new MapperConfiguration((MapperConfigurationExpression)configuration));
            configuration.YCreateMultiLingualMap<PostCategory, PostCategoryTranslation, PostCategoryDto>(context);

            #endregion

            #region Post
            configuration.CreateMap<PostTranslationDto, CreateOrEditPostDto>().IgnoreAllNonExisting(new MapperConfiguration((MapperConfigurationExpression)configuration));
            configuration.YCreateMultiLingualMap<Post, PostTranslation, PostDto>(context);
                //.EntityMap.ForMember(u => u.CategoryName, options => options.MapFrom(input => input.Category.Name));
            #endregion

            #region Topic

            #region Topic category
            configuration.CreateMap<TopicCategoryTranslationDto, CreateOrEditTopicCategoryDto>().IgnoreAllNonExisting(new MapperConfiguration((MapperConfigurationExpression)configuration));
            #endregion

            #region Topic
            configuration.CreateMap<TopicTranslationDto, CreateOrEditTopicDto>().IgnoreAllNonExisting(new MapperConfiguration((MapperConfigurationExpression)configuration));
            configuration.YCreateMultiLingualMap<Topic, TopicTranslation, TopicDto>(context);
            #endregion

            #endregion

            //#region Organizer
            //configuration.CreateMap<OrganizerTranslationDto, CreateOrEditOrganizerDto>().IgnoreAllNonExisting(new MapperConfiguration((MapperConfigurationExpression)configuration));
            //configuration.YCreateMultiLingualMap<Organizer, OrganizerTranslation, OrganizerDto>(context);

            //#endregion

            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
            .ForMember(u => u.IsEnabled, options => options.MapFrom(input => !input.IsDisabled));
        }
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression, IConfigurationProvider configuration)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = configuration.GetAllTypeMaps().First(x => x.SourceType == sourceType
                                                                 && x.DestinationType == destinationType);
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }
    }
}
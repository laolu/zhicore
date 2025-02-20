using My.ZhiCore.LanguageManagement.Languages;
using My.ZhiCore.LanguageManagement.LanguageTexts;

namespace My.ZhiCore.LanguageManagement
{
    public class LanguageManagementDomainAutoMapperProfile : Profile
    {
        public LanguageManagementDomainAutoMapperProfile()
        {
            CreateMap<Language, LanguageDto>();
            CreateMap<LanguageText, LanguageTextDto>();
            CreateMap<Language, LanguageInfo>();
        }
    }
}
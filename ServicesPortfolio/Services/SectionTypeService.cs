using DataPortfolio.Models;
using DataPortfolio.Repositories;
using System.Threading.Tasks;

namespace ServicesPortfolio.Services
{
    public class SectionTypeService
    {
        private readonly ISectionTypeRepository sectionTypeRepository;

        public SectionTypeService(ISectionTypeRepository sectionTypeRepository)
        {
            this.sectionTypeRepository = sectionTypeRepository;
        }

        public async Task<IReadOnlyList<SectionType>> GetSectionTypeList()
        {
            return await sectionTypeRepository.GetList();
        }
    }
}

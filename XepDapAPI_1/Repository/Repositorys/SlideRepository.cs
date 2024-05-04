using Data.DBContext;
using Data.Models;
using XeDapAPI.Repository.Interface;

namespace XeDapAPI.Repository.Repositorys
{
    public class SlideRepository : ISlideInterface
    {
        private readonly MyDB _dbContext;
        public SlideRepository(MyDB dbContext)
        {
            _dbContext = dbContext;
        }

        public ICollection<Slide> GetSlides()
        {
            return _dbContext.Slides.OrderBy(x => x.Sort).ToList();
        }
    }
}

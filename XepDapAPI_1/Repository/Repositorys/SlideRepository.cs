using Data.DBContext;
using Data.Models;
using Data.Models.Enum;
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

        public Slide GetSlideImage(int slideId)
        {
            return _dbContext.Slides.FirstOrDefault(x => x.Id == slideId);
        }

        public Slide GetSlideImageid4(int slideId = 4)
        {
            return _dbContext.Slides.FirstOrDefault(x => x.Id == slideId);
        }

        public Slide GetSlideImageid5(int slideId = 5)
        {
            return _dbContext.Slides.FirstOrDefault(x => x.Id == slideId);
        }

        public ICollection<Slide> GetSlides()
        {
            var slides = _dbContext.Slides.Where(x => x.Status == StatusSlide.Active).OrderBy(x => x.Sort).ToList();
            return slides;
        }
    }
}

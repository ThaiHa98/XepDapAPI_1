using Data.Models.Enum;

namespace Data.Dto
{
    public class SlideDto
    {
        public int UserId {  get; set; }
        public string SlideName { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public int Sort { get; set; }
    }
}

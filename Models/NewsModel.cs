namespace NewsPortal.Models
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Detail { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public IFormFile? Img { get; set; }
        public string? ImgPath { get; set; }
    }
}
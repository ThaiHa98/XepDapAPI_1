using Data.DBContext;
using Data.Dto;
using Data.Models;
using Data.Models.Enum;
using System;
using System.Xml.Linq;
using XeDapAPI.Service.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace XeDapAPI.Service.Services
{
    public class SlideService : ISlideIService
    {
        private readonly MyDB _dbContext;
        public SlideService(MyDB dbContext)
        {
            _dbContext = dbContext;
        }
        public string Create(SlideDto slideDto, IFormFile image)
        {
            try
            {
                if(slideDto == null)
                {
                    throw new ArgumentNullException(nameof(slideDto), "Slide object is null or missing required information.");
                }
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == slideDto.UserId);
                if (user == null)
                {
                    throw new ArgumentException("User not found");
                }
                Slide slide = new Slide
                {
                    Name = slideDto.SlideName,
                    Url = slideDto.Url,
                    Description = slideDto.Description,
                    Sort = slideDto.Sort,
                    Status = StatusSlide.Active,
                };
                if(image != null && image.Length > 0)
                {
                    string imagePath = SaveProductImage(image);
                    slide.Image = imagePath;
                    _dbContext.Slides.Add(slide);
                    _dbContext.SaveChanges();
                    return "Product added successfully!";
                }
                else
                {
                    return "No image provided.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There is an error when creating a Slide", ex);
            }
        }

        public bool Delete(int Id)
        {
            try
            {
                var slide = _dbContext.Slides.FirstOrDefault(x => x.Id == Id);
                if(slide == null)
                {
                    throw new Exception("slideId not found");
                }
                _dbContext.Slides.Remove(slide);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex) 
            {
                throw new Exception("There was an error while deleting the Slide",ex);
            }
        }

        public byte[] GetSileBytesImage(string imagePath)
        {
            try
            {
                if(string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                {
                    throw new FieldAccessException("Image not found!");
                }
                return File.ReadAllBytes(imagePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public byte[] GetSlideBytesImageid4(string imagePath)
        {
            try
            {
                if(string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                {
                    throw new FieldAccessException("Image not found!");
                }
                return File.ReadAllBytes(imagePath);
            }
            catch( Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public byte[] GetSlideBytesImageid5(string imagePath)
        {
            try
            {
                if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                {
                    throw new FieldAccessException("Image not found!");
                }
                return File.ReadAllBytes(imagePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        public string Update(UpdateSlideDto updateSlideDto)
        {
            try
            {
                var slide = _dbContext.Slides.FirstOrDefault(x => x.Id == updateSlideDto.Id);
                if(slide == null)
                {
                    throw new Exception("slideId not found");
                }
                Slide slide1 = new Slide
                {
                    Id = slide.Id,
                    Url = updateSlideDto.Url,
                    Sort = updateSlideDto.Sort,
                };
                _dbContext.SaveChanges();
                return "Updata Successfully";
            }
            catch(Exception ex)
            {
                throw new Exception("An error occurred while updating Slides",ex);
            }
        }
        private string SaveProductImage(IFormFile image)
        {
            try
            {
                string currentDateFolder = DateTime.Now.ToString("dd-MM-yyyy");
                string imagesFolder = Path.Combine(@"C:\Users\XuanThai\Desktop\ImageXedap", "Slide_images", currentDateFolder);
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(imagesFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                return filePath;
            }
            catch (Exception e)
            {
                throw new Exception($"An error occurred while saving the image: {e.Message}");
            }
        }
    }
}

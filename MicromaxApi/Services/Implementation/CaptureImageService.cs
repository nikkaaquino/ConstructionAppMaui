using MicromaxApi.Data.Entity;
using MicromaxApi.Data.Repositories.Interface;
using MicromaxApi.Model;
using MicromaxApi.Services.Config;
using MicromaxApi.Services.Dto;
using MicromaxApi.Services.Interface;

namespace MicromaxApi.Services.Implementation
{
    public class CaptureImageService : ErrorService, ICaptureImageService
    {
        private readonly ICaptureImageRepository _repo;

        public CaptureImageService(ICaptureImageRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ImagesResponse>> GetImages(string userid)
        {
            try
            {
                var result = await _repo.GetImages(userid);


                var response = result.Select(x => new ImagesResponse
                {
                    ImageName = x.ImageName,
                    //ImageData = x.ImageData,
                    ImageType = x.ImageType,
                    Location = x.Location,
                    User = userid,
                    DateCreated = x.DateCreated,                    
                }).ToList();

                return response;

            }
            catch (Exception ex)
            {
                Validation.Add("errors", ex.Message);
                return null;
            }
        }

        public async Task<bool> SaveImages(ImageModel model)
        {
            try
            {
                var saveEntity = new ImageEntity
                {
                    ImageName = model.ImageName,
                    ImageData = model.ImageData,
                    ImageType = model.ImageType,
                    Location = model.Location,
                    User = model.User,
                    DateCreated = DateTime.Now
                };

                var isSaved = await _repo.SaveImages(saveEntity);
                if (!isSaved)
                {
                    Validation.Add("errors", "Something went wrong. Please try again in a while");
                }

                return isSaved;
            }
            catch (Exception ex)
            {
                Validation.Add("errors", ex.Message);
                return false;
            }
        }
    }
}

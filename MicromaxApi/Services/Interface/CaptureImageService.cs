using MicromaxApi.Data.Entity;
using MicromaxApi.Data.Repositories.Interface;
using MicromaxApi.Model;
using MicromaxApi.Services.Config;
using MicromaxApi.Services.Dto;

namespace MicromaxApi.Services.Interface
{
    public class CaptureImageService : ErrorService, ICaptureImageService
    {
        private readonly ICaptureImageRepository _repo;

        public CaptureImageService(ICaptureImageRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ImageResponse>> GetImagesByUser(string userid)
        {
            try
            {
                var result = await _repo.GetImageByUser(userid);

                var response = result.Select(x => new ImageResponse
                {
                    ImageId = x.ImageId,
                    ImageName = x.ImageName,
                    ImageData = x.ImageData,
                    DateCreated = x.DateCreated,
                    CreatedBy = x.CreatedBy,

                }).ToList();

                return response;
                

            }
            catch (Exception ex)
            {
                Validation.Add("errors", "Something went wrong");
                return null;
            }
        }

        public async Task<bool> SaveImage(CaptureImageModel model)
        {
            try
            {

                var saveEntity = new CaptureImageEntity
                {
                    ImageId = model.ImageId,
                    ImageName = model.ImageName,
                    ImageData = model.ImageData,
                    CreatedBy = model.CreatedBy,
                    DateCreated = DateTime.Now
                };


                var isSaved = await this._repo.SaveImage(saveEntity);
                if (!isSaved)
                {
                    this.Validation.Add("errors", "Something went wrong. Please try again in a while");
                }

                return isSaved;
            }
            catch (Exception ex)
            {
                this.Validation.Add("errors", "Something went wrong. Please try again in a while");
                return false;
            }
        }
    }
}

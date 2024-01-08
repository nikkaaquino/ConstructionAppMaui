using Dapper;
using MicromaxApi.Context;
using MicromaxApi.Data.Entity;
using MicromaxApi.Data.Repositories.Interface;
using MicromaxApi.Model;

namespace MicromaxApi.Data.Repositories.Implementation
{
    public class CaptureImageRepository : ICaptureImageRepository
    {
        private readonly DapperContext _context;

        public CaptureImageRepository(DapperContext context)
        {
            _context = context;
        }


        public async Task<List<CaptureImageEntity>> GetImageByUser(string userid)
        {
            try
            {
                var sql = "select * from tblImageUpload where created_by = @UserId";

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<CaptureImageEntity>(sql, new { UserId = userid });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex}");
                throw;
            }
        }

        public async Task<List<ImageEntity>> GetImages(string userid)
        {
            try
            {
                var sql = "select ImageId, ImageName, ImageData, Location, User, DateCreated, ImageView, ImageType from tblImages where [User] = @UserId";

                using (var connection = _context.CreateConnection())
                {
                    var result = await connection.QueryAsync<ImageEntity>(sql, new { UserId = userid });
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex}");
                throw;
            }
        }

        public async Task<bool> SaveImage(CaptureImageEntity entity)
        {
            try
            {
                var sql = "insert into tblImageUpload (img_id, img_name, img, date_created, created_by) values (@imageId, @imageName, @imageData, @dateCreated, @createdBy)";

                using (var connection = _context.CreateConnection())
                {
                    var count = await connection.ExecuteAsync(sql, entity);
                    return count > 0;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SaveImages(ImageEntity entity)
        {
            try
            {
                var sql = "insert into tblImages (ImageId, ImageName, ImageData, DateCreated, Location, [User], ImageView, ImageType) values (@imageId, @imageName, @imageData, @dateCreated, @location, @user, @imageView, @imageType)";

                using (var connection = _context.CreateConnection())
                {
                    var count = await connection.ExecuteAsync(sql, entity);
                    return count > 0;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

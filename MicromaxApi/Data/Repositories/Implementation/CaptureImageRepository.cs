﻿using Dapper;
using MicromaxApi.Context;
using MicromaxApi.Data.Entity;
using MicromaxApi.Data.Repositories.Interface;

namespace MicromaxApi.Data.Repositories.Implementation
{
    public class CaptureImageRepository : ICaptureImageRepository
    {
        private readonly DapperContext _context;

        public CaptureImageRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<ImageEntity>> GetImages(string userid)
        {
            var sql = "select ImageId, ImageName, ImageData, Location, [User], DateCreated, ImageType from tblImages where [User] = @UserId";
            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<ImageEntity>(sql, new { UserId = userid });
            return result.ToList();
        }

        public async Task<bool> SaveImages(ImageEntity entity)
        {
                var sql = "insert into tblImages (ImageName, ImageData, DateCreated, Location, [User], ImageType) values (@imageName, @imageData, @dateCreated, @location, @user, @imageType)";
                using var connection = _context.CreateConnection();
                var count = await connection.ExecuteAsync(sql, entity);
                return count > 0;
        }
    }
}

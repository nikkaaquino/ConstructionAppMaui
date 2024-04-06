﻿namespace ConstructionApp.Model
{
    public class PhotoModel
    {
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public string Location { get; set; }
        public string User { get; set; }
        public string ImageType { get; set; }
    }

    [JsonSerializable(typeof(List<PhotoModel>))]
    internal sealed partial class PhotoModelContext : JsonSerializerContext
    {

    }
}

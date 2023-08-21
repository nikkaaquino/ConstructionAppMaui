using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionApp.Model
{
    public class CaptureImageModel : INotifyPropertyChanged
    {
        int _imageid;
        public int ImageId
        {
            get => _imageid;
            set
            {
                if (_imageid == value)
                    return;
                _imageid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageId)));
            }
        }
               
        string _imagename;

        public string ImageName
        {
            get => _imagename;
            set
            {
                if (_imagename == value)
                    return;
                _imagename = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageName)));
            }
        }

        byte[] _imagedata;

        public byte[] ImageData
        {
            get => _imagedata;
            set
            {
                if (_imagedata == value)
                    return;
                _imagedata = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageData)));
            }
        }

        string _createdby;
        public string CreatedBy
        {
            get => _createdby;
            set
            {
                if (_createdby == value)
                    return;
                _createdby = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CreatedBy)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC_backend.logic.Interfaces.Data
{
    public interface IImageRepository
    {
        Task DeleteImage();
        Task<string> UploadImage(IFormFile image);
    }
}

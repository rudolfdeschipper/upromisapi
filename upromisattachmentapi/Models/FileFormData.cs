using System;
using Microsoft.AspNetCore.Http;

namespace upromisattachmentapi.Models
{
    public class FileFormData
    {
        public string UploadedBy { get; set; }
        public Guid ParentItem { get; set; }
        public IFormFile File { get; set; } 
    }
}
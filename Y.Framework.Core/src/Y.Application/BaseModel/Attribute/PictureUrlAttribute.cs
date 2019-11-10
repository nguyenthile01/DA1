using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;

namespace Y.Dto
{
    public class PictureUrlAttribute : Attribute
    {
        public PictureUrlAttribute(string from)
        {
            From = from;
        }
        public PictureUrlAttribute(string from, int size)
        {
            From = from;
            Size = size;
        }
        public string From { get; set; }
        public int Size { get; set; }
    }
}

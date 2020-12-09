using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
    public class ByteRequestResult
    {
        public int StatusCode { get; set; }
        public byte[] Content { get; set; } = new byte[1024];
    }
}

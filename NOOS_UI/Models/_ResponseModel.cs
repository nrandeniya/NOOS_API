using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// evaluate and formulate more detialed response for user operations. This is called when the BaseRepository called Endpoints 
/// </summary>

namespace NOOS_UI.Models
{
    public class _ResponseModel
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public string Content { get; set; }
    }
}

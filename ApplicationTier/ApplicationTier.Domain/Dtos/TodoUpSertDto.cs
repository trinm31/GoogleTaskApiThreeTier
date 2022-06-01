using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTier.Domain.Dtos
{
    public class TodoUpSertDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int TaskId { get; set; }
    }
}

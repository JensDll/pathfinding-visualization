using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Response
{
    public class ValidationErrorResponseDto
    {
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}

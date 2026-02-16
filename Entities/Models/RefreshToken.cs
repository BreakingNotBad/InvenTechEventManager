using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class RefreshToken
    {
        public int RefreshTokenId { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
    }
}

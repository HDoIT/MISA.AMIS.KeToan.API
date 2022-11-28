using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    public class PagingResult<Employee>
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Employee>? Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public long? totalCount { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public decimal totalPages { get; set; }
    }
}

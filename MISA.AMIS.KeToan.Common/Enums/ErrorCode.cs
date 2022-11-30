using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Enums
{
    /// <summary>
    /// Enum sử dụng để mô tả các lỗi xảy ra
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Lỗi gặp exception
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Lỗi trùng mã
        /// </summary>
        DuplicateCode = 2,

        /// <summary>
        /// Lỗi nhập dữ liệu
        /// </summary>
        InvalidCode = 3,

        /// <summary>
        /// Lỗi ngày lớn hơn ngày hiện tại
        /// </summary>
        Date= 4,
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Constants
{
    public class Procedure
    {

        /// <summary>
        /// Format tên của procedure lấy tất cả bản ghi
        /// </summary>
        public static string GET_ALL = "Proc_{0}_GetAll";

        /// <summary>
        /// Format tên của procedure lấy bản ghi theo ID
        /// </summary>
        public static string GET_BY_ID = "Proc_{0}_GetBy{1}ID";

        /// <summary>
        /// Format tên của procedure thêm mới bản ghi
        /// </summary>
        public static string GET_INSERT = "Proc_{0}_Insert{1}";

        /// <summary>
        /// Format tên của procedure cập nhập 1 bản ghi
        /// </summary>
        public static string GET_UPDATE = "Proc_{0}_Update{1}";

        /// <summary>
        /// Format tên của procedure xóa 1 bản ghi
        /// </summary>
        public static string GET_DELETE = "Proc_{0}_Delete{1}";

        /// <summary>
        /// Format tên của procedure xóa nhiều bản ghi
        /// </summary>
        public static string GET_DELETE_MULTIPLE = "Proc_{0}_DeleteMultipleBy{1}ID";

        /// <summary>
        /// Format tên của procedure lấy giá trị mã bản ghi lớn nhất
        /// </summary>
        public static string GET_MAX = "Proc_{0}_GetMax{1}";

        /// <summary>
        /// Format tên của procedure Tìm kiếm và phân trang
        /// </summary>
        public static string GET_BY_FILTER_PAGING = "Proc_{0}_Get{1}sByFilterAndPaging";

    }
}

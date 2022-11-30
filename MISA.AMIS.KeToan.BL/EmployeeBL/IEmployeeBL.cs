using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace MISA.AMIS.KeToan.BL
{
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID nhân viên muốn xóa</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// Created by: LHD(19/11/2022)
        public int DeleteMultipleEmployees(ListEmployee listEmployeeID);

        /// <summary>
        /// Lấy danh sách theo sắp xếp, phân trang và tìm kiếm
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="sortColumn">Trường cần sắp xếp</param>
        /// <param name="sortOrder">Tăng dần(ASC) giảm dần(DESC)</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <returns>Danh sách  nhân viên</returns>
        /// Created by: LHD(19/11/2022)
        public PagingResult<Employee> GetByPagingAndFilter(string? keyword, string sortColumn, string sortOrder, int pageSize, int pageNumber);

        /// <summary>
        /// Lấy giá trị mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by: LHD(19/11/2022)
        public IEnumerable<Employee> GetMaxEmployee();



    }
}

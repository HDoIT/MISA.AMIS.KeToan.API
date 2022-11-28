using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace MISA.AMIS.KeToan.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Lấy danh sách Phân trang và tìm kiếm 
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="sortColumn">Trường cần sắp xếp</param>
        /// <param name="sortOrder">Tăng dần(DESC), Giảm dần(ASC)</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <returns>Danh sách nhân viên phân trang và tìm kiếm</returns>
        /// Created by: LHDO(19/11/2022)
        public PagingResult<Employee> GetByPagingAndFilter(string? keyword,string sortColumn,string sortOrder,int pageSize,int pageNumber);

        /// <summary>
        /// Xóa nhiều nhân viên theo danh sach ID
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID nhân viên muốn xóa</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// Created by: LHDO(19/11/2022)
        public int DeleteMultipleEmployees(string listEmployeeID);

        /// <summary>
        /// Lấy max mã nhân viên
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Employee> GetMaxEmployee();

        /// <summary>
        /// Kiểm tra trùng mã trước khi thêm mới
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// <returns>Bool</returns>
        /// Created by: LHDO(19/11/2022)
        bool CheckDuplicateCode(string employeeCode);

        /// <summary>
        /// Kiểm tra trung mã trước khi cập nhập bản ghi
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="employeeCode"></param>
        /// <returns></returns>
        bool CheckDuplicateCodeUpdate(string employeeID,string employeeCode);
    }
}

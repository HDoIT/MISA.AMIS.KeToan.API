using MISA.AMIS.KeToan.Common;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Enums;
using MISA.AMIS.KeToan.Common.Exceptions;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.BL.EmployeeBL
{
    public class EmployeeBL : BaseBL<Employee>,IEmployeeBL
    {

        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        /// <summary>
        /// Xóa nhiều nhân viên
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID nhân viên muốn xóa</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// Created by: LHD(19/11/2022)
        public int DeleteMultipleEmployees(ListEmployee listEmployeeID)
        {
            string employeeIDs = "";

            foreach (var emId in listEmployeeID.EmployeeIDs.Select((employee, index) => new { Value = employee, Index = index }))
            {
                Console.WriteLine( emId.Index);
                if(emId.Index != listEmployeeID.EmployeeIDs.Count-1)
                {
                    employeeIDs += $"{emId.Value}" + ",";
                }
                else
                {
                    employeeIDs += $"{emId.Value}";
                }
                
            }

            return _employeeDL.DeleteMultipleEmployees(employeeIDs);
        }

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
        public PagingResult<Employee> GetByPagingAndFilter(string? keyword, string sortColumn, string sortOrder, int pageSize, int pageNumber)
        {
            return _employeeDL.GetByPagingAndFilter(keyword, sortColumn, sortOrder, pageSize, pageNumber);
        }
        #endregion

        /// <summary>
        /// Kiểm tra trùng mã nhân viên
        /// </summary>
        /// <param name="employee">Nhân viên cần kiểm tra</param>
        /// <exception cref="MISAValidateException">Mã nhân viên đã tồn tại</exception>
        /// Created by: LHD(19/11/2022)
        protected override void ValidateDuplicateCode(Employee employee)
        {
            var isDuplicate = _employeeDL.CheckDuplicateCode(employee.EmployeeCode);
            if(isDuplicate == true)
            {
                throw new MISAValidateException(String.Format(Resources.Duplicate_Code, employee.EmployeeCode), ErrorCode.DuplicateCode);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordCode"></param>
        /// <param name="employee"></param>
        /// <exception cref="MISAValidateException"></exception>
        protected override void ValidateDuplicateCodeUpdate(Guid recordID,Employee employee)
        {

            var isDuplicate = _employeeDL.CheckDuplicateCodeUpdate(recordID.ToString(),employee.EmployeeCode);
            if (isDuplicate == true)
            {
                throw new MISAValidateException(String.Format(Resources.Duplicate_Code, employee.EmployeeCode), ErrorCode.DuplicateCode);
            }
        }

        protected override void ValidateDateTime(Employee employee)
        {
            if(employee.DateOfBirth > DateTime.Now)
            {
                throw new MISAValidateException(Resources.DateOfBirth_NotGTDateNow, ErrorCode.DateOfBirthCode);
            }
        }

        public IEnumerable<Employee> GetMaxEmployee()
        {
            return _employeeDL.GetMaxEmployee();
        }
    }
}

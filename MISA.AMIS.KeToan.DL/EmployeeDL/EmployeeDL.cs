using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.DL.BaseDL;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace MISA.AMIS.KeToan.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        /// <summary>
        /// Xóa nhiều nhân viên theo danh sach ID
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID nhân viên muốn xóa</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// Created by: LHDO(19/11/2022)
        public int DeleteMultipleEmployees(string listEmployeeID)
        {
            //Chuẩn bị câu lệnh SQL
            string storedProcedureName = String.Format(Procedure.GET_DELETE_MULTIPLE,typeof(Employee).Name, typeof(Employee).Name);

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeIDs", listEmployeeID);
            //Thực hiện gọi vào DB

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi vào DB
                int deleteMultipleRowDB = 0;
                mySqlConnection.Open();
                using (var mtransaction = mySqlConnection.BeginTransaction())
                {
                    deleteMultipleRowDB = mySqlConnection.Execute(storedProcedureName, parameters, transaction: mtransaction, commandType: System.Data.CommandType.StoredProcedure);
                    mtransaction.Commit();

                }
                return deleteMultipleRowDB;



            }
        }

        /// <summary>
        /// Lấy danh sách Phân trang và tìm kiếm 
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="sortColumn">Trường cần sắp xếp</param>
        /// <param name="sortOrder">Tăng dần(ASC), Giảm dần(DESC)</param>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageNumber">Số trang</param>
        /// <returns>Danh sách nhân viên phân trang và tìm kiếm</returns>
        /// Created by: LHDO(19/11/2022)
        public PagingResult<Employee> GetByPagingAndFilter(string? keyword, string sortColumn, string sortOrder, int pageSize, int pageNumber)
        {

            int offset = pageSize * (pageNumber- 1);
            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = String.Format(Procedure.GET_BY_FILTER_PAGING, typeof(Employee).Name, typeof(Employee).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@keyword", keyword);
            parameters.Add("@sortColumn", sortColumn);
            parameters.Add("@sortOrder", sortOrder);
            parameters.Add("@limit", pageSize);
            parameters.Add("@offset", offset);


            var records = new List<Employee>();
            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {

                var resultReturn = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
                records = resultReturn.Read<Employee>().ToList();
                var countList = resultReturn.Read<dynamic>().ToList();
                long totalRecords = countList?.FirstOrDefault()?.TotalRecord;

                decimal totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

                var dataResult = new PagingResult<Employee> { Data = records, totalCount = totalRecords, totalPages  = totalPages };

                return dataResult;
            }


        }

        /// <summary>
        /// Kiểm tra trùng mã
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên cần kiểm tra</param>
        /// <returns>Bool</returns>
        /// Created by: LHDO(19/11/2022)
        public bool CheckDuplicateCode(string employeeID, string employeeCode)
        {
            var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString);
            var sqlQueryDuplicateCode = $"SELECT EmployeeCode FROM employee WHERE EmployeeCode = @EmployeeCode AND EmployeeID != @EmployeeID";
            var employeeCodeDuplicate = mySqlConnection.QueryFirstOrDefault<string>(sqlQueryDuplicateCode, param: new { EmployeeCode = employeeCode, EmployeeID = employeeID });
            if (employeeCodeDuplicate != null)
            {
                return true;    
            }
            return false;
        }

        /// <summary>
        /// Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>mã nhân viên lớn nhất</returns>
        /// Created by: LHDO(19/11/2022)
        public IEnumerable<Employee> GetMaxEmployee()
        {
            string storedProcedureName = String.Format(Procedure.GET_MAX, typeof(Employee).Name, typeof(Employee).Name);

            // Chuẩn bị tham số đầu vào

            var records = new List<Employee>();
            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                records = (List<Employee>)mySqlConnection.Query<Employee>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
            }

            return records;
        }
    }
}

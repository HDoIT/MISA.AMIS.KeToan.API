using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlConnector;
using Dapper;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Enums;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [ApiController]
    public class EmployeesController : BasesController<Employee> // extends, implements
    {

        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor

        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        [HttpGet("MaxEmployeeCode")]
        public IActionResult GetMaxEmployee()
        {

            try
            {
                var records = _employeeBL.GetMaxEmployee();
                // Thành công: Trả về dữ liệu cho FE
                if (records != null)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                // Thất bại: Trả về lỗi
                return StatusCode(StatusCodes.Status200OK, new List<Employee>());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }

            // Try catch exception
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword">Từ khóa muốn lấy</param>
        /// <param name="departmentID"></param>
        /// <param name="positionID"></param>
        /// <param name="limit">Số bản ghi muốn lấy</param>
        /// <param name="offset">Vị trí của bản ghi bắt đầu lấy</param>
        /// <returns></returns>
        [HttpGet("filter")]
        public IActionResult GetEmployeesByFilterAndPaging(
            [FromQuery] string? keyword,
            [FromQuery] string sortColumn,
            [FromQuery] string sortOrder,
            [FromQuery] int limit,
            [FromQuery] int pageNumber
            )
        {
            try
            {
                // Khởi tạo kết nối DB với mysql
                var filterAndPaging = _employeeBL.GetByPagingAndFilter(keyword, sortColumn, sortOrder, limit, pageNumber);

                // Xử lý kết quả trả về

                // Thành công: Trả về dữ liệu cho FE
                if (filterAndPaging != null)
                {
                    return StatusCode(StatusCodes.Status200OK, filterAndPaging);
                }
                // Thất bại: Trả về lỗi
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            
        }

        /// <summary>
        /// API xóa nhiều nhân viên theo danh sách ID
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID nhân viên muốn xóa</param>
        /// <returns>status code 200</returns>
        /// CreateBy: LHDO (01/11/2022)
        [HttpPost("deletebatch")]
        public IActionResult DeleteMultipleEmployees([FromBody] ListEmployee listEmployeeID)
        {
            try
            {
                //Khởi tạo kết nối DB
                //Chuẩn bị câu lệnh SQL
                int numberOfListEmployeeID = _employeeBL.DeleteMultipleEmployees(listEmployeeID);

                if (numberOfListEmployeeID > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, numberOfListEmployeeID);
                }
                // Thất bại: Trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 9,
                    DevMsg = "Database delete failed",
                    UserMsg = "Xóa nhiều nhân viên thất bại",
                    MoreInfo = "http://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }
    }
}

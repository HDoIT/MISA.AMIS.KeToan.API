using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Enums;
using MISA.AMIS.KeToan.Common;
using MySqlConnector;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Exceptions;

namespace MISA.AMIS.KeToan.API.Controllers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {

        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion
        /// <summary>
        /// Lấy danh sách tất cả các bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// CreateBy: LHDO (01/11/2022)
        [HttpGet]
        public IActionResult GetALLRecords()
        {

            try
            {
                var records = _baseBL.GetALLRecords();
                // Thành công: Trả về dữ liệu cho FE
                if (records != null)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }
                // Thất bại: Trả về lỗi
                return StatusCode(StatusCodes.Status200OK, new List<T>());

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
        /// API lấy một bản ghi theo ID
        /// </summary>
        /// <returns>Lấy một nhân viên theo ID</returns>
        /// CreatedBy: LHDO(01/11/2022)
        [HttpGet("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {

            try
            {
                var record = _baseBL.GetRecordByID(recordID);
                // Xử lý kết quả trả về

                // Thành công: Trả về dữ liệu cho FE
                if (record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
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

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="keyword">Từ khóa muốn lấy</param>
        ///// <param name="departmentID"></param>
        ///// <param name="positionID"></param>
        ///// <param name="limit">Số bản ghi muốn lấy</param>
        ///// <param name="offset">Vị trí của bản ghi bắt đầu lấy</param>
        ///// <returns></returns>
        //[HttpGet("filter")]
        //public IActionResult GetEmployeesByFilterAndPaging(
        //    [FromQuery] string keyword,
        //    [FromQuery] string sortColumn,
        //    [FromQuery] string sortOrder,
        //    [FromQuery] Guid? departmentID,
        //    //[FromQuery] Guid? jobPositionID, 
        //    [FromQuery] int limit,
        //    [FromQuery] int offset
        //    )
        //{
        //    try
        //    {
        //        // Khởi tạo kết nối DB với mysql
        //        string connectionString = "Server=localhost;Port=3306;Database=misa.web09.ctm.lhdo.ketoan;Uid=root;Pwd=Anhdo2000@;";
        //        var mySqlConnection = new MySqlConnection(connectionString);

        //        // Chuẩn bị câu lệnh SQL
        //        string storedProcedureName = "Proc_employee_GetEmployeesByFilterAndPaging";

        //        // Chuẩn bị tham số đầu vào
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@keyword", keyword);
        //        parameters.Add("@sortColumn", sortColumn);
        //        parameters.Add("@sortOrder", sortOrder);
        //        parameters.Add("@departmentID", departmentID);
        //        //parameters.Add("@jobPositionID", jobPositionID);
        //        parameters.Add("@limit", limit);
        //        parameters.Add("@offset", offset);

        //        // Thực hiện gọi vào DB
        //        var filterAndPaging = mySqlConnection.Query(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

        //        // Xử lý kết quả trả về

        //        // Thành công: Trả về dữ liệu cho FE
        //        if (filterAndPaging != null)
        //        {
        //            return StatusCode(StatusCodes.Status200OK, filterAndPaging);
        //        }
        //        // Thất bại: Trả về lỗi
        //        return StatusCode(StatusCodes.Status404NotFound);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return StatusCode(StatusCodes.Status500InternalServerError, new
        //        {
        //            ErrorCode = 3,
        //            DevMsg = "Catched an exception: " + ex.Message,
        //            UserMsg = "Có lỗi xảy ra vui lòng liên hệ MISA",
        //            MoreInfo = "http://openapi.misa.com.vn/errorcode/1",
        //            TraceId = HttpContext.TraceIdentifier
        //        });
        //    }
        //    #region comment

        //    //return StatusCode(StatusCodes.Status200OK,
        //    //    new PagingResult
        //    //    {
        //    //        Data = new List<Employee>
        //    //        {
        //    //            new Employee
        //    //            {
        //    //                EmployeeID = Guid.NewGuid(),
        //    //                EmployeeCode = "NV001",
        //    //                EmployeeName = "Lê Hữu Độ",
        //    //                DateOfBirth = DateTime.Now,
        //    //                Gender = Enums.Gender.Female,
        //    //                IdentityNumber = "123213213213",
        //    //                IdentityIssueDate = DateTime.Now,
        //    //                IdentityIssuePlace= "Lê Hữu Độ",
        //    //                DepartmentID= "12321312",
        //    //                JobPositionName= "Lê Hữu Độ",
        //    //                Address= "Lê Hữu Độ",
        //    //                PhoneNumber= "Lê Hữu Độ",
        //    //                TelephoneNumber= "Lê Hữu Độ",
        //    //                Email= "Lê Hữu Độ",
        //    //                BankAccountNumber= "Lê Hữu Độ",
        //    //                BankName= "Lê Hữu Độ",
        //    //                BankBranchName= "Lê Hữu Độ",
        //    //                CreatedDate = DateTime.Now,
        //    //                CreatedBy = "Lê Hữu Độ",
        //    //                ModifiedDate = DateTime.Now,
        //    //                ModifiedBy = "Lê Hữu Độ",
        //    //            },
        //    //            new Employee
        //    //            {
        //    //                EmployeeID = Guid.NewGuid(),
        //    //                EmployeeCode = "NV001",
        //    //                EmployeeName = "Lê Hữu Độ",
        //    //                DateOfBirth = DateTime.Now,
        //    //                Gender = Enums.Gender.Female,
        //    //                IdentityNumber = "123213213213",
        //    //                IdentityIssueDate = DateTime.Now,
        //    //                IdentityIssuePlace= "Lê Hữu Độ",
        //    //                DepartmentID= "12321312",
        //    //                JobPositionName= "Lê Hữu Độ",
        //    //                Address= "Lê Hữu Độ",
        //    //                PhoneNumber= "Lê Hữu Độ",
        //    //                TelephoneNumber= "Lê Hữu Độ",
        //    //                Email= "Lê Hữu Độ",
        //    //                BankAccountNumber= "Lê Hữu Độ",
        //    //                BankName= "Lê Hữu Độ",
        //    //                BankBranchName= "Lê Hữu Độ",
        //    //                CreatedDate = DateTime.Now,
        //    //                CreatedBy = "Lê Hữu Độ",
        //    //                ModifiedDate = DateTime.Now,
        //    //                ModifiedBy = "Lê Hữu Độ",
        //    //            },
        //    //            new Employee
        //    //            {
        //    //                EmployeeID = Guid.NewGuid(),
        //    //                EmployeeCode = "NV001",
        //    //                EmployeeName = "Lê Hữu Độ",
        //    //                DateOfBirth = DateTime.Now,
        //    //                Gender = Enums.Gender.Female,
        //    //                IdentityNumber = "123213213213",
        //    //                IdentityIssueDate = DateTime.Now,
        //    //                IdentityIssuePlace= "Lê Hữu Độ",
        //    //                DepartmentID= "12321312",
        //    //                JobPositionName= "Lê Hữu Độ",
        //    //                Address= "Lê Hữu Độ",
        //    //                PhoneNumber= "Lê Hữu Độ",
        //    //                TelephoneNumber= "Lê Hữu Độ",
        //    //                Email= "Lê Hữu Độ",
        //    //                BankAccountNumber= "Lê Hữu Độ",
        //    //                BankName= "Lê Hữu Độ",
        //    //                BankBranchName= "Lê Hữu Độ",
        //    //                CreatedDate = DateTime.Now,
        //    //                CreatedBy = "Lê Hữu Độ",
        //    //                ModifiedDate = DateTime.Now,
        //    //                ModifiedBy = "Lê Hữu Độ",
        //    //            }
        //    //        },
        //    //        totalCount = 3
        //    //    }

        //    //); 
        //    #endregion
        //}

        /// <summary>
        /// API thêm mới 1 nhân viên
        /// </summary>
        /// <param name="record">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>ID của nhân viên vừa thêm mới</returns>
        /// Created by: LHDO(01/11/2022)
        [HttpPost]
        public IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                int numberOfRecord = _baseBL.InsertRecord(record);
                // Xử lý kết quả trả về
                Console.WriteLine(record);
                // Thành công: Trả về dữ liệu cho FE
                if (numberOfRecord > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, numberOfRecord);
                }
                // Thất bại: Trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 4,
                    DevMsg = "Database insert failed",
                    UserMsg = "Thêm mới nhân viên thất bại, Vui lòng liên hệ MISA!",
                    MoreInfo = "http://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (MISAValidateException ex)
            {
                var respone = new
                {
                    ErrorCode = ex.ErrorCodeValidate,
                    DevMsg = ex.Message,
                    UserMsg = ex.Message,
                    MoreInfo = "",
                    TraceId = HttpContext.TraceIdentifier

                };
                return BadRequest(respone);

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
            // statuscode là 1 hàm có 2 tham số là statuscode và một hàm trả về thông tin thêm mới thành công
        }

        /// <summary>
        /// Api sửa thông tin nhân viên
        /// </summary>
        /// <param name="recordID">ID của nhân viên muốn sửa</param>
        /// <param name="record">Đối tượng nhân viên muốn sửa</param>
        /// <returns>ID của nhân viên vừa sửa</returns>
        /// CreatedBY: LHD (01/11/2022)
        [HttpPut("{recordID}")]
        public IActionResult UpdateRecord([FromRoute] Guid recordID, [FromBody] T record)
        {

            try
            {
                int numberOfRecord = _baseBL.UpdateRecord(recordID,record);
                // Xử lý kết quả trả về
                // Thành công: Trả về dữ liệu cho FE
                if (numberOfRecord > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, numberOfRecord);
                }

                // Thất bại: Trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 6,
                    DevMsg = "Database update failed",
                    UserMsg = "Cập nhập nhân viên thất bại, Vui lòng liên hệ MISA!",
                    MoreInfo = "http://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (MISAValidateException ex)
            {
                var respone = new
                {
                    ErrorCode = ex.ErrorCodeValidate,
                    DevMsg = ex.Message,
                    UserMsg = ex.Message,
                    MoreInfo = "",
                    TraceId = HttpContext.TraceIdentifier

                };
                return BadRequest(respone);

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
        /// API Xóa thông tin bản ghi
        /// </summary>
        /// <param name="recordID">ID của bản ghi cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng hoặc lỗi nếu gặp exception</returns>
        /// CreatedBy: LHDO (01/11/2022)
        [HttpDelete("{recordID}")]
        public IActionResult DeleteRecord([FromRoute] Guid recordID)
        {
            try
            {
                int numberOfRecord = _baseBL.DeleteEmployee(recordID);

                if (numberOfRecord > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, numberOfRecord);
                }
                // Thất bại: Trả về lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 8,
                    DevMsg = "Database delete failed",
                    UserMsg = "Xóa nhân viên thất bại, Vui lòng liên hệ MISA!",
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

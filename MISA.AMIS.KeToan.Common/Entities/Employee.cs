using LinqToDB.Mapping;
using MISA.AMIS.KeToan.Common.Enums;
using MISA.AMIS.KeToan.Common.MISAAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MISA.AMIS.KeToan.Common.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employee
    {

        // ctrl + k + s
        #region Properties
        /// <summary>
        /// ID nhân viên
        /// </summary>
        [Key]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [NotEmpty]
        [PropertyName("Mã nhân viên")]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [NotEmpty]
        [PropertyName("Tên nhân viên")]
        public string? EmployeeName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [JsonConverter(typeof(NullableDateTimeConverter))]
        [ValidateDateTime]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp Số chứng minh nhân dân
        /// </summary>
        [JsonConverter(typeof(NullableDateTimeConverter))]
        public DateTime? IdentityIssueDate { get; set; }

        /// <summary>
        /// Nơi cấp chứng minh nhân dân
        /// </summary>
        public string? IdentityIssuePlace { get; set; }

        /// <summary>
        /// ID của Đơn vị
        /// </summary>
        [NotEmpty]
        [PropertyName("Tên đơn vị")]
        public Guid? DepartmentID { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string? JobPositionName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string? TelephoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedDate { get { return DateTime.Now; } }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Thời gian chỉnh sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// Người chỉnh sửa gần nhất
        /// </summary> 
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public string? GenderName{
            get{
                switch (Gender)
                {
                    case Gender.Male:
                        return Resources.Gender_Male;
                    case Gender.Female:
                        return Resources.Gender_Female;
                    case Gender.Other: 
                        return Resources.Gender_Other;
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Giá trị mã nhân viên lớn nhất
        /// </summary>
        public string? MaxEmployeeCode { get; set; }


        #endregion
    }
}

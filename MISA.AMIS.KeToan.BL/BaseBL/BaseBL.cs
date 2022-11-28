using MISA.AMIS.KeToan.BL.EmployeeBL;
using MISA.AMIS.KeToan.Common;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Enums;
using MISA.AMIS.KeToan.Common.Exceptions;
using MISA.AMIS.KeToan.Common.MISAAttribute;
using MISA.AMIS.KeToan.DL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace MISA.AMIS.KeToan.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;
        protected List<string> ValidateErrorsMsg;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
            ValidateErrorsMsg = new List<string>();
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi muốn xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created By: LHD(10/11/2022)
        public int DeleteEmployee(Guid recordID)
        {
            return _baseDL.DeleteEmployee(recordID);
        }

        #endregion
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// Created By: LHD(10/11/2022) 
        public IEnumerable<T> GetALLRecords()
        {
            return _baseDL.GetALLRecords();
        }

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghu muốn lấy</param>
        /// <returns>Danh sách tất cả nhân viên</returns>
        /// Created By: LHD(10/11/2022) 
        public T GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Đối tượng bản ghi cần thêm mới</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: LHDO(01/11/2022)
        public int InsertRecord(T record)
        {
            //Validate chung
            ValidateData(record);
            //Validate riêng
            //Validate Check mã trùng
            ValidateDuplicateCode(record);

            //Validate Check ngày sinh lớn hơn ngày hiện tại
            ValidateDateTime(record);
            return _baseDL.InsertRecord(record);

        }

        /// <summary>
        /// Cập nhập bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi cần update</param>
        /// <param name="record"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created By: LHD(10/11/2022)
        public int UpdateRecord(Guid recordID, T record)
        {
            //Validate chung
            ValidateData(record);

            //Validate riêng
            //Validate Check mã trùng
            ValidateDuplicateCodeUpdate(recordID,record);

            //Validate Check ngày sinh lớn hơn ngày hiện tại
            ValidateDateTime(record);

            return _baseDL.UpdateRecord(recordID, record);
        }

        /// <summary>
        /// Validate Thông tin không được để trống
        /// </summary>
        /// <param name="record"></param>
        /// <exception cref="MISAValidateException"></exception>
        /// Created By: LHD(10/11/2022)
        public void ValidateData(T record)
        {
            var isValid = true;
            var props = record.GetType().GetProperties();
            //lấy ra các property có attribute là NotEmpty
            var propNotEmpty = props.Where(p=> Attribute.IsDefined(p,typeof(NotEmpty)));

            foreach (var prop in propNotEmpty) {
                //Kiểm tra các property được đánh dấu là không được phép để trống
                var propValue = prop.GetValue(record);
                var propName = prop.Name;
                var nameDisplay = string.Empty;
                var propertyNames = prop.GetCustomAttributes(typeof(PropertyName), true);
                if(propertyNames.Length > 0)
                {
                    //Lấy ra tên attribute
                    nameDisplay= (propertyNames[0] as PropertyName).Name;
                }
                if(propValue ==null || String.IsNullOrEmpty(propValue.ToString()))
                {
                    nameDisplay = (nameDisplay == string.Empty ? propName : nameDisplay);
                    throw new MISAValidateException(String.Format(Resources.InforNotEmpty,nameDisplay),ErrorCode.InvalidCode);
                }
            }

            //return isValid;
        }

        /// <summary>
        /// Validate mã trùng trước khi thêm mới
        /// </summary>
        /// <param name="record"></param>
        /// Created By: LHD(10/11/2022)
        protected virtual void ValidateDuplicateCode(T record)
        {

        }

        /// <summary>
        /// Validate mã trùng trước khi cập nhập
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// Created By: LHD(10/11/2022)
        protected virtual void ValidateDuplicateCodeUpdate(Guid recordID,T record)
        {

        }

        /// <summary>
        /// Validate ngày sinh không lớn hơn ngày hiện tại
        /// </summary>
        /// <param name="record"></param>
        /// Created By: LHD(10/11/2022)
        protected virtual void ValidateDateTime(T record) { }
    }
}

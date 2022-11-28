using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created By: LHD(10/11/2022)
        public IEnumerable<T> GetALLRecords();

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Thông tin của bản ghi có ID muốn lấy</returns>
        /// Created By: LHD(10/11/2022)
        public T GetRecordByID(Guid recordID);

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Đối tượng bản ghi cần thêm mới</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: LHDO(01/11/2022)
        public int InsertRecord(T record);

        /// <summary>
        /// Cập nhập bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi cần update</param>
        /// <param name="record"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created By: LHD(10/11/2022)
        public int UpdateRecord(Guid recordID, T record);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi muốn xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created By: LHD(10/11/2022)
        public int DeleteEmployee(Guid recordID);

    }
}

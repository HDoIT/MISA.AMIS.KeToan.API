using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MISA.AMIS.KeToan.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL.BaseDL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created By: LHD(10/11/2022)
        public IEnumerable<T> GetALLRecords()
        {

            //Khởi tạo muộn nhất có thể
            //Giải phóng sớm nhất có thể

            //Khởi tạo kết nối đến DB mysql


            // Chuẩn bị câu lệnh SQL
            //string storedProcedureName = $"Proc_{typeof(T).Name}_GetAll";
            string storedProcedureName = String.Format(Procedure.GET_ALL, typeof(T).Name);

            // Chuẩn bị tham số đầu vào

            var records = new List<T>();
            // Thực hiện gọi vào DB
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                records = (List<T>)mySqlConnection.Query<T>(storedProcedureName, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
            }

            return records;
            // Thất bại: Trả về lỗi
        }

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID củ bản ghi muốn lấy</param>
        /// <returns>Thông tin của bản ghi có ID muốn lấy</returns>
        /// Created By: LHD(10/11/2022)
        public T GetRecordByID(Guid recordID)
        {

            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = String.Format(Procedure.GET_BY_ID, typeof(T).Name, typeof(T).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}ID", recordID);

            // Thực hiện gọi vào DB
            using(var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                var record = mySqlConnection.QueryFirstOrDefault<T>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return record;
            }

            // Xử lý kết quả trả về

        }

        /// <summary>
        /// Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="record">Đối tượng bản ghi cần thêm mới</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created by: LHDO(01/11/2022)
        public int InsertRecord(T record)
        {
            // Khởi tạo kết nối DB với mysql

            // Chuẩn bị câu lệnh SQL
            string storedProcedureName = String.Format(Procedure.GET_INSERT, typeof(T).Name, typeof(T).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            var newRecordId = Guid.NewGuid();
            foreach (var prop in record.GetType().GetProperties())
            {
                if (prop.Name == $"{typeof(T).Name}ID")
                {
                    parameters.Add($"@{typeof(T).Name}ID", newRecordId);
                }
                else
                {
                    parameters.Add("@" + prop.Name, prop.GetValue(record, null));
                }
            }

            int numberOfRowAffected = 0;
            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi vào DB
                numberOfRowAffected = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về

                // Thành công: Trả về dữ liệu cho FE
                // Thất bại: Trả về lỗi
            }
            return numberOfRowAffected;


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
            //Chuẩn bị câu lệnh SQL
            string storedProcedureName = string.Format(Procedure.GET_UPDATE, typeof(T).Name, typeof(T).Name);

            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            foreach (var prop in record.GetType().GetProperties())
            {
                if (prop.Name == $"{typeof(T).Name}ID")
                {
                    parameters.Add($"@{typeof(T).Name}ID", recordID);
                }
                else
                {
                    parameters.Add("@" + prop.Name, prop.GetValue(record, null));
                }
            }

            // Thực hiện gọi vào DB
            int updateOfRowAffected = 0;

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi vào DB
                updateOfRowAffected = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }
            return updateOfRowAffected;
            // Xử lý kết quả trả về
            // Thành công: Trả về dữ liệu cho FE
        }

        // <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="recordID">ID bản ghi muốn xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Created By: LHD(10/11/2022)
        public int DeleteEmployee(Guid recordID)
        {
            //Khởi tạo kết nối DB

            //Chuẩn bị câu lệnh SQL
            string storedProcedureName = String.Format(Procedure.GET_DELETE, typeof(T).Name, typeof(T).Name);

            Console.WriteLine(storedProcedureName);
            //Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}ID", recordID);

            //Thực hiện gọi vào DB
            int deleteOfRowDB = 0;

            using (var mySqlConnection = new MySqlConnection(DatabaseContext.ConnectionString))
            {
                // Thực hiện gọi vào DB
                deleteOfRowDB = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }

            return deleteOfRowDB;
            // Thất bại: Trả về lỗi
        }

    }
}

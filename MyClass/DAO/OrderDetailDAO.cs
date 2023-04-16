using MyClass.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyClass.DAO
{
    public class OrderDetailDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        //Trả về danh sách các mẫu tin
        public List<OrderDetail> getList(int? id)
        {
            return db.OrderDetails.Where(m => m.OrderId == id).ToList();
        }
        public List<OrderDetail> getList()
        {
            List<OrderDetail> list = db.OrderDetails.ToList();
            return list;
        }

        //Trả vê 1 mẫu tin
        public OrderDetail getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.OrderDetails.Find(id);
            }
        }
        public OrderDetail getRow(int orderID)
        {

            return db.OrderDetails.Where(m => m.OrderId == orderID).FirstOrDefault();
        }
        //Thêm mẫu tin
        public int Insert(OrderDetail row)
        {
            db.OrderDetails.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(OrderDetail row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(OrderDetail row)
        {
            db.OrderDetails.Remove(row);
            return db.SaveChanges();
        }
    }
}

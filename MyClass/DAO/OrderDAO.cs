using MyClass.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyClass.DAO
{
    public class OrderDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        public List<Order> getList(int userid)
        {
            List<Order> list = db.Orders.Where(m => m.UserId == userid && m.Status != 0).ToList();
            return list;
        }
        public List<Order> getListOrderDetail(int orderId)
        {
            List<Order> list = db.Orders.Where(m => m.Id == orderId && m.Status != 0).ToList();
            return list;
        }
        public List<Order> getListOrderUserDetail(int userId)
        {
            List<Order> list = db.Orders.Where(m => m.UserId == userId && m.Status != 0).OrderByDescending(m => m.CreateDate).ToList();
            return list;
        }

        //Trả về danh sách các mẫu tin
        public List<Order> getList(string status = "All")
        {
            List<Order> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Orders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.Orders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders.ToList();
                        break;
                    }
            }
            return list;

        }
        //public List<OrderInfor> getListTime()
        //{
        //}
        //Trả về danh sách các mẫu tin
        public List<OrderInfor> getListJoin(string status = "All")
        {
            List<OrderInfor> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Orders
                            .Join(
                                db.OrderDetails,
                                o => o.Id,
                                od => od.OrderId,
                                (o, od) => new OrderInfor
                                {
                                    Id = o.Id,
                                    UserId = o.UserId,
                                    DeliveryAddress = o.DeliveryAddress,
                                    DeliveryPhone = o.DeliveryPhone,
                                    DeliveryEmail = o.DeliveryEmail,
                                    DeliveryName = o.DeliveryName,
                                    DeliveryNote = o.DeliveryNote,
                                    UpdatedAt = o.UpdatedAt,
                                    UpdatedBy = o.UpdatedBy,
                                    Status = o.Status,
                                    CreateDate = o.CreateDate,
                                    Code = o.Code,
                                    OrderId = od.OrderId,
                                    ProductId = od.ProductId,
                                    Price = od.Price,
                                    Quantity = od.Quantity,
                                    Amount = od.Amount,
                                }
                            )
                            .Where(m => m.Status != 0).OrderByDescending(m => m.Amount).Take(5).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.Orders
                            .Join(
                                db.OrderDetails,
                                o => o.Id,
                                od => od.OrderId,
                                (o, od) => new OrderInfor
                                {
                                    Id = o.Id,
                                    UserId = o.UserId,
                                    DeliveryAddress = o.DeliveryAddress,
                                    DeliveryPhone = o.DeliveryPhone,
                                    DeliveryEmail = o.DeliveryEmail,
                                    DeliveryName = o.DeliveryName,
                                    DeliveryNote = o.DeliveryNote,
                                    UpdatedAt = o.UpdatedAt,
                                    UpdatedBy = o.UpdatedBy,
                                    Status = o.Status,
                                    OrderId = od.OrderId,
                                    ProductId = od.ProductId,
                                    Price = od.Price,
                                    Quantity = od.Quantity,
                                    Amount = od.Amount,
                                }
                            )
                            .Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders
                            .Join(
                                db.OrderDetails,
                                o => o.Id,
                                od => od.OrderId,
                                (o, od) => new OrderInfor
                                {
                                    Id = o.Id,
                                    UserId = o.UserId,
                                    DeliveryAddress = o.DeliveryAddress,
                                    DeliveryPhone = o.DeliveryPhone,
                                    DeliveryEmail = o.DeliveryEmail,
                                    DeliveryName = o.DeliveryName,
                                    DeliveryNote = o.DeliveryNote,
                                    UpdatedAt = o.UpdatedAt,
                                    UpdatedBy = o.UpdatedBy,
                                    Status = o.Status,
                                    OrderId = od.OrderId,
                                    ProductId = od.ProductId,
                                    Price = od.Price,
                                    Quantity = od.Quantity,
                                    Amount = od.Amount,
                                }
                            )
                            .ToList();
                        break;
                    }
            }
            return list;

        }
        //Trả vê 1 mẫu tin
        public Order getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }
        public Order getRow(string name = null)
        {
            if (name == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Where(m => m.DeliveryName == name).FirstOrDefault();
            }
        }
        //Thêm mẫu tin
        public int Insert(Order row)
        {
            db.Orders.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Order row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Order row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();
        }
    }
}

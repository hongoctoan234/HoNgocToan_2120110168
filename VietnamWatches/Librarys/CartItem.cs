using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ThietBiDienTu
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string NameProduct { get; set; }
        public string Images { get; set; }
        public decimal PriceBuy { get; set; }
        public int QtyBuy { get; set; }
        public decimal Amount { get; set; }
        public CartItem() { }
        public CartItem(int proId, string name, string img, decimal pricebuy, int qtyBuy) 
        {
            this.ProductId = proId;
            this.NameProduct = name;
            this.Images = img;
            this.PriceBuy = pricebuy;
            this.QtyBuy = qtyBuy;
            this.Amount = pricebuy * qtyBuy;
        }

    }
}
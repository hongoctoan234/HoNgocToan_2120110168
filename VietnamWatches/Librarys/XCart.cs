using System.Collections.Generic;
using System.Linq;

namespace ThietBiDienTu
{
    public class XCart
    {
        public List<CartItem> AddCart(CartItem cartItem, int productId)
        {
            List<CartItem> listCart;
            if (System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                //Giỏ hàng đang trống
                listCart = new List<CartItem>();
                listCart.Add(cartItem);
                System.Web.HttpContext.Current.Session["MyCart"] = listCart;
            }
            else
            {
                //Giỏ hàng không trống
                listCart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"]; //ép kiểu
                //Kiểm tra productId đã có trong giỏ hàng chưa?
                if (listCart.Where(m => m.ProductId == productId).Count() != 0)
                {
                    //ProductId đã có trong giỏ hàng
                    cartItem.QtyBuy++;
                    int vt = 0;
                    foreach (var item in listCart)
                    {
                        if (item.ProductId == productId)
                        {
                            listCart[vt].QtyBuy++;
                            listCart[vt].Amount = listCart[vt].PriceBuy * listCart[vt].QtyBuy;
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = listCart;
                }
                else
                {
                    //ProductId chưa có trong giỏ hàng
                    listCart.Add(cartItem);
                    System.Web.HttpContext.Current.Session["MyCart"] = listCart;
                }
            }
            return listCart;
        }
        public List<CartItem> AddCartDetail(CartItem cartItem, int productId, int qty)
        {
            List<CartItem> listCart;
            if (System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                //Giỏ hàng đang trống
                listCart = new List<CartItem>();
                listCart.Add(cartItem);
                System.Web.HttpContext.Current.Session["MyCart"] = listCart;
            }
            else
            {
                //Giỏ hàng không trống
                listCart = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"]; //ép kiểu
                //Kiểm tra productId đã có trong giỏ hàng chưa?
                if (listCart.Where(m => m.ProductId == productId).Count() != 0)
                {
                    //ProductId đã có trong giỏ hàng
                    cartItem.QtyBuy++;
                    int vt = 0;
                    foreach (var item in listCart)
                    {
                        if (item.ProductId == productId)
                        {
                            listCart[vt].QtyBuy++;
                            listCart[vt].Amount = listCart[vt].PriceBuy * listCart[vt].QtyBuy;
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = listCart;
                }
                else
                {
                    //ProductId chưa có trong giỏ hàng
                    listCart.Add(cartItem);
                    System.Web.HttpContext.Current.Session["MyCart"] = listCart;
                }
            }
            return listCart;
        }
        public void UpdateCart(string[] arrqty)
        {
            List<CartItem> listcart = this.getCart();
            int vt = 0;
            foreach (CartItem cartItem in listcart)
            {
                listcart[vt].QtyBuy = int.Parse(arrqty[vt]);
                listcart[vt].Amount = (listcart[vt].QtyBuy * listcart[vt].PriceBuy);
                vt++;
            }
            System.Web.HttpContext.Current.Session["MyCart"] = listcart;
        }

        public void UpdateCart(int arrQty, int arrVt)
        {
            List<CartItem> listCart = this.getCart();
            int vt = arrVt;
            //foreach (CartItem cartItem in listCart)
            //{
            //if (arrQty[vt].Equals("0"))
            //{
            //    this.DeleteCart(cartItem.ProductId);
            //}
            //else
            //{
            //    listCart[vt].QtyBuy = Int16.Parse(arrQty[vt]);
            //    listCart[vt].Amount = listCart[vt].PriceBuy * listCart[vt].QtyBuy;
            //}
            listCart[vt].QtyBuy = arrQty;
            listCart[vt].Amount = listCart[vt].PriceBuy * listCart[vt].QtyBuy;
            //vt++;
            //}
            System.Web.HttpContext.Current.Session["MyCart"] = listCart;
        }
        public void DeleteCart(int? productId = null)
        {
            if (!System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                List<CartItem> cartItems = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
                int vt = 0;
                foreach (var item in cartItems)
                {
                    if (item.ProductId == productId)
                    {
                        cartItems.RemoveAt(vt);
                        break;
                    }
                    vt++;
                }
                System.Web.HttpContext.Current.Session["MyCart"] = cartItems;
            }
        }
        public void DelAllCart(int? productId = null)
        {
            if (productId != null)
            {
                if (!System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
                {
                    List<CartItem> cartItems = (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
                    int vt = 0;
                    foreach (var item in cartItems)
                    {
                        if (item.ProductId == productId)
                        {
                            cartItems.RemoveAt(vt);
                            break;
                        }
                        vt++;
                    }
                    System.Web.HttpContext.Current.Session["MyCart"] = cartItems;
                }
            }
            else
            {
                System.Web.HttpContext.Current.Session["MyCart"] = "";
            }

        }
        public List<CartItem> getCart()
        {
            if (System.Web.HttpContext.Current.Session["MyCart"].Equals(""))
            {
                return null;
            }
            else
            {
                return (List<CartItem>)System.Web.HttpContext.Current.Session["MyCart"];
            }
        }
    }
}
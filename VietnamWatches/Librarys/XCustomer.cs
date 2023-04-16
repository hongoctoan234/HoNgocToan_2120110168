namespace ThietBiDienTu
{
    public class XCustomer
    {
        //public List<CusInfo> UpdateImgCustomer(CusInfo cusInfo, int cusId)
        //{
        //    List<CusInfo> listCustomer;
        //    if (System.Web.HttpContext.Current.Session["CustomerId"].Equals(""))
        //    {
        //        //Hình ảnh đang trống
        //        listCustomer = new List<CusInfo>();
        //        listCustomer.Add(cusInfo);
        //        System.Web.HttpContext.Current.Session["CustomerId"] = listCustomer;
        //    }
        //    else
        //    {
        //        //Giỏ hàng không trống
        //        listCustomer = (List<CusInfo>)System.Web.HttpContext.Current.Session["CustomerId"]; //ép kiểu
        //        //Kiểm tra productId đã có trong giỏ hàng chưa?
        //        //upload file img
        //        var fileImg = Request.Files["Img"];
        //        if (fileImg.ContentLength != 0)//Có chọn file
        //        {
        //            string[] FileExtensions = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".jepg" };
        //            if (FileExtensions.Contains(fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."))))
        //            {
        //                string imgName = slider.Link + fileImg.FileName.Substring(fileImg.FileName.LastIndexOf("."));
        //                //thực hiện lưu file
        //                slider.Img = imgName;
        //                string pathDir = "~/Public/images/slider/";
        //                string pathImg = Path.Combine(Server.MapPath(pathDir), imgName);
        //                //xoá file
        //                if (slider.Img != null)
        //                {
        //                    string delPath = Path.Combine(Server.MapPath(pathDir), imgName);
        //                    System.IO.File.Delete(delPath);
        //                }
        //                //thực hiện upload
        //                fileImg.SaveAs(pathImg);
        //            }
        //        }

        //        if (listCustomer.Where(m => m.CusId == cusId).Count() != 0)
        //        {
        //            //ProductId đã có trong giỏ hàng
        //            cartItem.QtyBuy++;
        //            int vt = 0;
        //            foreach (var item in listCart)
        //            {
        //                if (item.ProductId == productId)
        //                {
        //                    listCart[vt].QtyBuy++;
        //                    listCart[vt].Amount = listCart[vt].PriceBuy * listCart[vt].QtyBuy;
        //                }
        //                vt++;
        //            }
        //            System.Web.HttpContext.Current.Session["MyCart"] = listCart;
        //        }
        //        else
        //        {
        //            //ProductId chưa có trong giỏ hàng
        //            listCart.Add(cartItem);
        //            System.Web.HttpContext.Current.Session["MyCart"] = listCart;
        //        }
        //    }
        //    return listCustomer;
        //}
    }
}
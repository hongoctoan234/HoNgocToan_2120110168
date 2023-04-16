namespace ThietBiDienTu
{
    public class CusInfo
    {
        public int CusId { get; set; }
        public string FullName { get; set; }
        public string Images { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Gender { get; set; }
        public string Password { get; set; }
        public CusInfo() { }
        public CusInfo(int cusId, string fullName, string img, string address, string phone, int gender, string pass)
        {
            this.CusId = cusId;
            this.FullName = fullName;
            this.Images = img;
            this.Address = address;
            this.Phone = phone;
            this.Gender = gender;
            this.Password = pass;

        }
        public CusInfo(string img)
        {
            this.Images = img;

        }
        public CusInfo(int cusId, string fullName, string address, string phone, int gender, string pass)
        {
            this.CusId = cusId;
            this.FullName = fullName;
            this.Address = address;
            this.Phone = phone;
            this.Gender = gender;
            this.Password = pass;
        }

    }
}
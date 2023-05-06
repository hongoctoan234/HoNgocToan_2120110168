using MyClass.Models;
using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyClass.DAO
{
    public class PostDAO
    {
        private TBDTDBContext db = new TBDTDBContext();
        public List<Post> getListByType(string type)
        {
            return db.Posts.Where(m => m.Status == 1 && m.Type == type)
                 .OrderByDescending(m => m.CreatedAt)
                 .ToList();
        }
        public List<Post> getListPostByType(string type)
        {
            return db.Posts.Where(m => m.Status == 1 && m.Type == type)
                 .OrderByDescending(m => m.CreatedAt).Take(5)
                 .ToList();
        }
        public List<PostInfo> getListByTypePostInfo(string type)
        {
            List<PostInfo> list = db.Posts
                    .Join(
                        db.Topics,
                        p => p.TopId,
                        c => c.Id,
                        (p, c) => new PostInfo
                        {
                            Id = p.Id,
                            Title = p.Title,
                            TopId = p.TopId,
                            TopicName = c.Name,
                            Slug = p.Slug,
                            Images = p.Img,
                            Detail = p.Detail,
                            Type = p.Type,
                            MetaDesc = p.MetaDesc,
                            MetaKey = p.MetaKey,
                            CreatedBy = p.CreatedBy,
                            CreatedAt = p.CreatedAt,
                            UpdatedBy = p.UpdatedBy,
                            UpdatedAt = p.UpdatedAt,
                            Status = p.Status
                        }
                        ).Where(m => m.Status == 1 && m.Type == type)
                         .OrderByDescending(m => m.CreatedAt).Take(3).ToList();
            return list;
        }
        public List<PostInfo> getListByTopicId(int? topid, string type = "Post", int? notId = null)
        {
            List<PostInfo> list = null;
            if (notId == null)
            {
                list = db.Posts
              .Join(
                  db.Topics,
                  p => p.TopId,
                  c => c.Id,
                  (p, c) => new PostInfo
                  {
                      Id = p.Id,
                      Title = p.Title,
                      TopId = p.TopId,
                      TopicName = c.Name,
                      Slug = p.Slug,
                      Images = p.Img,
                      Detail = p.Detail,
                      Type = p.Type,
                      MetaDesc = p.MetaDesc,
                      MetaKey = p.MetaKey,
                      CreatedBy = p.CreatedBy,
                      CreatedAt = p.CreatedAt,
                      UpdatedBy = p.UpdatedBy,
                      UpdatedAt = p.UpdatedAt,
                      Status = p.Status
                  }
                  ).Where(m => m.Status == 1 && m.Type == type && m.TopId == topid).Take(3)
                   .OrderByDescending(m => m.CreatedAt).ToList();
            }
            else
            {
                list = db.Posts
             .Join(
                 db.Topics,
                 p => p.TopId,
                 c => c.Id,
                 (p, c) => new PostInfo
                 {
                     Id = p.Id,
                     Title = p.Title,
                     TopId = p.TopId,
                     TopicName = c.Name,
                     Slug = p.Slug,
                     Images = p.Img,
                     Detail = p.Detail,
                     Type = p.Type,
                     MetaDesc = p.MetaDesc,
                     MetaKey = p.MetaKey,
                     CreatedBy = p.CreatedBy,
                     CreatedAt = p.CreatedAt,
                     UpdatedBy = p.UpdatedBy,
                     UpdatedAt = p.UpdatedAt,
                     Status = p.Status
                 }
                 ).Where(m => m.Status == 1 && m.Type == type && m.TopId == topid && m.Id != notId).Take(3)
                  .OrderByDescending(m => m.CreatedAt).ToList();
            }
            return list;
        }
        public List<PostInfo> getList(string type = "Post")
        {
            List<PostInfo> list = db.Posts
                .Join(
                    db.Topics,
                    p => p.TopId,
                    c => c.Id,
                    (p, c) => new PostInfo
                    {
                        Id = p.Id,
                        Title = p.Title,
                        TopId = p.TopId,
                        TopicName = c.Name,
                        Slug = p.Slug,
                        Images = p.Img,
                        Detail = p.Detail,
                        Type = p.Type,
                        MetaDesc = p.MetaDesc,
                        MetaKey = p.MetaKey,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                    ).Where(m => m.Status == 1 && m.Type == type)
                     .OrderByDescending(m => m.CreatedAt).ToList();
            return list;
        }
        public List<PostInfo> getListTrash(string type)
        {
            List<PostInfo> list = db.Posts
                .Join(
                    db.Topics,
                    p => p.TopId,
                    c => c.Id,
                    (p, c) => new PostInfo
                    {
                        Id = p.Id,
                        Title = p.Title,
                        TopId = p.TopId,
                        TopicName = c.Name,
                        Slug = p.Slug,
                        Images = p.Img,
                        Detail = p.Detail,
                        Type = p.Type,
                        MetaDesc = p.MetaDesc,
                        MetaKey = p.MetaKey,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                    ).Where(m => m.Status == 0 && m.Type == type)
                     .OrderByDescending(m => m.CreatedAt).ToList();
            return list;
        }
        //Trả về danh sách các mẫu tin
        public IPagedList<PostInfo> getList(int pageSize, int pageNumber)
        {
            IPagedList<PostInfo> list = db.Posts
                .Join(
                    db.Topics,
                    p => p.TopId,
                    c => c.Id,
                    (p, c) => new PostInfo
                    {
                        Id = p.Id,
                        Title = p.Title,
                        TopId = p.TopId,
                        TopicName = c.Name,
                        Slug = p.Slug,
                        Images = p.Img,
                        Detail = p.Detail,
                        Type = p.Type,
                        MetaDesc = p.MetaDesc,
                        MetaKey = p.MetaKey,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                    ).Where(m => m.Status == 1 && m.Type == "Post")
                     .OrderByDescending(m => m.CreatedAt)
                    .ToPagedList(pageNumber, pageSize);
            return list;
        }
        //Trả về danh sách các mẫu tin
        public List<Post> getListTrashPage()
        {
            return db.Posts.Where(m => m.Status == 0 && m.Type == "Page").OrderByDescending(m => m.CreatedAt).ToList();
        }

        //Trả về danh sách các mẫu tin
        public List<Post> getList(string status = "All", string type = "Post")
        {
            List<Post> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Posts.Where(m => m.Status != 0 && m.Type == type).OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status==0
                        list = db.Posts.Where(m => m.Status == 0 && m.Type == type).OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Posts.Where(m => m.Type == type).OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
            }
            return list;

        }//Trả về danh sách các mẫu tin
        public List<PostInfo> getListJoin(string status = "All", string type = "Post")
        {
            List<PostInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Posts
                            .Join(
                db.Topics,
                p => p.TopId,
                c => c.Id,
                (p, c) => new PostInfo
                {
                    Id = p.Id,
                    Title = p.Title,
                    TopId = p.TopId,
                    TopicName = c.Name,
                    Slug = p.Slug,
                    Images = p.Img,
                    Detail = p.Detail,
                    Type = p.Type,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedBy = p.CreatedBy,
                    CreatedAt = p.CreatedAt,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedAt = p.UpdatedAt,
                    Status = p.Status
                }
                ).Where(m => m.Status != 0 && m.Type == type)
                 .OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                case "Trash":
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Posts
                            .Join(
                db.Topics,
                p => p.TopId,
                c => c.Id,
                (p, c) => new PostInfo
                {
                    Id = p.Id,
                    Title = p.Title,
                    TopId = p.TopId,
                    TopicName = c.Name,
                    Slug = p.Slug,
                    Images = p.Img,
                    Detail = p.Detail,
                    Type = p.Type,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedBy = p.CreatedBy,
                    CreatedAt = p.CreatedAt,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedAt = p.UpdatedAt,
                    Status = p.Status
                }
                ).Where(m => m.Status != 0 && m.Type == type)
                 .OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                default:
                    {
                        //Lấy ra những mẫu tin có status!=0
                        list = db.Posts
                            .Join(
                db.Topics,
                p => p.TopId,
                c => c.Id,
                (p, c) => new PostInfo
                {
                    Id = p.Id,
                    Title = p.Title,
                    TopId = p.TopId,
                    TopicName = c.Name,
                    Slug = p.Slug,
                    Images = p.Img,
                    Detail = p.Detail,
                    Type = p.Type,
                    MetaDesc = p.MetaDesc,
                    MetaKey = p.MetaKey,
                    CreatedBy = p.CreatedBy,
                    CreatedAt = p.CreatedAt,
                    UpdatedBy = p.UpdatedBy,
                    UpdatedAt = p.UpdatedAt,
                    Status = p.Status
                }
                ).Where(m => m.Status != 0 && m.Type == type)
                 .OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
            }
            return list;

        }
        //Trả vê 1 mẫu tin
        public Post getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Find(id);
            }
        }
        //Trả vê 1 mẫu tin
        public Post getRow(string slug)
        {
            if (slug == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Where(m => m.Slug == slug && m.Status == 1 && m.Type == "Post").FirstOrDefault();
            }
        }

        //Trả vê 1 mẫu tin
        public Post getRowType(string slug, string type)
        {
            if (slug == null)
            {
                return null;
            }
            else
            {
                return db.Posts.Where(m => m.Slug == slug && m.Status == 1 && m.Type == type).FirstOrDefault();
            }
        }

        //
        //Thêm mẫu tin
        public int Insert(Post row)
        {
            db.Posts.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin
        public int Update(Post row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Xoá mẫu tin
        public int Delete(Post row)
        {
            db.Posts.Remove(row);
            return db.SaveChanges();

        }

    }
}

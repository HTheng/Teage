using IServices;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Teage.ApiCommon;
using Teage.Common;
using Teage.Entity;
using Teage.Model;
namespace Services.System
{
    public partial class Expresses : Service, IExpress
    {
        public Queue<ExpressInfo> queue = new Queue<ExpressInfo>();

        //新增优递 
        public bool Add(Express model, ref Meta meta)
        {
            string msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Success);
            string msgCode = ErrorCode.Success.GetHashCode().ToString();
            model.ID = Guid.NewGuid().ToString().Replace("-", "");

            model.TypeID = 0;
            Try(context =>
            {
                context.Express.Add(model);
                context.SaveChanges();
            });
            meta.ErrorCode = msgCode;
            meta.ErrorMsg = msg;
            return true;
        }
        //优递列表
        public PageModel<Express> List(ExpressQuery queryInfo, ref Meta meta)
        {
            return Try(context =>
            {
                string sql = "SELECT * FROM Express WHERE 1=1  ";
                List<SqlParameter> paramList = new List<SqlParameter>();
                //查询
                if (!string.IsNullOrEmpty(queryInfo.ExpressName))
                {
                    sql += " AND ExpressName like @vName";
                    paramList.Add(new SqlParameter("@vName", "%" + queryInfo.ExpressName + "%"));
                }
                //列表显示 条件
                if (!string.IsNullOrEmpty(queryInfo.flag))
                {
                    if (queryInfo.flag == "1")
                    {
                        sql += " and TypeID=0 ";
                    }
                    else if (queryInfo.flag == "2")
                    {
                        sql += " AND UserID like @UserID";
                        paramList.Add(new SqlParameter("@UserID", "%" + queryInfo.UserID + "%"));
                    }
                    else if (queryInfo.flag == "3")
                    {
                        sql += " AND HamalUserID like @UserID";
                        paramList.Add(new SqlParameter("@UserID", "%" + queryInfo.UserID + "%"));
                    }
                }

                if (!string.IsNullOrEmpty(queryInfo.ID))
                {
                    sql += " AND ID like @ID";
                    paramList.Add(new SqlParameter("@ID", "%" + queryInfo.ID + "%"));
                }

                return context.QuerySql<Express>(sql, queryInfo, paramList.ToArray());
            });
        }
        //查询一条数据
        public List<Express> Query(ExpressQuery queryInfo, ref Meta meta)
        {
            var model = new List<Express>();
            Try(context =>
           {
               string sql = "SELECT TOP 1 * FROM Express WHERE 1=1 ";
               List<SqlParameter> paramList = new List<SqlParameter>();
               //详情显示 条件
               if (!string.IsNullOrEmpty(queryInfo.ID))
               {
                   sql += " AND ID like @ID";
                   paramList.Add(new SqlParameter("@ID", "%" + queryInfo.ID + "%"));
               }
               model = context.Database.SqlQuery<Express>(sql, paramList.ToArray()).ToList();
           });
            return model;
        }
        //客户做任务
        public bool Edit(ExpressInfo model, ref Meta meta)
        {
            string msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Apply);
            string msgCode = ErrorCode.Apply.GetHashCode().ToString();

            model.TypeID = 0;
            Try(context =>
            {
                var editModel = context.Express.FirstOrDefault(c => c.ID == model.ID);
                if (editModel != null)
                {
                    if (editModel.TypeID == 0)
                    {
                        editModel.HamalUserID = model.HamalUserID;
                        editModel.ReleaseTime = DateTime.Now;
                        editModel.TypeID = 1;
                        context.SaveChanges();
                    }
                    else
                    {
                        msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.ApplyError);
                        msgCode = ErrorCode.ApplyError.GetHashCode().ToString();
                    }
                }
            });
            meta.ErrorCode = msgCode;
            meta.ErrorMsg = msg;
            return true;
        }
        //修改 优递配送状态
        public bool Update(Express model, string flag, ref Meta meta)
        {
            string msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Delivery);
            string msgCode = ErrorCode.Delivery.GetHashCode().ToString();

            Try(context =>
            {
                var editModel = context.Express.FirstOrDefault(c => c.ID == model.ID);
                if (editModel != null)
                {
                    if (flag == "2")
                    {
                        editModel.ArrivalTime = DateTime.Now;
                        editModel.TypeID = 2;
                        context.SaveChanges();
                    }
                    else if (flag == "-1")
                    {
                        //插入记录表
                        ExpressCal cal = new ExpressCal();
                        cal.UserID = editModel.UserID;
                        cal.StartTime = editModel.ReleaseTime;
                        cal.EndTime = DateTime.Now;
                        cal.ExpressID = editModel.ID;
                        cal.Zy = "取消配送";
                        context.ExpressCal.Add(cal);
                        editModel.HamalUserID = "";
                        editModel.TypeID = 0;
                        context.SaveChanges();
                        msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.DeliveryCancel);
                        msgCode = ErrorCode.DeliveryCancel.GetHashCode().ToString();
                    }
                    else if (flag == "3")
                    {
                        //查看是否是 发起人 发起的取消
                        var Info = context.Express.Where(c => c.UserID == model.UserID);
                        if (Info != null)
                        {
                            //再查下是否被配送了没 
                            if (string.IsNullOrEmpty(editModel.HamalUserID))
                            {
                                editModel.TypeID = 3;
                                context.SaveChanges();
                                msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.DeliveryClose);
                                msgCode = ErrorCode.DeliveryClose.GetHashCode().ToString();
                            }
                            else
                            {
                                msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.CloseError);
                                msgCode = ErrorCode.CloseError.GetHashCode().ToString();
                            }
                        }
                        else
                        {
                            msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.NoUser);
                            msgCode = ErrorCode.NoUser.GetHashCode().ToString();
                        }
                    }
                    else
                    {
                        msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.DeliveryError);
                        msgCode = ErrorCode.DeliveryError.GetHashCode().ToString();
                    }
                }
            });
            meta.ErrorCode = msgCode;
            meta.ErrorMsg = msg;
            return true;
        }

        /// <summary>
        /// 将数据添加到队列中（写入）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="publishDate"></param>
        /// <param name="msg"></param>
        public void AddQueue(ExpressInfo model)
        {
            queue.Enqueue(model); //将输入添加到队列中（入队）
        }
        /// <summary>
        /// 放到队列中的数据是要从索引库删除的数据
        /// </summary>
        public void DeleteQueue(string id)
        {
            ExpressInfo model = new ExpressInfo();
            model.ID = id;
            model.JobType = EnumType.Delete;
            queue.Enqueue(model);
        }
        //开启线程.
        public void StartThread()
        {
            Thread thread = new Thread(StartWriteIndex);

            thread.IsBackground = true;
            thread.Start();
        }
        public void StartWriteIndex()
        {
            while (true)
            {
                if (queue.Count > 0)
                {
                    CreateIndexContent();
                }
                else
                {
                    Thread.Sleep(3000);//如果队列中没有数据，让线程休息一下，避免了CPU的空转。
                }
            }
        }
        /// <summary>
        /// 将队列中的取出来，写到索引库中。
        /// </summary>
        public void CreateIndexContent()
        {
            while (queue.Count > 0)
            {
                Meta meta = new Meta();
                ExpressInfo model = queue.Dequeue();//获取队列中的数据（出队）
                Edit(model, ref meta);
                if (model.JobType == EnumType.Delete)
                {
                    continue;
                }
            }
        }


        #region  以下方法 都是适用于后台管理 方法（降低耦合度）
        //后台列表
        public PageModel<Express> HostList(ExpressQuery queryInfo, ref Meta meta)
        {
            return Try(context =>
            {
                string sql = "SELECT * FROM Express WHERE 1=1  ";
                List<SqlParameter> paramList = new List<SqlParameter>();
                //查询
                if (!string.IsNullOrEmpty(queryInfo.ExpressName))
                {
                    sql += " AND ExpressName like @vName";
                    paramList.Add(new SqlParameter("@vName", "%" + queryInfo.ExpressName + "%"));
                }
                return context.QuerySql<Express>(sql, queryInfo, paramList.ToArray());
            });
        }
        //编辑
        public bool Modify(ExpressInfo model, ref Meta meta)
        {
            string msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Delivery);
            string msgCode = ErrorCode.Delivery.GetHashCode().ToString();

            Try(context =>
            {
                var editModel = context.Express.FirstOrDefault(c => c.ID == model.ID);
                if (editModel != null)
                {
                    context.SaveChanges();
                    msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.DeliveryError);
                    msgCode = ErrorCode.DeliveryError.GetHashCode().ToString();
                }
            });
            meta.ErrorCode = msgCode;
            meta.ErrorMsg = msg;
            return true;
        }
        //后台删除
        public bool Delete(ExpressInfo model, ref Meta meta)
        {
            string msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Success);
            string msgCode = ErrorCode.Success.GetHashCode().ToString();
            Try(context =>
            {
                //查询用户名是否存在
                var Info = context.Express.Where(c => c.ID == model.ID).FirstOrDefault();
                if (Info != null)
                {
                    msgCode = ErrorCode.Error.GetHashCode().ToString();
                    msg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.Error);
                }
                else
                {
                    Info.TypeID = 3;
                    context.SaveChanges();
                }
            });
            meta.ErrorCode = msgCode;
            meta.ErrorMsg = msg;
            return true;
        }
        #endregion


    }
}

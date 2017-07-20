using Newtonsoft.Json.Linq;
using Services;
using Services.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Teage.ApiCommon;
using Teage.Common;
using Teage.Entity;
using Teage.Model;
namespace Teage.SystemApi
{
    public class ExpressController : ApiController
    {
        private static readonly Expresses ExpressService = new Expresses();
        public Queue<ExpressInfo> queue = new Queue<ExpressInfo>();
        //添加派送优递
        [HttpGet, HttpPost]
        public Base<object> Add(ExpressInfo model)
        {
            Express entity = new Express();

            //取token值
            TokenReponse repository = new TokenReponse();
            Token token = repository.First(model.Token);
            Meta meta = new Meta();
            bool addNote = true;
            //校验数据

            if (token == null)
            {
                meta.ErrorCode = ErrorCode.LoginError.GetHashCode().ToString();
                meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.LoginError);
            }
            else
            {
                entity.UserID = token.UserId.ToString();
                entity.ExpressName = model.ExpressName;
                entity.ExpressPwd = model.ExpressPwd;
                entity.Phone = model.Phone;
                entity.Destination = model.Destination;
                entity.ExpectTime = model.ExpectTime;
                entity.Money = Convert.ToDecimal(model.vState);
                entity.Zy = model.Zy;
                entity.Payway = Convert.ToInt32(model.Rel);
                addNote = ExpressService.Add(entity, ref meta);
            }
            Base<object> result = new Base<object>
            {
                Meta = meta,
                Body = addNote
            };
            return result;
        }
        /// <summary>
        /// 获取优递列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public Base<object> List(ExpressQuery queryModel)
        {
            Meta meta = new Meta();
            PageModel<Express> list = new PageModel<Express>();
            if (queryModel.flag == "1")
            {
                list = ExpressService.List(queryModel, ref meta);
            }
            if (queryModel.flag == "2" || queryModel.flag == "3")
            {
                //取token值
                TokenReponse repository = new TokenReponse();
                Token token = repository.First(queryModel.Token);
                if (token == null)
                {
                    meta.ErrorCode = ErrorCode.LoginError.GetHashCode().ToString();
                    meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.LoginError);
                }
                else
                {

                    queryModel.UserID = token.UserId;
                    list = ExpressService.List(queryModel, ref meta);
                }
            }
            Base<object> response = new Base<object>
            {
                Body = list,
                Meta = meta
            };
            return response;
        }
        //加载详情页（About）
        [HttpGet, HttpPost]
        public Base<object> About(string ID)
        {
            ExpressQuery queryModel = new ExpressQuery();
            queryModel.ID = ID;
            Meta meta = new Meta();
            Base<object> response = new Base<object>
            {
                Body = Teage.Common.SerializableHelper.SerializableToString(ExpressService.Query(queryModel, ref meta)),
                Meta = meta
            };
            return response;
        }

        //开始做任务
        [HttpGet, HttpPost]
        public Base<object> Edit(ExpressInfo model)
        {
            Express entity = new Express();

            //取token值
            TokenReponse repository = new TokenReponse();
            Token token = repository.First(model.Token);
            Meta meta = new Meta();
            bool addNote = true;
            //校验数据
            if (token == null)
            {
                meta.ErrorCode = ErrorCode.LoginError.GetHashCode().ToString();
                meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.LoginError);
            }
            else
            {
                model.HamalUserID = token.UserId;
                //入队
                this.AddQueue(model);
                while (true)
                {
                    if (queue.Count > 0)
                    {
                        while (queue.Count > 0)
                        {
                            ExpressInfo models = queue.Dequeue();//获取队列中的数据（出队）
                            addNote = ExpressService.Edit(models, ref meta);
                            if (models.JobType == EnumType.Delete)
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Base<object> result = new Base<object>
            {
                Meta = meta,
                Body = addNote
            };
            return result;
        }
        //修改 优递配送状态
        [HttpGet, HttpPost]
        public Base<object> Update(ExpressInfo model)
        {
            Express entity = new Express();

            //取token值
            TokenReponse repository = new TokenReponse();
            Token token = repository.First(model.Token);
            Meta meta = new Meta();
            bool editNote = true;
            //校验数据
            if (token == null)
            {
                meta.ErrorCode = ErrorCode.LoginError.GetHashCode().ToString();
                meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.LoginError);
            }
            else
            {
                entity.UserID = token.UserId.ToString();
                entity.ID = model.ID;
                string flag = model.Flag;
                editNote = ExpressService.Update(entity, flag, ref meta);
            }
            Base<object> result = new Base<object>
            {
                Meta = meta,
                Body = editNote
            };
            return result;
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

        #region  以下都是  后台管理的方法
        /// <summary>
        /// 后台所有优递列表
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public Base<object> HostList(ExpressQuery queryModel)
        {
            Meta meta = new Meta();
            PageModel<Express> list = new PageModel<Express>();
            //取token值
            TokenReponse repository = new TokenReponse();
            Token token = repository.First(queryModel.Token);
            if (token == null)
            {
                meta.ErrorCode = ErrorCode.LoginError.GetHashCode().ToString();
                meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.LoginError);
            }
            else
            {
                queryModel.UserID = token.UserId;
                list = ExpressService.HostList(queryModel, ref meta);
            }
            Base<object> response = new Base<object>
            {
                Body = list,
                Meta = meta
            };
            return response;
        }

        //后台编辑
        [HttpGet, HttpPost]
        public Base<object> Modify(ExpressInfo model)
        {
            //取token值
            TokenReponse repository = new TokenReponse();
            Token token = repository.First(model.Token);
            Meta meta = new Meta();
            bool editNote = true;
            //校验数据
            if (token == null)
            {
                meta.ErrorCode = ErrorCode.LoginError.GetHashCode().ToString();
                meta.ErrorMsg = EnumHelper.GetDescriptionFromEnumValue(ErrorCode.LoginError);
            }
            else
            {
                model.UserID = token.UserId.ToString();
                editNote = ExpressService.Modify(model, ref meta);
            }
            Base<object> result = new Base<object>
            {
                Meta = meta,
                Body = editNote
            };
            return result;
        }
        //后台删除
        [HttpGet, HttpPost]
        public Base<object> Del(ExpressInfo model)
        {
            Meta meta = new Meta();
            bool delNote = ExpressService.Delete(model, ref meta);
            Base<object> result = new Base<object>
            {
                Meta = meta,
                Body = delNote
            };
            return result;
        }
        #endregion

    }
}

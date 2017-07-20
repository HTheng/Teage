using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teage.Entity
{
    public enum ErrorCode
    {
        /// <summary>
        /// 保存成功
        /// </summary>
        [Description("保存成功")]
        Success = 0,
        /// <summary>
        /// 验证码出错
        /// </summary>
        [Description("验证码出错")]
        ArgumentException = 1,
        /// <summary>
        /// 保存失败
        /// </summary>
        [Description("保存失败")]
        Error = -1,
        /// <summary>
        /// 登录失败
        /// </summary>
        [Description("登录失败")]
        LoginError = 2,
        /// <summary>
        /// 申请成功
        /// </summary>
        [Description("申请成功")]
        Apply = 3,
        /// <summary>
        /// 该优递商品已被申请
        /// </summary>
        [Description("该优递商品已被申请")]
        ApplyError = 4,
        /// <summary>
        /// 配送成功
        /// </summary>
        [Description("配送成功")]
        Delivery = 5,
        /// <summary>
        /// 修改状态失败
        /// </summary>
        [Description("修改状态失败")]
        DeliveryError = 6,
        /// <summary>
        /// 取消配送成功
        /// </summary>
        [Description("取消配送成功")]
        DeliveryCancel = 7,
        /// <summary>
        /// 关闭优递成功
        /// </summary>
        [Description("关闭优递成功")]
        DeliveryClose = 8,
        /// <summary>
        /// 关闭优递失败
        /// </summary>
        [Description("已派送")]
        CloseError = 9,
        /// <summary>
        /// 只有发起人才能关闭优递
        /// </summary>
        [Description("只有发起人才能关闭优递")]
        NoUser = 10,
        /// <summary>
        /// 获取验证码出错
        /// </summary>
        [Description("获取验证码出错")]
        CaptchaError = 11,
        /// <summary>
        /// 系统错误
        /// </summary>
        [Description("系统错误")]
        SystemError = 12,
        /// <summary>
        /// 还未注册
        /// </summary>
        [Description("还未注册")]
        NoReg = 13,
        /// <summary>
        ///  用户已存在
        /// </summary>
        [Description("该手机号已经被注册")]
        ExistReg = 14,
        /// <summary>
        ///     无效验证码或验证码超时
        /// </summary>
        /// 将提示信息“无效验证码或验证码超时”修改为“验证码已过期，请重新获取验证码”by:冯岩
        [Description("无效验证码或验证码超时")]
        Error1015 = 15,

        /// <summary>
        ///     验证码错误
        /// </summary>
        /// 将提示信息“验证码错误”修改为“验证码错误，请确认后重新输入”
        [Description("验证码错误")]
        Error1016 = 16,
        /// <summary>
        ///     无效Token
        /// </summary>
        /// 将提示信息“无效Token”修改为“用户登录超时，请重新登录”
        [Description("用户登录超时，请重新登录")]
        Error1017 = 17,
        /// <summary>
        /// 密码格式错误
        /// </summary>
        /// 将提示信息“密码格式错误，请输入6-12位数字、英文或者常用特殊字符”修改为“请输入6-12位数字、英文或常用字符作为密码”
        [Description("密码格式错误，请输入6-12位数字、英文或者常用特殊字符")]
        Error1018 = 18,
    }
}

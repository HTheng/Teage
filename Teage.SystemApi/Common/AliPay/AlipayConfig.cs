using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Teage.SystemApi.Common.AliPay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class Config
    {
        #region 字段
        private static string partner = "";
        private static string seller_id = "";
        private static string private_key = "";
        private static string public_key = "";
        private static string input_charset = "";
        private static string sign_type = "";


        private static string notify_url = "";
        private static string order_notify_url = "";
        //private static string return_url = "";
        private static string payment_type = "";
        private static string service = "";
        private static string show_url = "";
        private static string it_b_pay = "";
        #endregion

        static Config()
        {
            //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            partner = ConfigurationManager.AppSettings["partner_partner"].ToString();

            //收款支付宝账号
            seller_id = ConfigurationManager.AppSettings["seller_id_partner"].ToString();

            //商户私钥
            private_key = ConfigurationManager.AppSettings["private_key"].ToString();

            //支付宝公钥
            public_key = ConfigurationManager.AppSettings["public_key"].ToString();

            //↑↑↑↑↑↑↑↑↑↑请在这里配置您的基本信息↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

            //字符编码格式 目前支持 gbk 或 utf-8
            input_charset = "utf-8";

            //签名方式，选择项：RSA、DSA、MD5
            sign_type = "RSA";


            //充值通知回调页面
            notify_url = ConfigurationManager.AppSettings["notify_url_partner"].ToString();
            //订单支付通知回调页面
            order_notify_url = ConfigurationManager.AppSettings["order_notify_url_partner"].ToString();
            //通知回调页面
            //return_url = ConfigurationManager.AppSettings["return_url_partner"].ToString();
            //必填 规定只能1
            payment_type = "1";
            service = "mobile.securitypay.pay";
            it_b_pay = "30m";
            show_url = "m.alipay.com";
        }

        #region 属性
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Seller_id
        {
            get { return seller_id; }
            set { seller_id = value; }
        }

        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public static string Private_key
        {
            get { return private_key; }
            set { private_key = value; }
        }

        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public static string Public_key
        {
            get { return public_key; }
            set { public_key = value; }
        }
        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }


        ///// <summary>
        ///// 同步回调页面
        ///// </summary>
        //public static string ReturnUrl
        //{
        //    get { return return_url; }
        //    set { return_url = value; }
        //}

        /// <summary>
        /// 异步回调页面
        /// </summary>
        public static string NotifyUrl
        {
            get { return notify_url; }
            set { notify_url = value; }
        }
        /// <summary>
        /// 订单支付异步回调页面
        /// </summary>
        public static string OrderNotifyUrl
        {
            get { return order_notify_url; }
            set { order_notify_url = value; }
        }
        /// <summary>
        /// 必填 规定只能1
        /// </summary>
        public static string Payment_type
        {
            get { return payment_type; }
            set { payment_type = value; }
        }
        /// <summary>
        /// 定死
        /// </summary>
        public static string Service
        {
            get { return service; }
            set { service = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string Show_url
        {
            get { return show_url; }
            set { show_url = value; }
        }

        /// <summary>
        /// 未付款交易的超时时间
        /// </summary>
        public static string It_b_pay
        {
            get { return it_b_pay; }
        }

        #endregion
    }
}
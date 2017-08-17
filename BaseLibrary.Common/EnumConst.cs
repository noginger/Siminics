using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cee.Tools.Types;

namespace BaseLibrary.Common
{
    public class EnumConst
    {

        #region 客户类型
        /// <summary>
        /// 客户类型
        /// </summary>
        public enum CustomerType
        {
            /// <summary>
            /// 公司
            /// </summary>
            [EnumDescription("公司", 1)]
            Company = 1,
            /// <summary>
            /// 用户
            /// </summary>
            [EnumDescription("用户", 2)]
            Single = 2
        }

        #endregion

        #region 认证状态

        public enum ApproveState
        {
            /// <summary>
            /// 未认证
            /// </summary>
            [EnumDescription("未认证", 0)]
            Unautherized = 0,
            /// <summary>
            /// 初步认证
            /// </summary>
            [EnumDescription("初步认证", 1)]
            FirstApprove = 1,
            /// <summary>
            /// 正式认证
            /// </summary>
            [EnumDescription("正式认证", 2)]
            LastApprove = 2
        }

        #endregion

        #region 公司性质
        /// <summary>
        /// 公司性质
        /// </summary>
        public enum Quality
        {
            /// <summary>
            /// 国有
            /// </summary>
            [EnumDescription("国有", 1)]
            StateOwned = 1,
            /// <summary>
            /// 集体
            /// </summary>
            [EnumDescription("集体", 2)]
            Collective = 2,
            /// <summary>
            /// 联营
            /// </summary>
            [EnumDescription("联营", 3)]
            Joint = 3,
            /// <summary>
            /// 股份制
            /// </summary>
            [EnumDescription("股份制", 4)]
            Stock = 4,
            /// <summary>
            /// 民营
            /// </summary>
            [EnumDescription("民营", 5)]
            Private = 5,
            /// <summary>
            /// 有限公司
            /// </summary>
            [EnumDescription("有限公司", 6)]
            LimitedCompany = 6,
            /// <summary>
            /// 外资
            /// </summary>
            [EnumDescription("外资", 7)]
            Foreign = 7,
            /// <summary>
            /// 其它
            /// </summary>
            [EnumDescription("其它", 8)]
            Other = 8
        }
        #endregion

        #region  支付业务类型
        /// <summary>
        /// 支付业务类型
        /// </summary>
        public enum PayType
        {
            /// <summary>
            /// 其它
            /// </summary>
            [EnumDescription("其它", -1)]
            Other = -1,

            /// <summary>
            /// 展位
            /// </summary>
            [EnumDescription("展位", 1)]
            Booth = 1,

            /// <summary>
            /// 充值
            /// </summary>
            [EnumDescription("充值", 2)]
            Recharge = 2,

            /// <summary>
            /// 订单
            /// </summary>
            [EnumDescription("订单", 3)]
            Order = 3,
        }
        #endregion

        #region 订单状态

        /// <summary>
        /// 订单当前状态
        /// </summary>
        [EnumDescription("订单状态")]
        public enum OrderStatus
        {
            /// <summary>
            /// 关闭订单
            /// </summary>
            [EnumDescription("关闭订单", -1)]
            CloseOrder = -1,

            /// <summary>
            /// 下单成功
            /// </summary>
            [EnumDescription("下单成功", 0)]
            OrderSuccess = 0,
            
            /// <summary>
            /// 支付
            /// </summary>
            [EnumDescription("已支付", 1)]
            Pay = 1,

            /// <summary>
            /// 正在备货
            /// </summary>
            [EnumDescription("正在备货", 2)]
            Extracted = 2,
            
            /// <summary>
            /// 已发货 
            /// </summary>
            [EnumDescription("已发货", 3)]
            OutBound = 3,

            /// <summary>
            /// 确认收货 
            /// </summary>
            [EnumDescription("已完成", 4)]
            ConfirmReceipt = 4
        }
        #endregion

        #region 订单类型

        [EnumDescription("订单类型")]
        public enum OrderType
        {
            /// <summary>
            /// 新增用户 
            /// </summary>
            [EnumDescription("新增用户", 1)] 
            NewCustomer = 1,
            /// <summary>
            /// 续费订单
            /// </summary>
            [EnumDescription("续费订单", 2)]
            RenewCustomer = 2
        }

        #endregion

        #region 客户订单状态

        /// <summary>
        /// 订单当前状态
        /// </summary>
        [EnumDescription("订单状态")]
        public enum AccountOrderStatus
        {
            /// <summary>
            /// 下单成功
            /// </summary>
            [EnumDescription("下单成功", 0)] 
            OrderSuccess = 0,

            /// <summary>
            /// 撤销订单
            /// </summary>
            [EnumDescription("撤销订单", -1)] 
            Repeal = -1,

            /// <summary>
            /// 审核通过
            /// </summary>
            [EnumDescription("审核通过", 1)] 
            Pass = 1

        }

        #endregion

        #region 编号类型
        /// <summary>
        /// 编号类型
        /// </summary>
        public enum NumberType
        {
            /// <summary>
            /// 财务流水编号
            /// </summary>
            [EnumDescription("CW", 1)]
            CW = 1,
            /// <summary>
            /// 在线充值编号
            /// </summary>
            [EnumDescription("CZ", 2)]
            CZ = 2,
            /// <summary>
            /// 支付账单编号  
            /// </summary>
            [EnumDescription("ZD", 3)]
            ZD = 3,
            /// <summary>
            /// 订单编号      
            /// </summary>
            [EnumDescription("DD", 4)]
            DD = 4,
            /// <summary>
            /// 冲红补差单编号 
            /// </summary>
            [EnumDescription("CB", 5)]
            CB = 5,
            /// <summary>
            /// 发货单编号    WL（物流）
            /// </summary>
            [EnumDescription(" WL", 6)]
            WL = 6,
            /// <summary>
            /// 购买展位
            /// </summary>
            [EnumDescription(" WL", 7)]
            ZW = 7

        }
        #endregion

        #region 资金名目
        /// <summary>
        /// 资金名目
        /// </summary>
        public enum NotionalFundType
        {
            /// <summary>
            /// 充值
            /// </summary>
            [EnumDescription("充值")]
            CZ = 100,

            /// <summary>
            /// 返点
            /// </summary>
            [EnumDescription("返点")]
            FD = 110,

            /// <summary>
            /// 提现
            /// </summary>
            [EnumDescription("提现")]
            TX = 120,

            /// <summary>
            /// 支付
            /// </summary>
            [EnumDescription("支付")]
            ZF = 200,

            /// <summary>
            /// 结算
            /// </summary>
            [EnumDescription("结算")]
            JS = 210,

            /// <summary>
            /// 冻结
            /// </summary>
            [EnumDescription("冻结")]
            DJ = 220,

            /// <summary>
            /// 解冻
            /// </summary>
            [EnumDescription("解冻")]
            JD = 230,

            /// <summary>
            /// 退差
            /// </summary>
            [EnumDescription("退差")]
            TC = 500,

            /// <summary>
            /// 退款
            /// </summary>
            [EnumDescription("退款")]
            TK = 510,

            /// <summary>
            /// 调账
            /// </summary>
            [EnumDescription("调账")]
            TZ = 600,

            /// <summary>
            /// 冲红
            /// </summary>
            [EnumDescription("冲红")]
            CH = 700,

            /// <summary>
            /// 返现
            /// </summary>
            [EnumDescription("返现")]
            FX = 710,

            /// <summary>
            /// 补差
            /// </summary>
            [EnumDescription("补差")]
            BC = 720,

            /// <summary>
            /// 借款
            /// </summary>
            [EnumDescription("借款")]
            JK = 800,

            /// <summary>
            /// 还款
            /// </summary>
            [EnumDescription("还款")]
            HK = 810,

            /// <summary>
            /// 入账
            /// </summary>
            [EnumDescription("入账")]
            RZ = 998,

            /// <summary>
            /// 支出
            /// </summary>
            [EnumDescription("支出")]
            ZC = 999,
        }
        #endregion

        #region 交易类型
        /// <summary>
        /// 交易类型
        /// </summary>
        public enum TradeType
        {
            /// <summary>
            /// 交易
            /// </summary>
            [EnumDescription("交易", 1)]
            Trade = 1,

            /// <summary>
            /// 退款
            /// </summary>
            [EnumDescription("退款", 2)]
            Refund = 2,
        }
        #endregion

        #region 文本类型

        public enum ContentType
        {
            /// <summary>
            /// 企业介绍
            /// </summary>
            [EnumDescription("企业介绍", 1)]
            CompanyIntro = 1,
            /// <summary>
            /// 销售政策
            /// </summary>
            [EnumDescription("销售政策", 2)]
            SalePolicy = 2,
            /// <summary>
            /// 联系方式
            /// </summary>
            [EnumDescription("联系方式", 3)]
            Contact = 3
        }

        #endregion

        #region 提交方式
        /// <summary>
        /// 提交方式
        /// </summary>
        public enum HttpSubmitType
        {
            /// <summary>
            /// Post
            /// </summary>
            [EnumDescription("Post", 1)]
            Post = 1,
            /// <summary>
            /// Get
            /// </summary>
            [EnumDescription("Get", 2)]
            Get = 2
        }
        #endregion

        #region 支付方式

        /// <summary>
        /// 支付方式
        /// </summary>
        public enum PaidType
        {
            /// <summary>
            /// 线下
            /// </summary>
            [EnumDescription("网银/线下转账", 1)]
            Offline = 1,
            /// <summary>
            /// 现金
            /// </summary>
            [EnumDescription("在线支付", 2)]
            Online = 2
        }

        #endregion

        #region 产品分类
        /// <summary>
        /// 产品分类
        /// </summary>
        public enum ProductType
        {
            /// <summary>
            /// 模板
            /// </summary>
            [EnumDescription("模板", 1)]
            Template = 1,
            /// <summary>
            /// 本地
            /// </summary>
            [EnumDescription("本地", 2)]
            Local = 2,
            /// <summary>
            /// 微信
            /// </summary>
            [EnumDescription("微信", 3)]
            WeChat = 3
        }
        #endregion

        #region 上传类型
        /// <summary>
        /// 文件上传类型
        /// </summary>
        public enum UploadType
        {
            [EnumDescription("图片", 0)]
            Picture,
            [EnumDescription("文件", 1)]
            File,
            [EnumDescription("广告", 2)]
            Adver
        }
        #endregion

        #region 推广状态
        /// <summary>
        /// 推广状态
        /// </summary>
        public enum PopState
        {
            [EnumDescription("推广期", 1)]
            Popularize=1,
            [EnumDescription("收费期", 2)]
            Charge=2
        }
        
        #endregion

        #region 新闻分类
        /// <summary>
        /// 新闻分类
        /// </summary>
        public enum NewsType
        {
            [EnumDescription("产品动态", 1)]
            Dynamic = 1,
            [EnumDescription("产品案例", 2)]
            CaseStudy = 2
        }

        #endregion

        #region Cms_Banner位置

        /// <summary>
        /// Banner位置
        /// </summary>
        public enum CmsBanner
        {
            [EnumDescription("模板版块", 1)]
            Templates = 1,
            [EnumDescription("本地游戏版块", 2)]
            Local=2,
            [EnumDescription("微信版块", 4)]
            WeChat=3
        }

        #endregion

        #region 所属行业

        /// <summary>
        /// 所属行业
        /// </summary>
        public enum IndustryTyps
        {
            [EnumDescription("广告/公关/营销/媒体", 1)]
            FirstPage = 1,
            [EnumDescription("影视/艺术/文化传播", 2)]
            Art = 2,
            [EnumDescription("贸易/消费/制造/营运", 3)]
            Trade = 3,
            [EnumDescription("计算机/互联网/通信/电子", 4)]
            IT=4,
            [EnumDescription("服务业/酒店/旅游", 5)]
            Service=5,
            [EnumDescription("政府/非赢利机构", 6)]
            Government=6,
            [EnumDescription("文化创意产", 7)]
            Culture=7
        }

        #endregion

        #region 插件支持

        /// <summary>
        /// 插件支持
        /// </summary>
        public enum PlugSupport
        {
            [EnumDescription("IPAD", 1)]
            IPad = 1,
            [EnumDescription("微信", 2)]
            WeChart=2,
            [EnumDescription("Kinect", 3)]
            Kinect=3
        }

        #endregion

        #region 发票类型
        /// <summary>
        /// 发票类型
        /// </summary>
        public enum InvoiceType
        {
            [EnumDescription("普通发票", 1)]
            Normal=1,
            [EnumDescription("增值税发票", 2)]
            AddValues=2
        }

        #endregion

        #region 过期状态

        /// <summary>
        /// 过期状态
        /// </summary>
        public enum ExpireStatus
        {
            /// <summary>
            /// 已过期
            /// </summary>
            [EnumDescription("已过期", -1)]
            Expire = -1,
            /// <summary>
            /// 正常
            /// </summary>
            [EnumDescription("使用中",1)]
            Normal=1,
            /// <summary>
            /// 初始化
            /// </summary>
            [EnumDescription("未激活",0)]
            Initialize=0

        }

        #endregion

        #region 使用时长

        /// <summary>
        /// 使用时长
        /// </summary>
        public enum UseDay
        {
            /// <summary>
            /// 3天
            /// </summary>
            [EnumDescription("3天", 3)]
            Three=3,
            /// <summary>
            /// 包月
            /// </summary>
            [EnumDescription("包月", 30)]
            Month=30,
            /// <summary>
            /// 包年
            /// </summary>
            [EnumDescription("包年", 365)]
            Year = 365
        }
        
        #endregion

        #region 权限

        /// <summary>
        /// 权限
        /// </summary>
        public enum CmsAuthority
        {
            /// <summary>
            /// 1天
            /// </summary>
            [EnumDescription("照片墙", 1)]
            Wall = 1,
            /// <summary>
            /// 游戏
            /// </summary>
            [EnumDescription("本地游戏", 2)]
            Game = 2,
            /// <summary>
            /// 游戏
            /// </summary>
            [EnumDescription("微信游戏", 3)]
            WeChat = 3
        }

        #endregion

        #region 微信活动状态

        /// <summary>
        /// 权限
        /// </summary>
        public enum EventState
        {
            /// <summary>
            /// 未开始
            /// </summary>
            [EnumDescription("未开始", 0)]
            Invalid = 0,
            /// <summary>
            /// 倒计时
            /// </summary>
            [EnumDescription("倒计时", 1)]
            CountDown = 1,
            /// <summary>
            /// 正在开始
            /// </summary>
            [EnumDescription("正在开始",2)]
            Starting=2,
            /// <summary>
            /// 结束
            /// </summary>
            [EnumDescription("结束", 3)]
            End = 3
        }

        #endregion

    }
}

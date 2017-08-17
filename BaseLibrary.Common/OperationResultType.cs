using System;
using System.Collections.Generic;
using System.ComponentModel;
using Cee.Tools.Types;


namespace BaseLibrary.Common
{
    public enum OperationResultType
    {
        /// <summary>
        ///     操作成功
        /// </summary>
        [EnumDescription("操作成功。")]
        Success = 1,

        [EnumDescription("操作失败。")]
        Failed = 2,

        /// <summary>
        ///     操作取消或操作没引发任何变化
        /// </summary>
        [EnumDescription("操作没有引发任何变化，提交取消。")]
        NoChanged =3,

        /// <summary>
        ///     参数错误
        /// </summary>
        [EnumDescription("参数错误。")]
        ParamError = 4,

        /// <summary>
        ///     指定参数的数据不存在
        /// </summary>
        [EnumDescription("指定参数的数据不存在。")]
        QueryNull =5,

        /// <summary>
        ///     权限不足
        /// </summary>
        [EnumDescription("当前用户权限不足，不能继续操作。")]
        PurviewLack =6,

        /// <summary>
        ///     非法操作
        /// </summary>
        [EnumDescription("非法操作。")]
        IllegalOperation =7,

        /// <summary>
        ///     警告
        /// </summary>
        [EnumDescription("警告")]
        Warning =8,

        /// <summary>
        ///     操作引发错误
        /// </summary>
        [EnumDescription("操作引发错误。")]
        Error =9,

        [EnumDescription("访问页面不存在")]
        NoPage =10,
    }
}

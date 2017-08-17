using System;
using System.Collections;
using System.Text;
using FluentData;
using System.Collections.Generic;
using BaseLibrary.Common;

namespace BaseLibrary.SqlConfig
{
    public abstract class DbBase<TEntity> where TEntity : new()
    {
        /// <summary>
        ///  数据访问上下文
        /// </summary>
        public IDbContext Context { get { return MySqlHelper.Context; } }
        public IDbContext WeChatContext {get { return MySqlHelper.WeChatContext; }}
        protected string TableName { get; set; }
        protected string ColumnKey { get; set; }

        #region GetItem

        /// <summary>
        ///     读取一条数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam> 
        /// <param name="identity">自增值</param>
        /// <returns>T类型</returns>
        public virtual TEntity GetItem(int identity)
        {
            var entity = default(TEntity);
            if (string.IsNullOrEmpty(TableName))
                return entity;

            entity = Context.Sql(string.Format("select * from {0} where {1} = {2}", TableName, ColumnKey, identity))
                .QuerySingle<TEntity>();

            return entity == null ? new TEntity() : entity;
        }

        /// <summary>
        ///     读取一条数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="keyValue">关键值</param>
        /// <returns>T类型</returns>
        public virtual TEntity GetItem(string keyValue)
        {
            var entity = default(TEntity);
            if (string.IsNullOrEmpty(TableName))
                return entity;

            entity = Context.Sql(string.Format("select * from {0} where {1} = '{2}'", TableName, ColumnKey, MySqlHelper.FilterSpecialChar(keyValue)))
                .QuerySingle<TEntity>();

            return entity == null ? new TEntity() : entity;
        }

        #endregion

        #region GetItems

        /// <summary>
        /// 获取数据集合 不分页
        /// </summary>
        /// <param name="paramters">参数</param>
        /// <returns></returns>
        public List<TEntity> GetItems(DbQueryParamtersNoPage paramters)
        {
            //过滤
            //paramters.Condition = MySqlHelper.FilterSpecialChar(paramters.Condition);
            //paramters.OrderBy = MySqlHelper.FilterSpecialChar(paramters.OrderBy);
            //paramters.GroupBy = MySqlHelper.FilterSpecialChar(paramters.GroupBy);

            if (!string.IsNullOrEmpty(paramters.ColumnKey))
                ColumnKey = paramters.ColumnKey;

            StringBuilder sb = new StringBuilder();

            //字段
            sb.Append(" SELECT ");
            if (paramters.ColumnFields == null || paramters.ColumnFields.Length == 0)
                sb.Append(" * ");
            else
                sb.Append(string.Join(",", paramters.ColumnFields));

            sb.Append(string.Format(" FROM {0} {1} ", TableName, paramters.Join));

            //筛选条件
            if (!string.IsNullOrWhiteSpace(paramters.Condition))
            {
                if (paramters.Condition.ToLower().TrimStart(new[] { ' ' }).StartsWith("where"))
                    sb.Append(paramters.Condition);
                else
                    sb.AppendFormat(" WHERE {0} ", paramters.Condition);
            }
            //分组
            if (!string.IsNullOrWhiteSpace(paramters.GroupBy))
            {
                if (paramters.Condition.ToLower().IndexOf("group by", System.StringComparison.Ordinal) != -1)
                    sb.Append(paramters.GroupBy);
                else
                    sb.AppendFormat(" GROUP BY {0} ", paramters.GroupBy);
            }


            //排序
            if (!string.IsNullOrWhiteSpace(paramters.OrderBy))
            {
                if (paramters.OrderBy.ToLower().IndexOf("order by", System.StringComparison.Ordinal) != -1)
                    sb.Append(paramters.OrderBy);
                else
                    sb.AppendFormat(" ORDER BY {0} ", paramters.OrderBy);
            }

            if (paramters.PageSize > 0)
            {
                sb.AppendFormat(" limit {0} ", paramters.PageSize);
            }
            //执行
            return Context.Sql(sb.ToString()).QueryMany<TEntity>();
        }

        /// <summary>
        ///     获取数据集合
        /// </summary>
        /// <param name="paramters">查询参数</param>
        /// <returns>返回集合</returns>
        public ListBag<int, List<TEntity>> GetItems(DbQueryParamters paramters)
        {
            //过滤
            //paramters.Condition = MySqlHelper.FilterSpecialChar(paramters.Condition);
            //paramters.OrderBy = MySqlHelper.FilterSpecialChar(paramters.OrderBy);
            //paramters.GroupBy = MySqlHelper.FilterSpecialChar(paramters.GroupBy);

            if (paramters.PageIndex <= 0)
                paramters.PageIndex = 1;
            if (paramters.PageSize <= 0)
                paramters.PageSize = 15;

            if (!string.IsNullOrEmpty(paramters.ColumnKey))
                ColumnKey = paramters.ColumnKey;

            StringBuilder sb = new StringBuilder();
            string orderBy = string.Empty;
            int queryWay = 0; //0标识默认、1 其他
            bool orderByType = false; //true 降序
            string groupBy = string.Empty; //分组查询条件

            sb.Append(" SELECT ");
            if (paramters.ColumnFields == null || paramters.ColumnFields.Length == 0)
                sb.Append(" * ");
            else
                sb.Append(string.Join(",", paramters.ColumnFields));

            if (string.IsNullOrEmpty(paramters.Join))
            {
                if (string.IsNullOrEmpty(paramters.TableName))
                    paramters.TableName = TableName;
            }
            else
            {
                //如果paramters join 存在tablname
                var firstIndex = paramters.Join.ToLower().TrimStart().IndexOf(TableName.ToLower(), 0, TableName.Length, System.StringComparison.Ordinal);
                if (firstIndex < 0)
                {
                    paramters.TableName = TableName + " " + paramters.Join;
                }
                else
                {
                    paramters.TableName = paramters.Join;
                }
            }
            sb.Append(string.Format(" FROM {0} ", paramters.TableName));

            #region 判断当前使用查询语句方式

            if (string.IsNullOrEmpty(paramters.Condition) || paramters.Condition.IndexOf("like") == -1) //非like查询，不使用using语法
            {
                queryWay = 0;
                //杜非，2013-09-05 非like查询，不使用using语法
                /*
                if (!string.IsNullOrEmpty(paramters.OrderBy))
                {
                    if (paramters.OrderBy.ToLower().IndexOf(ColumnKey.ToLower(), System.StringComparison.Ordinal) > 0)
                    {
                        if (paramters.OrderBy.IndexOf(",", System.StringComparison.Ordinal) > 0)
                            queryWay = 1;
                    }
                    else
                    {
                        queryWay = 1;
                    }
                }
                 * */
            }
            else
            {
                queryWay = 1;
            }

            #endregion

            //指定了join查询方式
            if (string.IsNullOrEmpty(paramters.JoinKey))
                paramters.JoinKey = paramters.ColumnKey;
            if (!string.IsNullOrEmpty(paramters.JoinTable) && !string.IsNullOrEmpty(paramters.JoinKey))
            {
                queryWay = 1;
            }

            if (queryWay == 0)
            {
                #region 传统查询方式

                //修改目的：有Groupby时的查询
                if (!string.IsNullOrEmpty(paramters.GroupBy))
                    groupBy = paramters.GroupBy + " ";
                if (string.IsNullOrEmpty(paramters.OrderBy))
                    orderBy = " ORDER BY " + ColumnKey + " DESC";
                else
                    orderBy = paramters.OrderBy;

                if (orderBy.ToLower().IndexOf("desc") > 0)
                    orderByType = true;

                //2013-09-05 
                //sb.Append(string.Format(" Where {0} {1} {2} (", paramters.Condition, ColumnKey,
                //                        (orderByType ? "<=" : ">=")));
                //sb.Append(string.Format(" SELECT {0} FROM {1} {2} LIMIT {3},1) ", ColumnKey, paramters.TableName, orderBy, paramters.PageSize * (paramters.PageIndex - 1)));
                //sb.Append(string.Format(" {0} LIMIT {1};", orderBy, paramters.PageSize));

                if (!string.IsNullOrEmpty(paramters.Condition) && paramters.Condition.IndexOf("where") == -1)
                    sb.Append(string.Format(" Where {0} ", paramters.Condition));
                sb.Append(string.Format(" {3}{0} LIMIT {1},{2};", orderBy, paramters.PageSize * (paramters.PageIndex - 1), paramters.PageSize, groupBy));

                #endregion
            }
            else
            {
                if (paramters.IsSameColumnKey)
                {
                    sb.Append(string.Format(" USING({0}) ", ColumnKey));

                    if (!string.IsNullOrEmpty(paramters.Condition))
                    {
                        paramters.Condition = paramters.Condition.ToLower().Replace("where", "");
                        sb.Append(string.Format(" WHERE {0} ", paramters.Condition));
                    }
                    //修改目的：有Groupby时的查询
                    if (!string.IsNullOrEmpty(paramters.GroupBy))
                        sb.Append(paramters.GroupBy + " ");
                    if (!string.IsNullOrEmpty(paramters.OrderBy))
                        sb.Append(paramters.OrderBy + " ");
                    else
                        sb.Append(string.Format(" ORDER BY {0} DESC ", ColumnKey));
                    sb.Append(string.Format(" LIMIT {0},{1};", paramters.PageSize * (paramters.PageIndex - 1), paramters.PageSize));
                }
                else
                {
                    #region JION方式

                    if (!string.IsNullOrEmpty(paramters.JoinTable) && !string.IsNullOrEmpty(paramters.JoinKey))
                    {
                        sb.Append(string.Format("a JOIN (SELECT {0} FROM {1} ", paramters.JoinKey, paramters.JoinTable));
                    }
                    else
                    {
                        sb.Append(string.Format(" JOIN (SELECT {0} FROM {1} ", ColumnKey, paramters.TableName));
                    }


                    if (!string.IsNullOrEmpty(paramters.Condition))
                    {
                        paramters.Condition = paramters.Condition.ToLower().Replace("where", "");
                        sb.Append(string.Format(" WHERE {0} ", paramters.Condition));
                    }
                    //修改人：陈雨田 修改目的：有Groupby时的查询
                    if (!string.IsNullOrEmpty(paramters.GroupBy))
                        sb.Append(paramters.GroupBy + " ");
                    if (!string.IsNullOrEmpty(paramters.OrderBy))
                        sb.Append(paramters.OrderBy + " ");
                    else
                        sb.Append(string.Format(" ORDER BY {0} DESC ", ColumnKey));
                    if (!string.IsNullOrEmpty(paramters.JoinTable) && !string.IsNullOrEmpty(paramters.JoinKey))
                    {
                        string usingKey = string.Empty;
                        string[] _arrKey = paramters.JoinKey.Split(',');
                        foreach (string s in _arrKey)
                        {
                            if (!string.IsNullOrEmpty(usingKey))
                                usingKey += " and ";

                            usingKey += string.Format("a.{0}=t.{0}", s);

                            for (int i = 0; i < paramters.ColumnFields.Length; i++)
                            {
                                if (paramters.ColumnFields[i] == s)
                                    paramters.ColumnFields[i] = "a." + s;
                            }
                        }

                        sb.Append(string.Format(" LIMIT {0},{1})t on {2} ", paramters.PageSize * (paramters.PageIndex - 1), paramters.PageSize, usingKey));
                    }
                    else
                    {
                        sb.Append(string.Format(" LIMIT 4000)t USING({0}) ", ColumnKey));

                        if (!string.IsNullOrEmpty(paramters.Condition))
                        {
                            paramters.Condition = paramters.Condition.ToLower().Replace("where", "");
                            sb.Append(string.Format(" WHERE {0} ", paramters.Condition));
                        }
                    }

                    if (!string.IsNullOrEmpty(paramters.OrderBy))
                        sb.Append(paramters.OrderBy + " ");
                    else
                        sb.Append(string.Format(" ORDER BY {0} DESC ", ColumnKey));
                    if (!string.IsNullOrEmpty(paramters.JoinTable) && !string.IsNullOrEmpty(paramters.JoinKey))
                    {
                        sb.Append(";");
                    }
                    else
                    {
                        sb.Append(string.Format(" LIMIT {0},{1};", paramters.PageSize * (paramters.PageIndex - 1), paramters.PageSize));
                    }

                    #endregion
                }
            }

            if (paramters.IsCount)
            {
                if (paramters.IsSameColumnKey)
                {
                    //使用有on的语句时
                    if (!string.IsNullOrEmpty(paramters.Join) && paramters.Join.IndexOf("On") > -1)
                        sb.Append(string.Format("SELECT COUNT({0}) AS RecordCount FROM {1} USING({0}) {2}", ColumnKey, paramters.TableName.Substring(0, paramters.TableName.IndexOf("On")), string.IsNullOrEmpty(paramters.Condition) ? string.Empty : "Where " + paramters.Condition));
                    else if (string.IsNullOrEmpty(paramters.Condition))
                        sb.Append(string.Format("SELECT COUNT({0}) AS RecordCount FROM {1} USING({0})", ColumnKey, paramters.TableName));
                    else
                        sb.Append(string.Format("SELECT COUNT({0}) AS RecordCount FROM {1} USING({0}) where {2}", ColumnKey, paramters.TableName, paramters.Condition));
                    //分组条数  修改目的：有Groupby时的总条数
                    if (!string.IsNullOrEmpty(paramters.GroupBy))
                        sb.AppendFormat(paramters.GroupBy);
                    sb.Append(";");
                }
                else
                {
                    if (!string.IsNullOrEmpty(paramters.JoinTable))
                    {
                        if (string.IsNullOrEmpty(paramters.Condition))
                            sb.Append(string.Format("SELECT COUNT({0}) AS RecordCount FROM {1}", ColumnKey, paramters.JoinTable));
                        else
                            sb.Append(string.Format("SELECT COUNT({0}) AS RecordCount FROM {1} where {2}", ColumnKey, paramters.JoinTable,
                                                    paramters.Condition));
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(paramters.Condition))
                            sb.Append(string.Format("SELECT COUNT({0}) AS RecordCount FROM {1}", ColumnKey, paramters.TableName));
                        else
                            sb.Append(string.Format("SELECT COUNT({0}) AS RecordCount FROM {1} where {2}", ColumnKey, paramters.TableName,
                                                    paramters.Condition));
                    }
                    //分组条数 修改目的：有Groupby时的总条数
                    if (!string.IsNullOrEmpty(paramters.GroupBy))
                        sb.AppendFormat(paramters.GroupBy);
                    sb.AppendFormat(";");
                }

            }
            int recordCount = 0;
            List<TEntity> lists = new List<TEntity>();

            if (paramters.IsCount)
            {
                using (var command = Context.MultiResultSql)
                {
                    lists = command.Sql(sb.ToString()).QueryMany<TEntity>();
                    List<DataCount> dataCounts = command.QueryMany<DataCount>();
                    //无分组总条数 修改目的：有Groupby时的总条数
                    if (dataCounts.Count > 0 && string.IsNullOrEmpty(paramters.GroupBy))
                        recordCount = dataCounts[0].RecordCount;
                    else if (dataCounts.Count > 0 && !string.IsNullOrEmpty(paramters.GroupBy))
                        recordCount = dataCounts.Count;
                }
            }
            else
            {
                lists = Context.Sql(sb.ToString()).QueryMany<TEntity>();
            }

            try
            {
                var myList = new ListBag<int, List<TEntity>>(recordCount, lists);
                return myList;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Delete

        /*
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int Delete(int keyValue)
        {
            if (string.IsNullOrEmpty(TableName))
                return 0;

            int rowsAffected = Context.Sql(@"delete from @0 where @1 = @2")
                .Parameters(TableName, ColumnKey, keyValue)
                .Execute();

            return rowsAffected;
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int Delete(string keyValue)
        {
            if (string.IsNullOrEmpty(TableName))
                return 0;

            int rowsAffected = Context.Sql(@"delete from @0 where @1 = '@2'")
                .Parameters(TableName, ColumnKey, keyValue)
                .Execute();

            return rowsAffected;
        }
        */
        #endregion
    }

    public class DataCount
    {
        public int RecordCount { get; set; }
    }


}

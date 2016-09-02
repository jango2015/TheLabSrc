using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jango.Lab.Repositories.InfraCom
{
    public static class PageLinqExtensions
    {
        public static PagedList<T> AsPagedList<T>
           (
           this IQueryable<T> allItems,
           int pageIndex,
           int pageSize,
           Action<T> actionMethod = null
           )
        {
            if (pageIndex < 1)
                pageIndex = 1;
            var itemIndex = (pageIndex - 1) * pageSize;
            var totalItemCount = allItems.Count();

            //page fixed
            var totalPageCnt = (int)Math.Ceiling((double)(totalItemCount / pageSize));
            //TODO:第一页还是最后一页？
            if (itemIndex >= totalItemCount) pageIndex = 1; //(totalPageCnt <= 0 ? 1 : totalPageCnt);
            itemIndex = (pageIndex - 1) * pageSize;

            var pageOfItems = allItems.Skip(itemIndex).Take(pageSize);

            if (actionMethod != null)
            {
                var actionOfItems = pageOfItems.ToList();
                actionOfItems.ForEach(o => actionMethod(o));
                return new PagedList<T>(actionOfItems, pageIndex, pageSize, totalItemCount);
            }

            return new PagedList<T>(pageOfItems, pageIndex, pageSize, totalItemCount);
        }

        public static PagedList<T> AsPagedList<T>
            (
            this IEnumerable<T> allItems,
            IPageQuery query,
            Action<T> actionMethod = null
            )
        {
            return allItems.AsQueryable().AsPagedList(query.PageNumber, query.PageSize, actionMethod);
            // return AsPagedList(allItems, query.PageNumber, query.PageSize, actionMethod);
        }

        public static PagedList<T> AsPagedList<T>
            (
            this IEnumerable<T> currentItems,
            IPageQuery query,
            int totalCount,
            Action<T> actionMethod = null
            )
        {
            return new PagedList<T>(currentItems, query.PageNumber, query.PageSize, totalCount);
        }

        public static PagedList<TResult> AsPagedList<T, TResult>(this IEnumerable<T> items, IPageQuery query,
            Func<T, TResult> changeTypeFunc)
        {
            var returnList = items.Select(changeTypeFunc);

            return new PagedList<TResult>(returnList, query.PageNumber, query.PageSize);
        }
    }

    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> allItems, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            var items = allItems as IList<T> ?? allItems.ToList();
            TotalItemCount = items.Count();
            CurrentPageIndex = pageIndex;
            AddRange(items.Skip(StartItemIndex - 1).Take(pageSize));
        }

        public PagedList(IEnumerable<T> currentPageItems, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(currentPageItems);
            TotalItemCount = totalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        public PagedList(IQueryable<T> allItems, int pageIndex, int pageSize)
        {
            int startIndex = (pageIndex - 1) * pageSize;
            AddRange(allItems.Skip(startIndex).Take(pageSize));
            TotalItemCount = allItems.Count();
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        public PagedList(IQueryable<T> currentPageItems, int pageIndex, int pageSize, int totalItemCount)
        {
            AddRange(currentPageItems);
            TotalItemCount = totalItemCount;
            CurrentPageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int CurrentPageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalItemCount { get; set; }

        public int TotalPageCount { get { return (int)Math.Ceiling(TotalItemCount / (double)PageSize); } }

        public int StartItemIndex { get { return (CurrentPageIndex - 1) * PageSize + 1; } }
        public int EndItemIndex { get { return TotalItemCount > CurrentPageIndex * PageSize ? CurrentPageIndex * PageSize : TotalItemCount; } }
    }
    public interface IPagedList : IEnumerable
    {
        int CurrentPageIndex { get; set; }

        int PageSize { get; set; }

        int TotalItemCount { get; set; }
    }

    public interface IPagedList<out T> : IReadOnlyList<T>, IPagedList { }

    public interface IPageQuery
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        int PageNumber { get; set; }

        /// <summary>
        /// 显示行数
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 要排序的字段
        /// </summary>
        string SortItem { get; set; }

        /// <summary>
        /// 排序的方式
        /// </summary>
        string SortOrder { get; set; }

        bool ExpotingToExcel { get; set; }

        /// <summary>
        /// 序列化查询字符串(json格式)
        /// </summary>
        /// <returns></returns>
        string ToQueryString();
    }
}
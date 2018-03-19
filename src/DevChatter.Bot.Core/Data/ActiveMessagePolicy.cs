﻿using System;
using System.Linq.Expressions;
using DevChatter.Bot.Core.Model;

namespace DevChatter.Bot.Core.Data
{
    public class DataItemPolicy<T> : ISpecification<T> where T : DataItem
    {
        protected DataItemPolicy(Expression<Func<T, bool>> expression)
        {
            Criteria = expression;
        }

        public static DataItemPolicy<T> ByStatus(DataItemStatus dataItemStatus)
        {
            return new DataItemPolicy<T>(x => x.DataItemStatus == dataItemStatus);
        }

        public static DataItemPolicy<T> ActiveOnly()
        {
            return ByStatus(DataItemStatus.Active);
        }

        public Expression<Func<T, bool>> Criteria { get; }
        public string CacheKey => $"{typeof(T).Name}-{Criteria}";
        // Example: "SimpleCommand-x => x.DataItemStatus == DataItemStatus.Active"
        // Example: "SimpleCommand-x => x.DataItemStatus == DataItemStatus.Draft"
        // Example: "SimpleCommand-x => x.Id == 42"
    }
}
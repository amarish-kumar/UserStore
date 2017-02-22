﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Training.DAL.Interfaces.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<IQueryable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        Task EditAsync(Expression<Func<T, bool>> predicate, T entity);
    }
}
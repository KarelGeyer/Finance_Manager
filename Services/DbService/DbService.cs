﻿using System.Linq;
using System.Linq.Expressions;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DbService
{
	public class DbService<T> : IDbService<T>
		where T : BaseDbModel
	{
		protected readonly DbContext _context;

		public DbService(DbContext context)
		{
			_context = context;
		}

		/// <inheritdoc />
		public async Task<T?> Get(Expression<Func<T, bool>>? where = null)
		{
			return await _context.Set<T>().FirstOrDefaultAsync(where);
		}

		/// <inheritdoc />
		public async Task<T?> Get(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		/// <inheritdoc />
		public async Task<List<T>> GetAll(Expression<Func<T, bool>>? where = null)
		{
			var query = _context.Set<T>().Where(where);
			return await query.ToListAsync();
		}

		/// <inheritdoc />
		public List<T> GetAll<TKey>(Func<T, TKey> keySelector, Expression<Func<T, bool>>? where = null)
		{
			var query = _context.Set<T>().Where(where).OrderBy(keySelector).ToList();
			return query;
		}

		/// <inheritdoc />
		public async Task<decimal> GetSum(Expression<Func<T, decimal>> selector, Expression<Func<T, bool>>? where = null)
		{
			return await _context.Set<T>().Where(where).SumAsync(selector);
		}

		/// <inheritdoc />
		public async Task<double> GetSum(Expression<Func<T, double>> selector, Expression<Func<T, bool>>? where = null)
		{
			return await _context.Set<T>().Where(where).SumAsync(selector);
		}

		/// <inheritdoc />
		public async Task<int> Create(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			return await _context.SaveChangesAsync();
		}

		/// <inheritdoc />
		public async Task<T?> Delete(Expression<Func<T, bool>>? where)
		{
			T? entity = _context.Set<T>().FirstOrDefault(where);
			if (entity is null)
				return null;
			_context.Set<T>().Remove(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		/// <inheritdoc />
		public async Task<T?> Update(T entity, Expression<Func<T, bool>>? where = null)
		{
			T? foundEntity = _context.Set<T>().FirstOrDefault(where);
			if (foundEntity is null)
				return null;
			_context.Entry(foundEntity).CurrentValues.SetValues(entity);
			await _context.SaveChangesAsync();
			return entity;
		}
	}
}

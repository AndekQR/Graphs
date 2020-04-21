using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ServiceTest {
    public class FakeDb<T> : IDbSet<T> where T : class {
        private readonly IQueryable _query;

        public FakeDb() {
            Local = new ObservableCollection<T>();
            _query = Local.AsQueryable();
        }

        public virtual T Find(params object[] keyValues) {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public T Add(T item) {
            Local.Add(item);
            return item;
        }

        public T Remove(T item) {
            Local.Remove(item);
            return item;
        }

        public T Attach(T item) {
            Local.Add(item);
            return item;
        }

        public T Create() {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local { get; }

        Type IQueryable.ElementType => _query.ElementType;

        Expression IQueryable.Expression => _query.Expression;

        IQueryProvider IQueryable.Provider => _query.Provider;

        IEnumerator IEnumerable.GetEnumerator() {
            return Local.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return Local.GetEnumerator();
        }

        public T Detach(T item) {
            Local.Remove(item);
            return item;
        }

        public int SaveChanges() {
            return 1;
        }
    }
}
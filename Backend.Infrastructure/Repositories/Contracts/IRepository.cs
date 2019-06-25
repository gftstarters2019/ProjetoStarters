﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infrastructure.Repositories.Contracts
{
    public interface IRepository<T>
    {
        T Find(Guid id);
        T FindCPF(string cpf);
        IEnumerable<T> Get();
        bool Add(T t);
        bool Remove(Guid id);
        T Update(Guid id, T t);
        bool Save();
    }
}
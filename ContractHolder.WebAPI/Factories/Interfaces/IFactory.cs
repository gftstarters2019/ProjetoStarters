﻿namespace Contract.WebAPI.Factories.Interfaces
{
    public interface IFactory<T, U>
    {
        T Create(U u);
    }
}

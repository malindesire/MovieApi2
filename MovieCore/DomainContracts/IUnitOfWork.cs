﻿namespace MovieCore.DomainContracts
{
    public interface IUnitOfWork
    {
        IMovieRepository Movies { get; }
        IActorRepository Actors { get; }
        IReviewRepository Reviews { get; }
        Task CompleteAsync();
    }
}

﻿namespace MovieServiceContracts
{
    public interface IServiceManager
    {
        public IMovieService Movies { get; }
        public IReviewService Reviews { get; }
        public IActorService Actors { get; }
    }
}

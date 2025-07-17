using MovieCore.DomainContracts;
using MovieServiceContracts;

namespace MovieServices
{
    public class ServiceManager : IServiceManager
    {
        public IMovieService Movies { get; }

        public IReviewService Reviews { get; }

        public ServiceManager(IUnitOfWork uow)
        {
            Movies = new MovieService(uow);
            Reviews = new ReviewService(uow);
        }
    }
}

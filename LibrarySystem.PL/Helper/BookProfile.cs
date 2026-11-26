using AutoMapper;

namespace LibrarySystem.PL.Helper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookViewModel>().ReverseMap();
        }
    }
}
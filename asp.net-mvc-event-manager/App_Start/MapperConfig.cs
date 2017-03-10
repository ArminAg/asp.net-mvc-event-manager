using asp.net_mvc_event_manager.Dtos;
using asp.net_mvc_event_manager.Models;
using AutoMapper;

namespace asp.net_mvc_event_manager.App_Start
{
    public class MapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(m =>
            {
                m.CreateMap<Notification, NotificationDto>().ReverseMap();
                m.CreateMap<ApplicationUser, UserDto>().ReverseMap();
                m.CreateMap<Event, EventDto>().ReverseMap();
                m.CreateMap<Genre, GenreDto>().ReverseMap();
            });
        }
    }
}
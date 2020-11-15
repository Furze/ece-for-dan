using System;
using AutoMapper;

namespace MoE.ECE.Domain.Read.Model.Services
{
    public class SessionTimeModelMapper : Profile
    {
        public SessionTimeModelMapper()
        {
            CreateMap<DateTimeOffset?, SessionTimeModel>()
                .ConvertUsing<SessionTimeModelConverter>();
        }
    }

    public class SessionTimeModelConverter : ITypeConverter<DateTimeOffset?, SessionTimeModel>
    {
        public SessionTimeModel Convert(DateTimeOffset? source, SessionTimeModel destination, ResolutionContext context)
        {
            return new SessionTimeModel
            {
                Hour = source?.Hour ?? 0,
                Minute = source?.Minute ?? 0,
            };
        }
    }
}
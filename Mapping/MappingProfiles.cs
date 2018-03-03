using AutoMapper;
using dashboard.Controllers.Resources;
using dashboard.Core.Models;
using System.Linq;
using System.Collections.Generic;
using last_fm_now_playing;

namespace dashboard.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //Domain to API Resource
            CreateMap<Team, TeamResource>();
            CreateMap<TeamMember, TeamMemberResource>();
            CreateMap<Goal, GoalResource>();
            CreateMap<TrackInfo, TrackInfoResource>();
            CreateMap<TeamEnvironment, TeamEnvironmentResource>();
            CreateMap<UpcomingEvent, UpcomingEventResource>();
            CreateMap<ClientGroup, ClientGroupResource>();


            CreateMap<Photo, PhotoResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>)); //Special mapping for Generic types
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource { Id = vf.FeatureId, Name = vf.Feature.Name })));


            //API Resource to Domain         
            CreateMap<SaveTeamEnvironmentResource, TeamEnvironment>();
            CreateMap<SaveGoalResource, Goal>();

            CreateMap<VehicleQueryResource, VehicleQuery>();
            CreateMap<SaveVehicleResource, Vehicle>()
            .ForMember(v => v.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(v => v.Features, opt => opt.Ignore())
            .AfterMap((vr, v) =>
            {
                //Find and Remove unselected features
                var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                foreach (var f in removedFeatures)
                    v.Features.Remove(f);

                // Add features not existing
                var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id }).ToList();
                foreach (var feature in addedFeatures)
                    v.Features.Add(feature);

            });
            // .ForMember(v => v.Features, opt => opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature {FeatureId = id})));
        }
    }
}
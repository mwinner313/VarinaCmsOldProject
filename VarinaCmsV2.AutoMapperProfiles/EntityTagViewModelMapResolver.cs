//using System.Collections.Generic;
//using System.Linq;
//using AutoMapper;
//using VarinaCmsV2.Core.Contracts.DataServices;
//using VarinaCmsV2.DomainClasses.Contracts;
//using VarinaCmsV2.DomainClasses.Entities;
//using VarinaCmsV2.ViewModel.Infrastructures;
//using VarinaCmsV2.ViewModel.Page;

//namespace VarinaCmsV2.AutoMapperProfiles
//{
//    internal class EntityTagViewModelMapResolver : IValueResolver<ITaggedItem, IViewModelTaggedItem, List<string>>
//    {
       
//        private readonly ITagDataService _tagDataService;

//        public EntityTagViewModelMapResolver(ITagDataService tagDataService)
//        {
//            _tagDataService = tagDataService;
//        }

//        public ICollection<Tag> Resolve(IViewModelTaggedItem source, ITaggedItem destination, ICollection<Tag> destMember, ResolutionContext context)
//        {
//            var list = new List<Tag>();
//            if (source.Tags == null) return new List<Tag>();
//            foreach (var tag in source.Tags)
//            {
//                list.Add(_tagDataService.Query.Any(x => x.Title == tag)
//                    ? _tagDataService.Query.First(x => x.Title == tag)
//                    : new Tag() { Title = tag });
//            }
//            return list;
//        }

//        public List<string> Resolve(ITaggedItem source, IViewModelTaggedItem destination, List<string> destMember, ResolutionContext context)
//        {
//            return source.Tags.Select(x => x.Title).ToList();
//        }
//    }
//}
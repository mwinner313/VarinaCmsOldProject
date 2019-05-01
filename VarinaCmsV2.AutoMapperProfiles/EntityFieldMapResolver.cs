using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.DomainClasses.Entities.EShop;
using VarinaCmsV2.ViewModel;
using VarinaCmsV2.ViewModel.Eshop;

namespace VarinaCmsV2.AutoMapperProfiles
{
    internal class EntityFieldMapResolver<TCategory> : IValueResolver<IEntityViewModel, IEntity<TCategory>, ICollection<Field>>  where TCategory :ICategory
    {
        private readonly IUnitOfWork _unitOfWork;

        public EntityFieldMapResolver(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ICollection<Field> Resolve(IEntityViewModel source, IEntity<TCategory> destination, ICollection<Field> destMember, ResolutionContext context)
        {
            var newOneIds = source.Fields.Select(x => x.Id);
            destMember.Where(x => !newOneIds.Contains(x.Id)).ToList().ForEach(x =>
            {
                _unitOfWork.Delete(x);
                destMember.Remove(x);
            });
            source.Fields.Where(x => x.Id == Guid.Empty).ToList().MapToModel().ForEach(x =>
            {
                _unitOfWork.Add(x);
                destMember.Add(x);
            });
            source.Fields.Where(x => x.Id != Guid.Empty).ToList().ForEach(x =>
            {
                var old = destMember.First(o => o.Id == x.Id);
                x.MapToExisting(old);
                _unitOfWork.Update(old);
            });
            return destMember.ToList();
        }
    }
}
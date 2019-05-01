using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.Core.Contracts.DataServices;
using VarinaCmsV2.Core.Logic.Helpers;
using VarinaCmsV2.Core.LogicFacade;
using VarinaCmsV2.Data.Contracts;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;

namespace VarinaCmsV2.Core.Logic.Facades
{
    public class FieldDefenitionFacade : IFieldDefentionFacade
    {
        private readonly CustomFieldFactoryProvider<JObject> _customFieldFactoryProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbSet<FieldDefenition> _fieldDefenitions;
        private readonly List<FieldDefenition> _addings = new List<FieldDefenition>();
        private readonly List<FieldDefenition> _updatings = new List<FieldDefenition>();
        private readonly List<Guid> _deletings = new List<Guid>();

        public FieldDefenitionFacade(CustomFieldFactoryProvider<JObject> customFieldFactoryProvider, IUnitOfWork unitOfWork)
        {
            _customFieldFactoryProvider = customFieldFactoryProvider;
            _unitOfWork = unitOfWork;
            _fieldDefenitions = _unitOfWork.Set<FieldDefenition>();
        }

        public void PendToAdd(FieldDefenition model)
        {
            _addings.Add(model);
        }

        public void PendToUpdate(FieldDefenition model)
        {
            _updatings.Add(model);
        }
        public void PendToUpdate(List<FieldDefenition> model)
        {
            _updatings.AddRange(model);
        }
        public void PendToDelete(Guid id)
        {

            _deletings.Add(id);

        }


        public async Task SaveChangesAsync()
        {


            _addings.ForEach(x =>
            {

                x.CheckFieldDefenition(_customFieldFactoryProvider);
                x.SanitizeHandle();
                _unitOfWork.Add(x);
            });

             _updatings.ForEach( updating =>
             {
                 var fd =  _fieldDefenitions.First(x => x.Id == updating.Id);
                 updating.CheckFieldDefenition(_customFieldFactoryProvider);
                 updating.MapToViewModel().MapToExisting(fd);
                 _unitOfWork.Update(fd);
             });

            _deletings.ForEach( id =>
            {
                var fd =  _fieldDefenitions.First(x => x.Id == id);
                _unitOfWork.Delete(fd);
            });


            await _unitOfWork.SaveChangesAsync();
        }

        public void IgnoreChanges()
        {
            _addings.Clear();
            _updatings.Clear();
            _deletings.Clear();
        }
    }
}

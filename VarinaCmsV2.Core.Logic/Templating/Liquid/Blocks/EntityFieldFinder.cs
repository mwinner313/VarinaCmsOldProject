using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotLiquid;
using Newtonsoft.Json.Linq;
using VarinaCmsV2.Common;
using VarinaCmsV2.DomainClasses.Entities;
using VarinaCmsV2.ViewModel.Entities;
using VarinaCmsV2.ViewModel.Eshop;
using Tag = DotLiquid.Tag;

namespace VarinaCmsV2.Core.Logic.Templating.Liquid.Blocks
{
    public class EntityFieldFinder : Tag
    {
        private readonly CustomFieldFactoryProvider<JObject> _fieldFactoryProvider;
        private string _product = string.Empty;
        private string _fd = string.Empty;
        public EntityFieldFinder(CustomFieldFactoryProvider<JObject> fieldFactoryProvider)
        {
            _fieldFactoryProvider = fieldFactoryProvider;
        }

        public override void Initialize(string tagName, string markup, List<string> tokens)
        {
            var keys = markup.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            try
            {
                _product = keys[0];
                _fd = keys[1];
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"user created entities paginator {markup}");
            }
            base.Initialize(tagName, markup, tokens);
        }

        public override void Render(Context context, TextWriter result)
        {
            try
            {
                var product = context[_product] as ProductLiquidAdapter;
                var fd = context[_fd] as FieldDefenitionLiquidVeiwModel;
                if (product is null)
                {
                    result.Write("خطا عدم دسترسی به محصول درون قالب  {render_field} !");
                    return;
                }
                if (fd is null)
                {
                    result.Write("خطا عدم دسترسی به تعریف فیلد درون قالب  {render_field} !");
                    return;
                }
                var field = product.Fields.FirstOrDefault(x => x.FieldDefenitionId == fd.Id);
                if (field is null)
                {
                    if(fd.IsRequired)
                    result.Write($"فیلد اجباری بدون مقدار {fd.Title}");
                    return;
                }

                result.Write(_fieldFactoryProvider.GetFieldFactory(fd.Type).LoadField(field.RawValue, fd.Title, fd.MetaData)
                    .Veiw());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.Write("خطا");
            }
        }
    }
}

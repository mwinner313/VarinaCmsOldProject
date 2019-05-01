using System.Web;
using VarinaCmsV2.DomainClasses.Entities;

namespace VarinaCmsV2.Core.Contracts
{
    public class FileFieldDataWrapper
    {
        public FileFieldDataWrapper(HttpPostedFileBase file, string path, string name)
        {
            Path = path;
            Name = name;
        }
        public FileFieldDataWrapper(HttpPostedFileBase file)
        {
            Path = file.FileName;
        }

        public FileFieldDataWrapper(FileData file)
        {
            Path = file.Path;
            Name = file.Name;
        }
        public string Path { get; }    
        public string Name { get; }    
    }
}

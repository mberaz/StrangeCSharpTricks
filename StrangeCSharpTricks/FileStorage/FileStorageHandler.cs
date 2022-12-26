using System;
using System.Collections.Generic;

namespace StrangeCSharpTricks.DictionaryIsTheNewIf.FileStorage
{
    public interface IFileStorageHandler
    {
        public string ReadFile(FileStorageTypes fileStorageType, string path);
    }
    public class FileStorageHandler:IFileStorageHandler
    {
        Dictionary<FileStorageTypes, Func<string,string>> dictionary = new Dictionary<FileStorageTypes, Func<string, string>>();
        public FileStorageHandler(IEnumerable<IFileStorageProxy> fileStorageProxies)
        {
            foreach (var item in fileStorageProxies)
            {
                dictionary.Add(item.StorageType(), item.ReadFile);
            }
        }

        public string ReadFile(FileStorageTypes fileStorageType, string path)
        {
            return dictionary[fileStorageType](path);
        }
    }
}

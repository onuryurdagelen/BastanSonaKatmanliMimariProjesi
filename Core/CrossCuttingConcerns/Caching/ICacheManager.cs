using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);
        object Get(string key);
        void Add(string key, object data, int duration);
        //duration => cache'te ne kadar duracak.
        bool IsAdd(string key); //Cache'te var mı?
        void Remove(string key); //Cache'ten sil.
        void RemoveByPattern(string pattern); 
        //Özel silme filtreleri için kullanılır.Örnek Başında Get olanları sil ya da içinde Category olanları sil.
    }
}

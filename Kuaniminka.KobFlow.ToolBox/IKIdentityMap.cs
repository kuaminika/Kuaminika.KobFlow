using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuaniminka.KobFlow.ToolBox
{
    public interface IKIdentityMap<T>
    {
        void AddToMap(int id,T result);

        T  ? Get(int id);

        List<T> GetAllFromMap();
        bool IsPopulated();
        void PopulateMap(List<T> result);
        void RemoveFromMap(int id);
        void UpdateInMap(int id, T result);
   
    }

}

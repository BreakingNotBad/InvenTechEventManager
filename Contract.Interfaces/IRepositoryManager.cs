using Contract.Interfaces.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Interfaces
{
    public interface IRepositoryManager
    {
        IEventRepository Event { get; }   // ช่วงแรกมีแค่ Event ก่อน
        Task SaveAsync();
    }
}

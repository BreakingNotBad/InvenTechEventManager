using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Interfaces.IRepository
{
    public interface IRepositoryManager
    {
        IEventRepository Event { get; }   // ช่วงแรกมีแค่ Event ก่อน
        Task SaveAsync();
    }
}

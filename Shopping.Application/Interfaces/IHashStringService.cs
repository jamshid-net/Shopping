using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.Interfaces
{
    public interface IHashStringService
    {
        public Task<string> HashStringAsync(string hashString);
    }
}

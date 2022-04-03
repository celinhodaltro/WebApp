using Lib.Data;
using System;

namespace FacadeApp.Services
{
    public abstract class BaseService
    {
        public DataBaseContext AppContext { get; }

        public BaseService(DataBaseContext item)
        {
            AppContext = item;
        }
    }
}

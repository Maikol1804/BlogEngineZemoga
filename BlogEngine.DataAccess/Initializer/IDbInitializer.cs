using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.DataAccess.Initializer
{
    public interface IDbInitializer
    {
        void SeedData();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentTeslaServerTests.ServicesTests.Fixture
{

        [CollectionDefinition("Database collection")]
        public class DatabaseCollection : ICollectionFixture<DataBaseFixture>
        {

        }
    
}

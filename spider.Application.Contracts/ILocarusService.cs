using spider.AdvantageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace spider
{
    public interface ILocarusService
    {
        public IEnumerable<Car> GetCars();
    }
}

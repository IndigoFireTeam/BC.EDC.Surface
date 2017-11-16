using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EmitMapper;

namespace BcSoft.EDC.Surface.Helper
{
    public class MapperHelper
    {
        public static TTo Mapper<TFrom,TTo>(TFrom objects)
        {
            var objectMapper = ObjectMapperManager.DefaultInstance.GetMapper<TFrom, TTo>();

            return objectMapper.Map(objects);
        }
    }
}

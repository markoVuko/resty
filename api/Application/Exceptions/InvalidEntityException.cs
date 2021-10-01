using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class InvalidEntityException : Exception
    {
        public InvalidEntityException(int id, Type type)
            : base($"({DateTime.Now}) - Couldn't find entity {type.Name} entity with an ID of {id}")
        {

        }
    }
}

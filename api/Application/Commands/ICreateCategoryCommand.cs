﻿using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface ICreateCategoryCommand : ICommand<CategoryDto>
    {
    }
}

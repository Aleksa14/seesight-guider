﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Exeptions
{
    public class UserCreationExeption : Exception
    {
        public string ErrorMessage { get; set; }
    }
}
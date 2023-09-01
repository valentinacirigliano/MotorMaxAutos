﻿using AutoMapper;
using MotorMax.Web.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MotorMax.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper Mapper { get; private set; }

        public static void Initialize()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
﻿using System;
namespace MyAdventureAPI.models
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;
        
    }
}
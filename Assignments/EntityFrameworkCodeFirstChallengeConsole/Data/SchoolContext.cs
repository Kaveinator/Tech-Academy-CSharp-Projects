﻿using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace EntityFrameworkCodeFirstChallengeConsole.Data {
    public class SchoolContext : DbContext {
        public SchoolContext() : base() {}

        public DbSet<Student> Students { get; set; }
    }
}

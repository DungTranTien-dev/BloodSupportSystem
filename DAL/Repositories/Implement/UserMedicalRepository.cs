﻿using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implement
{
    public class UserMedicalRepository : GenericRepository<UserMedical>, IUserMedicalRepository
    {
        public readonly ApplicationDbContext _context;

        public UserMedicalRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}

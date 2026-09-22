using Microsoft.EntityFrameworkCore;
using EF_Core.Data;

using AppDbContext context = new AppDbContext();

context.Database.EnsureCreated();

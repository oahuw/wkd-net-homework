using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cart.Models;

namespace Cart.Data
{
    public class CartContext : DbContext
    {
        public CartContext (DbContextOptions<CartContext> options)
            : base(options)
        {
        }

        public DbSet<Cart.Models.Carts> Carts { get; set; } = default!;
    }
}

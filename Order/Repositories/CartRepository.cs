using Order.Contracts.Repositories;
using Order.DbContexts;
using Order.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Order.Repositories
{
    public class CartRepository : ICartRepository
    {

        private readonly OrderContext _context;
        public CartRepository(OrderContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        ///<summary>
        ///add product to cart
        ///</summary>
        ///<param name="cart"></param>
        public void AddProductToCart(Cart cart)
        {
            _context.Carts.Add(cart);
        }

        ///<summary>
        ///get cart for user
        ///</summary>
        ///<param name="authId"></param>
        public IEnumerable<Cart> GetCartDetailsForUser(Guid authId)
        {
            return _context.Carts.Where(x => x.UserId == authId && x.IsActive);
        }

        ///<summary>
        ///check if proudct name already exist in cart
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="userId"></param>
        public bool CheckProductExistInCart(Guid productId, Guid userId)
        {
            return _context.Carts.Any(e => e.UserId == userId && e.ProductId == productId && e.IsActive);
        }

        ///<summary>
        ///get cart product by id
        ///</summary>
        ///<param name="productId"></param>
        ///<param name="userId"></param>
        public Cart GetProductFromCart(Guid productId, Guid userId)
        {
            return _context.Carts.First(e => e.UserId == userId && e.ProductId == productId && e.IsActive);
        }

        ///<summary>
        ///update product in cart
        ///</summary>
        ///<param name="cart"></param>
        public void UpdateCartProduct(Cart cart)
        {
            _context.Carts.Update(cart);
        }

        ///<summary>
        ///save all changes
        ///</summary>
        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}

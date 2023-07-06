using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAO
{
    public class CartService
    {
        private static Dictionary<int, int> cartItems = new Dictionary<int, int>();

        public void AddItemToCart(int productId)
        {
            cartItems.Add(productId, 1);
        }

        public void RemoveItemFromCart(int productId)
        {
            if (cartItems.ContainsKey(productId))
            {
                cartItems.Remove(productId);
            }
        }

        public void ClearCart()
        {
            cartItems.Clear();
        }

        public Dictionary<int, int> GetCartItems()
        {
            return cartItems;
        }
    }
}

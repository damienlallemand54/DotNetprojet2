using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => GetCartLineList();

        /// <summary>
        /// Add an internal list that stores all cart lines
        /// </summary>
        private List<CartLine> _cartLines = new List<CartLine>();

        /// <summary>
        /// Return the current list of cart lines
        /// </summary>
        /// <returns>The list of cart lines.</returns>
        private List<CartLine> GetCartLineList()
        {
            return _cartLines;
        }

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            //TODO implement the method
            var cartLines = GetCartLineList();
            var existingLine = cartLines.FirstOrDefault(l => l.Product.Id == product.Id);
            if (existingLine != null)
            {
                existingLine.Quantity += quantity;
            }
            else
            {
                cartLines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            GetCartLineList().RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            // TODO implement the method
            var cartLines = GetCartLineList();
            return cartLines.Sum(l => l.Product.Price * l.Quantity);
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            // TODO implement the method
            var cartLines = GetCartLineList();
           double totalQuantity = cartLines.Sum(l => l.Quantity);
            if (totalQuantity > 0)
            {
                return GetTotalValue() / totalQuantity;
            }
            // If there are no lines, return 0
            else
            {
                return 0.0;
            }
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            // TODO implement the method
            var cartLines = GetCartLineList();
            var foundLine = cartLines.FirstOrDefault(l => l.Product.Id == productId);
            if (foundLine != null)
            {
                return foundLine.Product;
            }
            // If not found, return null
            return null;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = GetCartLineList();
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLEPIKOV30323WEB.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        /// <summary>
        /// Список объектов в корзине
        /// key - идентификатор объекта
        /// </summary>
        public Dictionary<int, CartItem> CartItems { get; set; } = new();
        /// <summary>
        /// Добавить объект в корзину
        /// </summary>
        /// <param name="product">Добавляемый объект</param>
        public virtual void AddToCart(Product product)
        {
            if (CartItems.ContainsKey(product.Id))
            {
                CartItems[product.Id].Qty++;
            }
            else
            {
                CartItems.Add(product.Id, new CartItem
                {
                    Item = product,
                    Qty = 1
                });
            };
        }
        /// <summary>
        /// Удалить объект из корзины
        /// </summary>
        /// <param name="product">удаляемый объект</param>
        public virtual void RemoveItems(int id)
        {
            CartItems.Remove(id);
        }
        /// <summary>
        /// Очистить корзину
        /// </summary>
        public virtual void ClearAll()
        {
            CartItems.Clear();
        }
        /// <summary>
        /// Количество объектов в корзине
        /// </summary>
        public int Count { get => CartItems.Sum(item => item.Value.Qty); }
        /// <summary>
        /// Общая стоимость
        /// </summary>
        public int TotalPrice
        {
            get => CartItems.Sum(item => item.Value.Item.Price * item.Value.Qty);
        }
    }
}

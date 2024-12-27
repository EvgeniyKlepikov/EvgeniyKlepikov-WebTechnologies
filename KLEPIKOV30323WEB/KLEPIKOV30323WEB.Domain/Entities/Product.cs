using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KLEPIKOV30323WEB.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; } // id прдукта
        public string Name { get; set; } // название продукта
        public string Description { get; set; } // описание продукта
        public int Price { get; set; } // стоимость продукта
        public string? Image { get; set; } // путь к файлу изображения
                                           // Навигационные свойства
        /// <summary>
        /// группа блюд (например, супы, напитки и т.д.)
        /// </summary>
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}

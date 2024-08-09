using Store.DomainEntities.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositories.Repository
{
    public class ProductRepository:Repository<Product> , IRepository.ProductIRepository
    {
        public ProductRepository() { }
    }
}

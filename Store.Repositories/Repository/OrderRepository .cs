using Store.DomainEntities.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositories.Repository
{
    public class OrderRepository:Repository<Order> , IRepository.OrderIRepository
    {
        public OrderRepository() { }
    }
}

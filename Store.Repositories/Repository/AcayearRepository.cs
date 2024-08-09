using Store.DomainEntities.DBEntities;
using Store.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repositories.Repository
{
    public class AcadyearRepository : Repository<Acayear>,IRepository.AcayearIRepository
    {
           public AcadyearRepository() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DotNet.Utils.EFModels
{
    public class MenuModelBLL
    {

        public int Add(MENUS menu)
        {
            using (DOTNETDEMOEntities de = new DOTNETDEMOEntities())
            {
                //int id = de.Database.SqlQuery<int>("select SEQ_MENUS.nextval from dual").FirstOrDefault();
                de.MENUS.Add(menu);
                return de.SaveChanges();
            }
        }

        public int Delete(int id)
        {
            using (DOTNETDEMOEntities de = new DOTNETDEMOEntities())
            {
                MENUS menu = de.MENUS.FirstOrDefault(p => p.ID == id);
                if (menu != null)
                {
                    de.MENUS.Remove(menu);
                }
                return de.SaveChanges();
            }
        }

        public List<MENUS> GetAll(int pageCount, int pageSize, out int count)
        {
            using (DOTNETDEMOEntities de = new DOTNETDEMOEntities())
            {
                List<MENUS> list = de.MENUS.OrderBy(p => p.ID).Skip(pageSize * (pageCount - 1)).Take(pageSize).ToList();
                count = de.MENUS.Count();
                return list;
            }
        }
        public int Update(MENUS menu)
        {
            using (DOTNETDEMOEntities de = new DOTNETDEMOEntities())
            {
                //修改方法1:
                //根据主键,修改所有属性的字段,如果属性未赋值则修改为空
                //de.Entry<MENUS>(menu).State = System.Data.EntityState.Modified;
                //修改方法2.1:
                //根据主键获取要修改的实体,修改此实体的部分属性.
                MENUS menu1 = de.MENUS.FirstOrDefault(p => p.ID == menu.ID);
                menu1.NAME = menu.NAME;
                menu1.URL = menu.URL;
                //修改方法2.2:
                //de.MENUS.Attach(menu);
                //de.Entry<MENUS>(menu).Property(p => p.NAME).IsModified = true;
                //de.Entry<MENUS>(menu).Property(p => p.URL).IsModified = true;
                //修改方法2.3: 
                //de.MENUS.Attach(menu);
                //var setEntry = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)de).ObjectContext.ObjectStateManager.GetObjectStateEntry(menu);
                //setEntry.SetModifiedProperty("NAME");
                //setEntry.SetModifiedProperty("URL");
                return de.SaveChanges();
            }
        }

        public void 事务()
        {
            DOTNETDEMOEntities de = new DOTNETDEMOEntities();
            using (var scope = new TransactionScope())
            {
                //执行多个操作
                var user1 = new MENUS
                {
                    NAME = "bomo" 
                };
                de.MENUS.Add(user1);
                de.SaveChanges();

                var user2 = new MENUS
                {
                    NAME = "toroto" 
                };
                de.MENUS.Add(user2);
                de.SaveChanges(); 
                //提交事务
                scope.Complete();
            }
        }
    }
}

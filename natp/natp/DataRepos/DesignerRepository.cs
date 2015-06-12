using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public class DesignerRepository : IDesignerRepository
    {
        private npDataContext db;

        public DesignerRepository(npDataContext db)
        {
            this.db = db;
        }


        public List<Designer> getAllDesigners()
        {
            var result = new List<Designer>();
            try
            {
                result = db.Designers.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new List<Designer>();
            }
            return result;
        }

        public Designer getDesigner(int id)
        {
            var result = new Designer();
            try
            {
                result = db.Designers.First(a => a.DesignerId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new Designer() { DesignerId = -1 };
            }
            return result;
        }

        public Designer getDesignerByAccount(int id)
        {
            var result = new Designer();
            try
            {
                result = db.Designers.First(a => a.AccountId == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = new Designer() { DesignerId = -1 };
            }
            return result;
        }

        public int addDesigner(Designer designer)
        {
            try
            {
                this.db.Designers.InsertOnSubmit(designer);
                this.db.SubmitChanges();
                string schedLoc =  Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "DesignerSchedules", designer.DesignerId.ToString()+"_workSched.json");
                File.Create(schedLoc);
                var workSchedule = new WorkSchedule() { DesignerId = designer.DesignerId, Location = schedLoc };
                db.WorkSchedules.InsertOnSubmit(workSchedule);
                db.SubmitChanges();
                return designer.DesignerId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public bool updateDesigner(Designer designer)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
   public  class WorkScheduleRepository : IWorkScheduleRepository
    {
       private npDataContext db;

       public WorkScheduleRepository(npDataContext db)
       {
           this.db = db;
       }


       public List<WorkSchedule> getAllWorkSchedules()
       {
           var result = new List<WorkSchedule>();
           try
           {
               result = db.WorkSchedules.ToList();
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.ToString());
               result = new List<WorkSchedule>();
           }
           return result;
       }

       public WorkSchedule getWorkSchedule(int id)
       {
           var result = new WorkSchedule();
           try
           {
               result = db.WorkSchedules.First(a => a.WorkScheduleId == id);
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.ToString());
               result = new WorkSchedule() { WorkScheduleId = -1 };
           }
           return result;
       }

       public WorkSchedule getWorkScheduleByDesigner(int id)
       {
           var result = new WorkSchedule();
           try
           {
               result = db.WorkSchedules.First(a => a.DesignerId == id);
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.ToString());
               result = new WorkSchedule() { WorkScheduleId = -1 };
           }
           return result;
       }

       public int addWorkSchedule(WorkSchedule workSchedule)
       {
           try
           {
               this.db.WorkSchedules.InsertOnSubmit(workSchedule);
               this.db.SubmitChanges();
               return workSchedule.WorkScheduleId;
           }
           catch (Exception ex)
           {
               Console.WriteLine(ex.ToString());
               return -1;
           }
       }

       public bool updateWorkSchedule(WorkSchedule workSchedule)
       {
           throw new NotImplementedException();
       }
    }
}

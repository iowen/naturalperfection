using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public interface IWorkScheduleRepository
    {
        List<WorkSchedule> getAllWorkSchedules();
        WorkSchedule getWorkSchedule(int id);
        WorkSchedule getWorkScheduleByDesigner(int id);
        int addWorkSchedule(WorkSchedule workSchedule);
        bool updateWorkSchedule(WorkSchedule workSchedule);
    }
}

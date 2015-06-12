using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace natp.DataRepos
{
    public interface IDesignerRepository
    {

        List<Designer> getAllDesigners();
        Designer getDesigner(int id);
        Designer getDesignerByAccount(int id);
        int addDesigner(Designer designer);
        bool updateDesigner(Designer designer);
    }
}

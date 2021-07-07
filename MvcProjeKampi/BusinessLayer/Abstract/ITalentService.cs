using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITalentService
    {
        Talent GetByID(int id);

        List<Talent> GetList();

        void TalentAdd(Talent talent);

        void TalentDelete(Talent talent);

        void TalentUpdate(Talent talent);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoloSharp
{
    public class PlateStringArrange
    {
        public static List <bbox_t> Arrange(List<bbox_t> lstIn, int imgWidth, int imgheight)
        {
            List<bbox_t> res = new List<bbox_t>();
            if(imgWidth > 2* imgheight)
            {//bien 1 dong
                res = lstIn.OrderBy(x=> x.x).ToList();
            }
            else
            {//bien 2 dong
                var lstup = new List<bbox_t>();
                var lstDown = new List<bbox_t>();
                for (int i=0; i< lstIn.Count; i++)
                {
                    if ((lstIn[i].y + lstIn[i].h/2) < imgheight / 2) lstup.Add(lstIn[i]);
                    else lstDown.Add(lstIn[i]);
                }
                lstup = lstup.OrderBy(x => x.x).ToList();
                lstDown = lstDown.OrderBy(x => x.x).ToList();
                for (int i = 0; i < lstup.Count; i++)
                {
                    res.Add(lstup[i]);
                }
                for (int i = 0; i < lstDown.Count; i++)
                {
                    res.Add(lstDown[i]);
                }
            } 
                
            return res;
        }
    }
}

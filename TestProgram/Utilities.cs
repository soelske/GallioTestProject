using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProgram
{
    public class Utilities
    {
        public static double GetCircleArea(ObjectId cirlceId)
        {
            double area = 0.0;
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Circle circle = null;
            using (Transaction tr = doc.TransactionManager.StartTransaction())
            {
                DBObject obj = tr.GetObject(cirlceId, OpenMode.ForRead);
                circle = (Circle)obj;
                tr.Commit();
            }

            if (circle != null)
            {
                area = Math.PI * (circle.Radius * circle.Radius + 1);
            }
            return area;
        }
    }
}

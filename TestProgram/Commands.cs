using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Windows.Forms;

namespace TestProgram
{
    public class Commands
    {
        [CommandMethod("CalculateCircleArea")]
        public void HelloWorld()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            TypedValue[] filterlist = new TypedValue[1];
            //select circle
            filterlist[0] = new TypedValue(0, "CIRCLE");
            SelectionFilter filter = new SelectionFilter(filterlist);
            PromptSelectionOptions opts = new PromptSelectionOptions();
            opts.MessageForAdding = "Select circle: ";
            PromptSelectionResult selRes = ed.GetSelection(opts, filter);
            if (selRes.Status != PromptStatus.OK)
            {
                ed.WriteMessage("\nerror in getting the circle");
                return;
            }
            ObjectId[] ids = selRes.Value.GetObjectIds();
            if (ids.Length > 0)
            {
                ObjectId circleId = ids[0];
                double area = Utilities.GetCircleArea(circleId);
                MessageBox.Show("area = " + Math.Round(area, 2) + " mm²");
            }
        }
    }
}

using Autodesk.AutoCAD.DatabaseServices;
using MbUnit.Framework;
using System;


namespace TestProgram.Test
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            var id = db.Create<Circle>(circle => circle.Radius = 0.9);

            var area = Utilities.GetCircleArea(id);
            Assert.AreEqual(area, Math.PI * (0.9 * 0.9));
        }

        [Test]
        public void TestMethod2()
        {
            var doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;

            var id = db.Create<Circle>(circle => circle.Radius = 0.9);

            var area = Utilities.GetCircleArea(id);
            Assert.AreEqual(area, Math.PI * (0.9 * 0.92));
        }
    }
}

public static class ExtensionMethods
{
    public static ObjectId Create<T>(this Database database, Action<T> action)
        where T : Entity, new()
    {
        using (var tr = database.TransactionManager.StartTransaction())
        {
            try
            {
                // Open model space
                var blockTable = (BlockTable)tr
                    .GetObject(database.BlockTableId, OpenMode.ForRead);
                var modelSpace = (BlockTableRecord)tr
                    .GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                var obj = new T();
                obj.SetDatabaseDefaults();
                action(obj);
                var objectId = modelSpace.AppendEntity(obj);
                tr.AddNewlyCreatedDBObject(obj, true);
                tr.Commit();
                return objectId;
            }
            catch (Exception)
            {
                tr.Abort();
                throw;
            }
        }
    }
}


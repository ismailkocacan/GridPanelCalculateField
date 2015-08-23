using Ext.Net;
using System.Web.Mvc;
using Ext.Net.MVC;

using System.Collections;
using System.Collections.Generic;


namespace GridPanelCalculateFields.Controllers
{
    public class GridPanelCalculateFieldController : Controller
    {
        //
        // GET: /GridPanelCalculateField/

        public ActionResult Index()
        {
            return View(TestData());
        }


        public static IEnumerable TestData()
        {
            List<VmStock> list = new List<VmStock>();
            list.Add(new VmStock() { Name = "Apple", UnitPrice = 0, Amount = 0 });
            list.Add(new VmStock() { Name = "Banana", UnitPrice = 0, Amount = 0 });
            list.Add(new VmStock() { Name = "Tomato", UnitPrice = 0, Amount = 0 });
            return list;
        }

        public ActionResult GetData()
        {
            return this.Store(TestData());
        }


        public DirectResult CellEditing(string id, string field, string oldValue, string newValue, object vmStock)
        {
            string json = JSON.Serialize(vmStock);
            json = json.Substring(2, json.Length - 4).Replace(@"\", "");
            VmStock vmStockObj = JSON.Deserialize<VmStock>(json);

            vmStockObj.Total = vmStockObj.UnitPrice * vmStockObj.Amount;

            Store store = X.GetCmp<Store>("Store1");
            ModelProxy modelProxy = store.GetById(id);
            modelProxy.Set("Total", vmStockObj.Total);
            store.GetById(id).Commit();

            return this.Direct();
        }
    }
}

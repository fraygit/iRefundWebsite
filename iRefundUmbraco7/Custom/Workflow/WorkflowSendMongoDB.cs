using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;

namespace iRefundUmbraco7.Custom.Workflow
{
    public class WorkflowSendMongoDB : Umbraco.Forms.Core.WorkflowType
    {
        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            // first we log it
            Umbraco.Core.Logging.LogHelper.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "the IP " + record.IP + " has submitted a record");
            // we can then iterate through the fields
            foreach (RecordField rf in record.RecordFields.Values)
            {
                // and we can then do something with the collection of values on each field
                List<object> vals = rf.Values;

                // or just get it as a string
                //rf.ValuesAsString();
            }

            //// If we altered a field, we can save it using the record storage
            //RecordStorage store = new RecordStorage();
            //store.UpdateRecord(record, e.Form);
            //store.Dispose();

            //// we then invoke the recordservice which handles all record states //and make the service delete the record.
            //RecordService rs = new RecordService();
            //rs.Delete(record, e.Form);
            ////rs.Dispose(record, e.Form);

            return WorkflowExecutionStatus.Completed;
        }

        public override List<Exception> ValidateSettings()
        {
            throw new NotImplementedException();
        } 
    }
}

using iRefundUmbraco7.Custom.Data.Model;
using iRefundUmbraco7.Custom.Data.Repository;
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

        public WorkflowSendMongoDB(){
            this.Name = "Update MongoDB";
            this.Id = new Guid("7677964A-4AD9-4A0C-84E5-B6CEB9AAD6FD"); 
            this.Description = "This will save an entry to the mongodb";
        }

        [Umbraco.Forms.Core.Attributes.Setting("Log Header",
        description = "Log item header")]
        public string LogHeader { get; set; }

        public override List<Exception> ValidateSettings()
        {
            return new List<Exception>();
        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            // first we log it
            Umbraco.Core.Logging.LogHelper.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, "the IP " + record.IP + " has submitted a record");
            // we can then iterate through the fields

            var refundApplication = new RefundApplication();

            foreach (RecordField rf in record.RecordFields.Values)
            {
                // and we can then do something with the collection of values on each field
                List<object> vals = rf.Values;
                switch (rf.Alias)
                {
                    case "title":
                        refundApplication.Title = (string)rf.Values.FirstOrDefault();
                        break;
                    case "irdNumber":
                        refundApplication.IRDNumber = (string)rf.Values.FirstOrDefault();
                        break;
                    case "firstName":
                        refundApplication.FirstName = (string)rf.Values.FirstOrDefault();
                        break;
                    case "lastName":
                        refundApplication.LastName = (string)rf.Values.FirstOrDefault();
                        break;
                    case "emailAddress":
                        refundApplication.Email = (string)rf.Values.FirstOrDefault();
                        break;
                }

                UpdateRecord(refundApplication);

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

        private async Task<bool> UpdateRecord(RefundApplication refundApplication)
        {
            var refundApplicationRepo = new RefundApplicationRepository();
            return await refundApplicationRepo.Update(refundApplication.IRDNumber, refundApplication.Title, refundApplication.FirstName, refundApplication.LastName, refundApplication.Email);

        }

    }
}

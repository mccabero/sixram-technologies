using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Sixram.Models.Request;

namespace Sixram.Web.Components.Pages.Administrator
{
    public partial class CompanyDetails
    {
        #region Private Properties
        private MudForm form;
        private string[] errors = { };
        private bool success;

        private MudMessageBox mboxCustom { get; set; }
        private string mBoxCustomMessage { get; set; }
        private MudMessageBox mboxError { get; set; }
        private MudMessageBox mbox { get; set; }
        private bool IsLoading { get; set; }
        private bool IsEditMode { get; set; }

        private CompanyRequestModel CompanyRequestModel { get; set; } = new();
        #endregion

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        IList<IBrowserFile> _files = new List<IBrowserFile>();
        private void UploadFiles(IBrowserFile file)
        {
            _files.Add(file);
            //TODO upload the files to the server
        }
    }
}

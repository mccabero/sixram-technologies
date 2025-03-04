using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sixram.Web.Services;
using Sixram.Web.ViewModel;

namespace Sixram.Web.Components.Pages.Administrator
{
    public partial class UserRoleList
    {
        #region Parameters
        #endregion

        #region Dependency Injection
        [Inject]
        protected NavigationManager? _navigationManager { get; set; }
        [Inject]
        private RoleService? _roleService { get; set; }
        [Inject]
        private ISnackbar SnackbarService { get; set; }
        #endregion

        #region Private Properties
        private string mBoxCustomMessage { get; set; }
        private MudMessageBox mboxError { get; set; }
        private MudMessageBox mbox { get; set; }
        private bool IsLoading { get; set; }

        private MudDataGrid<RoleViewModel>? dataGrid;
        private string? searchString;
        private List<RoleViewModel> RoleRequestModel = new List<RoleViewModel>();


        #endregion

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            IsLoading = true;

            //if (!RoleRequestModel.Any())
            //    await ReloadData();

            IsLoading = false;
            StateHasChanged();
            
        }

        private async Task ReloadData()
        {
            var dataList = await _roleService!.GetAllUserRoles();

            if (dataList == null)
            {
                IsLoading = false;
                return;
            }

            foreach (var ul in dataList)
            {
                RoleRequestModel.Add(new RoleViewModel()
                {
                    Id = ul.Id,
                    Name = ul.Name,
                    Description = ul.Description,
                });
            }
        }

        private async Task<GridData<RoleViewModel>> ServerReload(GridState<RoleViewModel> state)
        {
            if (!RoleRequestModel.Any())
                await ReloadData();

            IEnumerable<RoleViewModel> data = new List<RoleViewModel>();
            data = RoleRequestModel.OrderByDescending(x => x.Id);

            await Task.Delay(300);
            data = data.Where(element =>
            {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (!string.IsNullOrEmpty(element.Description))
                {
                    if (element.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                        return true;
                }

                return false;
            }).ToArray();

            var totalItems = data.Count();

            var sortDefinition = state.SortDefinitions.FirstOrDefault();
            if (sortDefinition != null)
            {
                switch (sortDefinition.SortBy)
                {
                    case nameof(RoleViewModel.Name):
                        data = data.OrderByDirection(
                            sortDefinition.Descending ? SortDirection.Descending : SortDirection.Ascending,
                            o => o.Name
                        );
                        break;
                    case nameof(RoleViewModel.Description):
                        data = data.OrderByDirection(
                            sortDefinition.Descending ? SortDirection.Descending : SortDirection.Ascending,
                            o => o.Description
                        );
                        break;
                }
            }

            var pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();

            return new GridData<RoleViewModel>
            {
                TotalItems = totalItems,
                Items = pagedData
            };
        }

        private Task OnSearch(string text)
        {
            searchString = text;
            return dataGrid.ReloadServerData();
        }

        private void OnAddClick()
        {
            _navigationManager.NavigateTo("/administrators/user-roles/add");
        }

        private async Task OnDeleteClick(RoleViewModel role)
        {
            try
            {
                if (role != null)
                {
                    bool? result = await mbox.ShowAsync();
                    var proceed = result == null ? false : true;

                    if (proceed)
                    {
                        IsLoading = true;

                        await _roleService.DeleteRoleById(role.Id);
                        SnackbarService.Add("User Role Successfuly Deleted!", Severity.Normal, config => { config.ShowCloseIcon = true; });

                        IsLoading = false;
                        StateHasChanged();

                        _navigationManager.NavigateTo("/administrators/user-roles", true);
                    }
                }
            }
            catch (Exception)
            {
                mBoxCustomMessage = "Unable to delete the this record. This might be used in another transaction.";
                await mboxError.ShowAsync();

                IsLoading = false;
                StateHasChanged();
                return;
            }
        }
    }
}

using System.Collections;
using UserManagementApi.Models;
using UserManagementApi.Validator.SearchSortValidation;

namespace UserManagementApi.BusinessLayer.BusinessHelper
{
    public static class SearchSortHashTable
    {
        private static Hashtable ParameterList = new Hashtable();
        public static Hashtable GetSearchSortHashTable(SearchFilter searchFilter)
        {
            ParameterList.Clear();
            searchFilter = searchFilter == null ? new SearchFilter() : searchFilter;

            PageSizeIndexValidation.IsPageSizeIndexValid(searchFilter.PageIndex, searchFilter.PageSize);

            AddFields(searchFilter);
            AddDateTime(searchFilter);
            AddSortPagenation(searchFilter);

            return ParameterList;
        }
        private static void AddFields(SearchFilter searchFilter)
        {
            ParameterList.Add("@SearchText", searchFilter.SearchText);
            ParameterList.Add("@Email", searchFilter.Filter == null ? null : searchFilter.Filter.UserEmail);
            ParameterList.Add("@UserName", searchFilter.Filter == null ? null : searchFilter.Filter.UserName);
            ParameterList.Add("@Age", searchFilter.Filter == null ? 0 : searchFilter.Filter.UserAge);
            ParameterList.Add("@ID", searchFilter.Filter == null ? null : searchFilter.Filter.UserID);
            ParameterList.Add("@Designation", searchFilter.Filter == null ? null : searchFilter.Filter.UserRole);
            ParameterList.Add("@Salary", searchFilter.Filter == null ? 0 : searchFilter.Filter.UserSalary);
            ParameterList.Add("@UserDescription", searchFilter.Filter == null ? null : searchFilter.Filter.UserDescription);
        }
        private static void AddDateTime(SearchFilter searchFilter)
        {
            if (searchFilter.Filter != null)
            {
                ParameterList.Add("@DateOfBirth", searchFilter.Filter.UserDateOfBirth.ToString("yyyy-MM-dd") == "0001-01-01" ? null : searchFilter.Filter.UserDateOfBirth.ToString("yyyy-MM-dd"));
                ParameterList.Add("@CreationTime", searchFilter.Filter.UserCreationTime.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00" ? null : searchFilter.Filter.UserCreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
                ParameterList.Add("@LastModified", searchFilter.Filter.UserLastModified.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00" ? null : searchFilter.Filter.UserLastModified.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                ParameterList.Add("@DateOfBirth", null);
                ParameterList.Add("@CreationTime", null);
                ParameterList.Add("@LastModified", null);
            }
        }
        private static void AddSortPagenation(SearchFilter searchFilter)
        {
            ParameterList.Add("@SortBy", searchFilter.SortBy);
            ParameterList.Add("@SortDirection", searchFilter.SortDirection);
            ParameterList.Add("@PageSize", searchFilter.PageSize == 0 ? 10 : searchFilter.PageSize);
            ParameterList.Add("@PageIndex", searchFilter.PageIndex * (searchFilter.PageSize == 0 ? 10 : searchFilter.PageSize));
        }
    }
}
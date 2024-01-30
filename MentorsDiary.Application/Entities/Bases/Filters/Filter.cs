using System.Reflection;
using MentorsDiary.Application.Bases.Enums;

namespace MentorsDiary.Application.Entities.Bases.Filters;

public class FilterParams
{
    public string ColumnName { get; set; } = string.Empty;

    public string FilterValue { get; set; } = string.Empty;

    public FilterOptions FilterOption { get; set; } = FilterOptions.Contains;
}

public class Filter<T>
{
    public static async Task<IEnumerable<T>> FilteredData(IEnumerable<FilterParams> filterParams, IEnumerable<T> data)
    {
        var distinctColumns = filterParams
            .Where(x => !string.IsNullOrEmpty(x.ColumnName))
            .Select(x => x.ColumnName)
            .Distinct();

        foreach (var colName in distinctColumns)
        {
            var filterColumn = typeof(T).GetProperty(colName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
            if (filterColumn != null)
            {
                var filterValues = filterParams
                    .Where(x => x.ColumnName.Equals(colName))
                    .Distinct();

                if (filterValues.Count() > 1)
                {
                    var sameColData = Enumerable.Empty<T>();

                    foreach (var val in filterValues)
                    {
                        sameColData = sameColData.Concat(await FilterData(val.FilterOption, data, filterColumn, val.FilterValue));
                    }

                    data = data.Intersect(sameColData);
                }
                else
                {
                    data = await FilterData(filterValues.FirstOrDefault()!.FilterOption, data, filterColumn, filterValues.FirstOrDefault().FilterValue);
                }
            }
        }
        return data;
    }

    private static Task<IEnumerable<T>> FilterData(FilterOptions filterOption, IEnumerable<T> data, PropertyInfo filterColumn, string filterValue)
    {
        int outValue;
        DateTime dateValue;

        switch (filterOption)
        {
            #region [StringDataType]  

            case FilterOptions.StartsWith:
                data = data.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString().ToLower().StartsWith(filterValue.ToString().ToLower())).ToList();
                break;
            case FilterOptions.EndsWith:
                data = data.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString().ToLower().EndsWith(filterValue.ToString().ToLower())).ToList();
                break;
            case FilterOptions.Contains:
                data = data.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null)!.ToString().ToLower().Contains(filterValue.ToString().ToLower())).ToList();
                break;
            case FilterOptions.DoesNotContain:
                data = data.Where(x => filterColumn.GetValue(x, null) == null ||
                                 (filterColumn.GetValue(x, null) != null && !filterColumn.GetValue(x, null).ToString().ToLower().Contains(filterValue.ToString().ToLower()))).ToList();
                break;
            case FilterOptions.IsEmpty:
                data = data.Where(x => filterColumn.GetValue(x, null) == null ||
                                 (filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null).ToString() == string.Empty)).ToList();
                break;
            case FilterOptions.IsNotEmpty:
                data = data.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null).ToString() != string.Empty).ToList();
                break;
            #endregion

            #region [Custom]  

            case FilterOptions.IsGreaterThan:
                if ((filterColumn.PropertyType == typeof(int) || filterColumn.PropertyType == typeof(int?)) && int.TryParse(filterValue, out outValue))
                {
                    data = data.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) > outValue).ToList();
                }
                else if ((filterColumn.PropertyType == typeof(DateTime?)) && DateTime.TryParse(filterValue, out dateValue))
                {
                    data = data.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) > dateValue).ToList();
                }
                break;

            case FilterOptions.IsGreaterThanOrEqualTo:
                if ((filterColumn.PropertyType == typeof(int) || filterColumn.PropertyType == typeof(int?)) && int.TryParse(filterValue, out outValue))
                {
                    data = data.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) >= outValue).ToList();
                }
                else if ((filterColumn.PropertyType == typeof(DateTime?)) && DateTime.TryParse(filterValue, out dateValue))
                {
                    data = data.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) >= dateValue).ToList();
                    break;
                }
                break;

            case FilterOptions.IsLessThan:
                if ((filterColumn.PropertyType == typeof(Int32) || filterColumn.PropertyType == typeof(Nullable<Int32>)) && Int32.TryParse(filterValue, out outValue))
                {
                    data = data.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) < outValue).ToList();
                }
                else if ((filterColumn.PropertyType == typeof(Nullable<DateTime>)) && DateTime.TryParse(filterValue, out dateValue))
                {
                    data = data.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) < dateValue).ToList();
                    break;
                }
                break;

            case FilterOptions.IsLessThanOrEqualTo:
                if ((filterColumn.PropertyType == typeof(Int32) || filterColumn.PropertyType == typeof(Nullable<Int32>)) && Int32.TryParse(filterValue, out outValue))
                {
                    data = data.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) <= outValue).ToList();
                }
                else if ((filterColumn.PropertyType == typeof(Nullable<DateTime>)) && DateTime.TryParse(filterValue, out dateValue))
                {
                    data = data.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) <= dateValue).ToList();
                    break;
                }
                break;

            case FilterOptions.IsEqualTo:
                if (filterValue == string.Empty)
                {
                    data = data.Where(x => filterColumn.GetValue(x, null) == null
                                    || (filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null).ToString().ToLower() == string.Empty)).ToList();
                }
                else
                {
                    if ((filterColumn.PropertyType == typeof(Int32) || filterColumn.PropertyType == typeof(Nullable<Int32>)) && Int32.TryParse(filterValue, out outValue))
                    {
                        data = data.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) == outValue).ToList();
                    }
                    else if ((filterColumn.PropertyType == typeof(Nullable<DateTime>)) && DateTime.TryParse(filterValue, out dateValue))
                    {
                        data = data.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) == dateValue).ToList();
                        break;
                    }
                    else
                    {
                        data = data.Where(x => filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null).ToString().ToLower() == filterValue.ToLower()).ToList();
                    }
                }
                break;

            case FilterOptions.IsNotEqualTo:
                if ((filterColumn.PropertyType == typeof(Int32) || filterColumn.PropertyType == typeof(Nullable<Int32>)) && Int32.TryParse(filterValue, out outValue))
                {
                    data = data.Where(x => Convert.ToInt32(filterColumn.GetValue(x, null)) != outValue).ToList();
                }
                else if ((filterColumn.PropertyType == typeof(Nullable<DateTime>)) && DateTime.TryParse(filterValue, out dateValue))
                {
                    data = data.Where(x => Convert.ToDateTime(filterColumn.GetValue(x, null)) != dateValue).ToList();
                    break;
                }
                else
                {
                    data = data.Where(x => filterColumn.GetValue(x, null) == null ||
                                     (filterColumn.GetValue(x, null) != null && filterColumn.GetValue(x, null).ToString().ToLower() != filterValue.ToLower())).ToList();
                }
                break;
                #endregion
        }

        return Task.FromResult(data);
    }
}
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EventSite.Models
{
    public class Validate
    {
        private const string CategoryKey = "validCategory";
        private const string OrganizerKey = "validOrganizer";

        private ITempDataDictionary tempData { get; set; }
        public Validate(ITempDataDictionary temp) => tempData = temp;

        public bool IsValid { get; private set; }
        public string ErrorMessage { get; private set; }

        public void CheckCategory(string categoryId, IRepository<Category> data)
        {
            Category entity = data.Get(categoryId);
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" : 
                $"Category id {categoryId} is already in the database.";
        }
        public void MarkCategoryChecked() => tempData[CategoryKey] = true;
        public void ClearCategory() => tempData.Remove(CategoryKey);
        public bool IsCategoryChecked => tempData.Keys.Contains(CategoryKey);

        public void CheckOrganizer(string organizerName, string operation, IRepository<Organizer> data)
        {
			Organizer entity = null; 
            if (Operation.IsAdd(operation)) {
                entity = (Organizer)data.Get(new QueryOptions<Organizer> {
                    Where = a => a.OrganizerName == organizerName
				});
            }
            IsValid = (entity == null) ? true : false;
            ErrorMessage = (IsValid) ? "" : 
                $"Organizer {entity.OrganizerName} is already in the database.";
        }
        public void MarkOrganizerChecked() => tempData[OrganizerKey] = true;
        public void ClearOrganizer() => tempData.Remove(OrganizerKey);
        public bool IsOrganizerChecked => tempData.Keys.Contains(OrganizerKey);

    }
}

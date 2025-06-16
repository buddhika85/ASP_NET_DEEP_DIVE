namespace _8_MVC_Views.Model
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private List<Department> Departments = new List<Department>
        {
            new Department(1, "Sales", "Sales Department"),
            new Department(2, "Engineering", "Engineering Department"),
            new Department(3, "QA", "Quanlity Assurance")
        };

        public List<Department> GetDepartments() => Departments;

        public Department? GetDepartmentById(int id)
        {
            return Departments.FirstOrDefault(x => x.Id == id);
        }

        public void AddDepartment(Department? Department)
        {
            if (Department is not null)
            {
                int maxId = Departments.Max(x => x.Id);
                Department.Id = maxId + 1;
                Departments.Add(Department);
            }
        }

        public bool UpdateDepartment(Department? Department)
        {
            if (Department is not null)
            {
                var dept = Departments.FirstOrDefault(x => x.Id == Department.Id);
                if (dept is not null)
                {
                    dept.Name = Department.Name;
                    dept.Description = Department.Description;

                    return true;
                }
            }

            return false;
        }

        public bool DeleteDepartment(Department? Department)
        {
            if (Department is not null)
            {
                Departments.Remove(Department);
                return true;
            }

            return false;
        }
    }
}

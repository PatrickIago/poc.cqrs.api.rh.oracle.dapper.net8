using Poc.Oracle.DTO;
namespace Poc.Oracle.SQL;
public static class EmployeeSqlConsts
{
    public const string SQL_GET =
    @$"
            SELECT EMPLOYEE_ID as {nameof(EmployeeSqlDTO.Id)}, 
                   FIRST_NAME as {nameof(EmployeeSqlDTO.Name)}, 
                   EMAIL as {nameof(EmployeeSqlDTO.Email)},
                   PHONE_NUMBER as {nameof(EmployeeSqlDTO.PhoneNumber)},
                   HIRE_DATE as {nameof(EmployeeSqlDTO.HireDate)},
                   JOB_ID as {nameof(EmployeeSqlDTO.JobId)},
                   SALARY as {nameof(EmployeeSqlDTO.Salary)},
                   MANAGER_ID as {nameof(EmployeeSqlDTO.ManagerId)},
                   DEPARTMENT_ID as {nameof(EmployeeSqlDTO.Department.Id)}
            FROM HR.EMPLOYEES
        ";

    public const string SQL_MAX = @$"SELECT MAX(EMPLOYEE_ID + 1) FROM HR.EMPLOYEES";

    public const string SQL_GET_BY_ID =
    @$"
            SELECT EMPLOYEE_ID as {nameof(EmployeeSqlDTO.Id)}, 
                   FIRST_NAME as {nameof(EmployeeSqlDTO.Name)}, 
                   EMAIL as {nameof(EmployeeSqlDTO.Email)},
                   PHONE_NUMBER as {nameof(EmployeeSqlDTO.PhoneNumber)},
                   HIRE_DATE as {nameof(EmployeeSqlDTO.HireDate)},
                   JOB_ID as {nameof(EmployeeSqlDTO.JobId)},
                   SALARY as {nameof(EmployeeSqlDTO.Salary)},
                   MANAGER_ID as {nameof(EmployeeSqlDTO.ManagerId)},
                   DEPARTMENT_ID as {nameof(EmployeeSqlDTO.Department.Id)}
            FROM HR.EMPLOYEES
            WHERE EMPLOYEE_ID = :PR_EMPLOYEE_ID
        ";

    public const string SQL_INSERT =
    @$"
            INSERT INTO HR.EMPLOYEES (EMPLOYEE_ID, NAME, EMAIL, PHONE_NUMBER, HIRE_DATE, JOB_ID, SALARY, MANAGER_ID, DEPARTMENT_ID) 
            VALUES (:PR_ID, :PR_NAME, :PR_EMAIL, :PR_PHONE_NUMBER, :PR_HIRE_DATE, :PR_JOB_ID, :PR_SALARY, :PR_MANAGER_ID, :PR_DEPARTMENT)
        ";

    public const string SQL_UPDATE =
    @$"
            UPDATE HR.EMPLOYEES
            SET NAME = :PR_NAME,
                EMAIL = :PR_EMAIL,
                PHONE_NUMBER = :PR_PHONE_NUMBER,
                HIRE_DATE = :PR_HIRE_DATE,
                JOB_ID = :PR_JOB_ID,
                SALARY = :PR_SALARY,
                MANAGER_ID = :PR_MANAGER_ID,
                DEPARTMENT_ID = :PR_DEPARTMENT
            WHERE EMPLOYEE_ID = :PR_EMPLOYEE_ID
        ";

    public const string SQL_DELETE =
    @$"
            DELETE FROM HR.EMPLOYEES
            WHERE EMPLOYEE_ID = :PR_EMPLOYEE_ID
        ";
}
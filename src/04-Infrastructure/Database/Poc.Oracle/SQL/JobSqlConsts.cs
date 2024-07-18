using Poc.Oracle.DTO;

namespace Poc.Oracle.SQL;
public static class JobSqlConsts
{
    public const string SQL_GET =
    @$"
            SELECT JOB_ID as {nameof(JobSqlDTO.JobId)}, 
                   JOB_TITLE as {nameof(JobSqlDTO.JobTitle)}, 
                   MIN_SALARY as {nameof(JobSqlDTO.MinSalary)}, 
                   MAX_SALARY as {nameof(JobSqlDTO.MaxSalary)}
            FROM HR.JOBS
        ";


    public const string SQL_GET_BY_ID =
    @$"
            SELECT JOB_ID as {nameof(JobSqlDTO.JobId)}, 
                   JOB_TITLE as {nameof(JobSqlDTO.JobTitle)}, 
                   MIN_SALARY as {nameof(JobSqlDTO.MinSalary)}, 
                   MAX_SALARY as {nameof(JobSqlDTO.MaxSalary)}
            FROM HR.JOBS
            WHERE JOB_ID = :PR_JOB_ID
        ";

    public const string SQL_INSERT =
    @$"
            INSERT INTO HR.JOBS (JOB_ID, JOB_TITLE, MIN_SALARY, MAX_SALARY) 
            VALUES (:PR_JOB_ID, :PR_JOB_TITLE, :PR_MIN_SALARY, :PR_MAX_SALARY)
        ";

    public const string SQL_UPDATE =
    @$"
            UPDATE HR.JOBS
            SET JOB_TITLE = :PR_JOB_TITLE,
                MIN_SALARY = :PR_MIN_SALARY,
                MAX_SALARY = :PR_MAX_SALARY
            WHERE JOB_ID = :PR_JOB_ID
        ";

    public const string SQL_DELETE =
    @$"
            DELETE FROM HR.JOBS
            WHERE JOB_ID = :PR_JOB_ID
        ";
}
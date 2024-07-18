namespace Poc.Contract.Command.Job.Response;
public class CreateJobResponse
{
    public CreateJobResponse(string id) => JobId = id;
    public string JobId { get; }
}


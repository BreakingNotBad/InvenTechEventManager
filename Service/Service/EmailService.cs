using FluentEmail.Core;
using Service.Contracts.IService;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _email;

    public EmailService(IFluentEmail email)
    {
        _email = email;
    }

    public async Task SendAsync(string to, string subject, string body)
    {
        await _email
            .To(to)
            .Subject(subject)
            .Body(body)
            .SendAsync();
    }
}

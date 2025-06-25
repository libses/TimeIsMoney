using TimeIsMoney.Application;

namespace TimeIsMoney.Telegram.SimpleButtons;

public class ChangeSalaryButton : IButton
{
    private readonly IRegistrationService registrationService;

    public ChangeSalaryButton(IRegistrationService registrationService)
    {
        this.registrationService = registrationService;
    }

    public string Text => "Изменить ЗП";
    public async Task<MessageAction> ActionAsync(string userId)
    {
        await registrationService.UnregisterAsync(userId);
        return new MessageAction() {RespondText = "Зарплата сброшена. Можно указать новую."};
    }
}
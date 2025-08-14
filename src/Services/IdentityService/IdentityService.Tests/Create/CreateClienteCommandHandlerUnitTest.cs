using IdentityService.Application.Commands.ClienteCommand.Create;
using IdentityService.Application.Interfaces;
using IdentityService.Domain.Primitives;

namespace IdentityService.Tests.Create;
public class CreateClienteCommandHandlerUnitTest
{

    private readonly Mock<IClienteRepository> _mockClienteRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateClienteCommandHandler _handler;

    public CreateClienteCommandHandlerUnitTest()
    {
        _mockClienteRepository = new Mock<IClienteRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();

        _handler = new CreateClienteCommandHandler(_mockClienteRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task HandleCreateClient_WhenIsOk_ShouldReturn_OK()
    {
        CreateClienteCommand command = new CreateClienteCommand("", true, "", "Masculino", 32, "0606180396", "", "0998493809");
        var result = await _handler.Handle(command, default);

        result.IsError.Should().BeFalse();
    }

}

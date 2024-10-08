﻿using Inno_Shop.Authentification.Presentation.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inno_Shop.Authentification.Application.Commands;

public class RegisterCommand : IRequest<UserDTO>
{
    public RegisterDTO Registerdto { get; set; } = null!;
}
﻿using LibraryManagementSystem.Business.DTOs.BookAuthorDtos;

namespace LibraryManagementSystem.Business.DTOs.AuthorDtos;

public class AuthorGetDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Nationality { get; set; }
}

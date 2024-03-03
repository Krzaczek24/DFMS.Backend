
using DFMS.Database.Dto.Users;
using System;
using System.Collections.Generic;

namespace DFMS.WebApi.DataContracts.Users
{
    public class SearchUsersInput
    {
        public string? PhraseFilter { get; set; }
        public DateTime? OnlineAfter { get; set; }
        public bool? Active { get; set; }
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }

    public class SearchUsersOutput
    {
        public int TotalResults { get; set; }

        public IEnumerable<UserDto> Results { get; set; } = new List<UserDto>();
    }
}

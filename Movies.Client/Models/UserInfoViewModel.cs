using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Client.Models
{
    public class UserInfoViewModel
    {
        public UserInfoViewModel(Dictionary<string, string> userInfoDictionary)
        {
            UserInfoDictionary = userInfoDictionary;
        }

        public Dictionary<string, string> UserInfoDictionary { get; set; } = null;
    }
}
